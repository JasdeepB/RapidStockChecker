using Microsoft.AspNetCore.Mvc;
using RSC.DataAccess.Services;
using RSC.Models;
using System.Collections.Generic;
using System.Linq;

namespace RapidStockCheckerAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RestockHistoryController : Controller
    {
        private IRestockHistoryRepository historyRepository;

        public RestockHistoryController(IRestockHistoryRepository historyRepository)
        {
            this.historyRepository = historyRepository;
        }

        [HttpGet]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<RestockHistory>))]
        public IActionResult GetAllRestockHistory()
        {
            var history = this.historyRepository.GetRestockHistories().ToList();

            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            var historyList = new List<RestockHistory>();

            foreach (var value in history)
            {
                historyList.Add(new RestockHistory
                {
                    Id = value.Id,
                    DateTime = value.DateTime
                });
            }
            return Ok(historyList);
        }
    }
}
