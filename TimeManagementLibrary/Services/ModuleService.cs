using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManagementLibrary.Services
{
    public class ModuleService
    {
        private readonly TimeManagementDbContext _context;

        public ModuleService(TimeManagementDbContext context)
        {
            _context = context;
        }

        public void AddModule(int userId, string code, string name, int credits, int classHoursPerWeek)
        {
            var module = new Module
            {
                UserId = userId,
                Code = code,
                Name = name,
                Credits = credits,
                ClassHoursPerWeek = classHoursPerWeek
            };

            _context.Modules.Add(module);
            _context.SaveChanges();
        }
    }

}
