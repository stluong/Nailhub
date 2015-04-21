using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoLucCore;
using EFColuc;
using TNT.Core.Repository;
using TNT.Core.UnitOfWork;
using TNT.Infracstructure.Services;

namespace CoLucService
{
    public class ProductService: Service<Product>, IProductService
    {
        private readonly IRepositoryAsync<Product> rpoProduct;
        
        public ProductService(IRepositoryAsync<Product> _rpoProduct) 
            :base(_rpoProduct)
        {
            this.rpoProduct = _rpoProduct;
        }
        public IEnumerable<Product> GetProducts()
        {
            return this.rpoProduct.Query().Get();
        }
    }
}
