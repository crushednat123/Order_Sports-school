using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsSchool.Entities
{
    partial class Parents
    {
        public string FullName
        {
            get
            {
                if (Id > 0)
                {
                    var fullName = $"{Name} {SurName} {Patronymic}";

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
