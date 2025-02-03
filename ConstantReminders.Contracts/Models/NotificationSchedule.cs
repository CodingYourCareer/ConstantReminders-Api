using ConstantReminders.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstantReminders.Contracts.Models;

public class NotificationSchedule : IEntity

{    
    public virtual required Guid Id { get; set; }
    public required NotificationType NotificationType { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public DateTime UpdatedDateTime { get; set; }
    public required string CreatedBy { get; set; }
    public required string UpdatedBy { get; set; }
    public required TimeSpan FrequencyWithinDay { get; set; }
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public Guid EventId { get; set; }
    public Event Event { get; set; }
    public required List<DaysOfWeekEntity> DaysOfWeek { get; set; }
    public int? DurationInDays { get; set; }
}
