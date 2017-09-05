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

        public ProgramTask(IPersonHelper personHelper, IFileHelper fileHelper)
        {
            _personHelper = personHelper;
            _fileHelper = fileHelper;
        }


        public void MainAsync(string[] args)
        {
            if (args.Length == 0)
            {
                throw new ArgumentException("No input file specified");
            }

            _inputFileName = args[0];
            if (!_fileHelper.CheckFileExists(_inputFileName))
            {
                throw new ArgumentException("Specified input file not found!");
            }

            _outputNameStatsFilename = args.Length > 1 ? args[1] : "nameStats.csv";
            _outputAddressStatsFilename = args.Length > 2 ? args[2] : "addressStats.csv";

            var people = _fileHelper.ReadCsvAsync(_inputFileName).Result;
            
            _fileHelper.WriteCsv(_personHelper.GetPersonNames(people), _outputNameStatsFilename);
            
            _fileHelper.WriteCsv(_personHelper.GetPersonAddresses(people), _outputAddressStatsFilename);
            return;
        }

        public string GetInputFileName()
        {
            return _inputFileName;
        }

        public string GetNamesFilename()
        {
            return _outputNameStatsFilename;
        }

        public string GetAddressFilename()
        {
            return _outputAddressStatsFilename;
        }
    }
}
