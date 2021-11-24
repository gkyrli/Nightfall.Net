using System.Text.Json.Serialization;

namespace Nightfall.Net.Models.Requests
{
    public class InitiateUploadRequest
    {
        [JsonPropertyName("fileSizeBytes")]
        public long FileSizeBytes { get; set; }

        public InitiateUploadRequest()
        {
            
        }
        public InitiateUploadRequest(long fileSizeBytes)
        {
            FileSizeBytes = fileSizeBytes;
        }
    }
}