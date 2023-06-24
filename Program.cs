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
using ChannelEngineConsoleApp.Controllers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ChannelEngineConsoleApp
{
    internal class Program {
        static string API_PATH = "https://api-dev.channelengine.net/api/";
        static string API_KEY = "541b989ef78ccb1bad630ea5b85c6ebff9ca3322";
        static string STATUS_IN_PROGRESS = "IN_PROGRESS";

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
        /// Method that performs the task for the assignment.
        /// Uses the helper methods to compute top-5 ranking and item updating and ensures the console printing.
        /// </summary>
        static async void RunTaskAsync() {
            try {
                List<RankingProduct> top5Products = await RankingController.GetRanking();
                OutputTopFive(top5Products);
            } catch (Exception e) { 
                Console.WriteLine(e.Message);
            }
        }
            
        static void Main(string[] args) {
            RunTaskAsync(); // perform the assignment task
            Console.ReadKey();
            Environment.Exit(0);
            return;
        }
    }
}
