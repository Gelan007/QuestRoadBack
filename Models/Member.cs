using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestRoadBack.Models
{
    public class Member
    {
        public int User_id { get; set; }
        public int Team_id { get; set; }
        public DateTime When_assigned { get; set; }
    }
}
