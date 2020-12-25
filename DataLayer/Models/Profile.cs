using System;
using System.Collections.Generic;
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

        public List<Profile> Vänner { get; set; }

        //public List<Profile> Meddelanden { get; set; }


        public byte[] UserPicture { get; set; }

        public Profile() { }
    }
}
