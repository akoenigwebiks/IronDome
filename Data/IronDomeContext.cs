using Microsoft.EntityFrameworkCore;

namespace IronDome.Data
{
    public class IronDomeContext : DbContext
    {
        public IronDomeContext (DbContextOptions<IronDomeContext> options)
            : base(options)
        {
        }

        public DbSet<IronDome.Models.Attack> Attack { get; set; } = default!;
        public DbSet<IronDome.Models.Defense> Defense { get; set; } = default!;
    }
}
