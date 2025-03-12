using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstantReminders.Contracts.Models;

public class TwilioPhoneMessage
{
    string phoneNumber { get; set; }
    string phoneMessage { get; set; }
}
