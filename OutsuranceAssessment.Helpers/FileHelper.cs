using OutsuranceAssessment.Helpers.Interfaces;
using OutsuranceAssessment.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutsuranceAssessment.Helpers
{
    public class FileHelper : IFileHelper
    {

        private IPersonHelper _personHelper;

        public FileHelper()
        {
            _personHelper = new PersonHelper();
        }

        public async Task<List<Person>> ReadCsvAsync(string fileName)
        {
            var result = new List<Person>();
            using (var reader = File.OpenText(fileName))
            {
                while (!reader.EndOfStream)
                {
                    result.Add(_personHelper.CreatePerson((await reader.ReadLineAsync()).Split(',')));
                }
            }

            return result;
        }

        public void WriteCsv<T>(List<T> items, string fileName)
        {
            File.WriteAllText(
                    fileName,
                    string.Join("\n",
                        items
                        .Select(item => string.Join(",", item.GetType().GetProperties().Select(x => x.GetValue(item, null)).ToList()))
                        .ToArray()
                     )
                );
        }
    }
}
