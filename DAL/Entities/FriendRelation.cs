namespace DAL.Entities
{
    public class FriendRelation
    {
        public string Id { get; set; }
        public string FriendId { get; set; }

        public string InitiatorId { get; set; }
        public UserProfile Initiator { get; set; }
    }
}
