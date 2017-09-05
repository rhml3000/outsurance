using OutsuranceAssessment.Helpers;
using OutsuranceAssessment.Helpers.Interfaces;
using OutsuranceAssessment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OutsuranceAssessment
{
    public class ProgramTask
    {
        private static string _inputFileName;
        private static string _outputNameStatsFilename;
        private static string _outputAddressStatsFilename;

        private IPersonHelper _personHelper;
        private IFileHelper _fileHelper;


        public ProgramTask()
        {
            _personHelper = new PersonHelper();
            _fileHelper = new FileHelper();
        }


        public async Task MainAsync(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("No input file specified");
                return;
            }

            _inputFileName = args[0];
            if (!System.IO.File.Exists(_inputFileName))
            {
                Console.WriteLine("Specified input file not found!");
                return;
            }

            _outputNameStatsFilename = args.Length > 1 ? args[1] : "nameStats.csv";
            _outputAddressStatsFilename = args.Length > 2 ? args[2] : "addressStats.csv";

            var people = await _fileHelper.ReadCsvAsync(_inputFileName);

            var names = _personHelper.GetPersonNames(people);
            _fileHelper.WriteCsv(names, _outputNameStatsFilename);

            var addresses = _personHelper.GetPersonAddresses(people);            
            _fileHelper.WriteCsv(addresses, _outputAddressStatsFilename);
            return;
        }
    }
}
