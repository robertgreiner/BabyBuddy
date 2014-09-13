using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using BabyBuddy.Api.Models;

namespace BabyBuddy.Api.Services
{
    public class SamplesService
    {
        public void MotionDetected(string deviceId)
        {
            var storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            
            var tableClient = storageAccount.CreateCloudTableClient();
            var table = tableClient.GetTableReference("samples");
            table.CreateIfNotExists();

            var sample = new Sample(deviceId) { SampleType = SampleTypes.Motion, Message = "Motion Detected" };

            var insert = TableOperation.Insert(sample);
            table.Execute(insert);
        }
    }
}