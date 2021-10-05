using Microsoft.AspNetCore.Mvc;
using RSC.DataAccess.Dtos;
using RSC.DataAccess.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RapidStockCheckerAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TypeController : Controller
    {
        private ITypeRepository typeRepository;

        public TypeController(ITypeRepository typeRepository)
        {
            this.typeRepository = typeRepository;
        }

        [HttpGet]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TypeDto>))]
        public IActionResult GetTypes()
        {
            var types = this.typeRepository.GetAllTypes().ToList();

            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            var typesDto = new List<TypeDto>();

            foreach (var type in types)
            {
                typesDto.Add(new TypeDto
                {
                    Id = type.Id,
                    Name = type.Name,
                    DiscordRef = type.DiscordRef
                });
            }    
            return Ok(typesDto);
        }

        [HttpGet("{typeId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(TypeDto))]
        public IActionResult GetType(int typeId)
        {
            if (this.typeRepository.TypeExists(typeId) == false)
            {
                return NotFound();
            }

            var type = this.typeRepository.GetType(typeId);

            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            var typeDto = new TypeDto()
            {
                Id = type.Id,
                Name = type.Name,
                DiscordRef = type.DiscordRef
            };
            
            return Ok(typeDto);
        }

        [HttpGet("types/{productId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(TypeDto))]
        public IActionResult GetTypeOfAnProduct(int productId)
        {
            //TO DO - Validate the product exists

                                           //Same method name, but is actaully implemented elsewhere
            var type = this.typeRepository.GetTypeOfAnProduct(productId);

            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            var typeDto = new TypeDto()
            {
                Id = type.Id,
                Name = type.Name,
                DiscordRef = type.DiscordRef
            };

            return Ok(typeDto);
        }

        //TO DO GetProductsFromType
    }
}
