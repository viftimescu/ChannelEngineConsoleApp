using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChannelEngineConsoleApp.Data {
    internal class RankingProduct {
        public string Name { get; set; }
        public string Gtin { get; set; }
        public int Quantity { get; set; }

        public RankingProduct(string name, string gtin, int quantity) {
            this.Name = name;
            this.Gtin = gtin;
            this.Quantity = quantity;
        }
    }
}
