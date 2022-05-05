using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using TinkoffWatcher_Api.Enums;

namespace TinkoffWatcher_Api.Models.Entities
{
    public class Mark : BaseEntity
    {
        public int Value { get; set; }
        public string Review { get; set; }

        public int Year { get; set; }
        public SemesterEnum Semester { get; set; }

        [ForeignKey(nameof(Student))]
        public Guid StudentId { get; set; }
        [InverseProperty(nameof(ApplicationUser.MarksAsStudent))]
        public virtual ApplicationUser Student { get; set; }

        [ForeignKey(nameof(Agent))]
        public Guid AgentId { get; set; }
        [InverseProperty(nameof(ApplicationUser.MarksAsAgent))]
        public virtual ApplicationUser Agent { get; set; }
    }
}