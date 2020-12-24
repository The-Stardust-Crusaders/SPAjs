using System;

namespace SPAjs.Models
{
    public class RequestViewModel
    {
        public string LandsatName { get; set; }
        public float CloudCover { get; set; }
        public DateTime DateTime { get; set; }
        public bool Status { get; set; }
        public string Description { get; set; }
    }
}
