using Microsoft.AspNetCore.Identity;
using System;
using TinkoffWatcher_Api.Enums;
using TinkoffWatcher_Api.Models.Entities;

namespace TinkoffWatcher_Api.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public string Discord { get; set; }
        public string Skype { get; set; }
        public bool IsTelegram { get; set; }
        public bool IsWhatsApp { get; set; }
        public bool IsViber { get; set; }

        public Guid? CvId { get; set; }
        public Cv Cv { get; set; }

        public Guid? CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
