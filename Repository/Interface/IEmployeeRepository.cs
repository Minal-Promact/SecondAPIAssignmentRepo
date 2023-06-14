using SecondAPIAssignmentRepo.DTO;
using SecondAPIAssignmentRepo.Model;

namespace SecondAPIAssignmentRepo.Repository.Interface
{
    public interface IEmployeeRepository
    {
        Task<List<EmployeeResponseDTO>> GetAllEmployees();
        Task<EmployeeResponseDTO> GetEmployeesById(Guid empId);
        Task<Employee> GetndCheckEmployeesById(Guid empId);
        Task<Employee> CheckEmailExistsInEmployee(string Email);
        Task<EmployeeResponseDTO> AddEmployee(EmployeeRequest addEmployeeRequest);
        Task<EmployeeResponseDTO> UpdateEmployee(Employee emp, EmployeeRequest updateEmployeeRequest);        
        void DeleteEmployee(Employee emp);
    }
}
