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
    public class FileHelper_IntegrationTests
    {
        [Test]
        public void FileHelper_WriteCsv_CreatesFile()
        {
            var fileName = "outputFileTest.csv";
            var fileHelper = new FileHelper();

            fileHelper.WriteCsv(new List<string>(), fileName);

            Assert.IsTrue(File.Exists(fileName));

            File.Delete(fileName);
        }

        [Test]
        public void FileHelper_ReadCsv_ParsesFile()
        {
            var expectedResult = new List<Person>();
            expectedResult.Add(new Person
            {
                FirstName = "Reuel",
                LastName = "Lawrence",
                Address = "22 B Street"
            });
            expectedResult.Add(new Person
            {
                FirstName = "Christina",
                LastName = "Lawrence",
                Address = "22 A Street"
            });

            var inputFileName = "inputFileTest.csv";
            var inputData =
                "Reuel,Lawrence,22 B Street\n" +
                "Christina,Lawrence,22 A Street";
            File.WriteAllText(inputFileName, inputData);
            var actualResult = new FileHelper().ReadCsvAsync(inputFileName).Result;

            Assert.That(expectedResult, NUnit.DeepObjectCompare.Is.DeepEqualTo(actualResult));

            File.Delete(inputFileName);
        }
    }
}
