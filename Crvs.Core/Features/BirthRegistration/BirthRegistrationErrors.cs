using Crvs.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crvs.Core.Features.BirthRegistration
{
    public static class BirthRegistrationErrors
    {
        public static Error ItemNotFound => new("NotFound", "The item was not found");
        public static Error ItemExists => new("ItemExists", "The item already exists");
    }
}
