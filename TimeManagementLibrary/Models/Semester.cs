using System;
using TimeManagementApp.Models;

namespace TimeManagementLibrary.Models
{
    public class Semester
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        public int NumberOfWeeks { get; set; }
        public DateTime StartDate { get; set; }
    }
}
