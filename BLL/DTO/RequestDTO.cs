using System;
using System.ComponentModel.DataAnnotations;

namespace BLL.DTO
{
    public class RequestDTO
    {
        [Required]
        public string LandsatName { get; set; }

        [Required]
        public float CloudCover { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public bool Status { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
