using ConstantReminders.Contracts.Config;
using ConstantReminders.Contracts.Interfaces.Business;
using ConstantReminders.Contracts.Models;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;


namespace ConstantReminders.Services;

public class TwilioService : ITwilioService
{
    private TwilioConfig _twilioConfig;
    public TwilioService( TwilioConfig twilioConfig)
    {
        _twilioConfig = twilioConfig;
    }
     
    public async Task<TwilioResponse> SendMessageAsync(TwilioPhoneMessage message)
    {
        var accountSid = _twilioConfig.AccountID;
        var authToken = _twilioConfig.AuthToken;
        //when sending message, if it fails an exception is thrown. Use try Catch
        

        try
        {
            TwilioClient.Init(accountSid, authToken);
            await Task.CompletedTask;
            var messageOptions = new CreateMessageOptions("+18777804236");
            messageOptions.From = new PhoneNumber("+18773092720");
            messageOptions.Body = message.PhoneMessage;
            var messageSend = MessageResource.Create(messageOptions);
            Console.WriteLine(messageSend.Body);
            var response = new TwilioResponse(true, "none");
            return response;
        }

        catch (Exception ex)
        {
            return new TwilioResponse(false, ex.Message);
        }
        
        
        
    }
}