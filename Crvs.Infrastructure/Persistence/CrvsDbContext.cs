using Crvs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crvs.Infrastructure.Persistence
{
    public class CrvsDbContext : DbContext
    {
        public CrvsDbContext(DbContextOptions<CrvsDbContext> dbContextOptions): base(dbContextOptions) { }

        public DbSet<BirthRegistration> BirthRegisration { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
