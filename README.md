# Nightfall.Net

Register and sign in to create an API key. [Dashboard](https://app.nightfall.ai/developer-platform)

Visit the developer documentation for more details about integrating with the Nightfall API. [Documentation](https://docs.nightfall.ai/docs/entities-and-terms-to-know)

# ToDo

* Nuget for .NetCore3.1 & .Net6
* Automate nuget package generation

# Dependencies
Needs .Net Core 3.1

# Installation
For now clone and reference the Nightfall.Net.csproj file in Nightfall.Net/Nightfall.Net folder 

*TODO: A Nuget will be provided*

# Various quality of life features 

### DetectorGlossary Enum provides all the predefined detectors from nightfall.

```
new Detector(DetectorGlossary.IP_ADDRESS, "IpAddress provided detector")
```
### Derived Exceptions for every possible error the API can throw according to the [api reference](https://docs.nightfall.ai/reference/scanpayloadv3)
*Provided exception classes:*
```
BaseNightfallException
NightfallInvalidRequest400
NightfallAuthenticationFailure401
NightfallInvalidFileId404
NightfallIncorrectFileState409
NightfallUnprocessableRequestPayload422
NightfallRateLimit429
NightfallInternalServer500
NightfallUnknownExceptionResponse 
```

### Detectors provide a fluent api allowing for a cleaner way of configuring your detector 
```
//Some examples
WithRegexExclusionRule(MatchType matchType=MatchType.PARTIAL,string pattern = null, bool isCaseSensitive = default)
WithContextRule(string pattern,bool isCaseSensitive=default, int windowBefore = 10, int windowAfter = 10,
            ConfidenceEnum confidenceAdjustment = ConfidenceEnum.VERY_LIKELY)

and more...
```
# Samples

## Sample console code:
```
class Program
    {
        static async Task Main(string[] args)
        {
            var nightfallClient = new NightfallClient("your API-KEY");
            await ScanRequest(nightfallClient);
            var scanFileRequest = await ScanFileRequest(nightfallClient);
            Console.WriteLine(scanFileRequest);
        }

        private static async Task ScanRequest(NightfallClient nightfallClient)
        {
            var requestData = new ScanRequestConfig();
            requestData.AddPayload("192.168.162.5", "0.0.0.0", "1.1.1.1 dasdasd 129.0.0.2");
            var anyDetectionRule = DetectionRule.GetANYDetectionRule("Any of the provided detectors");
            // var detector = new Detector(new Regex("(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)"),"display")
            // .WithMaskRedactionConfig("*",null,1,true);
            var detector = new Detector(DetectorGlossary.IP_ADDRESS, "IpAddress provided detector")
            .WithMaskRedactionConfig("*", null, 1, true);
            anyDetectionRule.AddDetectors(detector);
            requestData.AddDetectionRules(anyDetectionRule);
            var content = await nightfallClient.GetScanFindingsAsync(requestData);
            Console.WriteLine(JsonSerializer.Serialize(content));
        }

        private static async Task<string> ScanFileRequest(NightfallClient nightfallClient)
        {
            var requestData = new ScanUploadedRequest("https://e307-93-109-43-66.ngrok.io/webhook/night",
                requestMetadata: "its your test brooo");
            requestData.AddDetectionRuleUUids("b7d263e5-c7f9-43dc-b8ab-b972bcb7a0c2");

            Console.WriteLine(JsonSerializer.Serialize(requestData));
            var uploadFile = await UploadFile(nightfallClient);
            var content = await nightfallClient.GetScanFilePlainTextAsync(requestData, uploadFile.Id);
            return content;
        }

        private static async Task<UploadResponse> UploadFile(NightfallClient nightfallClient)
        {
            var dataToUpload = Encoding.ASCII.GetBytes(RandomStringUtil.GenerateRandomString(length: 3100000));
            var stream = new BufferedStream(new MemoryStream(dataToUpload));
            try
            {
                return await nightfallClient.Upload(stream);
            }
            //Specialize your logging depending on the exception received
            catch (NightfallRateLimit429 e)
            {
                Console.WriteLine("Log rate limit exceeded");
                throw;
            }
            catch (NightfallIncorrectFileState409 e)
            {
                Console.WriteLine(e.ApiErrorResponse.Dump());
                throw;
            }
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

## Sample Webhook server code for testing file scan endpoint

* Clone and run the NightfallWebHookServer provided in the repository.
* Download [ngrok](https://ngrok.com/) and use it to make the webhook server available publicly.
* Make sure you expose the NightfallWebHookServer using the http port for now. The https port is not working for now.
* The webhook url will look similar to this *https://fb71-128-0-214-119.ngrok.io/webhook/night*
* In the console app sample seen above replace *webhook_url_to_replace* with the url you got from ngrok concatenated with /webhook/night


