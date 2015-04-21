using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFColuc;
using TNT.Core.Service;

namespace CoLucCore
{
    public interface IProductService : IService<Product>
    {
        IEnumerable<Product> GetProducts();
    }
}
