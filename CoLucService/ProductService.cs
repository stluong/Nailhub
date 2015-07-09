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
using System.Data.Entity;

namespace CoLucService
{
    public class ProductService: Service<Product>
        , IProductService
    {
        private readonly IRepositoryAsync<Product> rpoProduct;
        
        public ProductService(IRepositoryAsync<Product> _rpoProduct) 
            :base(_rpoProduct)
        {
            this.rpoProduct = _rpoProduct;
        }
        //public IEnumerable<Product> GetProducts()
        //{
        //    return this.rpoProduct.Query().Get();
        //}


        IEnumerable<Product> IProductService.GetProducts()
        {
            //using (var brus = new CoLucEntities())
            //{
            //    return brus.Products
            //        .Include(p => p.ProductDetails)
            //        .ToList()
            //    ;
            //}

            return this.rpoProduct.Query()
                .Include(p => p.ProductDetails)
                .Get()
            ;
        }

        IEnumerable<xProduct> IProductService.GetXProducts(int? productId, int? langId) {
            using (var co = new CoLucEntities(TNT.App.EFConnection.ToString())) {
                return co.GetProduct(productId, langId)
                    .ToList()
                ;
            }
        }

        void TNT.Core.Service.IService<Product>.Delete(Product entity)
        {
            this.rpoProduct.Delete(entity);
        }

        void TNT.Core.Service.IService<Product>.Delete(object id)
        {
            this.rpoProduct.Delete(id);
        }

        Task<bool> TNT.Core.Service.IService<Product>.DeleteAsync(System.Threading.CancellationToken cancellationToken, params object[] keyValues)
        {
            throw new NotImplementedException();
        }

        Task<bool> TNT.Core.Service.IService<Product>.DeleteAsync(params object[] keyValues)
        {
            throw new NotImplementedException();
        }

        Product TNT.Core.Service.IService<Product>.Find(params object[] keyValues)
        {
            return this.rpoProduct.Find(keyValues);
        }

        Task<Product> TNT.Core.Service.IService<Product>.FindAsync(System.Threading.CancellationToken cancellationToken, params object[] keyValues)
        {
            throw new NotImplementedException();
        }

        Task<Product> TNT.Core.Service.IService<Product>.FindAsync(params object[] keyValues)
        {
            throw new NotImplementedException();
        }

        System.Web.Http.SingleResult<Product> TNT.Core.Service.IService<Product>.GetSingleResult(params object[] keyValues)
        {
            throw new NotImplementedException();
        }

        void TNT.Core.Service.IService<Product>.Insert(Product entity)
        {
            this.rpoProduct.Insert(entity);
        }

        void TNT.Core.Service.IService<Product>.InsertGraph(Product entity)
        {
            throw new NotImplementedException();
        }

        void TNT.Core.Service.IService<Product>.InsertGraphRange(IEnumerable<Product> entities)
        {
            throw new NotImplementedException();
        }

        void TNT.Core.Service.IService<Product>.InsertRange(IEnumerable<Product> entities)
        {
            throw new NotImplementedException();
        }

        IQueryable<Product> TNT.Core.Service.IService<Product>.ODataQueryable()
        {
            throw new NotImplementedException();
        }

        IQueryable TNT.Core.Service.IService<Product>.ODataQueryable(System.Web.Http.OData.Query.ODataQueryOptions<Product> oDataQueryOptions)
        {
            throw new NotImplementedException();
        }

        IQueryableOperation<Product> TNT.Core.Service.IService<Product>.Query(System.Linq.Expressions.Expression<Func<Product, bool>> query)
        {
            throw new NotImplementedException();
        }

        IQueryableOperation<Product> TNT.Core.Service.IService<Product>.Query(IQueryExpression<Product> queryObject)
        {
            throw new NotImplementedException();
        }

        IQueryableOperation<Product> TNT.Core.Service.IService<Product>.Query()
        {
            throw new NotImplementedException();
        }

        IQueryable<Product> TNT.Core.Service.IService<Product>.SelectQuery(string query, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        void TNT.Core.Service.IService<Product>.Update(Product entity)
        {
            this.rpoProduct.Update(entity);
        }


        public IEnumerable<Brand> GetBrands()
        {
            //return this.rpoProduct.GetRepository<Brand>().Queryable()
            //    .Where(b => b.EndDate == null)
            //    .ToList()
            //;

            return this.rpoProduct.GetBrands();
        }
    }
}
