using Microsoft.AspNetCore.Mvc;
using RSC.DataAccess.Dtos;
using RSC.DataAccess.Services;
using RSC.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RapidStockCheckerAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private IProductRepository productRepository;
        private ITypeRepository typeRepository;
        private IDiscordRepository discordRepository;

        public ProductController(IProductRepository productRepository, ITypeRepository typeRepository, IDiscordRepository discordRepository)
        {
            this.productRepository = productRepository;
            this.typeRepository = typeRepository;
            this.discordRepository = discordRepository;
        }

        [HttpGet]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ProductDto>))]
        public IActionResult GetProducts()
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            var products = this.productRepository.GetAllProducts();

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
                    InStock = prod.InStock,
                    Retailer = prod.Retailer
                });
            }
            return Ok(productList);
        }

        [HttpGet("/Product/discord")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Product>))]
        public IActionResult GetProductsForDiscord()
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            var products = this.productRepository.GetAllProductsInStock();

            var productList = new List<Product>();

            foreach (var prod in products)
            {
                productList.Add(new Product
                {
                    Id = prod.Id,
                    SKU = prod.SKU,
                    Title = prod.Title,
                    ImageUrl = prod.ImageUrl,
                    Retailer = prod.Retailer,
                    Url = prod.Url,
                    InStock = prod.InStock,
                    Type = GetTypeDto(this.productRepository.ProductType(prod.SKU)),
                    Discord = GetDiscordDto(this.discordRepository.GetDiscordOfAnProduct(prod.SKU))
                });
            }
            return Ok(productList);
        }

        private Type GetTypeDto(Type type)
        {
            return new Type()
            {
                Id = type.Id,
                Name = type.Name
            };
        }

        private Discord GetDiscordDto(Discord discord)
        {
            return new Discord()
            {
                Channel = discord.Channel,
                Role = discord.Role,
                SleepTime = discord.SleepTime,
                Color = discord.Color
            };
        }

        [HttpGet("SKU", Name = "GetProduct")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(ProductDto))]
        public IActionResult GetProduct(string SKU)
        {
            if (this.productRepository.ProductExists(SKU) == false)
            {
                return NotFound();
            }

            var product = this.productRepository.GetProduct(SKU);

            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            var ProductDto = new ProductDto()
            {
                Id = product.Id,
                SKU = product.SKU,
                Title = product.Title,
                ImageUrl = product.ImageUrl,
                Url = product.Url,
                Retailer = product.Retailer,
                InStock = product.InStock
            };

            return Ok(ProductDto);
        }

        [HttpGet("typeId")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(ProductDto))]
        public IActionResult GetProductsByType(int typeId)
        {
            if (this.typeRepository.TypeExists(typeId) == false)
            {
                return NotFound();
            }

            var products = this.productRepository.GetAllProductsByTypeId(typeId);

            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            var productList = new List<ProductDto>();

            foreach (var value in products)
            {
                productList.Add(new ProductDto
                {
                    Id = value.Id,
                    SKU = value.SKU,
                    Title = value.Title,
                    ImageUrl = value.ImageUrl,
                    Url = value.Url,
                    Retailer = value.Retailer,
                    InStock = value.InStock
                }); 
            }

            return Ok(productList);
        }

/*        [HttpGet("{productId}/retockHistory")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<RestockHistory>))]
        public IActionResult GetRestockHistory(int productId)
        {
            var history = this.productRepository.GetRestockHistory(productId);

            if (productRepository.ProductExists(productId) == false)
            {
                ModelState.AddModelError("", "Product doesn't exist");
            }

            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            var restockHistory = new List<RestockHistory>();

            foreach (var value in history)
            {
                restockHistory.Add(new RestockHistory
                {
                    Id = value.Id,
                    DateTime = value.DateTime
                });
            }
            return Ok(restockHistory);
        }*/

