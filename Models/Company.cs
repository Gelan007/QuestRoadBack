using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestRoadBack.Models
{
    public class Company
    {
        public int Company_id { get; }
        public string Name { get; set; }
        public string Company_check { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Company_account { get; set; }
        public bool Is_comfirmed { get; set; }
        public string Adress { get; set; }

    }
}
