using Crvs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crvs.Infrastructure.Persistence
{
    internal class CrvsDbContext : DbContext
    {
        public DbSet<BirthRegistration> BirthRegisration { get; set; }
    }
}
