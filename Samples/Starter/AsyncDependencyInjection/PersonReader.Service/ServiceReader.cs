using Common;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace PersonReader.Service
{
    public class ServiceReader : IPersonReader
    {
        HttpClient client = new HttpClient();
        JsonSerializerOptions options =
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true };


        public ServiceReader(ServiceReaderUri baseUri)
        {
            client.BaseAddress = new Uri(baseUri.ServiceUriString);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<IReadOnlyCollection<Person>> GetPeopleAsync()
        {
            HttpResponseMessage response = await client.GetAsync("people").ConfigureAwait(false);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var stringResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonSerializer.Deserialize<List<Person>>(stringResult, options);
            }
            return new List<Person>();
        }

        public async Task<Person> GetPersonAsync(int id)
        {
            HttpResponseMessage response = await client.GetAsync($"people/{id}").ConfigureAwait(false);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var stringResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonSerializer.Deserialize<Person>(stringResult, options);
            }
            return new Person();
        }
    }
}
