using System.Text.Json.Serialization;

namespace Nightfall.Net.Models.Requests
{
    public class InitiateUploadRequest
    {
        [JsonPropertyName("fileSizeBytes")]
        public int FileSizeBytes { get; set; }

        public InitiateUploadRequest()
        {
            
        }
        public InitiateUploadRequest(int fileSizeBytes)
        {
            FileSizeBytes = fileSizeBytes;
        }
    }
}