using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ChannelEngineConsoleApp.Data;
using Newtonsoft.Json;

namespace ChannelEngineConsoleApp
{
    internal class Program {
        static string API_PATH = "https://api-dev.channelengine.net/api";
        static string API_KEY = "541b989ef78ccb1bad630ea5b85c6ebff9ca3322";

        /*// Method for setting up the HTTP client.
        static async Task<HttpClient> SetupHttpClient() {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(API_PATH);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }*/

        // Method for performing a GET request for all the orders in the API.
        static async Task<List<Order>> GetOrdersAsync() {
            // setup the HTTP client
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(API_PATH);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            List<Order> orders = null;
            HttpResponseMessage response = await client.GetAsync(API_PATH + "/v2/orders/new?apikey=" + API_KEY);
            if (response.IsSuccessStatusCode) {
                string jsonString = await response.Content.ReadAsStringAsync();
                Content content = JsonConvert.DeserializeObject<Content>(jsonString); // get the Content root JSON object

                Console.WriteLine(content.Orders);

                orders = content.Orders.ToList(); // retrieve the list of orders from the root object
            }

            foreach (var order in orders)
                Console.WriteLine(order);

            return orders;
        }

        // Method for retrieving all the products from a list of orders.
        static List<Product> GetProducts(List<Order> orders) {
            // only retain the "IN_PROGRESS" orders
            List<Order> inProgressOrders = orders.Where(o => o.Status == "IN_PROGRESS").ToList();

            // get all the products from the orders
            List<Product> products = new List<Product>();
            inProgressOrders.ForEach(o => products.AddRange(o.Products));

            return products;
        }

        // Method that retrieves the top-5 of the products in terms of total quantity.
        static List<RankingProduct> GetTopFive(List<Product> products) {
            // only retain the name, gtin and quantity of each product
            List<RankingProduct> rankingProducts = new List<RankingProduct>();
            products.ForEach(p => rankingProducts.Add(new RankingProduct(p.Description, p.Gtin, p.Quantity)));

            // compute the top-5 ranking
            var groupedProducts = rankingProducts.GroupBy(p => p.Gtin)
                                    .Select(g => new RankingProduct(g.First().Name, g.First().Gtin, g.Sum(p => p.Quantity)));
            List<RankingProduct> top5Products = groupedProducts.OrderByDescending(g => g.Quantity).Take(5).ToList();
            
            return top5Products;
        }

        // Method that performs the assignment task.
        static async Task RunTaskAsync() {
            try {
                List<Order> orders = await GetOrdersAsync(); // get all the orders 
                List<Product> products = GetProducts(orders); // retrieve all products
                List<RankingProduct> top5Products = GetTopFive(products); // compute top-5

                // output the result to console
                foreach (var product in top5Products)
                    Console.WriteLine(product.ToString());

            } catch (Exception e) { 
                Console.WriteLine(e.Message);
            }
        }
            
        static void Main(string[] args) {
            RunTaskAsync().Wait(); // perform the assignment task
            Console.ReadKey();
            Environment.Exit(0);
            return;
        }
    }
}
