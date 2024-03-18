using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crvs.Domain.Entities
{
    public class Father : Person
    {
        public Father()
            :base(sex:Sex.Male)
        {
            
        }
    }
}
