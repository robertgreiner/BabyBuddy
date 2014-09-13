using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.Storage.Table;

namespace BabyBuddy.Api.Models
{
    public class Sample : TableEntity
    {
        public Sample(string deviceId)
        {
            PartitionKey = deviceId;
            RowKey = Guid.NewGuid().ToString();
        }

        public string SampleType { get; set; }
        public string Message { get; set; }
    }
}