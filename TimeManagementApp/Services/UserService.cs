using System.Data;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.Sqlite;
using BCrypt.Net;

namespace TimeManagementApp.Services
{
    public class UserService
    {
        private readonly string _connectionString = "Data Source=TimeManagement.db";

        public async Task<bool> RegisterUserAsync(string username, string password)
        {
            using IDbConnection db = new SqliteConnection(_connectionString);

            var existingUser = await db.QueryFirstOrDefaultAsync<string>(
                "SELECT Username FROM Users WHERE Username = @Username", new { Username = username });

            if (existingUser != null)
                return false;

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            await db.ExecuteAsync("INSERT INTO Users (Username, PasswordHash) VALUES (@Username, @PasswordHash)",
                new { Username = username, PasswordHash = hashedPassword });

            return true;
        }

        public async Task<int?> AuthenticateUserAsync(string username, string password)
        {
            using IDbConnection db = new SqliteConnection(_connectionString);

            var user = await db.QueryFirstOrDefaultAsync<UserDto>(
                "SELECT Id, PasswordHash FROM Users WHERE Username = @Username",
                new { Username = username });

            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                return user.Id;

            return null;
        }

        private class UserDto
        {
            public int Id { get; set; }
            public string PasswordHash { get; set; }
        }
    }
}
