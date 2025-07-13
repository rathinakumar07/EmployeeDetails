using EmployeeApp.Models;

namespace EmployeeApp.Parser._Interfaces
{
    internal interface IEmployeeFileParser
    {
        IList<EmployeeDetails> GetEmployees(Stream file);
        IList<EmpSalaryDetails> GetSalaries(Stream file);

    }
}
