using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecondAPIAssignmentRepo.Model
{
    [Table("department")]
    public class Department 
    {
        [Key,Required]
        public Guid Id { get; set; } = Guid.NewGuid();

        [StringLength(50,ErrorMessage = "DepartmentName should be maximum 30 length")]
        [Required(ErrorMessage = "Please Enter Department Name.")]
        public string DepartmentName { get; set; }
    }
}
