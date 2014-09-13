using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;

namespace BabyBuddy.Infrastructure.Services
{
    public class MessagingService
    {
        public void SendSms(string message)
        {
            var AccountSid = "AC4295b2da12b979f1d5007e31b86c4239";
            var AuthToken = "0468165cd423ae7b70190795c4bd79c1";
            var fromNumber = "+12143076468";
            var toNumber = "+12149860739";

            var twilio = new TwilioRestClient(AccountSid, AuthToken);
            var sentMessage = twilio.SendMessage(fromNumber, toNumber, message, "");

            Console.WriteLine(sentMessage.Sid);
        }
    }
}
