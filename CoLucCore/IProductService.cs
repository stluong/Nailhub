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
        IEnumerable<Brand> GetBrands();
        IEnumerable<xProduct> GetXProducts(int? productId = null, int? langId = null);
        IEnumerable<xProduct> GetSpecialProduct(int? eventId = null, int langId = 1);
        IEnumerable<xOrder> GetXOrder(int? orderId = null, int? statusId = null, DateTime? fromDate = null);
        int CrudOrder(IEnumerable<xProduct> prods, string invoiceNo = null, string email = null);
        Order UpdateTracking(int orderId, string trackingNo);
        IEnumerable<string> GetImages(int? prodId);
    }
}
