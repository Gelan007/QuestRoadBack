using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestRoadBack.Models
{
    public class BookingConstructor
    {
        public int Quest_id { get; set; }
        public int User_id { get; set; }
        public string TeamName { get; set; }
        public int CountOfUsers { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public DateTime Date { get; set; }
    }
}
