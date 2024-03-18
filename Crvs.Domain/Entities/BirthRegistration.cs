using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crvs.Domain.Entities
{
    public class BirthRegistration : BaseEntity
    {
        public Person Applicant { get; set; }
        public Father Father { get; set; }
        public Mother Mother { get; set; }
    }
}
