using System;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json.Linq;

namespace AzureEventsDemo.Functions.Models
{
    public class AzureEventEntity : TableEntity
    {
        public string ResourceProvider { get; set; }
        public string ResourceUri { get; set; }
        public DateTime? EventTime { get; set; }
        public string Emailaddress { get; set; }
        public string Name { get; set; }
        public string Oid { get; set; }
        public string OperationName { get; set; }
        public string Status { get; set; }

        public static AzureEventEntity Parse(JToken eventGridEvent)
        {
            return new AzureEventEntity
            {
                PartitionKey = "TODO",
                RowKey = (string)eventGridEvent["id"],
                ResourceProvider = (string)eventGridEvent["data"]["resourceProvider"],
                ResourceUri = (string)eventGridEvent["data"]["resourceUri"],
                EventTime = (DateTime?)eventGridEvent["eventTime"],
                Emailaddress = (string)eventGridEvent["data"]["claims"]["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"],
                Name = (string)eventGridEvent["data"]["name"],
                Oid = (string)eventGridEvent["data"]["claims"]["http://schemas.microsoft.com/identity/claims/objectidentifier"],
                OperationName = (string)eventGridEvent["data"]["operationName"],
                Status = (string)eventGridEvent["data"]["status"]
            };
        }
    }
}
