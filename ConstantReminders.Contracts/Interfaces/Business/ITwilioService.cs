using ConstantReminders.Contracts.Models;


namespace ConstantReminders.Contracts.Interfaces.Business;

public interface ITwilioService
{
    Task<TwilioResponse> SendMessageAsync(TwilioPhoneMessage message);
}
