using System;
using System.Collections.Generic;
using System.Text;
using TimeManagementLibrary;

namespace TimeManagementApp
{
    class ModuleCollection
    {
        // ModuleCollection.cs
        public List<Module> Modules { get; set; }

        public ModuleCollection()
        {
            Modules = new List<Module>();
        }
        
    }
}
