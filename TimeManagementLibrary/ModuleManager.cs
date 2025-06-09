using System;
using System.Collections.Generic;
using System.Linq;
using TimeManagementLibrary.Models;

namespace TimeManagementLibrary
{
    public class ModuleManager
    {
        private readonly List<Module> modules = new();

        public void AddModule(Module module)
        {
            modules.Add(module);
        }

        public List<Module> GetAllModules()
        {
            return modules;
        }

        public int CalculateTotalStudyHours()
        {
            return modules.Sum(m => m.CalculateTotalStudyHours());
        }

        public List<Module> GetModulesAboveCreditThreshold(int threshold)
        {
            return modules.Where(m => m.Credits > threshold).ToList();
        }

        public Module? GetModuleByCode(string code)
        {
            return modules.FirstOrDefault(m => m.Code.Equals(code, StringComparison.OrdinalIgnoreCase));
        }

        public void AddStudyTimeToModule(string code, DateTime date, double hours)
        {
            var module = GetModuleByCode(code);
            module?.AddStudyRecord(date, hours);
        }


    }
}
