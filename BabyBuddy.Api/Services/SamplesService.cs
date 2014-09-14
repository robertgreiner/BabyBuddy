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

        public SampleEntity MotionDetected(string deviceId)
        {
            var table = SamplesTable();

            var sample = new SampleEntity(deviceId) { SampleType = SampleTypes.Motion, Message = "Motion Detected" };

            var insert = TableOperation.Insert(sample);
            table.Execute(insert);
            return sample;
        }

        public List<SampleEntity> GetMotionSamples(string deviceId)
        {
            var table = SamplesTable();
            //var rangeQuery = new TableQuery<SampleEntity>().Where(
            //    TableQuery.CombineFilters(
            //        TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, deviceId),
            //        TableOperators.And,
            //        TableQuery.GenerateFilterCondition("Timestamp", QueryComparisons.GreaterThan, DateTime.UtcNow.Subtract(new TimeSpan(0, 0, 5, 0)).ToString())));

            TableQuery<SampleEntity> query = new TableQuery<SampleEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, deviceId));

            if (query == null)
            {
                return new List<SampleEntity>();
            }
            var result = query.ToList();

            return result;
        }

        private static CloudTable SamplesTable()
        {
            var storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

            var tableClient = storageAccount.CreateCloudTableClient();
            var table = tableClient.GetTableReference("samples");
            table.CreateIfNotExists();
            return table;
        }
    }
}