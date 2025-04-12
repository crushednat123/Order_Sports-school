using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsSchool.Entities
{
    partial class Coach
    {
        public string FullName
        {
            get
            {

                if (Id > 0)
                {
                    var fullName = $"Тренер: {Name} {SurName} {Patronymic}";

                    return fullName;
                }

                else
                {
                    return FullName;
                }

            }
        }

        
    }
}
