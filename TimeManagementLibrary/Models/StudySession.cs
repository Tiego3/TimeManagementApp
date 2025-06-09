using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeManagementLibrary.Models;

namespace TimeManagementApp.Models
{
    public class StudySession
    {
        public int StudySessionId { get; set; }
        public int ModuleId { get; set; }
        public Module Module { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public DateTime Date { get; set; }
        public double HoursSpent { get; set; }
    }

}
