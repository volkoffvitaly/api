using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Tinkoff.Trading.OpenApi.Models;
using TinkoffWatcher_Api.Models;

namespace TinkoffWatcher_Api.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<PositionSettings> PositionsSettings { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<UserPosition> UsersPositions { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserPosition>().HasKey(x => new { x.OwnerId, x.PositionFigi });

            base.OnModelCreating(builder);
        }
    }
}
