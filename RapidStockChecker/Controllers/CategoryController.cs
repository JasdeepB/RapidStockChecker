using Microsoft.AspNetCore.Mvc;
using RSC.DataAccess.Services;
using RSC.Models;
using System.Collections.Generic;

namespace RapidStockCheckerAPI.Controllers
{
    public class CategoryController : Controller
    {
        private ICategoryRepository categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public IActionResult Index()
        {
            List<Category> list = (List<Category>)this.categoryRepository.GetCategories();
            return View(list);
        }
    }
}
