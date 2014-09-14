using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BabyBuddy.Infrastructure.Models;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Newtonsoft.Json;

namespace BabyBuddy.Infrastructure.Services
{
    public class QueueService
    {
        private static CloudStorageAccount CloudStorageAccount
        {
            get
            {
                return CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            }
        }

        public void ProcessMessage()
        {
            var storageAccount = CloudStorageAccount;
            var queueClient = storageAccount.CreateCloudQueueClient();
            var queue = queueClient.GetQueueReference("babybuddy");
            queue.CreateIfNotExists();

            var queueMessage =  queue.GetMessage(new TimeSpan(0, 0, 15, 0));

            if (queueMessage == null)
            {
                return;
            }

            var jsonMessage = queueMessage.AsString;

            var message = JsonConvert.DeserializeObject<SampleMessage>(jsonMessage);

            var messagingService = new MessagingService();
            messagingService.SendSms(message.Message);

            queue.DeleteMessage(queueMessage);
        }
    }
}
