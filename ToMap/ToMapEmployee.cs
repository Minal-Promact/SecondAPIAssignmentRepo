using Microsoft.EntityFrameworkCore;
using SecondAPIAssignmentRepo.Data;
using SecondAPIAssignmentRepo.DTO;
using SecondAPIAssignmentRepo.Model;

namespace SecondAPIAssignmentRepo.ToMap
{
    public static class ToMapEmployee
    {
        public static List<EmployeeResponseDTO> GetListEmployeeResponseDTO(List<Employee> lstEmployee)
        {
            List<EmployeeResponseDTO> lstEmployeeResponseDTO = new List<EmployeeResponseDTO>();            
            foreach (var emp in lstEmployee)
            {
                EmployeeResponseDTO employeeResponseDTO = new EmployeeResponseDTO();
                employeeResponseDTO.Id = emp.Id;
                employeeResponseDTO.Name = emp.Name;
                employeeResponseDTO.Age = emp.Age;
                employeeResponseDTO.Email = emp.Email;
                employeeResponseDTO.Salary = emp.Salary;
                employeeResponseDTO.DepartmentName = emp.Department.DepartmentName;
                lstEmployeeResponseDTO.Add(employeeResponseDTO);
            }
            return lstEmployeeResponseDTO;
        }

        public static EmployeeResponseDTO GetEmployeeResponseDTO(EFDataContext dbContext,Employee employee)
        {
            EmployeeResponseDTO employeeResponseDTO = new EmployeeResponseDTO(); 
            
            employeeResponseDTO.Id = employee.Id;
            employeeResponseDTO.Name = employee.Name;
            employeeResponseDTO.Age = employee.Age;
            employeeResponseDTO.Email = employee.Email;
            employeeResponseDTO.Salary = employee.Salary;
            var department = dbContext.Departments.Include(a=>a.Employees).Where(a => a.Id == employee.DepartmentId).FirstOrDefault();
            employeeResponseDTO.DepartmentName = department.DepartmentName;                
            
            return employeeResponseDTO;
        }
    }
}
