using System;

namespace ConstantReminders.Contracts.Interfaces.Business;

public interface ITwilioService
{

    Task SendSms(string toNumber, string message);

}
