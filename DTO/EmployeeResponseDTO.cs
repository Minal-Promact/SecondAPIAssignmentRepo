using System.ComponentModel.DataAnnotations;

namespace SecondAPIAssignmentRepo.DTO
{
    public class EmployeeResponseDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }        
        public int Age { get; set; }
        public decimal Salary { get; set; }        
        public string Email { get; set; }
        public string DepartmentName { get; set; }
    }
}
