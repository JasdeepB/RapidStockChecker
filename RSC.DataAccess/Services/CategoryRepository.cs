using RapidStockChecker.DataAccess;
using RSC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSC.DataAccess.Services
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext dbContext;

        public CategoryRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool CategoryExists(int categoryId)
        {
            return this.dbContext.Categories
                .Any(c => c.Id == categoryId);
        }

        public bool CreateCategory(Category category)
        {
            this.dbContext.Add(category);
            return Save();
        }

        public bool DeleteCategory(Category category)
        {
            this.dbContext.Remove(category);
            return Save();
        }

        public ICollection<Models.Type> GetAllTypesForCategory(int categoryId)
        {
            var v = this.dbContext
                .Types
                .Where(type => type.Category.Id == categoryId)
                .ToList();
            return v;
        }

        public ICollection<Category> GetCategories()
        {
            return this.dbContext
                .Categories
                .OrderBy(c => c.Name)
                .ToList();
        }

        public Category GetCategory(int categoryId)
        {
            return this.dbContext
                .Categories
                .Where(c => c.Id == categoryId)
                .FirstOrDefault();
        }

        public Category GetCategoryOfAnType(int typeId)
        {
            return this.dbContext
                .Types
                .Where(t => t.Id == typeId)
                .Select(c => c.Category)
                .FirstOrDefault();
        }

        public bool IsDuplicateCategory(int categoryId, string categoryName)
        {
            var category = this.dbContext
                .Categories
                .Where(c => c.Name.Trim().ToUpper() == categoryName.Trim().ToUpper()
                 && c.Id != categoryId)
                .FirstOrDefault();

            return category == null ? false : true;
        }

        public bool Save()
        {
            var saved = this.dbContext.SaveChanges();
            return saved >= 0 ? true : false;
        }

        public bool UpdateCategory(Category category)
        {
            this.dbContext.Update(category);
            return Save();
        }
    }
}
