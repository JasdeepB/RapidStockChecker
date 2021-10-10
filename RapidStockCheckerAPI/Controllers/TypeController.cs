using Microsoft.AspNetCore.Mvc;
using RSC.DataAccess.Dtos;
using RSC.DataAccess.Services;
using RSC.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RapidStockCheckerAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TypeController : Controller
    {
        private ITypeRepository typeRepository;
        private ICategoryRepository categoryRepository;
        private IProductRepository productRepository;

        public TypeController(ITypeRepository typeRepository, ICategoryRepository categoryRepository, IProductRepository productRepository)
        {
            this.typeRepository = typeRepository;
            this.categoryRepository = categoryRepository;
            this.productRepository = productRepository;
        }

        [HttpGet]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TypeDto>))]
        public IActionResult GetAllTypes()
        {
            try
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
                    });
                }
                return Ok(typesDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex);
            }
        }

        [HttpGet("{typeId}", Name = "GetType")]
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
                Name = type.Name
            };

            return Ok(typeDto);
        }

        [HttpGet("typeName")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(TypeDto))]
        public IActionResult GetType(string typeName)
        {
            if (this.typeRepository.TypeExists(typeName) == false)
            {
                return NotFound();
            }

            var type = this.typeRepository.GetType(typeName);

            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            var typeDto = new TypeDto()
            {
                Id = type.Id,
                Name = type.Name
            };

            return Ok(typeDto);
        }

        /*        [HttpGet("discord/{typeId}")]
                [ProducesResponseType(400)]
                [ProducesResponseType(404)]
                [ProducesResponseType(200, Type = typeof(Discord))]
                public IActionResult GetDiscord(int typeId)
                {
                    if (this.typeRepository.TypeExists(typeId) == false)
                    {
                        return NotFound();
                    }

                    var discord = this.typeRepository.GetDiscordOfAnType(typeId);

                    if (ModelState.IsValid == false)
                    {
                        return BadRequest(ModelState);
                    }

                    var dis = new Discord()
                    {
                        Id = discord.Id,
                        Channel = discord.Channel,
                        Role = discord.Role,
                        SleepTime = discord.SleepTime,
                        Color = discord.Color,
                        Name = discord.Name
                    };

                    return Ok(dis);
                }*/

        /*        [HttpGet("discord/typeName")]
                [ProducesResponseType(400)]
                [ProducesResponseType(404)]
                [ProducesResponseType(200, Type = typeof(Discord))]
                public IActionResult GetDiscord(string name)
                {
                    if (this.typeRepository.TypeExists(name) == false)
                    {
                        return NotFound();
                    }

                    var discord = this.typeRepository.GetDiscordOfAnType(name);

                    if (ModelState.IsValid == false)
                    {
                        return BadRequest(ModelState);
                    }

                    var dis = new Discord()
                    {
                        Id = discord.Id,
                        Channel = discord.Channel,
                        Role = discord.Role,
                        SleepTime = discord.SleepTime,
                        Color = discord.Color,
                        Name = discord.Name
                    };

                    return Ok(dis);
                }*/

        [HttpGet("category/{typeId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(Category))]
        public IActionResult GetCategory(int typeId)
        {
            if (this.typeRepository.TypeExists(typeId) == false)
            {
                return NotFound();
            }

            var category = this.typeRepository.GetCategoryOfAnType(typeId);

            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            var cat = new Category()
            {
                Id = category.Id,
                Name = category.Name
            };

            return Ok(cat);
        }

        [HttpGet("category/typeName")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(Category))]
        public IActionResult GetCategory(string typeName)
        {
            if (this.typeRepository.TypeExists(typeName) == false)
            {
                return NotFound();
            }

            var category = this.typeRepository.GetCategoryOfAnType(typeName);

            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            var cat = new Category()
            {
                Id = category.Id,
                Name = category.Name
            };

            return Ok(cat);
        }

        [HttpGet("product/{productId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(TypeDto))]
        public IActionResult GetTypeOfAnProduct(int productId)
        {
            //TO DO - Validate the product exists
            if (this.productRepository.ProductExists(productId) == false)
            {
                ModelState.AddModelError("", "Product doesn't exist");
            }
            //Same method name, but is actaully implemented elsewhere
            var type = this.typeRepository.GetTypeOfAnProduct(productId);

            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            var typeDto = new TypeDto()
            {
                Id = type.Id,
                Name = type.Name
            };

            return Ok(typeDto);
        }

        [HttpGet("{typeId}/products")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ProductDto>))]
        public IActionResult GetCategories(int typeId)
        {
            var products = this.typeRepository.GetProductsFromAType(typeId);

            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            var productList = new List<ProductDto>();

            foreach (var prod in products)
            {
                productList.Add(new ProductDto
                {
                    Id = prod.Id,
                    SKU = prod.SKU,
                    Title = prod.Title,
                    ImageUrl = prod.ImageUrl,
                    Url = prod.Url,
                    InStock = prod.InStock
                });
            }
            return Ok(productList);
        }

        [HttpGet("typeName/products")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ProductDto>))]
        public IActionResult GetCategories(string name)
        {
            var products = this.typeRepository.GetProductsFromAType(name);

            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            var productList = new List<ProductDto>();

            foreach (var prod in products)
            {
                productList.Add(new ProductDto
                {
                    Id = prod.Id,
                    SKU = prod.SKU,
                    Title = prod.Title,
                    ImageUrl = prod.ImageUrl,
                    Url = prod.Url,
                    InStock = prod.InStock
                });
            }
            return Ok(productList);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(RSC.Models.Type))]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public IActionResult CreateType([FromBody] RSC.Models.Type typeToCreate)
        {
            if (typeToCreate == null)
            {
                return BadRequest(ModelState);
            }

            if (this.categoryRepository.CategoryExists(typeToCreate.Category.Id) == false)
            {
                ModelState.AddModelError("", "Category doesn't exist");
            }

            if (ModelState.IsValid == false)
            {
                return StatusCode(404, ModelState);
            }

            typeToCreate.Category = this.categoryRepository.GetCategory(typeToCreate.Category.Id);

            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            if (this.typeRepository.CreateType(typeToCreate) == false)
            {
                ModelState.AddModelError("", $"Something went wrong saving {typeToCreate.Name}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetType", new { typeId = typeToCreate.Id }, typeToCreate);
        }

        [HttpPut("{typeId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public IActionResult UpdateType(int typeId, [FromBody] RSC.Models.Type updatedTypeInfo)
        {
            if (updatedTypeInfo == null)
            {
                return BadRequest(ModelState);
            }

            if (typeId != updatedTypeInfo.Id)
            {
                return BadRequest(ModelState);
            }

            if (this.typeRepository.TypeExists(typeId) == false)
            {
                return NotFound();
            }

            if (this.typeRepository.IsDuplicateType(typeId, updatedTypeInfo.Name) == true)
            {
                ModelState.AddModelError("", $"Type {updatedTypeInfo.Name} already exists");
                return StatusCode(422, ModelState);
            }

            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            if (this.typeRepository.UpdateType(updatedTypeInfo) == false)
            {
                ModelState.AddModelError("", $"Something went wrong updating {updatedTypeInfo.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{typeId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public IActionResult DeleteType(int typeId)
        {
            if (this.typeRepository.TypeExists(typeId) == false)
            {
                return NotFound();
            }

            var typeToDelete = this.typeRepository.GetType(typeId);

            if (this.typeRepository.GetProductsFromAType(typeId).Count > 0)
            {
                ModelState.AddModelError("", $"Type {typeToDelete.Name} cannot be deleted because it is used by at least one product");
                return StatusCode(409, ModelState);
            }

            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            if (this.typeRepository.DeleteType(typeToDelete) == false)
            {
                ModelState.AddModelError("", $"Something went wrong deleting {typeToDelete.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
