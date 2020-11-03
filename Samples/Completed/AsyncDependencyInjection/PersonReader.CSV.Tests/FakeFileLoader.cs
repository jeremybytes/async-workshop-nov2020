using PersonReader.CSV;
using System.Collections.Generic;
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

        public Task<IReadOnlyCollection<string>> LoadFile()
        {
            switch (dataType)
            {
                case FakeDataType.Good:
                    return Task.FromResult<IReadOnlyCollection<string>>(TestData.WithGoodRecords);
                case FakeDataType.Bad:
                    return Task.FromResult<IReadOnlyCollection<string>>(TestData.WithOnlyBadRecords);
                case FakeDataType.Mixed:
                    return Task.FromResult<IReadOnlyCollection<string>>(TestData.WithGoodAndBadRecords);
                case FakeDataType.Empty:
                    return Task.FromResult<IReadOnlyCollection<string>>(new List<string>());
                default:
                    return Task.FromResult<IReadOnlyCollection<string>>(TestData.WithGoodRecords);
            }

        }
    }
}
