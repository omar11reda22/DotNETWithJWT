using System.ComponentModel.DataAnnotations;

namespace practise_JWT.DTOS
{
    public class RegisterDTO
    {
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
        [Required]
        [Compare("Password")]
        public string Confimepassword { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;

    }
}
