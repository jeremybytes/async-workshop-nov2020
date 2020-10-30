using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using TaskAwait.Shared;

namespace TaskAwait.Library
{
    public class PersonReader
    {
        HttpClient client = 
            new HttpClient() { BaseAddress = new Uri("http://localhost:9874") };
        JsonSerializerOptions options = 
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        public async Task<List<Person>> GetPeopleAsync(
            CancellationToken cancelToken = new CancellationToken())
        {
            HttpResponseMessage response = 
                await client.GetAsync("people", cancelToken).ConfigureAwait(false);

            cancelToken.ThrowIfCancellationRequested();

            //throw new NotImplementedException("Jeremy did not finish this method");

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var stringResult = 
                    await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonSerializer.Deserialize<List<Person>>(stringResult, options);
            }
            return new List<Person>();
        }

        public async Task<Person> GetPersonAsync(int id,
            CancellationToken cancelToken = new CancellationToken())
        {
            HttpResponseMessage response = 
                await client.GetAsync($"people/{id}", cancelToken).ConfigureAwait(false);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                cancelToken.ThrowIfCancellationRequested();
                string stringResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                Person result = JsonSerializer.Deserialize<Person>(stringResult, options);
                return result;
            }
            return new Person();
        }

        public async Task<Person> GetPersonAsyncWithFailures(int id,
            CancellationToken cancelToken = new CancellationToken())
        {
            HttpResponseMessage response =
                await client.GetAsync($"people/{id}", cancelToken).ConfigureAwait(false);

            if (id == 2)
            {
                throw new InvalidOperationException("Using id=2 is not supported by this method");
            }

            if (id == 5)
            {
                throw new NotImplementedException("Using id=5 has not been implemented");
            }

            if (response.StatusCode == HttpStatusCode.OK)
            {
                cancelToken.ThrowIfCancellationRequested();
                string stringResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                Person result = JsonSerializer.Deserialize<Person>(stringResult, options);
                return result;
            }
            return new Person();
        }

        public async Task<List<int>> GetIdsAsync(
            CancellationToken cancelToken = new CancellationToken())
        {
            HttpResponseMessage response = 
                await client.GetAsync("people/ids", cancelToken).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var stringResult = 
                    await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonSerializer.Deserialize<List<int>>(stringResult);
            }
            return new List<int>();
        }

        private void ThrowAggregateException()
        {
            var innerEx1 = new InvalidOperationException("You can't do that!");
            var innerEx2 = new TimeoutException("This took too long to finish!");

            var aggregate = new AggregateException(innerEx1, innerEx2);

            throw aggregate;
        }
    }
}