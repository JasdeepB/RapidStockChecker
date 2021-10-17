using Nager.AmazonProductAdvertising;
using RapidStockChecker.DataAccess;
using RSC.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RSC.DataAccess.Services
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext dbContext;

        public ProductRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool CreateProduct(Product product)
        {
            this.dbContext.Add(product);
            return Save();
        }

        public async Task<Product> CreateProduct(string SKU, int discordId, int typeId)
        {
            var authentication = new AmazonAuthentication("AKIAJVMZ5BI4GYNFNJYQ", "61plkh/hS7ltiwS24FiQWJQBBo/Fb6vvis2wO4QO");
            var client = new AmazonProductAdvertisingClient(authentication, AmazonEndpoint.US, "rapidstockche-20");
            string[] ASIN = { SKU };
            Product product = new Product();

            var result = await client.GetItemsAsync(ASIN);

            if (result.ItemsResult.Items != null)
            {
                product.SKU = result.ItemsResult.Items[0].ASIN;
                product.Title = result.ItemsResult.Items[0].ItemInfo.Title.DisplayValue;
                product.ImageUrl = result.ItemsResult.Items[0].Images.Primary.Large.URL;
                product.Url = result.ItemsResult.Items[0].DetailPageURL;
                product.Retailer = "Amazon";
                product.InStock = false;

                product.Discord = new Discord() { Id = discordId };
                product.Type = new RSC.Models.Type() { Id = typeId };
            }

            return product;
        }

        public bool DeleteProduct(Product product)
        {
            this.dbContext.Remove(product);
            return Save();
        }

        public ICollection<Product> GetAllProducts()
        {
            return this.dbContext
                .Products
                .OrderBy(p => p.Title)
                .ToList();
        }

        public ICollection<Product> GetAllProductsByTypeId(int typeId)
        {
            return this.dbContext
                .Products
                .Where(p => p.Type.Id == typeId)
                .ToList();
        }

        public ICollection<Product> GetAllProductsInStock()
        {
            return this.dbContext
                .Products
                .Where(p => p.InStock == true)
                .ToList();
        }

        public Product GetProduct(int productId)
        {
            return this.dbContext.Products
                .Where(P => P.Id == productId)
                .FirstOrDefault();
        }

        public Product GetProduct(string SKU)
        {
            return this.dbContext.Products
                .Where(P => P.SKU == SKU)
                .FirstOrDefault();
        }

/*        public ICollection<RestockHistory> GetRestockHistory(int productId)
        {
            return this.dbContext
                .RestockHistory
                .Where(h => h.Product.Id == productId)
                .OrderBy(hs => hs.DateTime)
                .ToList();
        }*/

/*        public ICollection<RestockHistory> GetRestockHistory(string SKU)
        {
            return this.dbContext
                .RestockHistory
                .Where(h => h.Product.SKU == SKU)
                .OrderBy(hs => hs.DateTime)
                .ToList();
        }*/

        public bool IsDuplicateProduct(int productId, string productSKU)
        {
            var product = this.dbContext
                .Products
                .Where(p => p.SKU.Trim().ToUpper() == productSKU.Trim().ToUpper()
                 && p.Id != productId)
                .FirstOrDefault();

            return product == null ? false : true;
        }

        public Discord ProductDiscord(int productId)
        {
            return this.dbContext
                .Products
                .Where(p => p.Id == productId)
                .Select(d => d.Discord)
                .FirstOrDefault();
        }

        public Discord ProductDiscord(string SKU)
        {
            return this.dbContext
                .Products
                .Where(p => p.SKU == SKU)
                .Select(d => d.Discord)
                .FirstOrDefault();
        }

        public bool ProductExists(int productId)
        {
            return this.dbContext
                .Products
                .Any(p => p.Id == productId);
        }

        public bool ProductExists(string SKU)
        {
            return this.dbContext
                .Products
                .Any(p => p.SKU == SKU);
        }

        public bool Save()
        {
            var saved = this.dbContext.SaveChanges();
            return saved >= 0 ? true : false;
        }

        public bool UpdateProduct(Product product)
        {
            this.dbContext.Update(product);
            return Save();
        }
    }
}
