﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using TinkoffWatcher_Api.Dto.Company;
using TinkoffWatcher_Api.Dto.Cv;
using TinkoffWatcher_Api.Dto.Feedback;
using TinkoffWatcher_Api.Enums;

namespace TinkoffWatcher_Api.Dto.User
{
    public class FullUserInfoDto
    {
        // From IdentityUser
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public ICollection<string> Roles { get; set; }


        // From ApplicationUser
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FCs { get; set; }
        public int? Group { get; set; }
        public Grade? Grade { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public string Discord { get; set; }
        public string Skype { get; set; }
        public bool IsTelegram { get; set; }
        public bool IsWhatsApp { get; set; }
        public bool IsViber { get; set; }

        public Guid? CvId { get; set; }
        public CvDto Cv { get; set; }
        public Guid? CompanyId { get; set; }
        public CompanyDto Company { get; set; }
        public string Post { get; set; }

        public virtual ICollection<CompanyDto> CompanySubscriptions { get; set; }
        public virtual ICollection<MarkDto> MarksAsStudent { get; set; }
        public virtual ICollection<MarkDto> MarksAsAgent { get; set; }
    }
}
