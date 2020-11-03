using System.Collections.Generic;

namespace PersonReader.CSV.Tests
{
    public static class TestData
    {
        public static List<string> WithGoodRecords =
            new List<string>()
            {
                "1,John,Koenig,1975/10/17,6,",
                "3,Leela,Turanga,1999/3/28,8,{1} {0}",
            };

        public static List<string> WithGoodAndBadRecords =
            new List<string>()
            {
                "1,John,Koenig,1975/10/17,6,",
                "INVALID DATA",
                "3,Leela,Turanga,1999/3/28,8,{1} {0}",
                "MORE INVALID DATA",
            };

        public static List<string> WithOnlyBadRecords =
            new List<string>()
            {
                "INVALID DATA",
                "MORE INVALID DATA",
            };

    }
}
