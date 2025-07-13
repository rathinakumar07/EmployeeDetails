namespace EmployeeApp.Dtos
{
    public class EmpSalaryDetailsDto
    {
        public int Id { get; set; }
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
