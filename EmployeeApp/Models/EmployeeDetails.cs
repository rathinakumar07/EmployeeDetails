using Microsoft.VisualBasic;

namespace EmployeeApp.Models
{
    public class EmployeeDetails
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Department { get; set; }
        public string? Position { get; set; }

        public DateAndTime DataOfJoining { get; set; }
    }
}
