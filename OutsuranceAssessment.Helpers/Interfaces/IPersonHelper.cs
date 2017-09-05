using OutsuranceAssessment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutsuranceAssessment.Helpers.Interfaces
{
    public interface IPersonHelper
    {
        Person CreatePerson(string[] person);

        List<PersonName> GetPersonNames(List<Person> people);

        List<PersonAddress> GetPersonAddresses(List<Person> people);
    }
}
