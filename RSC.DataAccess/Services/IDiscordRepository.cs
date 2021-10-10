using RSC.Models;
using System.Collections.Generic;

namespace RSC.DataAccess.Services
{
    public interface IDiscordRepository
    {
        ICollection<Discord> GetAllDiscords();
        Discord GetDiscord(int discordId);
        int GetDiscordInt(int discordId);
        Discord GetDiscordOfAnProduct(string SKU);
        bool DiscordExists(int discordId);
        bool DiscordExists(string discordName);
    }
}
