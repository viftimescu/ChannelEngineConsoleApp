using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ChannelEngineConsoleApp.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ChannelEngineConsoleApp.Controllers {
    internal class EditProductController {
        static DataController dataController;

        public EditProductController(string apiPath, string apiKey) {
            dataController = new DataController(apiPath, apiKey);
        }

        /// <summary>
        /// Helper method that gets the new credentials of a product at a specific index.
        /// </summary>
        /// <param name="products"> the list of products </param>
        /// <param name="index"> the index of the product in the list </param>
        /// <returns> the new credentials of the product </returns>
        string GetCredentials(List<Line> products, int index) {
            var stock = new JObject();
            stock.Add("Stock", 25);
            stock.Add("StockLocationId", products[index].StockLocation.Id);
            var stockArray = new JArray(stock);
            var jsonString = new JObject();
            jsonString.Add("MerchantProductNo", products[index].MerchantProductNo);
            jsonString.Add("StockLocations", stockArray);
            var jsonArray = new JArray(jsonString);

            return jsonArray.ToString();
        }

        /// <summary>
        /// Method that modifies the stock of a product at a specific index to 25.
        /// </summary>
        /// <param name="index"> the index of the product to be modified </param>
        public async void ModifyProduct25(int index) {
            // create the payload credentials for the product modification
            List<Line> products = await dataController.GetInProgressProducts();
            string credentials = GetCredentials(products, index);
            var httpContent = new StringContent(credentials, Encoding.UTF8, "application/json");

            // perform the POST request
            HttpResponseMessage response = await dataController.PutRequest("v2/offer/stock/", httpContent);
            if (response.IsSuccessStatusCode) { // check if request was successful
                string jsonString = await response.Content.ReadAsStringAsync();
                var data = (JObject)JsonConvert.DeserializeObject(jsonString);

                // output the HTTP response to the console.
                Console.WriteLine(data.ToString());
            }
        }
    }
}
