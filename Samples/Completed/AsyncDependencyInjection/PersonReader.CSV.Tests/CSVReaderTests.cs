using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PersonReader.CSV.Tests
{
    [TestClass]
    public class CSVReaderTests
    {
        private CSVFilePath unusedPath = new CSVFilePath("");
        private CSVFilePath badPath = new CSVFilePath("BAD_FILE.txt");

        [TestMethod]
        public async Task GetPeople_WithGoodRecords_ReturnsGoodRecords()
        {
            var reader = new CSVReader(unusedPath);
            reader.FileLoader = new FakeFileLoader(FakeDataType.Good);

            var result = await reader.GetPeopleAsync();

            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public async Task GetPeople_WithMixedRecords_ReturnsGoodRecords()
        {
            var reader = new CSVReader(unusedPath);
            reader.FileLoader = new FakeFileLoader(FakeDataType.Mixed);

            var result = await reader.GetPeopleAsync();

            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public async Task GetPeople_WithOnlyBadRecordsReturnsEmptyList()
        {
            var reader = new CSVReader(unusedPath);
            reader.FileLoader = new FakeFileLoader(FakeDataType.Bad);

            var result = await reader.GetPeopleAsync();

            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public async Task GetPeople_WithEmptyFile_ReturnsEmptyList()
        {
            var reader = new CSVReader(unusedPath);
            reader.FileLoader = new FakeFileLoader(FakeDataType.Empty);

            var result = await reader.GetPeopleAsync();

            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public async Task GetPeople_WithBadFileName_ThrowsFileNotFoundException()
        {
            var reader = new CSVReader(badPath);

            try
            {
                var result = await reader.GetPeopleAsync();
                Assert.Fail();
            }
            catch (FileNotFoundException)
            {
                // This is the passing state.
                // Note: Other frameworks have an "Assert.Pass"
                // that we can put here to make it more obvious
                // that this is the desired state.
            }
        }
    }
}
