using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
{
    public class FriendRequest
    {
        [Key]
        public int FriendRequestId { get; set; }

        public int FriendSenderId { get; set; }
        public int FriendReceiverId { get; set; }

        public bool Accepted { get; set; }

        [ForeignKey("FriendSenderId")]
        public Profile FriendSender { get; set; }

        [ForeignKey("FriendReceiverId")]
        public Profile FriendReceiver { get; set; }
    }
}