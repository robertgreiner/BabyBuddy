using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using BabyBuddy.Infrastructure.Services;

namespace BabyBuddy.Worker
{
    public class WorkerRole : RoleEntryPoint
    {
        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private readonly ManualResetEvent runCompleteEvent = new ManualResetEvent(false);

        public override void Run()
        {
            Trace.TraceInformation("BabyBuddy.Worker is running");

            //var messagingService = new MessagingService();
            //messagingService.SendSms("test message from worker role");

            try
            {
                this.RunAsync(this.cancellationTokenSource.Token).Wait();
            }
            finally
            {
                this.runCompleteEvent.Set();
            }
        }

        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections
            ServicePointManager.DefaultConnectionLimit = 12;

            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.

            bool result = base.OnStart();

            Trace.TraceInformation("BabyBuddy.Worker has been started");

            return result;
        }

        public override void OnStop()
        {
            Trace.TraceInformation("BabyBuddy.Worker is stopping");

            this.cancellationTokenSource.Cancel();
            this.runCompleteEvent.WaitOne();

            base.OnStop();

            Trace.TraceInformation("BabyBuddy.Worker has stopped");
        }

        private async Task RunAsync(CancellationToken cancellationToken)
        {
            var queueService = new QueueService();
            while (!cancellationToken.IsCancellationRequested)
            {
                queueService.ProcessMessage();
                await Task.Delay(10000);
            }
        }
    }
}
