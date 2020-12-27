using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DatingHemsida_Grupp_9.Models
{
    public class Profile
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Firstname is required")]
        public String Firstname { get; set; }

        [Required(ErrorMessage = "Lastname is required")]
        public String Lastname { get; set; }

        [Required(ErrorMessage = "Age is required")]
        [Range(18,100,ErrorMessage = "Minimum age is 18")]
        public int Age { get; set; }

        [EmailAddress(ErrorMessage = "Please enter a valid email")]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public String Gender { get; set; }

        [Required(ErrorMessage = "Sexual Orientation is a required field")]
        [Display(Name ="Intreseted in:")]
        public string SexualOrientation { get; set; }


        public bool Active { get; set; }

       
        public byte[] UserPicture { get; set; }

        public ICollection<Message> SentMessages { get; set; } = new List<Message>();
        public ICollection<Message> RecivedMessages { get; set; } = new List<Message>();

        public ICollection<FriendRequest> SentFriendRequests { get; set; } = new List<FriendRequest>();
        public ICollection<FriendRequest> RecivedFriendRequests { get; set; } = new List<FriendRequest>();

        public Profile() { }
    }
    
}
