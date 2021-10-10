using RapidStockChecker.DataAccess;
using RSC.Models;
using System.Collections.Generic;
using System.Linq;

namespace RSC.DataAccess.Services
{
    public class TypeRepository : ITypeRepository
    {
        private readonly ApplicationDbContext dbContext;

        public TypeRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool CreateType(RSC.Models.Type type)
        {
            this.dbContext.Add(type);
            return Save();
        }

        public bool DeleteType(RSC.Models.Type type)
        {
            this.dbContext.Remove(type);
            return Save();
        }

        public ICollection<RSC.Models.Type> GetAllTypes()
        {
            return this.dbContext.Types
                .OrderBy(t => t.Name)
                .ToList();
        }

        public Category GetCategoryOfAnType(int typeId)
        {
            return this.dbContext
                .Types
                .Where(t => t.Id == typeId)
                .Select(c => c.Category)
                .FirstOrDefault();
        }

        public Category GetCategoryOfAnType(string typeName)
        {
            return this.dbContext
                .Types
                .Where(t => t.Name == typeName)
                .Select(c => c.Category)
                .FirstOrDefault();
        }

        /*        public Discord GetDiscordOfAnType(int typeId)
                {
                    return this.dbContext
                        .Types
                        .Where(t => t.Id == typeId)
                        .Select(d => d.Discord)
                        .FirstOrDefault();
                }


                public Discord GetDiscordOfAnType(string typeName)
                {
                    return this.dbContext
                        .Types
                        .Where(t => t.Name == typeName)
                        .Select(d => d.Discord)
                        .FirstOrDefault();
                }*/

        /// <summary>
        /// Retrieves all product associated with a type using the type ID
        /// </summary>
        public ICollection<Product> GetProductsFromAType(int typeId)
        {
            return this.dbContext.Products
                .Where(t => t.Type.Id == typeId)
                .ToList();
        }

        /// <summary>
        /// Retrieves all product associated with a type using the name of the type
        /// </summary>
        public ICollection<Product> GetProductsFromAType(string typeName)
        {
            return this.dbContext.Products
                .Where(t => t.Type.Name == typeName)
                .ToList();
        }

        public RSC.Models.Type GetType(int typeId)
        {
            return this.dbContext.Types
                .Where(t => t.Id == typeId)
                .FirstOrDefault();
        }

        public RSC.Models.Type GetType(string typeName)
        {
            return this.dbContext.Types
                .Where(t => t.Name == typeName)
                .FirstOrDefault();
        }

        /// <summary>
        /// Retrieves the type ID of a given product
        /// </summary>
        public RSC.Models.Type GetTypeOfAnProduct(int productId)
        {
            return this.dbContext.Products
                .Where(p => p.Id == productId)
                .Select(t => t.Type)
                .FirstOrDefault();
        }

        public bool IsDuplicateType(int typeId, string typeName)
        {
            var type = this.dbContext
                .Types
                .Where(t => t.Name.Trim().ToUpper() == typeName.Trim().ToUpper()
                 && t.Id != typeId)
                .FirstOrDefault();

            return type == null ? false : true;
        }

        public bool Save()
        {
            var saved = this.dbContext.SaveChanges();
            return saved >= 0 ? true : false;
        }

        public bool TypeExists(int typeId)
        {
            return this.dbContext.Types
                .Any(t => t.Id == typeId);
        }

        public bool TypeExists(string typeName)
        {
            return this.dbContext.Types
                .Any(t => t.Name == typeName);
        }

        public bool UpdateType(RSC.Models.Type type)
        {
            this.dbContext.Update(type);
            return Save();
        }
    }
}
