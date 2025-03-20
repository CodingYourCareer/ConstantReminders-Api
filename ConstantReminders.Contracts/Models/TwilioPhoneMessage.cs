using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstantReminders.Contracts.Models;

public class TwilioPhoneMessage
{
    public string PhoneNumber { get; set; }
    public string PhoneMessage { get; set; }
}
