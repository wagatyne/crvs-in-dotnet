using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crvs.Domain.Entities
{
    public class Person
    {
        public Person(Sex sex)
        {
            Sex = sex;
        }
        public string FirstName { get; set; } = string.Empty;
        public string MiddleName {  get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateOnly DateOfBirth { get; set; }
        public Sex Sex { get; private set; }
        protected void UpdateSex(Sex sex) => Sex = sex;
    }
}
