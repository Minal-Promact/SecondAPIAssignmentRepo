using Microsoft.EntityFrameworkCore;
using SecondAPIAssignmentRepo.Model;
using System.Diagnostics;

namespace SecondAPIAssignmentRepo.Data
{
    public class EFDataContext : DbContext
    {
        public EFDataContext()
        {
            
        }

        public EFDataContext(DbContextOptions<EFDataContext> options)
           : base(options)
        {
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.UseSerialColumns();           
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("WebApiDatabase");
            }
        }

       


    }
}
