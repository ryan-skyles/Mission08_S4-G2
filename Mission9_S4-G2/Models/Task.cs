using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mission8_S4_G2.Models
{
    public class Task
    {
        [Key]
        public int TaskId { get; set; }

        [Required(ErrorMessage = "Task name is required.")]
        public string? TaskName { get; set; }

        public string? DueDate { get; set; }

        [Required(ErrorMessage = "Quadrant is required.")]
        [Range(1, 4, ErrorMessage = "Quadrant must be between 1 and 4.")]
        public int Quadrant { get; set; }

        [ForeignKey("CategoryId")]
        [Required(ErrorMessage = "Please select a category.")]
        public int? CategoryId { get; set; }

        public Category? Category { get; set; }

        public bool Completed { get; set; }
    }
}
