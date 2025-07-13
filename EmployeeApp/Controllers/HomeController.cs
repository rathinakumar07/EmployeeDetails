using AspNetCoreGeneratedDocument;
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
                       // TODO: Save the details to DB
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
        
        public IActionResult ViewSalaryDetails(int id)
        {
            // TODO: Retreive the details from DB
            return View("EmployeeSalaryView");
        }
    }
}
