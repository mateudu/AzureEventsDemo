using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AzureEventsDemo.Functions.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AzureEventsDemo.Functions
{
    public static class ProcessMessage
    {
        [FunctionName("ProcessMessage")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            [Table("AzureEvents", Connection = "AzureWebJobsStorage")] IAsyncCollector<AzureEventEntity> table,
            ILogger log)
        {
            log.LogInformation("ProcessMessage: START");

            string response = string.Empty;
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            
            log.LogInformation($"Payload: {requestBody}");

            var events = JsonConvert.DeserializeObject<JArray>(requestBody);
            foreach (var eventGridEvent in events)
            {
                if (eventGridEvent["eventType"].Value<string>() == "Microsoft.EventGrid.SubscriptionValidationEvent")
                {
                    var responseData = new
                    {
                        ValidationResponse = eventGridEvent["data"]["validationCode"]
                    };

                    return new OkObjectResult(responseData);
                }
                else
                {
                    var requestObject = AzureEventEntity.Parse(eventGridEvent);
                    await table.AddAsync(requestObject);
                }
            }

            return new OkObjectResult(response);
        }
    }
}
