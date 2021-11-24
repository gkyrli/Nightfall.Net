using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Nightfall.Net.Exceptions;
using Nightfall.Net.Models.Requests;
using Nightfall.Net.Models.Responses;

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
    }

    public class NightfallClient
    {
        private readonly string _apiKey;
        private HttpClient HttpClient { get; set; }

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
            return await httpResponseMessage.Content.ReadAsStringAsync();
        }

        private async Task<TOut> BasePost<TOut>(object data, string url)
        {
            var content = new StringContent(JsonSerializer.Serialize(data));
            var httpResponseMessage = await HttpClient.PostAsync(url, content);
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
            return await httpResponseMessage.Content.ReadAsStringAsync();
        }

        public async Task<UploadResponse> Upload(byte[] dataToUpload) => await Upload(new BufferedStream(new MemoryStream(dataToUpload)));

        public async Task<UploadResponse> Upload(BufferedStream stream)
        {
            var fileMetadata = await InitiateFileUpload(new InitiateUploadRequest(stream.Length));
            var url = $"https://api.nightfall.ai/v3/upload/{fileMetadata.Id}";
            for (int i = 0; i < stream.Length; i += fileMetadata.ChunkSize)
            {
                HttpClient.DefaultRequestHeaders.TryAddWithoutValidation("X-Upload-Offset", i.ToString());
                var currentChunkSize = Math.Min(fileMetadata.ChunkSize, fileMetadata.FileSizeBytes-i);
                var dataToUpload = new byte[currentChunkSize];
                stream.Read(dataToUpload, 0, currentChunkSize);
                var response = await HttpClient.PatchAsync(url,
                    new ByteArrayContent(dataToUpload));
                if (!response.IsSuccessStatusCode)
                    throw new UploadException($"Could not update file properly, failed at {i + 1} chunk", fileMetadata);
                HttpClient.DefaultRequestHeaders.Remove("X-Upload-Offset");
            }

            return await BasePost<UploadResponse>(new object(),
                $"https://api.nightfall.ai/v3/upload/{fileMetadata.Id}/finish");
        }
        
        
    }
}