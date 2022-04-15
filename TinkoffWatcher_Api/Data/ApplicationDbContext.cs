using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TinkoffWatcher_Api.Models;
using TinkoffWatcher_Api.Models.Auth;
using TinkoffWatcher_Api.Models.Entities;

namespace TinkoffWatcher_Api.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
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
        public DbSet<WorkExperience> WorkExperiences { get; set; }

    }
}
