using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstantReminders.Contracts.Models
{
    public class NotificationSchedule : IEntity

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
        public enum Day
        {
            Monday,
            Tuesday,
            Wednesday,
            Thursday,
            Friday,
            Saturday,
            Sunday
        }
        public virtual required Guid Id { get; set; }
        public required NotificationType NotiType { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public required string CreatedBy { get; set; }
        public required string UpdatedBy { get; set; }
        public required TimeSpan FrequencyWithinDay { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public required Day[] DaysOfWeek { get; set; }
        public int? DurationInDays { get; set; }


    }
}
