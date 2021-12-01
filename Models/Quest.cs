using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestRoadBack.Models
{
    public class Quest
    {
        public int Quest_id { get; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Difficulty_level { get; set; }
        public string City { get; set; }
        public string Adress { get; set; }
        public string Category { get; set; }
        public string Actors { get; set; }
        public int Company_id { get; set; }
    }
}
