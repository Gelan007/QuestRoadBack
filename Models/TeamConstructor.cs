using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuestRoadBack.Models
{
    public class TeamConstructor
    {
        [Required(ErrorMessage = "Введите имя")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Введите количество")]
        public int Count { get; set; }
        [Required(ErrorMessage = "Введите телефон")]
        public string Phone { get; set; }
    }
}
