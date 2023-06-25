using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ChannelEngineConsoleApp.Controllers {
    internal class ModifyStockTaskSolver : ITaskSolver {
        DataController DataController;
        JObject messageData;

        string MerchantProductNo;
        int StockLocationId;
        int NewStock;

        /// <summary>
        /// Constructor for the ModifyStockTaskSolver class.
        /// </summary>
        /// <param name="merchantProductNo"> the number that the merchant gave to the product </param>
        /// <param name="stockLocationId"> the ID of the stock location from which the product came </param>
        /// <param name="newStock"> the new value that the stock should have </param>
        /// <param name="dataController"> the DataController used for sending HTTP requests to the API </param>
        public ModifyStockTaskSolver(string merchantProductNo, int stockLocationId, int newStock, DataController dataController) {
            this.MerchantProductNo = merchantProductNo;
            this.StockLocationId = stockLocationId;
            this.NewStock = newStock;
            this.DataController = dataController;
        }

        /// <summary>
        /// Helper method that gets the new credentials of a product having certain parameters.
        /// </summary>
        /// <returns> the new credentials of the product, in JSON format </returns>
        string GetCredentials() {
            var stock = new JObject();
            stock.Add("Stock", this.NewStock);
            stock.Add("StockLocationId", this.StockLocationId);
            var stockArray = new JArray(stock);
            var jsonString = new JObject();
            jsonString.Add("MerchantProductNo", this.MerchantProductNo);
            jsonString.Add("StockLocations", stockArray);
            var jsonArray = new JArray(jsonString);

            return jsonArray.ToString();
        }

        /// <summary>
        /// Method that outputs the result of the PUT request for modifying the stock of a product.
        /// </summary>
        public async Task PrintOutput() {
            Console.WriteLine(messageData.ToString());
            await Task.CompletedTask;
        }

        /// <summary>
        /// Method that modifies the stock of a product at a specific index to 25.
        /// </summary>
        public async Task SolveTask() {
            // create the credentials payload in HttpContent 
            string credentials = GetCredentials(); // get the credentials in JSON form
            var httpContent = new StringContent(credentials, Encoding.UTF8, "application/json");

            // perform the POST request
            HttpResponseMessage response = await this.DataController.PutRequest("v2/offer/stock/", httpContent);
            if (response.IsSuccessStatusCode) { // check if request was successful
                string jsonString = await response.Content.ReadAsStringAsync();
                this.messageData = (JObject)JsonConvert.DeserializeObject(jsonString);
            }

            await Task.CompletedTask;
        }
    }
}
