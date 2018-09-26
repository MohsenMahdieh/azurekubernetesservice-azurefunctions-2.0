using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace aksfunctionsample
{
    public static class TestBlobStorageFunc
    {
        [FunctionName("TestBlobStorageFunc")]
        public static void Run([BlobTrigger("testblob/{name}", Connection = "AzureBlobStorage")]Stream myBlob, string name, ILogger log)
        {
            log.LogInformation($"TestBlobStorageFunc Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");
        }
    }
}
