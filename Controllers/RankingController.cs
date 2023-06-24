using ChannelEngineConsoleApp.Data;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ChannelEngineConsoleApp.Controllers {
    internal class RankingController {
        static string API_PATH = "https://api-dev.channelengine.net/api/";
        static string API_KEY = "541b989ef78ccb1bad630ea5b85c6ebff9ca3322";
        static string STATUS_IN_PROGRESS = "IN_PROGRESS";

        /// <summary>
        /// Helper method that performs a GET request for all the orders in the API.
        /// </summary>
        /// <returns> A list of all the orders received from the API </returns>
        static async Task<List<Order>> GetOrdersAsync() {
            // get the HTTP response to the GET request
            HttpResponseMessage response = await new GetDataController(API_PATH, API_KEY).GetRequest("v2/orders/");

            List<Order> orders = null;
            if (response.IsSuccessStatusCode) { // check if request was successful
                string jsonString = await response.Content.ReadAsStringAsync();
                var data = (JObject)JsonConvert.DeserializeObject(jsonString);
                orders = data["Content"].ToObject<List<Order>>(); // retrieve the list of orders from the root object
            }

            return orders;
        }

        /// <summary>
        /// Helper method that retrieves all the products from a list of orders.
        /// </summary>
        /// <param name="orders"> the list of orders </param>
        /// <returns> A list of all the products in the list </returns>
        static List<Line> GetProducts(List<Order> orders) {
            // only retain the "IN_PROGRESS" orders
            List<Order> inProgressOrders = orders.Where(o => o.Status == STATUS_IN_PROGRESS).ToList();

            // get all the products from the orders
            List<Line> products = new List<Line>();
            inProgressOrders.ForEach(o => products.AddRange(o.Lines));

            return products;
        }

        /// <summary>
        /// Helper method that computes the top-5 of the products in decreasing order of total quantity.
        /// </summary>
        /// <param name="products"> the list of products for which the ranking is computed </param>
        /// <returns> The top-5 ranking of the products in the list </returns>
        static List<RankingProduct> GetTopFive(List<Line> products) {
            // only retain the name, gtin and quantity of each product
            List<RankingProduct> rankingProducts = new List<RankingProduct>();
            products.ForEach(p => rankingProducts.Add(new RankingProduct(p.Description, p.Gtin, p.Quantity)));

            // compute the top-5 ranking
            var groupedProducts = rankingProducts.GroupBy(p => p.Name)
                                    .Select(g => new RankingProduct(g.First().Name, g.First().Gtin, g.Sum(p => p.Quantity)));
            List<RankingProduct> top5Products = groupedProducts.OrderByDescending(g => g.Quantity).Take(5).ToList();

            return top5Products;
        }

        /// <summary>
        /// Method that performs the assignment task of computing the top-5 ranking.
        /// </summary>
        /// <returns> the top-5 ranking </returns>
        public static async Task<List<RankingProduct>> GetRanking() {
            List<Order> orders = await GetOrdersAsync(); // get all the orders 
            List<Line> products = GetProducts(orders); // retrieve all products
            List<RankingProduct> top5Products = GetTopFive(products); // compute top-5 ranking
            return top5Products;
        }
    }
}
