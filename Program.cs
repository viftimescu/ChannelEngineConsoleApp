using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ChannelEngineConsoleApp
{
    internal class Program {
        static HttpClient client = new HttpClient();
        static string API_PATH = "https://api-dev.channelengine.net/api";
        static string API_KEY = "541b989ef78ccb1bad630ea5b85c6ebff9ca3322";

        // Method for setting up the HTTP client
        static async Task SetupHttpClient() {
            client.BaseAddress = new Uri(API_PATH);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        static async Task<Product> GetOrdersAsync() {
            Product product = null;
            HttpResponseMessage response = await client.GetAsync(API_PATH + "/v2/orders/new?apikey=" + API_KEY);
            if (response.IsSuccessStatusCode) {
                string jsonString = await response.Content.ReadAsStringAsync();
                Console.WriteLine(jsonString);
            }
            return product;
        }

        // Method that performs the task of getting the top-5
        static async Task GetTopFiveAsync() {
            

            try {

            } catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }
            
        static void Main(string[] args) {
            GetOrdersAsync();
            //GetTopFiveAsync().Wait(); // perform the assignment task
            Console.ReadKey();
            Environment.Exit(0);
            return;
        }
    }
}
