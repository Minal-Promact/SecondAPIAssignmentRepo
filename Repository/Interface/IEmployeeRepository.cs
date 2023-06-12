using SecondAPIAssignmentRepo.Model;

namespace SecondAPIAssignmentRepo.Repository.Interface
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetAllEmployees();
        Task<Employee> GetEmployeesById(Guid empId);
        Task<Employee> CheckEmailExistsInEmployee(string Email);
        Task<Employee> AddEmployee(EmployeeRequest addEmployeeRequest);
        Task<Employee> UpdateEmployee(Employee emp, EmployeeRequest updateEmployeeRequest);        
        void DeleteEmployee(Employee emp);
    }
}
