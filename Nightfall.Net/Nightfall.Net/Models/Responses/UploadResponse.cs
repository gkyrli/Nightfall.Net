using System.Text.Json.Serialization;

namespace Nightfall.Net.Models.Responses
{
    public class UploadResponse
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("fileSizeBytes")]
        public long FileSizeBytes { get; set; }
        [JsonPropertyName("chunkSize")]
        public int ChunkSize { get; set; }
        [JsonPropertyName("mimeType")]
        public string MimeType { get; set; }
    }
}