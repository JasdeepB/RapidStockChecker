using RSC.Models;
using System.Collections.Generic;

namespace RSC.DataAccess.Services
{
    public interface ITypeRepository
    {
        ICollection<RSC.Models.Type> GetAllTypes();
        RSC.Models.Type GetType(int typeId);
        RSC.Models.Type GetType(string typeName);
        RSC.Models.Type GetTypeOfAnProduct(int productId);
        /*        Discord GetDiscordOfAnType(int typeId);
                Discord GetDiscordOfAnType(string typeName);*/
        Category GetCategoryOfAnType(int typeId);
        Category GetCategoryOfAnType(string typeName);
        ICollection<Product> GetProductsFromAType(int typeId);
        ICollection<Product> GetProductsFromAType(string typeName);
        bool IsDuplicateType(int typeId, string typeName);
        bool CreateType(Type type);
        bool UpdateType(Type type);
        bool DeleteType(Type type);
        bool Save();
        bool TypeExists(int typeId);
        bool TypeExists(string typeName);
    }
}
