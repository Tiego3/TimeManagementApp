using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TimeManagementLibrary.Models;

namespace TimeManagementLibrary.Services
{
    public class DataService
    {
        private readonly TimeManagementDbContext _db;
        public DataService(TimeManagementDbContext db) => _db = db;

        public async Task SaveSemester(int userId, DateTime start, int weeks)
        {
            var sem = await _db.Semesters.SingleAsync(s => s.UserId == userId);
            sem.StartDate = start;
            sem.NumberOfWeeks = weeks;
            await _db.SaveChangesAsync();
        }

        public async Task AddModule(int userId, Module module)
        {
            var sem = await _db.Semesters.SingleAsync(s => s.UserId == userId);
            module.Semester = sem;
            _db.Modules.Add(module);
            await _db.SaveChangesAsync();
        }

        public async Task AddStudyRecord(int moduleId, DateTime date, double hours)
        {
            _db.StudyTimeRecords.Add(new StudyTimeRecord { ModuleId = moduleId, StudyDate = date, HoursSpent = hours });
            await _db.SaveChangesAsync();
        }

        public async Task<List<Module>> GetModulesWithRecords(int userId)
        {
            return await _db.Modules
                            .Where(m => m.Semester.UserId == userId)
                            .Include(m => m.StudyTimeRecords)
                            .Include(m => m.Semester)
                            .ToListAsync();
        }
    }
}
