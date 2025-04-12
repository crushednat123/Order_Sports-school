using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsSchool.Logic
{
    public class CoachGroupViewModel
    {
        public int Id { get; set; }
        public string SurName { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Telephone { get; set; }
        public string Sports { get; set; } // Будет содержать список видов спорта через запятую
    }
}
