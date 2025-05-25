using System;
using System.Collections.Generic;
using System.Text;
using TimeManagementLibrary.Models;

namespace TimeManagementLibrary
{
    public class StudyTimeRecord
    {
        public DateTime StudyDate { get; set; }
        public double HoursSpent { get; set; }

        public int Id { get; set; }
        // Existing properties...

        public int ModuleId { get; set; }
        public Module Module { get; set; }

    }

}
