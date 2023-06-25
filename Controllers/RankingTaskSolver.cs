using ChannelEngineConsoleApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChannelEngineConsoleApp.Controllers {
    public class RankingTaskSolver : ITaskSolver {
        DataController DataController;
        List<Line> Products;
        public List<RankingProduct> RankingProducts;
        public List<RankingProduct> Top5Products;

        /// <summary>
        /// Constructor for the RankingTaskSolver class.
        /// </summary>
        /// <param name="dataController"> the DataController used for sending HTTP requests to the API </param>
        public RankingTaskSolver(DataController dataController) {
            this.DataController = dataController;
            this.Products = new List<Line>();
            this.RankingProducts = new List<RankingProduct>();
            this.Top5Products = new List<RankingProduct>();
        }

        /// <summary>
        /// Method that prints the result of the top-5 ranking to the screen.
        /// </summary>
        public async Task PrintOutput() {
            await Task.Run(() => SolveTask());
            Console.WriteLine("The top-5 most sold products are:");
            for (int i = 0; i < Math.Min(5, this.Top5Products.Count); ++i) {
                RankingProduct product = this.Top5Products[i];
                Console.WriteLine((i + 1) + ". " + product.Name);
            }
            Console.WriteLine();
            await Task.CompletedTask;
        }

        /// <summary>
        /// Method that performs the assignment task of computing the top-5 ranking.
        /// </summary>
        public async Task SolveTask() {
            this.RankingProducts = await DataController.GetRankingProducts();
            
            // compute the top-5 ranking
            var groupedProducts = this.RankingProducts.GroupBy(p => new { p.Name, p.Gtin })
                                    .Select(g => new {
                                        g.Key.Name,
                                        g.Key.Gtin,
                                        TotalQuantity = g.Sum(p => p.Quantity)
                                    }).OrderByDescending(p => p.TotalQuantity).Take(5).ToList();
            this.Top5Products = new List<RankingProduct>();
            groupedProducts.ForEach(p => { this.Top5Products.Add(new RankingProduct(p.Name, p.Gtin, p.TotalQuantity)); });
        }
    }
}
