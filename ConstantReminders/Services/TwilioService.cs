using ConstantReminders.Contracts.Interfaces.Business;
using ConstantReminders.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Twilio;
using Twilio.Types;


namespace ConstantReminders.Services;

public class TwilioService : ITwilioService
{
    public Task<TwilioPhoneMessage> SendMessageAsync(string number, string message)
    {
        var sendMessage = new TwilioPhoneMessage
        {
            PhoneNumber = number,
            PhoneMessage = message
        };

        

        return new Task<TwilioPhoneMessage>
        {
            
        };
        throw new NotImplementedException();
    }
}

using System;
using System.Collections.Generic;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

//class Example
//{
//    static void Main(string[] args)
//    {
//        var accountSid = "AC8a8d5e3c965c36bba4fd1af99a5476d4";
//        var authToken = "[AuthToken]";
//        TwilioClient.Init(accountSid, authToken);

//        var messageOptions = new CreateMessageOptions(
//          new PhoneNumber("+18777804236"));
//        messageOptions.Body = "Hello from Twilio";


//        var message = MessageResource.Create(messageOptions);
//        Console.WriteLine(message.Body);
//    }
//}
