using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class FriendRequest
    {
        [Key]
        public int FriendRequestId { get; set; }
        public int FriendSenderId { get; set; }
        public int FriendReciverId { get; set; }
        //public DateTime Date { get; set; }
        public bool Accepted { get; set; }

        [ForeignKey("FriendSenderId")]
        public Profile FriendSender { get; set; }

        [ForeignKey("FriendReciverId")]
        public Profile FriendReciver { get; set; }
    }
}
