using System.Collections.Generic;
using TimeManagementLibrary.Models;

namespace TimeManagementApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }

        public ICollection<Module> Modules { get; set; }
        public ICollection<StudySession> StudySessions { get; set; }

        public ICollection<Semester> Semesters { get; set; }  // ✅ Add this line
    }
}
