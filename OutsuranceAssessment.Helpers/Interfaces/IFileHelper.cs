using OutsuranceAssessment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutsuranceAssessment.Helpers.Interfaces
{
    public interface IFileHelper
    {
        Task<List<Person>> ReadCsvAsync(string fileName);
        void WriteCsv<T>(List<T> items, string fileName);
    }
}
