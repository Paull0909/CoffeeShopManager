using System.ComponentModel.DataAnnotations;

namespace Data.DTO.User
{
    public class LoginUser
    {

        public string Email { get; set; }
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
