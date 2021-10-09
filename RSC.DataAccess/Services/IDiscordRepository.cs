using RSC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSC.DataAccess.Services
{
    public interface IDiscordRepository
    {
        ICollection<Discord> GetAllDiscords();
        Discord GetDiscord(int discordId);
        int GetDiscordInt(int discordId);
        bool DiscordExists(int discordId);
        bool DiscordExists(string discordName);
    }
}
