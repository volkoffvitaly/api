using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using TinkoffWatcher_Api.Enums;
using TinkoffWatcher_Api.Models.Entities;

namespace TinkoffWatcher_Api.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public Grade? Grade { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public string Discord { get; set; }
        public string Skype { get; set; }
        public bool IsTelegram { get; set; }
        public bool IsWhatsApp { get; set; }
        public bool IsViber { get; set; }

        public Guid? CvId { get; set; }
        public virtual Cv Cv { get; set; }
                
        public Guid? CompanyId { get; set; }
        public virtual Company Company { get; set; }

        public virtual ICollection<SubscriberToCompany> Subscriptions { get; set; }
        public string Post { get; set; }

        public virtual ICollection<Mark> MarksAsStudent { get; set; }
        public virtual ICollection<Mark> MarksAsAgent { get; set; }


        [NotMapped]
        public string FCs
        {
            get
            {
                if (string.IsNullOrWhiteSpace(MiddleName))
                    return string.Concat(LastName, " ", FirstName);
                else
                    return string.Concat(LastName, " ", FirstName, " ", MiddleName);
            }
        }
    }
}
