using RapidStockChecker.DataAccess;
using RSC.Models;
using System.Collections.Generic;
using System.Linq;

namespace RSC.DataAccess.Services
{
    public class RestockHistoryRepository : IRestockHistoryRepository
    {
        private readonly ApplicationDbContext dbContext;

        public RestockHistoryRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool CreateHistory(RestockHistory restockHistory)
        {
            this.dbContext.Add(restockHistory);
            return Save();
        }

        public bool DeleteHistory(RestockHistory restockHistory)
        {
            this.dbContext.Remove(restockHistory);
            return Save();
        }

        public ICollection<RestockHistory> GetRestockHistories()
        {
            return this.dbContext
               .RestockHistory
               .OrderBy(h => h.DateTime)
               .ToList();
        }

        public RestockHistory GetRestockHistory(int historyId)
        {
            return this.dbContext.RestockHistory
                .Where(h => h.Id == historyId)
                .FirstOrDefault();
        }

        public bool RestockHistoryExists(int historyId)
        {
            return this.dbContext
                .Products
                .Any(h => h.Id == historyId);
        }

        public bool Save()
        {
            var saved = this.dbContext.SaveChanges();
            return saved >= 0 ? true : false;
        }
    }
}
