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
using System.Data;

namespace ChannelEngineConsoleApp
{
    internal class Program {
        static string API_PATH = "https://api-dev.channelengine.net/api/";
        static string API_KEY = "541b989ef78ccb1bad630ea5b85c6ebff9ca3322";

        /// <summary>
        /// Method that performs the top-5 task for the assignment.
        /// Uses the helper methods of RankingController to compute top-5 ranking and item updating and ensures the console printing.
        /// </summary>
        static async void RunTopFiveAsync() {
            try {
                new RankingController(API_PATH, API_KEY).GetRanking();
            } catch (Exception e) { 
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Method that performs the stock update task of the assignment.
        /// </summary>
        static async void RunUpdateProductAsync() {
            try {
                new EditProductController(API_PATH, API_KEY).ModifyProduct25(0);
            } catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }
            
        static void Main(string[] args) {
            RunUpdateProductAsync(); // perform the assignment task
            Console.ReadKey();
            Environment.Exit(0);
            return;
        }
    }
}
