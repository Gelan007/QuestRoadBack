using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestRoadBack.Models
{
    public class Booking
    {
        public int Booking_id { get; }
        public int Quest_id { get; set; }
        public int Team_id { get; set; }
        //добавил цену
        public int Price { get; set; }
        public DateTime Time { get; set; }
        public string Description { get; set; }
    }
}
