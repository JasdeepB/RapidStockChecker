using RSC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
