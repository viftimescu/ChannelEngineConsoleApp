using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChannelEngineConsoleApp.Data {
    public class Credentials {
        public string MerchantProductNo { get; set; }

        public Stocklocation Stocklocation { get; set; }

        public Credentials(string merchantProductNo, Stocklocation stocklocation) {
            this.MerchantProductNo = merchantProductNo;
            this.Stocklocation = stocklocation;
        }
    }
}
