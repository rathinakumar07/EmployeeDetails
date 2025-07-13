using AspNetCoreGeneratedDocument;
using EmployeeApp.DataAccess;
using EmployeeApp.Dtos;
using EmployeeApp.Models;
using EmployeeApp.Parser;
using EmployeeApp.Parser._Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.IO;

namespace EmployeeApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            try
            {
                if (file != null)
                {

                    // Save file
                    using (var fileStream = file.OpenReadStream())
                    {
                        IEmployeeFileParser parser = new EmployeeExcelParser();
                        IList<EmployeeDetails> employees = parser.GetEmployees(fileStream);
                        IList<EmpSalaryDetails> salaries = parser.GetSalaries(fileStream);

                        using(DbRepository dbRepository = new DbRepository())
                        {
                            // Save employees
                            foreach (var employee in employees)
                            {
                                await dbRepository.AddEmployeeAsync(employee);
                            }
                            // Save salaries
                            foreach (var salary in salaries)
                            {
                                await dbRepository.AddSalaryAsync(salary);
                            }
                        }

                        return View("Index", employees);
                    }                    
                }
                return BadRequest("No file selected");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        
        public ActionResult ViewSalaryDetails(int id)
        {
            using (DbRepository dbRepository = new DbRepository())
            {
                // Await the async method to get the result
                var detailsTask = dbRepository.GetSalaryByIdAsync(id);
                detailsTask.Wait();
                var details = detailsTask.Result;

                if (details == null)
                {
                    return NotFound($"No salary details found for employee ID {id}");
                }

                // Await the async method to get the employee name
                var employeeTask = dbRepository.GetEmployeeByIdAsync(details.EmpId);
                employeeTask.Wait();
                var employee = employeeTask.Result;

                EmpSalaryDetailsDto detailsDto = new EmpSalaryDetailsDto
                {
                    Id = details.EmpId,
                    Name = employee?.Name,
                    Month = details.Month,
                    Year = details.Year,
                    BasicSalary = details.BasicSalary,
                    Hra = details.Hra,
                    TransportAllowances = details.TransportAllowances,
                    DiningAllowances = details.DiningAllowances,
                    Reimbursement = details.Reimbursement,
                    IncomeTax = details.IncomeTax,
                    CrossEarningDeductions = details.CrossEarningDeductions,
                    Epf = details.Epf
                };
                return View("EmployeeSalaryView", detailsDto);
            }
        }
    }
}
