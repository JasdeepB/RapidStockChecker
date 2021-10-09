using Microsoft.AspNetCore.Mvc;
using RSC.DataAccess.Services;
using RSC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RapidStockCheckerAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DiscordController : Controller
    {
        private IDiscordRepository discordRepository;

        public DiscordController(IDiscordRepository discordRepository)
        {
            this.discordRepository = discordRepository;
        }

        [HttpGet]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Discord>))]
        public IActionResult GetDiscords()
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            var discords = this.discordRepository.GetAllDiscords();

            var discordList = new List<Discord>();

            foreach (var discord in discords)
            {
                discordList.Add(new Discord
                {
                    Id = discord.Id,
                    Channel = discord.Channel,
                    Role = discord.Role,
                    SleepTime = discord.SleepTime,
                    Color = discord.Color,
                    Name = discord.Name
                });
            }
            return Ok(discordList);
        }
    }
}
