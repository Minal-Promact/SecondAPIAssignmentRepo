using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SecondAPIAssignmentRepo.Data;
using SecondAPIAssignmentRepo.Model;
using SecondAPIAssignmentRepo.Repository.Interface;

namespace SecondAPIAssignmentRepo.Repository.Implementation
{
    public class DepartmentRepository :IDepartmentRepository
    {
        private readonly EFDataContext dbContext;        
        
        public DepartmentRepository(EFDataContext dbContext)
        {
            this.dbContext = dbContext;            
        }

        public async Task<List<Department>> GetAllDepartments()        
        {
            List<Department> departments = await dbContext.Departments.Include(p => p.Employees).ToListAsync();           
            return departments;            
        }

        public async Task<Department> GetDepartmentById(Guid departmentId)
        {
            var departments = dbContext.Departments.Include(p => p.Employees).Single(p => p.Id == departmentId);
            return departments;            
        }
        public async Task<Department> CheckDepartmentNameExistsInDepartments(string departmentName)
        {
            return await dbContext.Departments.FirstOrDefaultAsync(a => a.DepartmentName == departmentName);
        }

        public async Task<Department> AddDepartment(DepartmentRequest addDepartmentRequest)
        {
            var department = new Department()
            {
                DepartmentName = addDepartmentRequest.DepartmentName
            };
            var result = await dbContext.Departments.AddAsync(department);
            await dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Department> UpdateDepartment(Department dept, DepartmentRequest updateDepartmentRequest)
        {
            dept.DepartmentName = updateDepartmentRequest.DepartmentName;
            var result = dbContext.Departments.Update(dept);
            await dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public void DeleteDepartment(Department dept)
        {
            dbContext.Departments.Remove(dept);
            dbContext.SaveChangesAsync();
        }
    }
}
