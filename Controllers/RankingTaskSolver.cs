using ChannelEngineConsoleApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChannelEngineConsoleApp.Controllers {
    internal class RankingTaskSolver : ITaskSolver {
        DataController DataController;
        List<RankingProduct> top5Products;

        /// <summary>
        /// Constructor for the RankingTaskSolver class.
        /// </summary>
        /// <param name="dataController"> the DataController used for sending HTTP requests to the API </param>
        public RankingTaskSolver(DataController dataController) {
            this.DataController = dataController;
            this.top5Products = new List<RankingProduct>();
        }

        /// <summary>
        /// Method that prints the result of the top-5 ranking to the screen.
        /// </summary>
        public async Task PrintOutput() {
            await Task.Run(() => SolveTask());
            Console.WriteLine("The top-5 most sold products are:");
            for (int i = 0; i < Math.Min(5, this.top5Products.Count); ++i) {
                RankingProduct product = this.top5Products[i];
                Console.WriteLine((i + 1) + ". " + product.Name);
            }
            Console.WriteLine();
            await Task.CompletedTask;
        }

        /// <summary>
        /// Method that performs the assignment task of computing the top-5 ranking.
        /// </summary>
        public async Task SolveTask() {
            List<Line> products = await this.DataController.GetInProgressProducts();

            // only retain the name, GTIN and quantity of each product
            List<RankingProduct> rankingProducts = new List<RankingProduct>();
            products.ForEach(p => rankingProducts.Add(new RankingProduct(p.Description, p.Gtin, p.Quantity)));

            // compute the top-5 ranking
            var groupedProducts = rankingProducts.GroupBy(p => p.Name)
                                    .Select(g => new RankingProduct(g.First().Name, g.First().Gtin, g.Sum(p => p.Quantity)));
            this.top5Products = groupedProducts.OrderByDescending(g => g.Quantity).Take(5).ToList();
        }
    }
}
