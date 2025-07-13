using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeApp.Models
{
    public class EmpSalaryDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Id")]
        public int EmpId { get; set; }
        [MaxLength(20)]
        public string? Month { get; set; }
        public int Year { get; set; }
        public int BasicSalary { get; set; }
        public int Hra { get; set; }
        public int TransportAllowances { get; set; }
        public int DiningAllowances { get; set; }
        public int Reimbursement { get; set; }
        public int IncomeTax { get; set; }
        public int CrossEarningDeductions { get; set; }
        public int Epf { get; set; }
    }
}
