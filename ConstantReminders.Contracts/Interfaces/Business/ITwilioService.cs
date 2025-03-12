using ConstantReminders.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ConstantReminders.Contracts.Interfaces.Business;

public interface ITwilioService
{
    Task<TwilioPhoneMessage> SendMessageAsync(string phoneNumber, string phoneMessage);
}
