using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonReader.CSV
{
    public class CSVReader : IPersonReader
    {
        private string filePath;

        private ICSVFileLoader fileLoader;
        public ICSVFileLoader FileLoader
        {
            get { return fileLoader ??= new CSVFileLoader(filePath); }
            set { fileLoader = value; }
        }

        public CSVReader(CSVFilePath dataFilePath)
        {
            filePath = dataFilePath.FilePathString;
        }


        public async Task<IReadOnlyCollection<Person>> GetPeopleAsync()
        {
            var fileData = await FileLoader.LoadFile().ConfigureAwait(false);
            var people = ParseData(fileData);
            return people;
        }

        public async Task<Person> GetPersonAsync(int id)
        {
            var people = await GetPeopleAsync().ConfigureAwait(false);
            return people?.FirstOrDefault(p => p.Id == id);
        }

        private List<Person> ParseData(IReadOnlyCollection<string> csvData)
        {
            var people = new List<Person>();

            foreach (string line in csvData)
            {
                try
                {
                    var elems = line.Split(',');
                    var per = new Person()
                    {
                        Id = Int32.Parse(elems[0]),
                        GivenName = elems[1],
                        FamilyName = elems[2],
                        StartDate = DateTime.Parse(elems[3]),
                        Rating = Int32.Parse(elems[4]),
                        FormatString = elems[5],
                    };
                    people.Add(per);
                }
                catch (Exception)
                {
                    // Skip the bad record, log it, and move to the next record
                    // log.write("Unable to parse record", per);
                }
            }
            return people;
        }
    }
}
