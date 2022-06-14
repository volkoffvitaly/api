﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TinkoffWatcher_Api.Data;

namespace TinkoffWatcher_Api.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.16")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("TinkoffWatcher_Api.Models.ApplicationRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("TinkoffWatcher_Api.Models.ApplicationUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("CompanyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("CvId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Discord")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<Guid?>("InterviewId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsTelegram")
                        .HasColumnType("bit");

                    b.Property<bool>("IsViber")
                        .HasColumnType("bit");

                    b.Property<bool>("IsWhatsApp")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("MiddleName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Post")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Skype")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("InterviewId");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("TinkoffWatcher_Api.Models.Auth.RefreshToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TokenExpiryTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("TinkoffWatcher_Api.Models.Entities.Characteristic", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CharacteristicTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CharacteristicValueId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("EditedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid?>("MarkId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Other")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CharacteristicTypeId");

                    b.HasIndex("CharacteristicValueId");

                    b.HasIndex("MarkId");

                    b.ToTable("Characteristics");
                });

            modelBuilder.Entity("TinkoffWatcher_Api.Models.Entities.CharacteristicType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("EditedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("IsCurrent")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Version")
                        .HasColumnType("int");

                    b.Property<DateTime>("VersionDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("CharacteristicTypes");
                });

            modelBuilder.Entity("TinkoffWatcher_Api.Models.Entities.CharacteristicValue", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CharacteristicTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("EditedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("IsCurrent")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("Version")
                        .HasColumnType("int");

                    b.Property<DateTime>("VersionDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CharacteristicTypeId");

                    b.ToTable("CharacteristicValues");

                    b.HasDiscriminator<string>("Discriminator").HasValue("CharacteristicValue");
                });

            modelBuilder.Entity("TinkoffWatcher_Api.Models.Entities.Company", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("EditedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("TinkoffWatcher_Api.Models.Entities.Cv", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AboutMe")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("EditedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Cvs");
                });

            modelBuilder.Entity("TinkoffWatcher_Api.Models.Entities.Feedback", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("EditedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid>("InterviewId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Verdict")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("InterviewId");

                    b.ToTable("Feedbacks");
                });

            modelBuilder.Entity("TinkoffWatcher_Api.Models.Entities.Interview", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AdditionalInfo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("EditedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid?>("StudentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("VacancyId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.HasIndex("VacancyId");

                    b.ToTable("Interviews");
                });

            modelBuilder.Entity("TinkoffWatcher_Api.Models.Entities.Language", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("EditedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("TinkoffWatcher_Api.Models.Entities.LanguageProficiency", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid>("CvId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset?>("EditedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid>("LanguageId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("LanguageLevel")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CvId");

                    b.HasIndex("LanguageId");

                    b.ToTable("LanguageProficiencies");
                });

            modelBuilder.Entity("TinkoffWatcher_Api.Models.Entities.Mark", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AdditionalComment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("AgentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("EditedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("OverallMark")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Semester")
                        .HasColumnType("int");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AgentId");

                    b.HasIndex("StudentId");

                    b.ToTable("Marks");
                });

            modelBuilder.Entity("TinkoffWatcher_Api.Models.Entities.Slot", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("EditedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid?>("StudentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("VacancyId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.HasIndex("VacancyId");

                    b.ToTable("Slots");
                });

            modelBuilder.Entity("TinkoffWatcher_Api.Models.Entities.SubscriberToCompany", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SubscriberId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("SubscriberId");

                    b.ToTable("SubscriberToCompanies");
                });

            modelBuilder.Entity("TinkoffWatcher_Api.Models.Entities.UsefulLink", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid>("CvId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset?>("EditedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Link")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CvId");

                    b.ToTable("UsefulLinks");
                });

            modelBuilder.Entity("TinkoffWatcher_Api.Models.Entities.Vacancy", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("EditedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PositionAmount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Vacancies");
                });

            modelBuilder.Entity("TinkoffWatcher_Api.Models.Entities.WorkExperience", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid>("CvId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset?>("EditedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Position")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("WorkPlace")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WorkResponsibilities")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CvId");

                    b.ToTable("WorkExperiences");
                });

            modelBuilder.Entity("TinkoffWatcher_Api.Models.Entities.CharacteristicBoolValue", b =>
                {
                    b.HasBaseType("TinkoffWatcher_Api.Models.Entities.CharacteristicValue");

                    b.Property<bool?>("BoolValue")
                        .HasColumnType("bit");

                    b.HasDiscriminator().HasValue("CharacteristicBoolValue");
                });

            modelBuilder.Entity("TinkoffWatcher_Api.Models.Entities.CharacteristicIntValue", b =>
                {
                    b.HasBaseType("TinkoffWatcher_Api.Models.Entities.CharacteristicValue");

                    b.Property<int?>("IntValue")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("CharacteristicIntValue");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("TinkoffWatcher_Api.Models.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("TinkoffWatcher_Api.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("TinkoffWatcher_Api.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("TinkoffWatcher_Api.Models.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TinkoffWatcher_Api.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("TinkoffWatcher_Api.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TinkoffWatcher_Api.Models.ApplicationUser", b =>
                {
                    b.HasOne("TinkoffWatcher_Api.Models.Entities.Company", "Company")
                        .WithMany("Employees")
                        .HasForeignKey("CompanyId");

                    b.HasOne("TinkoffWatcher_Api.Models.Entities.Interview", null)
                        .WithMany("Agents")
                        .HasForeignKey("InterviewId");

                    b.Navigation("Company");
                });

            modelBuilder.Entity("TinkoffWatcher_Api.Models.Entities.Characteristic", b =>
                {
                    b.HasOne("TinkoffWatcher_Api.Models.Entities.CharacteristicType", "CharacteristicType")
                        .WithMany()
                        .HasForeignKey("CharacteristicTypeId");

                    b.HasOne("TinkoffWatcher_Api.Models.Entities.CharacteristicValue", "CharacteristicValue")
                        .WithMany()
                        .HasForeignKey("CharacteristicValueId");

                    b.HasOne("TinkoffWatcher_Api.Models.Entities.Mark", "Mark")
                        .WithMany("Characteristics")
                        .HasForeignKey("MarkId");

                    b.Navigation("CharacteristicType");

                    b.Navigation("CharacteristicValue");

                    b.Navigation("Mark");
                });

            modelBuilder.Entity("TinkoffWatcher_Api.Models.Entities.CharacteristicValue", b =>
                {
                    b.HasOne("TinkoffWatcher_Api.Models.Entities.CharacteristicType", "CharacteristicType")
                        .WithMany("CharacteristicValues")
                        .HasForeignKey("CharacteristicTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CharacteristicType");
                });

            modelBuilder.Entity("TinkoffWatcher_Api.Models.Entities.Cv", b =>
                {
                    b.HasOne("TinkoffWatcher_Api.Models.ApplicationUser", "User")
                        .WithOne("Cv")
                        .HasForeignKey("TinkoffWatcher_Api.Models.Entities.Cv", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("TinkoffWatcher_Api.Models.Entities.Feedback", b =>
                {
                    b.HasOne("TinkoffWatcher_Api.Models.Entities.Interview", null)
                        .WithMany("Feedbacks")
                        .HasForeignKey("InterviewId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TinkoffWatcher_Api.Models.Entities.Interview", b =>
                {
                    b.HasOne("TinkoffWatcher_Api.Models.ApplicationUser", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId");

                    b.HasOne("TinkoffWatcher_Api.Models.Entities.Vacancy", "Vacancy")
                        .WithMany("Interviews")
                        .HasForeignKey("VacancyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");

                    b.Navigation("Vacancy");
                });

            modelBuilder.Entity("TinkoffWatcher_Api.Models.Entities.LanguageProficiency", b =>
                {
                    b.HasOne("TinkoffWatcher_Api.Models.Entities.Cv", "Cv")
                        .WithMany("LanguageProficiencies")
                        .HasForeignKey("CvId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TinkoffWatcher_Api.Models.Entities.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cv");

                    b.Navigation("Language");
                });

            modelBuilder.Entity("TinkoffWatcher_Api.Models.Entities.Mark", b =>
                {
                    b.HasOne("TinkoffWatcher_Api.Models.ApplicationUser", "Agent")
                        .WithMany("MarksAsAgent")
                        .HasForeignKey("AgentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TinkoffWatcher_Api.Models.ApplicationUser", "Student")
                        .WithMany("MarksAsStudent")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Agent");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("TinkoffWatcher_Api.Models.Entities.Slot", b =>
                {
                    b.HasOne("TinkoffWatcher_Api.Models.ApplicationUser", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId");

                    b.HasOne("TinkoffWatcher_Api.Models.Entities.Vacancy", "Vacancy")
                        .WithMany("Slots")
                        .HasForeignKey("VacancyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");

                    b.Navigation("Vacancy");
                });

            modelBuilder.Entity("TinkoffWatcher_Api.Models.Entities.SubscriberToCompany", b =>
                {
                    b.HasOne("TinkoffWatcher_Api.Models.Entities.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TinkoffWatcher_Api.Models.ApplicationUser", "Subscriber")
                        .WithMany("Subscriptions")
                        .HasForeignKey("SubscriberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("Subscriber");
                });

            modelBuilder.Entity("TinkoffWatcher_Api.Models.Entities.UsefulLink", b =>
                {
                    b.HasOne("TinkoffWatcher_Api.Models.Entities.Cv", "Cv")
                        .WithMany("UsefulLinks")
                        .HasForeignKey("CvId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cv");
                });

            modelBuilder.Entity("TinkoffWatcher_Api.Models.Entities.Vacancy", b =>
                {
                    b.HasOne("TinkoffWatcher_Api.Models.Entities.Company", "Company")
                        .WithMany("Vacancies")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("TinkoffWatcher_Api.Models.Entities.WorkExperience", b =>
                {
                    b.HasOne("TinkoffWatcher_Api.Models.Entities.Cv", "Cv")
                        .WithMany("WorkExperiences")
                        .HasForeignKey("CvId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cv");
                });

            modelBuilder.Entity("TinkoffWatcher_Api.Models.ApplicationUser", b =>
                {
                    b.Navigation("Cv");

                    b.Navigation("MarksAsAgent");

                    b.Navigation("MarksAsStudent");

                    b.Navigation("Subscriptions");
                });

            modelBuilder.Entity("TinkoffWatcher_Api.Models.Entities.CharacteristicType", b =>
                {
                    b.Navigation("CharacteristicValues");
                });

            modelBuilder.Entity("TinkoffWatcher_Api.Models.Entities.Company", b =>
                {
                    b.Navigation("Employees");

                    b.Navigation("Vacancies");
                });

            modelBuilder.Entity("TinkoffWatcher_Api.Models.Entities.Cv", b =>
                {
                    b.Navigation("LanguageProficiencies");

                    b.Navigation("UsefulLinks");

                    b.Navigation("WorkExperiences");
                });

            modelBuilder.Entity("TinkoffWatcher_Api.Models.Entities.Interview", b =>
                {
                    b.Navigation("Agents");

                    b.Navigation("Feedbacks");
                });

            modelBuilder.Entity("TinkoffWatcher_Api.Models.Entities.Mark", b =>
                {
                    b.Navigation("Characteristics");
                });

            modelBuilder.Entity("TinkoffWatcher_Api.Models.Entities.Vacancy", b =>
                {
                    b.Navigation("Interviews");

                    b.Navigation("Slots");
                });
#pragma warning restore 612, 618
        }
    }
}
