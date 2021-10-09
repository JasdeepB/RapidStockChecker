using RSC.Models;
using System.Collections.Generic;

namespace RSC.DataAccess.Services
{
    public interface IRestockHistoryRepository
    {
        RestockHistory GetRestockHistory(int historyId);
        ICollection<RestockHistory> GetRestockHistories();
        bool CreateHistory(RestockHistory restockHistory);
        bool DeleteHistory(RestockHistory restockHistory);
        bool Save();
        bool RestockHistoryExists(int historyId);
    }
}
