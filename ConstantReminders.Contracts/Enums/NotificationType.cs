using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstantReminders.Contracts.Enums
{
    public enum NotificationType
    {
        Single,
        Daily,
        Weekly,
        Monthly,
        RepeatedWithinDay,
        Custom
    }
}