/*        [HttpGet("SKU/retockHistory")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<RestockHistory>))]
        public IActionResult GetRestockHistory(string SKU)
        {
            if (productRepository.ProductExists(SKU) == false)
            {
                ModelState.AddModelError("", "Product doesn't exist");
            }

            var history = this.productRepository.GetRestockHistory(SKU);

            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            var restockHistory = new List<RestockHistory>();

            foreach (var value in history)
            {
                restockHistory.Add(new RestockHistory
                {
                    Id = value.Id,
                    DateTime = value.DateTime
                });
            }
            return Ok(restockHistory);
        }*/

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Product))]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public IActionResult CreateProduct([FromBody] Product productToCreate)
        {
            if (productToCreate == null)
            {
                return BadRequest(ModelState);
            }

            var product = this.productRepository.GetAllProducts()
                .Where(p => p.SKU.Trim().ToUpper() == productToCreate.SKU.Trim().ToUpper())
                .FirstOrDefault();

            if (product != null)
            {
                ModelState.AddModelError("", $"Product {productToCreate.SKU} already exists");
                return StatusCode(422, ModelState);
            }

            if (this.typeRepository.TypeExists(productToCreate.Type.Id) == false)
            {
                ModelState.AddModelError("", "Type doesn't exist");
            }

            if (this.discordRepository.DiscordExists(productToCreate.Discord.Id) == false)
            {
                ModelState.AddModelError("", "Discord doesn't exist");
            }

            if (ModelState.IsValid == false)
            {
                return StatusCode(404, ModelState);
            }

            productToCreate.Type = this.typeRepository.GetType(productToCreate.Type.Id);
            productToCreate.Discord = this.discordRepository.GetDiscord(productToCreate.Discord.Id);

            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            if (this.productRepository.CreateProduct(productToCreate) == false)
            {
                ModelState.AddModelError("", $"Something went wrong saving {productToCreate.Title}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetProduct", new { SKU = productToCreate.SKU }, productToCreate);
        }

        [HttpPost("SKU")]
        [ProducesResponseType(201, Type = typeof(Product))]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateProductAsync(string SKU, [FromQuery] int discordId, [FromQuery] int typeId, [FromQuery(Name = "retailer")] string retailer)
        {
            Product productToCreate = null;

            if (retailer == "Amazon")
            {
                productToCreate = await this.productRepository.CreateAmazonProduct(SKU, discordId, typeId);
            }
            else if (retailer == "Best Buy")
            {
                productToCreate = this.productRepository.CreateBestBuyProduct(SKU, discordId, typeId);

                if (productToCreate == null)
                {
                    ModelState.AddModelError("", "Best Buy has blacklisted this product");
                    return StatusCode(422, ModelState);
                }
            }

            if (productToCreate == null)
            {
                return BadRequest(ModelState);
            }

            var product = this.productRepository.GetAllProducts()
                .Where(p => p.SKU.Trim().ToUpper() == productToCreate.SKU.Trim().ToUpper())
                .FirstOrDefault();

            if (product != null)
            {
                ModelState.AddModelError("", $"Product {productToCreate.SKU} already exists");
                return StatusCode(422, ModelState);
            }

            if (string.IsNullOrWhiteSpace(retailer))
            {
                ModelState.AddModelError("", "No retailer was provided");
            }

            if (this.typeRepository.TypeExists(productToCreate.Type.Id) == false)
            {
                ModelState.AddModelError("", "Type doesn't exist");
            }

            if (this.discordRepository.DiscordExists(productToCreate.Discord.Id) == false)
            {
                ModelState.AddModelError("", "Discord doesn't exist");
            }

            if (ModelState.IsValid == false)
            {
                return StatusCode(404, ModelState);
            }

            productToCreate.Type = this.typeRepository.GetType(productToCreate.Type.Id);
            productToCreate.Discord = this.discordRepository.GetDiscord(productToCreate.Discord.Id);

            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            if (this.productRepository.CreateProduct(productToCreate) == false)
            {
                ModelState.AddModelError("", $"Something went wrong saving {productToCreate.Title}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetProduct", new { typeId = productToCreate.Id }, productToCreate);
        }

        [HttpPut("{productId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public IActionResult UpdateProduct(int productId, [FromBody] Product updatedProductInfo)
        {
            if (updatedProductInfo == null)
            {
                return BadRequest(ModelState);
            }

            if (productId != updatedProductInfo.Id)
            {
                return BadRequest(ModelState);
            }

            if (this.productRepository.ProductExists(productId) == false)
            {
                return NotFound();
            }

            if (this.productRepository.IsDuplicateProduct(productId, updatedProductInfo.SKU) == true)
            {
                ModelState.AddModelError("", $"Product {updatedProductInfo.Title} already exists");
                return StatusCode(422, ModelState);
            }

            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            if (this.productRepository.UpdateProduct(updatedProductInfo) == false)
            {
                ModelState.AddModelError("", $"Something went wrong updating {updatedProductInfo.Title}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
