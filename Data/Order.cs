using System;
using System.Collections.Generic;

namespace ChannelEngineConsoleApp
{
    internal class Order {
        public int Id { get; set; }
        public string Status { get; set; }
        public List<Product> Products { get; set; }
    }
}
