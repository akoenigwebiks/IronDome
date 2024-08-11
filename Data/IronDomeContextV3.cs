using Microsoft.EntityFrameworkCore;
using IronDome.Models;

namespace IronDome.Data
{
    public class IronDomeContextV3 : DbContext
    {
        public IronDomeContextV3 (DbContextOptions<IronDomeContextV3> options)
            : base(options)
        {
        }

        public DbSet<IronDome.Models.Attacker> Attacker { get; set; } = default!;
        public DbSet<IronDome.Models.Volley> Volley { get; set; } = default!;
        public DbSet<IronDome.Models.Launcher> Launcher { get; set; } = default!;
    }
}
