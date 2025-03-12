using ConstantReminders.Contracts.Interfaces.Business;
using ConstantReminders.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstantReminders.Services;

public class TwilioService : ITwilioService
{
    public Task<TwilioPhoneMessage> SendMessageAsync(string phoneNumber, string phoneMessage)
    {
        
        throw new NotImplementedException();
    }
}
