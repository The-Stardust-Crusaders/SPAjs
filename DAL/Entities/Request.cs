using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public class Request
    {
        public string Id { get; set; }
        public string LandsatName { get; set; }
        public float CloudCover { get; set; }
        public DateTime? DateTime { get; set; }
        public bool Status { get; set; }
        public string Description { get; set; }

        public string SenderId { get; set; }
        public UserProfile Sender { get; set; }

        public ICollection<CustomMap> Maps { get; set; }
        public ICollection<Repost> Reposts { get; set; }
    }
}
