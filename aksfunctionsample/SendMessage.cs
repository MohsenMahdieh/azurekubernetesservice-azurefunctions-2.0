using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace aksfunctionsample
{
    public static class MessageFunc
    {
        [FunctionName("sendmessage")]
        public static async Task<IActionResult> SendMessage(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", "options")]HttpRequest req,
            [SignalR(HubName = "testazuresignalrhub")]IAsyncCollector<SignalRMessage> signalRMessages, ILogger log)
        {
            try
            {
                if (!req.HttpContext.Response.Headers.ContainsKey("Access-Control-Allow-Credentials"))
                {
                    req.HttpContext.Response.Headers.Add("Access-Control-Allow-Credentials", "true");
                }

                if (req.Headers.ContainsKey("Origin") && !req.HttpContext.Response.Headers.ContainsKey("Access-Control-Allow-Origin"))
                {
                    req.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", req.Headers["Origin"][0]);
                }

                if (req.Headers.ContainsKey("Access-Control-Request-Headers"))
                {
                    req.HttpContext.Response.Headers.Add("Access-Control-Allow-Headers", req.Headers["access-control-request-headers"][0]);
                }

                // Commeting since not passing json payload
                // var message = new JsonSerializer().Deserialize(new JsonTextReader(new StreamReader(req.Body)));
                var message = new StreamReader(req.Body).ReadToEnd();

                await signalRMessages.AddAsync(
                    new SignalRMessage
                    {
                        Target = "sendMessage",
                        Arguments = new[] { message }
                    });

                log.LogInformation("sendMessage API succeded.");

                return new OkResult();
            }
            catch
            {
                return new BadRequestResult();
            }
        }
    }
}
