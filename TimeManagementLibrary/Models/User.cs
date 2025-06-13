using System.Collections.Generic;
using TimeManagementLibrary.Models;

namespace TimeManagementApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = "";
        public string PasswordHash { get; set; } = "";

        public virtual Semester Semester { get; set; }
    }
}
