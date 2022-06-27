using Models;
using System;
using TinkoffWatcher_Api.Enums;

namespace TinkoffWatcher_Api.Models.Entities
{
    public class DiarySettings : BaseEntity
    {
        public Grade Grade { get; set; }
        public string Order { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
    }
}
