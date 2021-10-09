using RSC.Models;
using System.Collections.Generic;

namespace RSC.DataAccess.Services
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetCategories();
        Category GetCategory(int categoryId);
        Category GetCategoryOfAnType(int productId);
        ICollection<RSC.Models.Type> GetAllTypesForCategory(int categoryId);
        bool CategoryExists(int categoryId);
        bool IsDuplicateCategory(int categoryId, string categoryName);
        bool CreateCategory(Category category);
        bool UpdateCategory(Category category);
        bool DeleteCategory(Category category);
        bool Save();
    }
}
