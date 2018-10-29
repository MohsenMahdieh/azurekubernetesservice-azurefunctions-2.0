
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using Newtonsoft.Json;

namespace aksfunctionsample
{
    public static class Negotiate
    {
        [FunctionName("negotiate")]
        public static IActionResult GetSignalRInfo(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", "options")]HttpRequest req,
            [SignalRConnectionInfo(HubName = "testazuresignalrhub")]SignalRConnectionInfo connectionInfo, ILogger log)
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

                log.LogInformation("negotiate API succeded.");

                if (connectionInfo == null)
                {
                    return new NotFoundObjectResult("Azure SignalR not found.");
                }

                return new OkObjectResult(connectionInfo);
            }
            catch
            {
                return new BadRequestResult();
            }
        }
    }
}
