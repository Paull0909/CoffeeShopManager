using System.ComponentModel.DataAnnotations;

namespace Data.DTO.User
{
    public class CreateUpdateUserRequest
    {
        [Required(ErrorMessage = "UserName is Required")]
        public string UserName { get; set; }
        public int EmployeeID { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

    }
}
