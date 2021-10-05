using RapidStockChecker.DataAccess;
using RSC.Models;
using System;
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

        public ICollection<Models.Type> GetAllTypes()
        {
            return this.dbContext.Types
                .OrderBy(t => t.Name)
                .ToList();
        }

        public ICollection<Product> GetProductFromAType(int typeId)
        {
            return this.dbContext.Products
                .Where(t => t.Type.Id == typeId)
                .ToList();
        }

        public Models.Type GetType(int typeId)
        {
            return this.dbContext.Types
                .Where(t => t.Id == typeId)
                .FirstOrDefault();
        }

        public Models.Type GetTypeOfAnProduct(int productId)
        {
            return this.dbContext.Products
                .Where(p => p.Id == productId)
                .Select(t => t.Type)
                .FirstOrDefault();
        }

        public bool TypeExists(int typeId)
        {
            return this.dbContext.Types
                .Any(t => t.Id == typeId);
        }
    }
}
