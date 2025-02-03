using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstantReminders.Contracts.Models;

public class DaysOfWeekEntity : IEntity
{
    public virtual required Guid Id { get; set; }
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public DateTime UpdatedDateTime { get; set; }
    public required string CreatedBy { get; set; }
    public required string UpdatedBy { get; set; }
    public required NotificationSchedule NotificationSchedule { get; set; }
}
