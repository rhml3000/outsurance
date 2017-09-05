using OutsuranceAssessment.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutsuranceAssessment.Helpers
{
    public class Utilities : IUtilities
    {
        public virtual string GetArrayItem(string[] person, int index)
        {
            if (index < 0)
            {
                throw new ArgumentException("Argument index cannot be less than 0");
            }
            if (person != null && index < person.Length)
            {
                return person[index];
            }
            else
            {
                return "";
            }
        }
    }
}
