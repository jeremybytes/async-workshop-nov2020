using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProductOrder.Library
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
    }

    public class ProductReader : DataReader
    {
        public async Task<List<Product>> GetProductsForOrderAsync(int orderId)
        {
            HttpResponseMessage response =
                await client.GetAsync($"product/fororder/{orderId}").ConfigureAwait(false);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var stringResult =
                    await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonSerializer.Deserialize<List<Product>>(stringResult, options);
            }
            return new List<Product>();
        }

        public async Task<Product> GetProductAsync(int productId)
        {
            HttpResponseMessage response =
                await client.GetAsync($"product/{productId}").ConfigureAwait(false);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var stringResult =
                    await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonSerializer.Deserialize<Product>(stringResult, options);
            }
            return new Product();
        }
    }
}
