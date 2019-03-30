using System;
using Microsoft.WindowsAzure.Storage.Table;

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
    }
}
