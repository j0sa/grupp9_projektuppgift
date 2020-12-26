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


        //public virtual ICollection<Friend> SentFriendRequests { get; set; }

        //public virtual ICollection<Friend> ReceievedFriendRequests { get; set; }

        //[NotMapped]
        //public virtual ICollection<Friend> Friends
        //{
        //    get
        //    {
        //        var friends = SentFriendRequests.Where(x => x.Approved).ToList();
        //        friends.AddRange(ReceievedFriendRequests.Where(x => x.Approved));
        //        return friends;
        //    }
        //}

        //public virtual ICollection<Friend> Friends { get; set; }

        //public List<string> Vänner { get; set; }

        ////public List<Profile> Meddelanden { get; set; }


        public byte[] UserPicture { get; set; }

        //public virtual ICollection<Profile> IFriendsWith { get; set; }
        //public virtual ICollection<Profile> FriendsWithMe { get; set; }
       


        //public Profile Sender { get; set; }
        //public Profile Reciver { get; set; }

        public Profile() {
            //SentFriendRequests = new List<Friend>();
            //ReceievedFriendRequests = new List<Friend>();
        }
    }
}
