using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace aksfunctionsample
{
    public static class TestEventHubFunc
    {
        [FunctionName("TestEventHubFunc")]
        public static void Run([EventHubTrigger("testeventhub", Connection = "AzureEventHubConnectionString")]string myEventHubMessage, ILogger log)
        {
            log.LogInformation($"TestEventHubFunc Event Hub trigger function processed a message: {myEventHubMessage}");
        }
    }
}
