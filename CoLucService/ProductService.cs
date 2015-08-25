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
        private decimal _shpCost;
        
        public ProductService(IRepositoryAsync<Product> _rpoProduct) 
            :base(_rpoProduct)
        {
            this.rpoProduct = _rpoProduct;
            this.rpoProvider = new RepositoryProvider(new RepositoryFactory());
            this._shpCost = AppSettings.Get<decimal?>("shippingCost") ?? 2.99m;
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

        public int DeleteImage(int prdid, string imgId) {
            using (var co = new CoLucEntities(TNT.App.EFConnection.ToString())) {
                var myImgId = int.Parse(imgId);
                var imgProds = co.Images
                    .Where(i => i.productId == prdid && i.imageId == myImgId)
                    .ToList()
                ;
                foreach (var imgProd in imgProds) {
                    imgProd.EndDate = DateTime.Now;
                    imgProd.ObjectState = ObjectState.Modified;
                }
                //Remove setiamge if it is setting
                foreach( var prod in co.Products
                    .Where(p => p.SetImageId == myImgId)
                    .ToList()
                )
                {
                    prod.SetImageId = null;
                    prod.ObjectState = ObjectState.Modified;
                }
                return co.SaveChanges();
            }
        }
        
        /// <summary>
        /// Set default image for product
        /// </summary>
        /// <param name="prdid"></param>
        /// <param name="img"></param>
        /// <returns></returns>
        public int SetImage(int prdid, string imgId)
        {
            using (var co = new CoLucEntities(TNT.App.EFConnection.ToString()))
            {
                //var img = 
                    //co.Images
                    //.Where(i => i.productId == prdid 
                    //    && i.Path.Trim().ToUpper() == img.Trim().ToUpper()
                    //    && i.EndDate == null)
                    //.Select(i => i.imageId)
                //;
                if (!string.IsNullOrWhiteSpace(imgId))
                {
                    var myProd = co.Products.Find(prdid);
                    myProd.SetImageId = int.Parse(imgId);
                    myProd.ObjectState = ObjectState.Modified;
                }
                return co.SaveChanges();
            }
        }
        public xProduct Crud(xProduct xprod)
        {
            using (var co = new CoLucEntities(TNT.App.EFConnection.ToString()))
            using(var uow = new UnitOfWork(co, rpoProvider))
            {
                try
                {
                    uow.BeginTransaction();
                    if (xprod.productid != 0)
                    {
                        if (xprod.code.Equals("XOAXOAXOA"))
                        {
                            //delete
                            Delete(co, xprod);
                        }
                        else {
                            //update
                            Update(co, xprod);
                        }
                    }
                    else { 
                        //insert
                        AddNew(co, xprod);
                    }

                    uow.Commit();                    
                }
                catch(Exception ex) {
                    Mailing.SendException(ex);
                    uow.Rollback();
                    throw;
                }
                
            }
            return xprod;
        }

        #region Crud XProduct
        private void AddNew(CoLucEntities co, xProduct xprod) {
            //add new product
            var prod = new Product
            {
                BrandId = xprod.brandid,
                Code = xprod.code,
                Price = xprod.price,
                EnteredBy = 1,
                EnteredDate = DateTime.Now,
                ObjectState = ObjectState.Added
            };
            co.Products.Add(prod);
            co.SaveChanges();
            xprod.productid = prod.ProductId;
            //product detail, english
            co.ProductDetails.Add(new ProductDetail
            {
                ProductId = xprod.productid,
                LangId = 1,
                Name = xprod.name,
                Description = xprod.description,
                ObjectState = ObjectState.Added
            });
            //product detail, vietnamese
            co.ProductDetails.Add(new ProductDetail
            {
                ProductId = xprod.productid,
                LangId = 2,
                Name = xprod.Ten,
                Description = xprod.MieuTa,
                ObjectState = ObjectState.Added
            });
            //co.SaveChanges();
            //insert image
            var myImage = new Image
            {
                productId = xprod.productid,
                Path = xprod.image?? string.Empty,
                EnteredBy = 1,
                EnteredDate = DateTime.Now,
                ObjectState = ObjectState.Added
            };
            co.Images.Add(myImage);
            co.SaveChanges();
            //Set default image for product
            prod.SetImageId = (int?)myImage.imageId;
            prod.ObjectState = ObjectState.Modified;
            co.SaveChanges();
            //insert inventory
            foreach (var size in xprod.Sizes.ToList())
            {
                co.Inventories.Add(new Inventory
                {
                    ProductId = xprod.productid,
                    Cost = 9999,
                    DidOrder = 9999,
                    EnteredBy = 1,
                    EnteredDate = DateTime.Now,
                    OnHand = 9999,
                    OnOrder = 0,
                    Quantity = 9999,
                    Size = size,
                    ObjectState = ObjectState.Added
                });
            }
            co.SaveChanges();
        }
        private void Update(CoLucEntities co, xProduct xprod)
        {
            //update product
            var updatingProduct = co.Products
                .Include(p => p.ProductDetails)
                .Include(p => p.Inventories)
                .Where(p => p.ProductId == xprod.productid)
                .SingleOrDefault()
            ;
            updatingProduct.BrandId = xprod.brandid;
            updatingProduct.Code = xprod.code;
            updatingProduct.Price = decimal.Parse(xprod.price.ToString());
            updatingProduct.ObjectState = ObjectState.Modified;
            //update product detail
            var udtProductDetail = updatingProduct.ProductDetails.Where(pd => pd.LangId == xprod.langid).SingleOrDefault();
            udtProductDetail.Name = xprod.name;
            udtProductDetail.Description = xprod.description;
            udtProductDetail.ObjectState = ObjectState.Modified;
            //update sizes
            var sqlEndDate = string.Format("update coluc..inventory set enddate = '{0}' where productid = {1} and size not in({2})"
                , DateTime.Now
                , xprod.productid
                , string.Join(",", xprod.Sizes)
            );
            co.Database.ExecuteSqlCommand(sqlEndDate);
            co.SaveChanges();

            //remove enddate, or insert new record
            foreach (var size in xprod.Sizes.ToList())
            {
                //enddate none existing size
                var updatingSize = updatingProduct.Inventories
                    .Where(p => p.ProductId == xprod.productid && p.EndDate == null)
                    .Where(p => p.Size == size)
                    .SingleOrDefault()
                ;
                if (updatingSize != null)
                {
                    //remove enddate
                    updatingSize.EndDate = null;
                    updatingSize.ObjectState = ObjectState.Modified;
                }
                else
                {
                    //insert new
                    co.Inventories.Add(new Inventory
                    {
                        ProductId = xprod.productid,
                        Cost = 9999,
                        DidOrder = 9999,
                        EnteredBy = 1,
                        EnteredDate = DateTime.Now,
                        OnHand = 9999,
                        OnOrder = 0,
                        Quantity = 9999,
                        Size = size,
                        ObjectState = ObjectState.Added
                    });
                }
            }
            co.SaveChanges();

            //check if has new image, insert new
            if (!string.IsNullOrWhiteSpace(xprod.image))
            {
                co.Images.Add(new Image
                {
                    productId = xprod.productid,
                    Path = xprod.image,
                    EnteredBy = 1,
                    EnteredDate = DateTime.Now,
                    ObjectState = ObjectState.Added
                });

                co.SaveChanges();
            }
        }
        private void Delete(CoLucEntities co, xProduct xprod) {
            var myProd = co.Products.Find(xprod.productid);
            myProd.EndDate = DateTime.Now;
            myProd.ObjectState = ObjectState.Modified;
            co.SaveChanges();
        }
        #endregion
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
                    .Where(pi=>pi.p.EndDate == null)
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
                    var total = prods.Sum(p => p.price * p.quantity);
                    var shpFee = total > 30 ? 0m : _shpCost;
                    var order = new Order
                    {
                        CustId = retailCustomerId,
                        EnteredBy = 1,
                        EnteredDate = DateTime.Now,
                        OrderStatus = 0,
                        Delivery = shpFee,
                        SubTotal = total + shpFee,
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

        public IEnumerable<string> GetImages(int? prodId) {
            using (var co = new CoLucEntities(TNT.App.EFConnection.ToString())) {
                return co.Images
                    .Where(i => i.productId == prodId)
                    .Select(i => i.Path)
                    .ToList()
                ;
            }
        }
    }
}
