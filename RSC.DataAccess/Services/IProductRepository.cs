using RSC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RSC.DataAccess.Services
{
    public interface IProductRepository
    {
        Product GetProduct(int productId);
        Product GetProduct(string SKU);
        ICollection<Product> GetAllProducts();
        ICollection<Product> GetAllProductsByType(int typeId);
        Discord ProductDiscord(int productId);
        Discord ProductDiscord(string SKU);
        ICollection<Product> GetAllProductsInStock();
/*        ICollection<RestockHistory> GetRestockHistory(int productId);
        ICollection<RestockHistory> GetRestockHistory(string SKU);*/
        bool IsDuplicateProduct(int productId, string productSKU);
        bool CreateProduct(Product product);
        Task<Product> CreateProduct(string SKU, int discordId, int typeId);
        bool UpdateProduct(Product product);
        bool DeleteProduct(Product product);
        bool Save();
        bool ProductExists(int productId);
        bool ProductExists(string SKU);
    }
}
