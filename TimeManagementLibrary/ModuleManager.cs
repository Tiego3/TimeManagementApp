using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManagementLibrary
{
    // Manages a collection of modules and provides operations on them.
    internal class ModuleManager
    {
        // Adds a new module to the collection.
        private List<Module> modules = new List<Module>();       
        public void AddModule(Module module)
        {
            modules.Add(module);
        }
        
        // Retrieves all modules.
        public List<Module> GetAllModules()
        {
            return modules;
        }
       
        // Calculates the total study hours for all modules.
        public int CalculateTotalStudyHours()
        {
            return modules.Sum(m => m.CalculateStudyHours());
        }

        // Finds modules with credits above a specified threshold.
        public List<Module> GetModulesAboveCreditThreshold(int threshold)
        {
            return modules.Where(m => m.Credits > threshold).ToList();
        }
    }
}
