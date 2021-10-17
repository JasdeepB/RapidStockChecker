using RapidStockChecker.DataAccess;
using RSC.Models;
using System.Collections.Generic;
using System.Linq;

namespace RSC.DataAccess.Services
{
    public class DiscordRepository : IDiscordRepository
    {
        private readonly ApplicationDbContext dbContext;

        public DiscordRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool CreateDiscord(Discord discord)
        {
            this.dbContext.Add(discord);
            return Save();
        }

        public bool DeleteDiscord(Discord discord)
        {
            this.dbContext.Remove(discord);
            return Save();
        }

        public bool DiscordExists(int discordId)
        {
            return this.dbContext
                .Discord
                .Any(d => d.Id == discordId);
        }

        public bool DiscordExists(string discordName)
        {
            return this.dbContext
                .Discord
                .Any(d => d.Name == discordName);
        }

        public ICollection<Discord> GetAllDiscords()
        {
            return this.dbContext
                .Discord
                .ToList();
        }

        public Discord GetDiscord(int discordId)
        {
            return this.dbContext
                .Discord
                .Where(d => d.Id == discordId)
                .FirstOrDefault();
        }

        public Discord GetDiscordOfAnProduct(string SKU)
        {
            return this.dbContext
                .Products
                .Where(p => p.SKU == SKU)
                .Select(d => d.Discord)
                .FirstOrDefault();
        }

        public bool Save()
        {
            var saved = this.dbContext.SaveChanges();
            return saved >= 0 ? true : false;
        }

        public bool UpdateDiscord(Discord discord)
        {
            this.dbContext.Update(discord);
            return Save();
        }

        int IDiscordRepository.GetDiscordInt(int discordId)
        {
            return this.dbContext
                .Discord
                .Where(d => d.Id == discordId)
                .Select(d => d.Id)
                .FirstOrDefault();
        }
    }
}
