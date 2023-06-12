using System.ComponentModel.DataAnnotations;

namespace SecondAPIAssignmentRepo.Model
{
    public class DepartmentRequest
    {
        [StringLength(50, ErrorMessage = "DepartmentName should be maximum 30 length")]
        [Required(ErrorMessage = "Please Enter Department Name.")]
        public string DepartmentName { get; set; }
    }
}
