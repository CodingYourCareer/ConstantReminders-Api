using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstantReminders.Contracts.Models;

public class TwilioPhoneMessage : IEntity 
{
    public Guid Id { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public DateTime UpdatedDateTime { get; set; }
    public string CreatedBy { get; set; }
    public string UpdatedBy { get; set; }
    public string PhoneNumber { get; set; }
    public string PhoneMessage { get; set; }
}
