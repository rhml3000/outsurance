using OutsuranceAssessment.Helpers.Interfaces;
using OutsuranceAssessment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace OutsuranceAssessment.Helpers
{
    public class PersonHelper : IPersonHelper
    {

        private IUtilities _utilities;

        public PersonHelper()
        {
            _utilities = new Utilities();
        }

        public PersonHelper(Utilities utilities)
        {
            _utilities = utilities;
        }

        public Person CreatePerson(string[] person)
        {
            return new Person
            {
                FirstName = _utilities.GetArrayItem(person, 0),
                LastName = _utilities.GetArrayItem(person, 1),
                Address = _utilities.GetArrayItem(person, 2)
            };
        }

        public List<PersonName> GetPersonNames(List<Person> people)
        {
            var names = people.Select(x => new PersonName { Name = x.FirstName, Type = "fn", Count = 1 })
                .Concat(people.Select(y => new PersonName { Name = y.LastName, Type = "ln", Count = 1 }))
                .GroupBy(a => a.Name)
                .Select(b => new PersonName { Name = b.First().Name, Type = b.First().Type, Count = b.Sum(c => c.Count) })
                .OrderByDescending(o1 => o1.Count)
                .ThenBy(o2 => o2.Name)
                .ToList();
            return names;
        }

        public List<PersonAddress> GetPersonAddresses(List<Person> people)
        {
            var regEx = new Regex(@"([a-zA-Z]).*");
            return people
                .Select(x => new PersonAddress { Street = x.Address })
                .OrderBy(o => regEx.Match(o.Street).ToString())
                .ToList();            
        }
    }
}
