using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading.Tasks;
using Nightfall.Net.Exceptions;
using Nightfall.Net.Models.Requests;
using Nightfall.Net.Models.Responses;
using static Nightfall.Net.ApiExceptionFactory;

namespace Nightfall.Net
{
    internal class NightFallHttpClient : HttpClient
    {
        public NightFallHttpClient(string apiKey, HttpMessageHandler handler = null, bool disposeHandler = false) :
            base(handler ?? new HttpClientHandler(), disposeHandler)
        {
            DefaultRequestHeaders.TryAddWithoutValidation("Accept", "application/json");
            DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"Bearer {apiKey}");
            DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
        }

        public async Task<HttpResponseMessage> PatchAsync(string url, HttpContent content,
            params (string, string)[] headers)
        {
            using var request = new HttpRequestMessage(new HttpMethod("PATCH"), url) {Content = content};
            foreach (var (key, value) in headers)
                request.Headers.TryAddWithoutValidation(key, value);

            return await SendAsync(request);
        }
    }

    public class NightfallClient
    {
        private readonly string _apiKey;
        private NightFallHttpClient HttpClient { get; set; }

        public HttpClientHandler CustomHttpClientHandler
        {
            set => HttpClient = new NightFallHttpClient(_apiKey, value);
        }

        public NightfallClient(string apiKey)
        {
            _apiKey = apiKey;
            HttpClient = new NightFallHttpClient(_apiKey);
        }

        private async Task<string> BasePost<T>(T data, string url)
        {
            var content = new StringContent(JsonSerializer.Serialize(data));
            var httpResponseMessage = await HttpClient.PostAsync(url, content);
            await HandleResponse(httpResponseMessage);
            return await httpResponseMessage.Content.ReadAsStringAsync();
        }

        private async Task<TOut> BasePost<TOut>(object data, string url)
        {
            var content = new StringContent(JsonSerializer.Serialize(data));
            var httpResponseMessage = await HttpClient.PostAsync(url, content);
            await HandleResponse(httpResponseMessage);
            return JsonSerializer.Deserialize<TOut>(await httpResponseMessage.Content.ReadAsStringAsync());
        }

        private async Task<string> GetScanPlainTextAsync(ScanRequestConfig config)
        {
            var url = "https://api.nightfall.ai/v3/scan";
            return await BasePost(config, url);
        }

        private async Task<UploadResponse> InitiateFileUpload(InitiateUploadRequest request)
        {
            var url = "https://api.nightfall.ai/v3/upload";
            return await BasePost<UploadResponse>(request, url);
        }

        public async Task<ScanResponse> GetScanFindingsAsync(ScanRequestConfig config)
        {
            var content = await GetScanPlainTextAsync(config);
            return JsonSerializer.Deserialize<ScanResponse>(content);
        }

        public async Task<string> GetScanFilePlainTextAsync(ScanUploadedRequest config, string fileId)
        {
            var url = $"https://api.nightfall.ai/v3/upload/{fileId}/scan";
            var content = new StringContent(JsonSerializer.Serialize(config));
            var httpResponseMessage = await HttpClient.PostAsync(url, content);
            await HandleResponse(httpResponseMessage);
            return await httpResponseMessage.Content.ReadAsStringAsync();
        }

        public async Task<UploadResponse> Upload(byte[] dataToUpload) =>
            await Upload(new BufferedStream(new MemoryStream(dataToUpload)));

        /// <summary>
        /// Uploads the bytes in the provided stream
        /// </summary>
        /// <exception cref="BaseNightfallException"> May throw nightfall exceptions with codes (400,401,404,409,429,500)</exception>
        /// <returns>An uploadResponse object with the relevant uploaded file metadata</returns>
        public async Task<UploadResponse> Upload(BufferedStream stream, int concurrentUploads = 2)
        {
            var fileMetadata = await InitiateFileUpload(new InitiateUploadRequest(stream.Length));
            var url = $"https://api.nightfall.ai/v3/upload/{fileMetadata.Id}";
            var tasks = new List<Task<HttpResponseMessage>>();
            for (long i = 0; i < stream.Length; i += fileMetadata.ChunkSize)
            {
                var currentChunkSize = (int) Math.Min(fileMetadata.ChunkSize, fileMetadata.FileSizeBytes - i);
                var dataToUpload = new byte[currentChunkSize];
                stream.Read(dataToUpload);
                tasks.Add(HttpClient.PatchAsync(url, new ByteArrayContent(dataToUpload),
                    ("X-Upload-Offset", i.ToString())));
                if (tasks.Count == concurrentUploads)
                {
                    await DoUploads(tasks, fileMetadata);
                }
            }
            await DoUploads(tasks, fileMetadata);
            return await BasePost<UploadResponse>(new object(),
                $"https://api.nightfall.ai/v3/upload/{fileMetadata.Id}/finish");
        }

        private static async Task DoUploads(ICollection<Task<HttpResponseMessage>> tasks, UploadResponse fileMetadata)
        {
            var responses = await Task.WhenAll(tasks);
            foreach (var httpResponseMessage in responses)
            {
                await HandleResponse(httpResponseMessage);
            }
            tasks.Clear();
        }
    }

    public static class ApiExceptionFactory
    {
        public static async Task HandleResponse(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode) return;
            Func<Task<ApiErrorResponse>> getResponse = async () => JsonSerializer.Deserialize<ApiErrorResponse>(await response.Content.ReadAsStringAsync());
            switch (response.StatusCode)
            {
                case HttpStatusCode.BadRequest:
                    throw new NightfallInvalidRequest400(await getResponse());
                case HttpStatusCode.Unauthorized:
                    throw new NightfallAuthenticationFailure401(await getResponse());
                case HttpStatusCode.NotFound:
                    throw new NightfallInvalidFileId404(await getResponse());
                case HttpStatusCode.Conflict:
                    throw new NightfallIncorrectFileState409(await getResponse());
                case HttpStatusCode.UnprocessableEntity:
                    throw new NightfallUnprocessableRequestPayload422(await getResponse());
                case HttpStatusCode.TooManyRequests:
                    throw new NightfallRateLimit429(await getResponse());
                case HttpStatusCode.InternalServerError:
                    throw new NightfallInternalServer500(await getResponse());
                default:
                    throw new NightfallUnknownExceptionResponse(await getResponse());
            }
        }
    }
}