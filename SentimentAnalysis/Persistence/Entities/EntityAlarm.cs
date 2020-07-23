using System.ComponentModel.DataAnnotations;

namespace Persistence.Entities
{
    public class EntityAlarm
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public virtual Alarm Alarm { get; set; }

        [Required]
        public virtual Entity Entity { get; set; }
    }
}
