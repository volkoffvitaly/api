﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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

            builder.Entity<CharacteristicQuestion>()
                .HasMany(x => x.CharacteristicAnswers)
                .WithOne(x => x.CharacteristicQuestion)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ApplicationUser>()
                .HasOne(x => x.Company)
                .WithMany(x => x.Employees)
                .OnDelete(DeleteBehavior.NoAction);
    
            builder.Entity<ApplicationUser>()
                .HasMany(x => x.MarksAsStudent)
                .WithOne(x => x.Student)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ApplicationUser>()
                .HasMany(x => x.MarksAsAgent)
                .WithOne(x => x.Agent)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Mark>()
                .HasMany(x => x.Characteristics)
                .WithOne(x => x.Mark)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ApplicationUser>()
                .HasOne(x => x.Company)
                .WithMany(x => x.Employees)
                .OnDelete(DeleteBehavior.SetNull);
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
        public DbSet<Characteristic> Characteristics { get; set; }
        public DbSet<CharacteristicAnswer> CharacteristicAnswers { get; set; }
        public DbSet<CharacteristicQuestion> CharacteristicQuestions { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<DiarySettings> DiarySettings { get; set; }

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
