using System.ComponentModel.DataAnnotations;

namespace Persistence.Entities
{
    public class Entity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
