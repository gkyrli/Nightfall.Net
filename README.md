# Nightfall.Net


Register and sign in to create an API key. [Dashboard](https://app.nightfall.ai/developer-platform)

Visit the developer documentation for more details about integrating with the Nightfall API. [Documentation](https://docs.nightfall.ai/docs/entities-and-terms-to-know)

# Dependencies
Needs .Net Core 3.1

# Installation
For now clone and reference the Nightfall.Net.csproj file in Nightfall.Net/Nightfall.Net folder 

* TODO: A Nuget will be provided *

# Samples

Sample console code:
```
class Program
    {
        static async Task Main(string[] args)
        {
            var nightfallClient = new NightfallClient("secret");
            await ScanRequest(nightfallClient);
            var scanFileRequest = await ScanFileRequest(nightfallClient);

            Console.WriteLine(scanFileRequest);
        }

        private static async Task ScanRequest(NightfallClient nightfallClient)
        {
            var requestData = new ScanRequestConfig();
            requestData.AddPayload("192.168.162.5", "0.0.0.0", "1.1.1.1 dasdasd 129.0.0.2");
            requestData.AddDetectionRuleUUids("b7d263e5-c7f9-43dc-b8ab-b972bcb7a0c2");
            var content = await nightfallClient.GetScanFindingsAsync(requestData);
            Console.WriteLine(JsonSerializer.Serialize(content));
        }

        private static async Task<string> ScanFileRequest(NightfallClient nightfallClient)
        {
            var requestData = new ScanUploadedRequest("https://fb71-128-0-214-119.ngrok.io/webhook/night",
                requestMetadata: "its your test brooo");
            requestData.AddDetectionRuleUUids("b7d263e5-c7f9-43dc-b8ab-b972bcb7a0c2");
            Console.WriteLine(JsonSerializer.Serialize(requestData));

            var uploadFile = await UploadFile(nightfallClient);
            var content = await nightfallClient.GetScanFilePlainTextAsync(requestData, uploadFile.Id);
            return content;
        }

        private static async Task<UploadResponse> UploadFile(NightfallClient nightfallClient)
        {
            var dataToUpload = Encoding.ASCII.GetBytes(RandomStringUtil.GenerateRandomString(length: 100000));
            return await nightfallClient.Upload(dataToUpload);
        }
    }
    class RandomStringUtil
    {
        private static Random random = new Random();
        public static string GenerateRandomString(int length)
        {
            const string chars = "0123456789.";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray())+". 19.2.12.5";
        }
    }

```
