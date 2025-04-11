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
        //when sending message, if it fails an exception is thrown. Use try Catch
        var orgAccountSid = _twilioConfig.AccountID;
        var authToken = _twilioConfig.AuthToken;
        var userAccountSid = message.Id.ToString();
        var recieve = message.PhoneNumber;
        var sender = message.CreatedBy;
        TwilioClient.Init(orgAccountSid, authToken);

        Twilio.Base.ResourceSet<Twilio.Rest.PreviewIam.Organizations.AccountResource> accountList = null;
        accountList = Twilio.Rest.PreviewIam.Organizations.AccountResource.Read(pathOrganizationSid: orgAccountSid);
        var account = Twilio.Rest.PreviewIam.Organizations.AccountResource.Fetch(pathOrganizationSid: orgAccountSid, pathAccountSid: userAccountSid);

        var messageOptions = new CreateMessageOptions(new PhoneNumber(recieve));
        messageOptions.Body = message.PhoneMessage;

        var messageResource = MessageResource.Create(messageOptions);
        

        ;
    }
}