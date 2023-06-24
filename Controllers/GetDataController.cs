using ChannelEngineConsoleApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ChannelEngineConsoleApp.Controllers {
    internal class GetDataController {
        private string BaseAddress {  get; set; }
        private string ApiKey { get; set; }

        private HttpClient Client;

        public GetDataController(string baseAddress, string apiKey) {
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
    }
}
