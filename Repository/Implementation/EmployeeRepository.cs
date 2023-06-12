using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SecondAPIAssignmentRepo.AutomapperConfig;
using SecondAPIAssignmentRepo.Data;
using SecondAPIAssignmentRepo.Model;
using SecondAPIAssignmentRepo.Repository.Interface;

namespace SecondAPIAssignmentRepo.Repository.Implementation
{
    public class EmployeeRepository :IEmployeeRepository
    {
        private readonly EFDataContext dbContext;
        private readonly IMapper _mapper;

        public EmployeeRepository(EFDataContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<Employee>> GetAllEmployees()
        {            
             return await dbContext.Employees
            .Include(_ => _.Department).ToListAsync();        
        }

        public async Task<Employee> GetEmployeesById(Guid empId)
        {
            var employee =  await dbContext.Employees.Include(_ => _.Department).Where(e => e.Id == empId).FirstOrDefaultAsync();
            return employee;
        }

        public async Task<Employee> CheckEmailExistsInEmployee(string Email)
        {
            return await dbContext.Employees.FirstOrDefaultAsync(a => a.Email == Email);
        }        

        public async Task<Employee> AddEmployee(EmployeeRequest addEmployeeRequest)
        {            
            var employee = _mapper.Map<EmployeeRequest,Employee>(addEmployeeRequest);

            await dbContext.Employees.AddAsync(employee);
            await dbContext.SaveChangesAsync();  
            var department = dbContext.Departments.Where(a => a.Id == addEmployeeRequest.DepartmentId).FirstOrDefault();
            employee.Department = department;
            return employee;
        }

        public async Task<Employee> UpdateEmployee(Employee emp, EmployeeRequest updateEmployeeRequest)
        {
            emp.Name = updateEmployeeRequest.Name;
            emp.Age = updateEmployeeRequest.Age;
            emp.Salary = updateEmployeeRequest.Salary;
            emp.Email = updateEmployeeRequest.Email;
            dbContext.Employees.Update(emp);
            await dbContext.SaveChangesAsync();
            var department = dbContext.Departments.Where(a => a.Id == updateEmployeeRequest.DepartmentId).FirstOrDefault();
            emp.Department = department;
            return emp;
        }
        
        public void DeleteEmployee(Employee emp)
        {
            dbContext.Employees.Remove(emp);
            dbContext.SaveChangesAsync();
        }
    }
}
