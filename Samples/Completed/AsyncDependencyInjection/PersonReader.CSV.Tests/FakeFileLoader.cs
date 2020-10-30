using PersonReader.CSV;
using System.Threading.Tasks;

namespace PersonReader.CSV.Tests
{
    public enum FakeDataType
    {
        Good,
        Bad,
        Mixed,
        Empty,
    }

    public class FakeFileLoader : ICSVFileLoader
    {
        private FakeDataType dataType;

        public FakeFileLoader(FakeDataType dataType)
        {
            this.dataType = dataType;
        }

        public Task<string> LoadFile()
        {
            switch (dataType)
            {
                case FakeDataType.Good:
                    return Task.FromResult<string>(TestData.WithGoodRecords);
                case FakeDataType.Bad:
                    return Task.FromResult<string>(TestData.WithOnlyBadRecords);
                case FakeDataType.Mixed:
                    return Task.FromResult<string>(TestData.WithGoodAndBadRecords);
                case FakeDataType.Empty:
                    return Task.FromResult<string>(string.Empty);
                default:
                    return Task.FromResult<string>(TestData.WithGoodRecords);
            }

        }
    }
}
