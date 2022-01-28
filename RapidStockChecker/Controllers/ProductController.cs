using Microsoft.AspNetCore.Mvc;
using RSC.DataAccess.Services;
using RSC.Models;
using RSC.DataAccess.Dtos;
using System.Collections.Generic;
using System.Dynamic;

namespace RapidStockChecker.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository productRepository;
        private IRestockHistoryRepository historyRepository;

        public ProductController(IProductRepository productRepository, IRestockHistoryRepository historyRepository)
        {
            this.productRepository = productRepository;
            this.historyRepository = historyRepository;
        }

        public IActionResult Tests()
        {
            return View(GetPageResoucesByTypeId(4));
        }
        //localhost/Product/RTX3090
        public IActionResult RTX3090()
        {
            return View(GetPageResoucesByTypeId(5));
        }
        public IActionResult RTX3080Ti()
        {
            return View(GetPageResoucesByTypeId(6));
        }
        public IActionResult RTX3080()
        {
            return View(GetPageResoucesByTypeId(7));
        }
        public IActionResult RTX3070Ti()
        {
            return View(GetPageResoucesByTypeId(8));
        }
        public IActionResult RTX3070()
        {
            return View(GetPageResoucesByTypeId(9));
        }
        public IActionResult RTX3060Ti()
        {
            return View(GetPageResoucesByTypeId(10));
        }
        public IActionResult RTX3060()
        {
            return View(GetPageResoucesByTypeId(11));
        }
        public IActionResult RX6900XT()
        {
            return View(GetPageResoucesByTypeId(15));
        }
        public IActionResult RX6800XT()
        {
            return View(GetPageResoucesByTypeId(14));
        }
        public IActionResult RX6800()
        {
            return View(GetPageResoucesByTypeId(13));
        }
        public IActionResult RX6700XT()
        {
            return View(GetPageResoucesByTypeId(12));
        }
        public IActionResult PS5()
        {
            return View(GetPageResoucesByTypeId(17));
        }
        public IActionResult PS5Digital()
        {
            return View(GetPageResoucesByTypeId(18));
        }
        public IActionResult XBOXSeriesX()
        {
            return View(GetPageResoucesByTypeId(19));
        }
        public IActionResult XBOXSeriesS()
        {
            return View(GetPageResoucesByTypeId(20));
        }
        public IActionResult SwitchOLED()
        {
            return View(GetPageResoucesByTypeId(21));
        }

        public IActionResult RTX3050()
        {
            return View(GetPageResoucesByTypeId(23));
        }

        //EXAMPLE
        //http://localhost:63763/Product/GetProduct/SKU?SKU=B07GBZ4Q68
        [HttpGet()]
        public IActionResult GetProduct([FromQuery(Name = "SKU")] string SKU)
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

            var productDto = new ProductDto()
            {
                SKU = product.SKU,
                Title = product.Title,
                ImageUrl = product.ImageUrl,
                Url = product.Url,
                Retailer = product.Retailer
            };

            return View(productDto);
        }

        //To add another page
        //create a method like so
        /*      public IActionResult<Desired page name>()
                {
                    return View();
                }*/
        //Right click the method name and click 'Add View'
        //The routing will to the page will then be localhost/Product/<Desired name given>

        private dynamic GetPageResoucesByTypeId(int typeId)
        {
            List<Product> list = (List<Product>)this.productRepository.GetAllProductsByTypeId(typeId);
            List<RestockHistory> history = (List<RestockHistory>)this.historyRepository.GetRestockHistoryByTypeId(typeId);

            dynamic mymodel = new ExpandoObject();
            mymodel.Products = list;
            mymodel.History = history;

            return mymodel;
        }
    }
}
