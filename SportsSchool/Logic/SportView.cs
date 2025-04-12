using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsSchool.Logic
{
    public class SportView
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public CoachViewModel Coach { get; set; }
    }

    public class CoachViewModel
    {
        public string FullName { get; set; }
    }

}
