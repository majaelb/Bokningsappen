using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Bokningsappen.Models
{
    internal class MyDbContext : DbContext
    {
        public DbSet<Booking> Bookings { get; set; } = null!;
        public DbSet<Shift> Shifts { get; set; } = null!;
        public DbSet<Unit> Units { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=tcp:dbdemomaja.database.windows.net,1433;Initial Catalog=Bokningsappen;Persist Security Info=False;User ID=majasadmin;Password=onXPkQbvhHCh8Ap;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }
    }
}
