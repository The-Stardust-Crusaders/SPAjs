using System.ComponentModel.DataAnnotations;

namespace BLL.DTO
{
    public class RepostDTO
    {
        [Required]
        public string UserProfileId { get; set; }

        [Required]
        public string RequestId { get; set; }
    }
}
