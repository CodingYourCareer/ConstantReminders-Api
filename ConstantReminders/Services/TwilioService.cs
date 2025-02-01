using System;
using ConstantReminders.Contracts.Models.TwilioConfiguration;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace ConstantReminders.Services;

public class TwilioService
{

private readonly TwilioConfiguration _configuration;

 public TwilioService(TwilioConfiguration configuration)

    {

        _configuration = configuration;

    }

     public async Task SendSms(string toNumber, string message)

    {

    
       
         TwilioClient.Init(_configuration.AccountSID,_configuration.AuthToken);



        var message1 = await MessageResource.CreateAsync(
            body: "This is the ship that made the Kessel Run in fourteen parsecs?",
            from: new Twilio.Types.PhoneNumber("+2512748694"),
            to: new Twilio.Types.PhoneNumber("+9546382352"));

            Console.WriteLine(message1.Body);

    }



}
