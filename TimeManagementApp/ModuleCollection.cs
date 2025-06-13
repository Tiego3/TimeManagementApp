using System;
using System.Collections.Generic;
using System.Text;
using TimeManagementLibrary.Models;

namespace TimeManagementApp
{
    class ModuleCollection
    {
        public List<Module> Modules { get; set; }

        public ModuleCollection()
        {
            Modules = new List<Module>();
        }
        
    }
}
