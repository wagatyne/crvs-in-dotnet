using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crvs.Domain.Entities
{
    public class Mother : Person
    {
        public Mother()
            :base(sex: Sex.Female)
        {
            
        }
    }
}
