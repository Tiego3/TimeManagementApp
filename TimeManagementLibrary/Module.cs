using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManagementLibrary
{
    // Represents a study module with its associated credit value.
    internal class Module
    {        
        public string Name { get; set; }              
        public int Credits { get; set; }

        // Calculates the total study hours based on credits.
        public int CalculateStudyHours()
        {
            return Credits * 10;
        }
    }
}
