using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using TimeManagementLibrary;

namespace TimeManagementApp.Services
{
    public class UserService
    {
        private readonly TimeManagementDbContext _context = new();

        public bool RegisterUser(string username, string password)
        {
            if (_context.Users.Any(u => u.Username == username))
                return false;

            var hashed = HashPassword(password);

            var user = new User
            {
                Username = username,
                PasswordHash = hashed
            };

            _context.Users.Add(user);
            _context.SaveChanges();
            return true;
        }

        public User? AuthenticateUser(string username, string password)
        {
            var hashed = HashPassword(password);
            return _context.Users.FirstOrDefault(u => u.Username == username && u.PasswordHash == hashed);
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
