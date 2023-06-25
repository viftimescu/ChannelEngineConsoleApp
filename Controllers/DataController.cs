using ChannelEngineConsoleApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace ChannelEngineConsoleApp.Controllers {
    internal class DataController {
        private string BaseAddress {  get; set; }
        private string ApiKey { get; set; }

        private HttpClient Client;

        public DataController(string baseAddress, string apiKey) {
            this.BaseAddress = baseAddress;
            this.ApiKey = apiKey;

            this.Client = new HttpClient();
            Client.BaseAddress = new Uri(this.BaseAddress);
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <summary>
        /// Method that makes a GET request to the ChannelEngine API.
        /// </summary>
        /// <param name="path"> the additional path to the API request </param>
        /// <returns> the HTTP response message of the GET request </returns>
        public async Task<HttpResponseMessage> GetRequest(string path) {
            HttpResponseMessage response = await Client.GetAsync(path + "?apikey=" + this.ApiKey);
            return response;
        }

        /// <summary>
        /// Method that makes a POST request to the ChannelEngine API.
        /// </summary>
        /// <param name="path"> the additional path to the API request </param>
        /// <param name="content"> the content that should be delivered to the API </param>
        /// <returns> the HTTP response message of the POST request </returns>
        public async Task<HttpResponseMessage> PutRequest(string path, HttpContent httpContent) {
            HttpResponseMessage response = await Client.PutAsync(path + "?apikey=" + this.ApiKey, httpContent);
            return response;
        }

        /// <summary>
        /// Helper method that performs a GET request for all the orders in the API.
        /// </summary>
        /// <returns> A list of all the orders received from the API </returns>
        async Task<List<Order>> GetOrdersAsync() {
            // get the HTTP response to the GET request
            HttpResponseMessage response = await this.GetRequest("v2/orders/");

            List<Order> orders = null;
            if (response.IsSuccessStatusCode) { // check if request was successful
                string jsonString = await response.Content.ReadAsStringAsync();
                var data = (JObject)JsonConvert.DeserializeObject(jsonString);
                orders = data["Content"].ToObject<List<Order>>(); // retrieve the list of orders from the root object
            }

            return orders;
        }

        /// <summary>
        /// Method that retrieves all the products from a list of orders.
        /// </summary>
        /// <returns> A list of all the products in the list </returns>
        public async Task<List<Line>> GetInProgressProducts() {
            List<Order> orders = await this.GetOrdersAsync();
            // only retain the "IN_PROGRESS" orders
            List<Order> inProgressOrders = orders.Where(o => o.Status == "IN_PROGRESS").ToList();

            // get all the products from the orders
            List<Line> products = new List<Line>();
            inProgressOrders.ForEach(o => products.AddRange(o.Lines));

            return products;
        }
    }
}
