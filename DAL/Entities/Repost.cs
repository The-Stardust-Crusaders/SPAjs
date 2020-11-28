namespace DAL.Entities
{
    public class Repost
    {
        public string Id { get; set; }

        public string UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }

        public string RequestId { get; set; }
        public Request Request { get; set; }
    }
}
