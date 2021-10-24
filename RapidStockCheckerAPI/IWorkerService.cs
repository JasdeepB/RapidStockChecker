using System.Collections.Generic;

namespace RapidStockCheckerAPI
{
    public interface IWorkerService
    {
        IEnumerable<AmazonWorker> Workers { get; set; }
    }
}
