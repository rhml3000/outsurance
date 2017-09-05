using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using OutsuranceAssessment.Models;
using OutsuranceAssessment.Helpers;
using OutsuranceAssessment.Helpers.Interfaces;

namespace OutsuranceAssessment
{

    
    public class Program
    {
        
        static void Main(string[] args)
        {
            try
            {
                new ProgramTask().Run(args);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }        

    }
}
