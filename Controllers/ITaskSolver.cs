using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChannelEngineConsoleApp.Controllers {
    internal interface ITaskSolver {
        Task SolveTask();
        Task PrintOutput();
    }
}
