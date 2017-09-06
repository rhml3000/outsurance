using System;
using NUnit.Framework;
using Moq;
using OutsuranceAssessment.Helpers.Interfaces;
using OutsuranceAssessment.Helpers;
using OutsuranceAssessment.Models;
using System.Collections.Generic;
using System.IO;

namespace OutsuranceAssessment.Test
{
    public class Program_IntegrationTests
    {

        private ProgramTask _programTask;
        private string _testInputFileName;

        [OneTimeSetUp]
        public void Setup()
        {
            _testInputFileName = "sampleTestData.csv";
            var sampleTestInput =
                "Reuel,Lawrence,22 B Street\n" +
                "Christina,Lawrence,22 A Street";
            File.WriteAllText(_testInputFileName, sampleTestInput);
            _programTask = new ProgramTask();
            _programTask.Run(new string[] { _testInputFileName });
        }

        [Test]
        public void ProgramTask_CreatesOutputFiles()
        {

            Assert.IsTrue(File.Exists(_programTask.GetNamesFilename()));
            Assert.IsTrue(File.Exists(_programTask.GetAddressFilename()));
        }

        [Test]
        public void ProgramTask_NamesOutputIsCorrect()
        {
            var expectedResult =
                "Lawrence,2\n" +
                "Christina,1\n" +
                "Reuel,1";

            var actualResult = File.ReadAllText(_programTask.GetNamesFilename());

            Assert.That(expectedResult, Is.EqualTo(actualResult));
        }

        [Test]
        public void ProgramTask_AddressOutputIsCorrect()
        {
            var expectedResult =
                "22 A Street\n" +
                "22 B Street";

            var actualResult = File.ReadAllText(_programTask.GetAddressFilename());

            Assert.That(expectedResult, Is.EqualTo(actualResult));
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            if (File.Exists(_testInputFileName))
            {
                File.Delete(_testInputFileName);
            }

            if (File.Exists(_programTask.GetNamesFilename()))
            {
                File.Delete(_programTask.GetNamesFilename());
            }

            if (File.Exists(_programTask.GetAddressFilename()))
            {
                File.Delete(_programTask.GetAddressFilename());
            }
        }
    }
}
