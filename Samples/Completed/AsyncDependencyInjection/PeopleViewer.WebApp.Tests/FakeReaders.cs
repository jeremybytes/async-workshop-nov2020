using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeopleViewer.WebApp.Tests
{
    public class FakeGoodReader : IPersonReader
    {
        private List<Person> testData =
            new List<Person>()
            {
                new Person() {Id = 1,
                    GivenName = "John", FamilyName = "Smith",
                    Rating = 7, StartDate = new DateTime(2000, 10, 1)},
                new Person() {Id = 2,
                    GivenName = "Mary", FamilyName = "Thomas",
                    Rating = 9, StartDate = new DateTime(1971, 7, 23)},
            };

        public Task<IReadOnlyCollection<Person>> GetPeopleAsync()
        {
            return Task.FromResult<IReadOnlyCollection<Person>>(testData);
        }

        public async Task<Person> GetPersonAsync(int id)
        {
            var people = await GetPeopleAsync();
            return people?.FirstOrDefault(p => p.Id == id);
        }
    }

    public class FakeFaultedReader : IPersonReader
    {
        public Task<IReadOnlyCollection<Person>> GetPeopleAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Person> GetPersonAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
