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
            //Entity Configuration of "Department" entity 
            // ToTable => configure table name

            modelBuilder.Entity<Department>().ToTable("department");
            //Haskey
            modelBuilder.Entity<Department>().HasKey(a => a.Id);

            //Property configuration "Employee" entity
            modelBuilder.Entity<Employee>().Property(a => a.Id).ValueGeneratedNever()
                .HasColumnName("Id");

            modelBuilder.Entity<Employee>().Property(e => e.Name)
                .HasMaxLength(30)
                .IsUnicode(false);

           
            // configures one-to-many relationship
            
            modelBuilder.Entity<Department>()
                .HasMany(d => d.Employees)
                .WithOne(e => e.Department)
                .HasForeignKey(e => e.DepartmentId);

            // Instead of foreign key attribute or the EF core convetions,
            // We are going to use entity configurations here
            modelBuilder.Entity<Department>()
                .HasMany(t => t.Employees)
                .WithOne(c => c.Department)
                .HasForeignKey(c => c.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.UseSerialColumns();

            // Configure other entities...

            base.OnModelCreating(modelBuilder);
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
