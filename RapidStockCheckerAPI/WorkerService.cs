using System.Collections.Generic;

namespace RapidStockCheckerAPI
{
    public class WorkerService : IWorkerService
    {
        public IEnumerable<AmazonWorker> Workers { get; set; }
    }
}
