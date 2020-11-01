using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataProcessor.Library
{
    public class DataParser
    {
        ILogger logger;

        public DataParser(ILogger logger)
        {
            this.logger = logger ?? new NullLogger();
        }

        public async Task<IReadOnlyCollection<Person>> ParseData(IEnumerable<string> data)
        {
            var processedRecords = new List<Person>();
            foreach (var record in data)
            {
                var fields = record.Split(',');
                if (fields.Length != 6)
                {
                    await logger.LogMessage("Wrong number of fields in record", record)
                        .ConfigureAwait(false);
                    continue;
                }

                int id;
                if (!Int32.TryParse(fields[0], out id))
                {
                    await logger.LogMessage("Cannot parse Id field", record)
                        .ConfigureAwait(false);
                    continue;
                }

                DateTime startDate;
                if (!DateTime.TryParse(fields[3], out startDate))
                {
                    await logger.LogMessage("Cannot parse Start Date field", record)
                        .ConfigureAwait(false);
                    continue;
                }

                int rating;
                if (!Int32.TryParse(fields[4], out rating))
                {
                    await logger.LogMessage("Cannot parse Rating field", record)
                        .ConfigureAwait(false);
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
