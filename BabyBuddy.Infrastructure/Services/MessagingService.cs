using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Twilio;

namespace BabyBuddy.Infrastructure.Services
{
    public class MessagingService
    {
        public void SendSms(string message)
        {
            var accountSid = CloudConfigurationManager.GetSetting("TwilioAccountSid");
            var authToken = CloudConfigurationManager.GetSetting("TwilioAuthToken");
            var fromNumber = CloudConfigurationManager.GetSetting("TwilioFromNumber");
            var toNumber = CloudConfigurationManager.GetSetting("TwilioToNumber");

            var twilio = new TwilioRestClient(accountSid, authToken);
            var sentMessage = twilio.SendMessage(fromNumber, toNumber, message, "");

            Console.WriteLine(sentMessage.Sid);
        }
    }
}
