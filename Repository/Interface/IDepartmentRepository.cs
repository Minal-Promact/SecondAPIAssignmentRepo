using SecondAPIAssignmentRepo.DTO;
using SecondAPIAssignmentRepo.Model;

namespace SecondAPIAssignmentRepo.Repository.Interface
{
    public interface IDepartmentRepository
    {
        Task<List<DepartmentReponseDTO>> GetAllDepartments();
        Task<DepartmentReponseDTO> GetDepartmentById(Guid departmentId);
        Task<Department> CheckDepartmentNameExistsInDepartments(string departmentName);
        Task<Department> GetndCheckDepartmentById(Guid departmentId);
        Task<DepartmentReponseDTO> AddDepartment(DepartmentRequest addDepartmentRequest);
        Task<DepartmentReponseDTO> UpdateDepartment(Department dept, DepartmentRequest updateDepartmentRequest);
        void DeleteDepartment(Department dept);

    }
}
