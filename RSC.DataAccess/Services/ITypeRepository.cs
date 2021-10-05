using RSC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSC.DataAccess.Services
{
    public interface ITypeRepository
    {
        ICollection<RSC.Models.Type> GetAllTypes();
        RSC.Models.Type GetType(int typeId);
        RSC.Models.Type GetTypeOfAnProduct(int productId);
        ICollection<Product> GetProductFromAType(int typeId);
        bool TypeExists(int typeId);
    }
}
