using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TimeManagementApp.Models;
using TimeManagementLibrary.Models;

namespace TimeManagementLibrary.Services
{
    public class AuthService
    {
        private readonly TimeManagementDbContext _db;
        public AuthService(TimeManagementDbContext db) => _db = db;

        public async Task<bool> Register(string username, string password)
        {
            if (await _db.Users.AnyAsync(u => u.Username == username))
                return false;

            var user = new User { Username = username, PasswordHash = BCrypt.Net.BCrypt.HashPassword(password) };
            user.Semester = new Semester { StartDate = DateTime.Today, NumberOfWeeks = 12 };
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<User?> Login(string username, string password)
        {
            var user = await _db.Users
                .Include(u => u.Semester)
                .ThenInclude(s => s.Modules)
                .ThenInclude(m => m.StudyTimeRecords)
                .SingleOrDefaultAsync(u => u.Username == username);

            if (user is null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                return null;

            return user;
        }
    }

}
