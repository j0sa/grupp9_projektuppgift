namespace DatingHemsida_Grupp_9.Models
{
    public class FriendRequest
    {
        public int FriendRequestId { get; set; }

        public int FriendSenderId { get; set; }

        public int FriendReceiverId { get; set; }

        public bool Accepted { get; set; }
    }
}