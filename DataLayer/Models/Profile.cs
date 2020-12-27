using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class Profile
    {
        public int Id { get; set; }

        public String Firstname { get; set; }

       
        public String Lastname { get; set; }

        public int Age { get; set; }

        public string Email { get; set; }
      
        public String Gender { get; set; }

        
        public string SexualOrientation { get; set; }


        public bool Active { get; set; }
        public byte[] UserPicture { get; set; }

      

        public ICollection<Message> SentMessages { get; set; } = new List<Message>();
        public ICollection<Message> RecivedMessages { get; set; } = new List<Message>();

        public ICollection<FriendRequest> SentFriendRequests { get; set; } = new List<FriendRequest>();
        public ICollection<FriendRequest> RecivedFriendRequests { get; set; } = new List<FriendRequest>();
        public Profile() {
            
        }
    }
}
