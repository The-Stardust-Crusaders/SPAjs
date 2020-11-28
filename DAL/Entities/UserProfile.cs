using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace DAL.Entities
{
    public class UserProfile : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfileIconEncodedString { get; set; }

        public ICollection<FriendRelation> FriendRelations { get; set; }
        public ICollection<Request> Requests { get; set; }
        public ICollection<Repost> Reposts { get; set; }
    }
}
