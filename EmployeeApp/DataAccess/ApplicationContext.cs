using EmployeeApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeApp.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        private readonly string _dbPath;
        public DbSet<EmployeeDetails> EmployeeDetails { get; set; }
        public DbSet<EmpSalaryDetails> EmpSalaryDetails { get; set; }

        public ApplicationDbContext()
        {
            // Option 1: Application folder
            _dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "EmployeeDatabase.db");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={_dbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeDetails>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Department).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Position).HasMaxLength(100);
            });

            modelBuilder.Entity<EmpSalaryDetails>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.EmpId);
                entity.Property(e => e.Month).HasMaxLength(20);
                entity.Property(e => e.Year).IsRequired();
                entity.Property(e => e.BasicSalary).IsRequired();
                entity.Property(e => e.Hra).IsRequired();
                entity.Property(e => e.TransportAllowances).IsRequired();
                entity.Property(e => e.DiningAllowances).IsRequired();
                entity.Property(e => e.Reimbursement).IsRequired();
                entity.Property(e => e.IncomeTax).IsRequired();
                entity.Property(e => e.CrossEarningDeductions).IsRequired();
                entity.Property(e => e.Epf).IsRequired();
                entity.HasOne<EmployeeDetails>()
                    .WithMany()
                    .HasForeignKey(e => e.EmpId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
