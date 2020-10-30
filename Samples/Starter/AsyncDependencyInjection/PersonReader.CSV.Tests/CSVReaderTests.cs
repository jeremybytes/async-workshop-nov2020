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
        public void GetPeople_WithGoodRecords_ReturnsGoodRecords()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void GetPeople_WithMixedRecords_ReturnsGoodRecords()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void GetPeople_WithOnlyBadRecordsReturnsEmptyList()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void GetPeople_WithEmptyFile_ReturnsEmptyList()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void GetPeople_WithBadFileName_ThrowsFileNotFoundException()
        {
            Assert.Inconclusive();
        }
    }
}
