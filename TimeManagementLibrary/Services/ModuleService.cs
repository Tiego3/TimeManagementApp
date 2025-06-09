using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TimeManagementLibrary.Models;

namespace TimeManagementLibrary.Services
{
    public class ModuleService
    {
        private readonly TimeManagementDbContext _context;

        public ModuleService(TimeManagementDbContext context)
        {
            _context = context;
        }

        public List<Module> GetModulesForUser(int userId)
        {
            return _context.Modules
                .Where(m => m.UserId == userId)
                .ToList();
        } 
        public void AddStudyTime(int moduleId, DateTime date, double hours)
        {
            var module = _context.Modules
                .Include(m => m.StudyTimeRecords)
                .FirstOrDefault(m => m.Id == moduleId);

            if (module == null) return;

            module.StudyTimeRecords.Add(new StudyTimeRecord
            {
                StudyDate = date,
                HoursSpent = hours,
                ModuleId = moduleId
            });

            _context.SaveChanges();
        }

    }



    }
