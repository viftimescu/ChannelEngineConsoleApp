﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChannelEngineConsoleApp.Data {
    public class Stocklocation {
        public int Id { get; set; }
        public string Name { get; set; }

        public Stocklocation(int id, string name) {
            Id = id;
            Name = name;
        }
    }
}
