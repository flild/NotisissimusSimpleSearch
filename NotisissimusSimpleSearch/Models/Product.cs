
using System.ComponentModel.DataAnnotations;

namespace NotisissimusSimpleSearch.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        public string? Description { get; set; }
    }
}
