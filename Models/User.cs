using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestRoadBack.Models
{
    public class User
    {
        public int User_id { get; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }    
        public string Name { get; set; }
        public UserRole Role { get; set; }
        public int Company_id { get; set; }

    }
}
