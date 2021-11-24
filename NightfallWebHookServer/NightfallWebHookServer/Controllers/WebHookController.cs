using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nightfall.Net.Models.Webhook;
using Nightfall.Net.WebhookSignature;

namespace NightfallWebHookServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebHookController : ControllerBase
    {
        private readonly ILogger<WebHookController> _logger;

        private const string Secret =
            "your webhook secret key from dashboard";

        public WebHookController(ILogger<WebHookController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [Route("night")]
        public async Task<ActionResult<string>> NightfallWebHook()
        {
            var raw = await new StreamReader(Request.Body).ReadToEndAsync();
            var request = JsonSerializer.Deserialize<WebhookRequest>(raw);
            if (request.IsChallengeRequest())
                return request.Challenge;
            var checkSignatureExists =
                HttpContext.Request.Headers.TryGetValue("X-Nightfall-Signature", out var givenSignature);
            var checkTimestamp = HttpContext.Request.Headers.TryGetValue("X-Nightfall-Timestamp", out var nonce);
            if (!checkTimestamp || !checkSignatureExists)
                return NotFound("Timestamp or Signature headers were not found");

            var validator = new WebhookSignatureValidator(Secret);
            if (validator.Validate(raw, nonce[0], givenSignature[0]))
            {
                return Ok();
            }
            return Unauthorized();
        }
    }
}