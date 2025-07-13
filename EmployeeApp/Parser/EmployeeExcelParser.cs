using EmployeeApp.Models;
using EmployeeApp.Parser._Interfaces;
using OfficeOpenXml;
using System.ComponentModel;
using System.IO;

namespace EmployeeApp.Parser
{
    public class EmployeeExcelParser : IEmployeeFileParser
    {
        IList<EmployeeDetails> IEmployeeFileParser.GetEmployees(Stream file)
        {
            var employees = new List<EmployeeDetails>();

            // If using .NET Core, you need this line
            ExcelPackage.License.SetNonCommercialPersonal("Personal");

            using (var package = new ExcelPackage(file))
            {
                var worksheet = package.Workbook.Worksheets[0]; // First worksheet
                var rowCount = worksheet.Dimension.End.Row;
                for (int row = 2; row <= rowCount; row++) // Start from row 2 to skip header
                {
                    var employee = new EmployeeDetails
                    {
                        Id = Convert.ToInt32(worksheet.Cells[row, 1].Value),
                        Name = worksheet.Cells[row, 2].Value?.ToString(),
                        Department = worksheet.Cells[row, 3].Value?.ToString(),
                        Position = worksheet.Cells[row, 4].Value?.ToString(),
                    };
                    employees.Add(employee);
                }
            }
            return employees;
        }

        IList<EmpSalaryDetails> IEmployeeFileParser.GetSalaries(Stream file)
        {
            var salaries = new List<EmpSalaryDetails>();

            // If using .NET Core, you need this line
            ExcelPackage.License.SetNonCommercialPersonal("Personal");

            using (var package = new ExcelPackage(file))
            {
                var worksheet = package.Workbook.Worksheets[0]; // First worksheet
                var rowCount = worksheet.Dimension.End.Row;
                for (int row = 2; row <= rowCount; row++) // Start from row 2 to skip header
                {
                    var salary = new EmpSalaryDetails
                    {
                        Id = Convert.ToInt32(worksheet.Cells[row, 1].Value),
                        Month = worksheet.Cells[row, 5].Value.ToString(),
                        Year = Convert.ToInt32(worksheet.Cells[row, 6].Value),
                        BasicSalary = Convert.ToInt32(worksheet.Cells[row, 7].Value),
                        Hra = Convert.ToInt32(worksheet.Cells[row, 8].Value),
                        TransportAllowances = Convert.ToInt32(worksheet.Cells[row, 9].Value),
                        DiningAllowances = Convert.ToInt32(worksheet.Cells[row, 10].Value),
                        Reimbursement = Convert.ToInt32(worksheet.Cells[row, 11].Value),
                        IncomeTax = Convert.ToInt32(worksheet.Cells[row, 12].Value),
                        CrossEarningDeductions = Convert.ToInt32(worksheet.Cells[row, 13].Value),
                        Epf = Convert.ToInt32(worksheet.Cells[row, 14].Value),
                    };
                    salaries.Add(salary);
                }
            }
            return salaries;
        }
    }
}
