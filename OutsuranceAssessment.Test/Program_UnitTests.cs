using System;
using NUnit.Framework;
using Moq;
using OutsuranceAssessment;
using OutsuranceAssessment.Helpers.Interfaces;
using OutsuranceAssessment.Helpers;
using OutsuranceAssessment.Models;
using System.Collections.Generic;
using System.IO;

namespace OutsuranceAssessment.Test
{
    public class Program_UnitTests_DefaultSetup
    {

        private ProgramTask _programTask;

        [OneTimeSetUp]
        public void Setup()
        {
            var fileHelperMock = new Mock<IFileHelper>();
            var personHelperMock = new Mock<IPersonHelper>();

            fileHelperMock.Setup(x => x.CheckFileExists(It.IsAny<string>())).Returns(true);

            _programTask = new ProgramTask(personHelperMock.Object, fileHelperMock.Object);
        }

        [TestCase(new string[] { "sampleInput.csv" }, "sampleInput.csv", "nameStats.csv", "addressStats.csv")]
        [TestCase(new string[] { "mFile.csv" }, "mFile.csv", "nameStats.csv", "addressStats.csv")]
        [TestCase(new string[] { "thatFile.csv" }, "thatFile.csv", "nameStats.csv", "addressStats.csv")]
        public void ProgramTask_PassOneArg_SetDefaultOutputFilenames(
            string[] args, string expectedInputFilename, string expectedPersonFilename, string expectedAddressFilename)
        {
            
            _programTask.MainAsync(args);
            Assert.That(expectedInputFilename, Is.EqualTo(_programTask.GetInputFileName()));
            Assert.That(expectedPersonFilename, Is.EqualTo(_programTask.GetNamesFilename()));
            Assert.That(expectedAddressFilename, Is.EqualTo(_programTask.GetAddressFilename()));
        }

        [Test]
        public void ProgramTask_NoArgs_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => _programTask.MainAsync(new string[] { }));
        }
    }

    public class Program_UnitTests_FileNotFound
    {
        [TestCase(new string[] { "filename1.csv" }, "")]
        [TestCase(new string[] { "unknown.csv" }, "")]
        [TestCase(new string[] { "notFound.csv" }, "")]
        public void ProgramTask_FileDoesNotExist_ThrowArgumentException(string[] args, string expectedResult)
        {
            var fileHelperMock = new Mock<IFileHelper>();
            var personHelperMock = new Mock<IPersonHelper>();

            fileHelperMock.Setup(x => x.CheckFileExists(It.IsAny<string>())).Returns(false);

            var programTask = new ProgramTask(personHelperMock.Object, fileHelperMock.Object);

            Assert.Throws<ArgumentException>(() => programTask.MainAsync(args));
        }
    }
}
