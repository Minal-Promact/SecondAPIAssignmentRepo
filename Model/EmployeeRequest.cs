using System.ComponentModel.DataAnnotations;

namespace SecondAPIAssignmentRepo.Model
{
    public class EmployeeRequest
    {
        [StringLength(30, ErrorMessage = "Name should be maximum 30 length.")]
        [Required(ErrorMessage = "Please Enter Employee Name.")]
        [RegularExpression(pattern: "[a-zA-Z ]*$",ErrorMessage = "Please enter only alphabets.")]
        public string Name { get; set; }

        [Range(21, 100, ErrorMessage = "Please enter age between 21 to 100")]        
        public int Age { get; set; }

        public decimal Salary { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$", ErrorMessage = "Invalid pattern.")]
        public string Email { get; set; }

        [Required]
        public Guid DepartmentId { get; set; }
    }

}
