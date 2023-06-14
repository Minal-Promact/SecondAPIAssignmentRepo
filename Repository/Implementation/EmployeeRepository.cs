using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SecondAPIAssignmentRepo.AutomapperConfig;
using SecondAPIAssignmentRepo.Data;
using SecondAPIAssignmentRepo.DTO;
using SecondAPIAssignmentRepo.Model;
using SecondAPIAssignmentRepo.Repository.Interface;
using SecondAPIAssignmentRepo.ToMap;

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

        public async Task<List<EmployeeResponseDTO>> GetAllEmployees()
        {
            List<EmployeeResponseDTO> lstEmployeeDTO = null;
            var employee = await dbContext.Employees.Include(_ => _.Department).ToListAsync();
            if (employee != null)
            {
                lstEmployeeDTO = ToMapEmployee.GetListEmployeeResponseDTO(employee);
            }
            return lstEmployeeDTO;
        }

        public async Task<EmployeeResponseDTO> GetEmployeesById(Guid empId)
        {
            EmployeeResponseDTO employeeReponseDTO = null;
            var employee =  await dbContext.Employees.Include(_ => _.Department).Where(e => e.Id == empId).FirstOrDefaultAsync();
            if (employee != null)
            {
                employeeReponseDTO = ToMapEmployee.GetEmployeeResponseDTO(dbContext,employee);
            }
            return employeeReponseDTO;
        }

        public async Task<Employee> GetndCheckEmployeesById(Guid empId)
        {            
            var employee = await dbContext.Employees.Include(_ => _.Department).Where(e => e.Id == empId).FirstOrDefaultAsync();            
            return employee;
        }

        public async Task<Employee> CheckEmailExistsInEmployee(string Email)
        {
            return await dbContext.Employees.FirstOrDefaultAsync(a => a.Email == Email);
        }        

        public async Task<EmployeeResponseDTO> AddEmployee(EmployeeRequest addEmployeeRequest)
        {            
            var employee = _mapper.Map<EmployeeRequest,Employee>(addEmployeeRequest);

            await dbContext.Employees.AddAsync(employee);
            await dbContext.SaveChangesAsync();

            EmployeeResponseDTO employeeReponseDTO = null;
            if (employee != null)
            {
                employeeReponseDTO = ToMapEmployee.GetEmployeeResponseDTO(dbContext, employee);
            }
            
            return employeeReponseDTO;
        }

        public async Task<EmployeeResponseDTO> UpdateEmployee(Employee emp, EmployeeRequest updateEmployeeRequest)
        {
            emp.Name = updateEmployeeRequest.Name;
            emp.Age = updateEmployeeRequest.Age;
            emp.Salary = updateEmployeeRequest.Salary;
            emp.Email = updateEmployeeRequest.Email;
            emp.DepartmentId = updateEmployeeRequest.DepartmentId;
            dbContext.Employees.Update(emp);
            await dbContext.SaveChangesAsync();

            EmployeeResponseDTO employeeReponseDTO = null;
            if (emp != null)
            {
                employeeReponseDTO = ToMapEmployee.GetEmployeeResponseDTO(dbContext, emp);
            }

            return employeeReponseDTO;
        }
        
        public void DeleteEmployee(Employee emp)
        {
            dbContext.Employees.Remove(emp);
            dbContext.SaveChangesAsync();
        }
    }
}
