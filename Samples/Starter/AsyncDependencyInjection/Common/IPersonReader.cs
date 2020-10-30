using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common
{
    public interface IPersonReader
    {
        Task<IReadOnlyCollection<Person>> GetPeopleAsync();
        Task<Person> GetPersonAsync(int id);
    }
}
