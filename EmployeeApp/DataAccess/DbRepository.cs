using EmployeeApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeApp.DataAccess
{
    // Repository class
    public class DbRepository : IDisposable
    {
        private readonly ApplicationDbContext _context;

        public DbRepository()
        {
            _context = new ApplicationDbContext();
            CreateDatabase();
        }

        private void CreateDatabase()
        {
            try
            {
                _context.Database.EnsureCreated();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating database: {ex.Message}");
            }
        }

        // Create
        public async Task AddEmployeeAsync(EmployeeDetails employee)
        {
            try
            {
                if (_context.EmployeeDetails.Any(emp=> emp.Id==employee.Id))
                {
                    throw new InvalidOperationException("ID is exists already");
                }
                await _context.EmployeeDetails.AddAsync(employee);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding employee: {ex.Message}");
            }
        }

        // Read
        public async Task<List<EmployeeDetails>> GetAllEmployeesAsync()
        {
            return await _context.EmployeeDetails.ToListAsync();
        }

        public async Task<EmployeeDetails> GetEmployeeByIdAsync(int id)
        {
            return await _context.EmployeeDetails.FindAsync(id);
        }

        // Update
        public async Task UpdateEmployeeAsync(EmployeeDetails employee)
        {
            try
            {
                _context.EmployeeDetails.Update(employee);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating employee: {ex.Message}");
            }
        }

        // Delete
        public async Task DeleteEmployeeAsync(int id)
        {
            try
            {
                var employee = await _context.EmployeeDetails.FindAsync(id);
                if (employee != null)
                {
                    _context.EmployeeDetails.Remove(employee);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting employee: {ex.Message}");
            }
        }

        public async Task<EmpSalaryDetails> GetSalaryByIdAsync(int empId)
        {
            return await _context.EmpSalaryDetails.SingleOrDefaultAsync<EmpSalaryDetails>(sal=>sal.EmpId == empId);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        internal async Task AddSalaryAsync(EmpSalaryDetails salary)
        {
            try
            {
                await _context.EmpSalaryDetails.AddAsync(salary);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding employee: {ex.Message}");
            }
        }
    }
}
