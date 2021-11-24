using System;
using System.Text.Json;
using Nightfall.Net.Models.Responses;

namespace Nightfall.Net.Exceptions
{
    public class UploadException:Exception
    {
        public UploadException(string? message, UploadResponse metadata) : base(message)
        {
            this.metadata = metadata;
        }

        public UploadResponse metadata { get; set; }
        public override string ToString()
        {
            return $"{JsonSerializer.Serialize(metadata)} {Environment.NewLine} {base.ToString()}";
        }
    }
}