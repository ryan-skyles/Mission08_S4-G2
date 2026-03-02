using System.ComponentModel.DataAnnotations;

namespace Mission8_S4_G2.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        public string? CategoryName { get; set; }
    }
}
