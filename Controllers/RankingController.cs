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
        static DataController dataController;

        public RankingController(string apiPath, string apiKey) {
            dataController = new DataController(apiPath, apiKey);
        }

        /// <summary>
        /// Helper method that computes the top-5 of the products in decreasing order of total quantity.
        /// </summary>
        /// <param name="products"> the list of products for which the ranking is computed </param>
        /// <returns> The top-5 ranking of the products in the list </returns>
        List<RankingProduct> GetTopFiveHelp(List<Line> products) {
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
        /// Helper method that prints the result of the top-5 ranking to the screen.
        /// </summary>
        /// <param name="top5Products"> the ranking list </param>
        static void OutputTopFive(List<RankingProduct> top5Products) {
            Console.WriteLine("The top-5 most sold products are:");
            for (int i = 0; i < Math.Min(5, top5Products.Count); ++i) {
                RankingProduct product = top5Products[i];
                Console.WriteLine((i + 1) + ". " + product.Name);
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Method that performs the assignment task of computing the top-5 ranking.
        /// </summary>
        /// <returns> the top-5 ranking </returns>
        public async void GetRanking() {
            List<Line> products = await dataController.GetInProgressProducts(); // retrieve all products
            List<RankingProduct> top5Products = this.GetTopFiveHelp(products); // compute top-5 ranking
            OutputTopFive(top5Products);
        }
    }
}
