using SecondAPIAssignmentRepo.Model;

namespace SecondAPIAssignmentRepo.Repository.Interface
{
    public interface IDepartmentRepository
    {
        Task<List<Department>> GetAllDepartments();
        Task<Department> GetDepartmentById(Guid departmentId);
        Task<Department> CheckDepartmentNameExistsInDepartments(string departmentName);
        Task<Department> AddDepartment(DepartmentRequest addDepartmentRequest);
        Task<Department> UpdateDepartment(Department dept, DepartmentRequest updateDepartmentRequest);
        void DeleteDepartment(Department dept);

    }
}
