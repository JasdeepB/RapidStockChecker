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
