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
using TNTHelper;
using TNT.Core.Context;
using TNT.Infracstructure.UnitOfWorks;
using TNT.Infrastructure.Repositories;


namespace CoLucService
{
    public class ProductService: Service<Product>
        , IProductService
    {
        private readonly IRepositoryAsync<Product> rpoProduct;
        private readonly IRepositoryProvider rpoProvider;
        private const int retailCustomerId = 1;
        
        public ProductService(IRepositoryAsync<Product> _rpoProduct) 
            :base(_rpoProduct)
        {
            this.rpoProduct = _rpoProduct;
            this.rpoProvider = new RepositoryProvider(new RepositoryFactory());
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

        public IEnumerable<xProduct> GetSpecialProduct(int? eventId, int langId = 1)
        {
            using (var ct = new CoLucEntities(TNT.App.EFConnection.ToString())) {
                var mySpeProducts = ct.SpecialEvents
                    .Include(e => e.Product)
                    .Include(e => e.Product.ProductDetails)
                    .LeftJoin(ct.Images
                        , p => p.Product.SetImageId
                        , i => i.imageId
                        , (p, i) => new { p, i }
                    )
                    .Select(pi => new
                    {
                        productid = pi.p.ProductId,
                        name = pi.p.Product.ProductDetails.FirstOrDefault(pd => pd.LangId == 1).Name,
                        price = pi.p.Product.Price,
                        image = pi.i != null ? pi.i.Path : string.Empty
                    })
                    .ToList()
                ;

                return mySpeProducts.Select(p => new xProduct
                {
                    productid = p.productid,
                    name = p.name,
                    price = p.price,
                    image = p.image
                })
                .ToList();
            }
        }

        public IEnumerable<xOrder> GetXOrder(int? orderId = null, int? statusId = null, DateTime? fromDate = null) {
            using (var co = new CoLucEntities(TNT.App.EFConnection.ToString()))
            {
                return co.GetOrder(orderId, statusId, fromDate).ToList();
            }
        }

        public int CrudOrder(IEnumerable<xProduct> prods, string invoiceNo, string email) { 
            using(var ct = new CoLucEntities(TNT.App.EFConnection.ToString()))
            using (var uow = new UnitOfWork(ct, rpoProvider)) {
                uow.BeginTransaction();
                var rpoOrder = new Repository<Order>(ct, uow);
                var rpoOrderDetail = new Repository<OrderDetail>(ct, uow);
                try
                {
                    var order = new Order
                    {
                        CustId = retailCustomerId,
                        EnteredBy = 1,
                        EnteredDate = DateTime.Now,
                        OrderStatus = 0,
                        SubTotal = prods.Sum(p => p.price * p.quantity),
                        OrderDate = DateTime.Now,
                        OrderComment = prods.FirstOrDefault().note,
                        InvoiceNo = invoiceNo,
                        CustComment = email,
                        ObjectState = ObjectState.Added
                    };

                    rpoOrder.Insert(order);
                    uow.SaveChanges();

                    var orderDetails = prods.Select(p => new OrderDetail
                        {
                            OrderId = order.OrderId,
                            ProductId = p.productid,
                            Quantity = p.quantity ?? 1,
                            Price = p.price,
                            Total = p.price * p.quantity ?? 1,
                            Description = p.description,
                            ObjectState = ObjectState.Added
                        }).ToList()
                    ;

                    rpoOrderDetail.InsertGraphRange(orderDetails);
                    uow.SaveChanges();

                    uow.Commit();

                    return 1;
                }
                catch {
                    uow.Rollback();
                    return -1;
                }
                
                
            }   
        }


        public Order UpdateTracking(int orderId, string trackingNo)
        {
            using (var co = new CoLucEntities(TNT.App.EFConnection.ToString())) {
                var updatingOrder = co.Orders.FirstOrDefault(o => o.OrderId == orderId);
                updatingOrder.TrackingNo = trackingNo;
                updatingOrder.OrderStatus = 1;
                updatingOrder.ObjectState = ObjectState.Modified;
                co.SaveChanges();
                return updatingOrder;
            }
        }
    }
}
