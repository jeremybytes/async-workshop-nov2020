using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProductOrder.Library
{
    public class Customer
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
    }

    public class CustomerReader : DataReader
    {
        public async Task<Customer> GetCustomerForOrderAsync(int orderId)
        {
            HttpResponseMessage response =
                await client.GetAsync($"customer/fororder/{orderId}").ConfigureAwait(false);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var stringResult =
                    await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonSerializer.Deserialize<Customer>(stringResult, options);
            }
            return new Customer();
        }

        public async Task<Customer> GetProductAsync(int customerId)
        {
            HttpResponseMessage response =
                await client.GetAsync($"customer/{customerId}").ConfigureAwait(false);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var stringResult =
                    await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonSerializer.Deserialize<Customer>(stringResult, options);
            }
            return new Customer();
        }
    }
}
