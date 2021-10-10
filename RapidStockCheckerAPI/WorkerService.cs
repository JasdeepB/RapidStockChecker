using System.Collections.Generic;

namespace RapidStockCheckerAPI
{
    public class WorkerService : IWorkerService
    {
        public IEnumerable<Worker> Workers { get; set; }
    }
}
