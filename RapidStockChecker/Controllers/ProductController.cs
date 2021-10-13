using Microsoft.AspNetCore.Mvc;
using RSC.DataAccess.Services;
using RSC.Models;
using System.Collections.Generic;

namespace RapidStockChecker.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository productRepository;

        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public IActionResult Index()
        {
            List<Product> list = (List<Product>)this.productRepository.GetAllProductsByType(1);
            return View(list);
        }

        //localhost/Product/RTX3090
        public IActionResult RTX3090()
        {
            return View();
        }

        //To add another page
        //create a method like so
/*      public IActionResult<Desired page name>()
        {
            return View();
        }*/
        //Right click the method name and click 'Add View'
        //The routing will to the page will then be localhost/Product/<Desired name given>
    }
}
