using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Threading;
using System.Threading.Tasks;
using TinkoffWatcher_Api.Models;
using TinkoffWatcher_Api.Models.Auth;
using TinkoffWatcher_Api.Models.Entities;

namespace TinkoffWatcher_Api.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }


        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Cv> Cvs { get; set; }
        public DbSet<Interview> Interviews { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<LanguageProficiency> LanguageProficiencies { get; set; }
        public DbSet<UsefulLink> UsefulLinks { get; set; }
        public DbSet<Vacancy> Vacancies { get; set; }
        public DbSet<Mark> Marks { get; set; }
        public DbSet<Slot> Slots { get; set; }
        public DbSet<WorkExperience> WorkExperiences { get; set; }
        public DbSet<SubscriberToCompany> SubscriberToCompanies { get; set; }
        public DbSet<CharacteristicValue> CharacteristicValues { get; set; }
        public DbSet<CharacteristicBoolValue> CharacteristicBoolValues { get; set; }
        public DbSet<CharacteristicIntValue> CharacteristicIntValues { get; set; }
        public DbSet<CharacteristicType> CharacteristicTypes { get; set; }
        public DbSet<Characteristic> Characteristics { get; set; }

        public override int SaveChanges()
        {
            UpdateCreateAndModifyProperties();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            UpdateCreateAndModifyProperties();
            return base.SaveChangesAsync(cancellationToken);
        }

        public string GetTableName<TEntity>() => Model.FindEntityType(typeof(TEntity)).GetTableName();

        private void UpdateCreateAndModifyProperties()
        {
            ChangeTracker.DetectChanges();

            foreach (var entry in ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Detached:
                        break;
                    case EntityState.Unchanged:
                        break;
                    case EntityState.Deleted:
                        if (entry.Entity is BaseVersionedEntity trackedEntity)
                        {
                            entry.State = EntityState.Modified;
                            trackedEntity.EditedDate = DateTime.UtcNow;
                            trackedEntity.IsDeleted = true;
                        }
                        break;
                    case EntityState.Modified:
                        if (entry.Entity is BaseEntity trackModified)
                        {
                            trackModified.EditedDate = DateTime.UtcNow;
                        }
                        break;
                    case EntityState.Added:
                        if (entry.Entity is BaseEntity trackAdded)
                        {
                            trackAdded.CreatedDate = DateTime.UtcNow;
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}
