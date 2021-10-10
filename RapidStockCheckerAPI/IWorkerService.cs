using System.Collections.Generic;

namespace RapidStockCheckerAPI
{
    public interface IWorkerService
    {
        IEnumerable<Worker> Workers { get; set; }
    }
}
