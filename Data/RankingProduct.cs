using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ChannelEngineConsoleApp.Data {
    public class RankingProduct {
        public string Name { get; set; }
        public string Gtin { get; set; }
        public int Quantity { get; set; }

        public RankingProduct(string name, string gtin, int quantity) {
            this.Name = name;
            this.Gtin = gtin;
            this.Quantity = quantity;
        }

        public override bool Equals(Object other) {
            if (other == null || !this.GetType().Equals(other.GetType())) return false;
            if (ReferenceEquals(this, other)) return true;
            RankingProduct otherProduct = other as RankingProduct;
            return (this.Name.Equals(otherProduct.Name) && this.Gtin.Equals(otherProduct.Gtin)
                && this.Quantity == otherProduct.Quantity);
        }
    }
}
