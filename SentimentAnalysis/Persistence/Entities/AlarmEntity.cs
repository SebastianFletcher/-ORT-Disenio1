using BusinessLogic.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Persistence.Entities
{
    public class AlarmEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PostQuantity { get; set; }

        [Required]
        public int PostCount { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        [Required]
        public int NumberDays { get; set; }

        [Required]
        public SentimentType Type { get; set; }

        [Required]
        public virtual Entity Entity { get; set; }

    }
}
