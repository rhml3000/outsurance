using System;
using NUnit.Framework;
using Moq;
using OutsuranceAssessment.Helpers.Interfaces;
using OutsuranceAssessment.Helpers;
using OutsuranceAssessment.Models;
using System.Collections.Generic;

namespace OutsuranceAssessment.Test
{
    //[SetUpFixture]
    public class PeopleAnalysis_UnitTests
    {
        public static string[] SampleArray1 = { "Reuel", "Lawrence", "22 Helmcroft Cresent" };
        public static string[] SampleArray2 = { "Christina", "Lawrence", "342 Hillgrove Drive" };

        private static List<Person> _people;

        private IPersonHelper _personHelper;
        private IUtilities _utilities;
        private IFileHelper _fileHelper;

        [OneTimeSetUp]
        public void Setup()
        {
            _personHelper = new PersonHelper();
            _utilities = new Utilities();
            _fileHelper = new FileHelper();

            _people = new List<Person>();
            _people.Add(new Person
            {
                FirstName = "Reuel",
                LastName = "Lawrence",
                Address = "22 Helmcroft Crescent"
            });
            _people.Add(new Person
            {
                FirstName = "Christina",
                LastName = "Lawrence",
                Address = "342 Allgrove drive"
            });
        }

        [TestCase(new string[] { "Christina", "Lawrence", "22 Helmcroft Crescent" }, 0, "Christina")]
        [TestCase(new string[] { "Christina", "Lawrence", "22 Helmcroft Crescent" }, 1, "Lawrence")]
        [TestCase(new string[] { "Christina", "Lawrence", "22 Helmcroft Crescent" }, 2, "22 Helmcroft Crescent")]
        [TestCase(new string[] { "Christina", "Lawrence", "22 Helmcroft Crescent" }, 3, "")]
        public void GetPersonItem_ThreeItems_GetItem(string[] itemArray, int index, string expectedValue)
        {
            var actualResult = _utilities.GetArrayItem(itemArray, index);

            Assert.That(expectedValue, Is.EqualTo(actualResult));
        }

        [Test]
        public void GetPersonItem_NegativeIndex_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => _utilities.GetArrayItem(new string[] { }, -1));
        }

        [Test]
        public void CreatePerson_ReturnsPersonObject()
        {
            var actualResult = _personHelper.CreatePerson(SampleArray1);

            var expectedResult = new Person
            {
                FirstName = "Reuel",
                LastName = "Lawrence",
                Address = "22 Helmcroft Cresent"
            };

            Assert.That(expectedResult, NUnit.DeepObjectCompare.Is.DeepEqualTo(actualResult));
        }

        [Test]
        public void CreatePerson_Calls_GetArrayItem()
        {
            var mockUtilities = new Mock<IUtilities>();
            var personHelper = new PersonHelper(mockUtilities.Object);            
            var person = personHelper.CreatePerson(SampleArray1);
            mockUtilities.Verify(m => m.GetArrayItem(SampleArray1, It.IsAny<int>()), Times.Exactly(3));
        }

        [Test]
        public void GetPersonName_AggretatesCorrectly()
        {
            var actualResult = _personHelper.GetPersonNames(_people);

            var expectedResult = new List<PersonName>();
            expectedResult.Add(new PersonName
            {
                Name = "Lawrence",
                Count = 2
            });
            expectedResult.Add(new PersonName
            {
                Name = "Christina",
                Count = 1
            });
            expectedResult.Add(new PersonName
            {
                Name = "Reuel",
                Count = 1
            });

            Assert.That(expectedResult, NUnit.DeepObjectCompare.Is.DeepEqualTo(actualResult));
        }

        [Test]
        public void GetPersonAddress_SortsCorrectly()
        {
            var actualResult = _personHelper.GetPersonAddresses(_people);

            var expectedResult = new List<PersonAddress>();
            expectedResult.Add(new PersonAddress
            {
                Street = "342 Allgrove drive"
            });
            expectedResult.Add(new PersonAddress
            {
                Street = "22 Helmcroft Crescent"
            });

            Assert.That(expectedResult, NUnit.DeepObjectCompare.Is.DeepEqualTo(actualResult));
        }
    }
}
