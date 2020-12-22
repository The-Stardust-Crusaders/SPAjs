using System.ComponentModel.DataAnnotations;

namespace BLL.DTO
{
    public class RegisterDTO
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string UserEmail { get; set; }

        [Required]
        public string UserPass { get; set; }

        [Required]
        [Compare("UserPass")]
        public string ConfirmPass { get; set; }
    }
}
