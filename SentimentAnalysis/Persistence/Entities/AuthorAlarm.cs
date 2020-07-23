using BusinessLogic.Enums;
using System.ComponentModel.DataAnnotations;

namespace Persistence.Entities
{
    public class AuthorAlarm
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public virtual Alarm Alarm { get; set; }

        [Required]
        public TimeMeasure TimeMeasure { get; set; }
    }
}
