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

        public String Gender { get; set; }

        public byte[] UserPicture { get; set; }

        public Profile() { }
    }
}
