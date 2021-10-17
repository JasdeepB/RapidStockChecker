using Microsoft.AspNetCore.Mvc;
using RSC.DataAccess.Services;
using RSC.Models;
using System.Collections.Generic;

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

        [HttpGet (Name = "GetDiscords")]
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

        [HttpPost]
        public IActionResult CreateDiscord([FromBody] Discord discordToCreate)
        {
            if (discordToCreate == null)
            {
                return BadRequest(ModelState);
            }

            if (this.discordRepository.DiscordExists(discordToCreate.Id) == true)
            {
                ModelState.AddModelError("", $"Discord { discordToCreate.Name } already exists");
                return StatusCode(422, ModelState);
            }

            if (ModelState.IsValid == false)
            {
                return StatusCode(404, ModelState);
            }

            if (this.discordRepository.CreateDiscord(discordToCreate) == false)
            {
                ModelState.AddModelError("", $"Something went wrong saving {discordToCreate.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
