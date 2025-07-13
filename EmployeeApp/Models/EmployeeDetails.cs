using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;

namespace EmployeeApp.Models
{
    public class EmployeeDetails
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Name { get; set; }

        [MaxLength(100)]
        public string? Department { get; set; }

        [MaxLength(100)]
        public string? Position { get; set; }
    }
}
