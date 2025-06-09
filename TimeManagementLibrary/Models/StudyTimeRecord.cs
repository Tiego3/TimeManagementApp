using System;

namespace TimeManagementLibrary.Models
{
    public class StudyTimeRecord
    {
        public int Id { get; set; }
        public DateTime StudyDate { get; set; }
        public double HoursSpent { get; set; }

        public int ModuleId { get; set; }
        public Module Module { get; set; }
    }
}
