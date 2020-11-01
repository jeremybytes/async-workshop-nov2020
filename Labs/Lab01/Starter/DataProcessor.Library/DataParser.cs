using System;
using System.Collections.Generic;

namespace DataProcessor.Library
{
    public class DataParser
    {
        ILogger logger;

        public DataParser(ILogger logger)
        {
            this.logger = logger ?? new NullLogger();
        }

        public IReadOnlyCollection<Person> ParseData(IEnumerable<string> data)
        {
            var processedRecords = new List<Person>();
            foreach (var record in data)
            {
                var fields = record.Split(',');
                if (fields.Length != 6)
                {
                    logger.LogMessage("Wrong number of fields in record", record);
                    continue;
                }

                int id;
                if (!Int32.TryParse(fields[0], out id))
                {
                    logger.LogMessage("Cannot parse Id field", record);
                    continue;
                }

                DateTime startDate;
                if (!DateTime.TryParse(fields[3], out startDate))
                {
                    logger.LogMessage("Cannot parse Start Date field", record);
                    continue;
                }

                int rating;
                if (!Int32.TryParse(fields[4], out rating))
                {
                    logger.LogMessage("Cannot parse Rating field", record);
                    continue;
                }

                var person = new Person()
                {
                    Id = id,
                    GivenName = fields[1],
                    FamilyName = fields[2],
                    StartDate = startDate,
                    Rating = rating,
                    FormatString = fields[5]
                };
                // Successfully parsed record
                processedRecords.Add(person);
            }
            return processedRecords;
        }
    }
}
