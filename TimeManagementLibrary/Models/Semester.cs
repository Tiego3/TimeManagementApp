using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManagementApp.Models
{
    public class Semester
    {
        public int SemesterId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        public int NumberOfWeeks { get; set; }
        public DateTime StartDate { get; set; }
    }

}
