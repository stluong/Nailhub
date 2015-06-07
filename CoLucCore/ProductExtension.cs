using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFColuc;
using TNT.Core.Repository;

namespace CoLucCore
{
    public static class ProductExtension
    {
        public static IEnumerable<Brand> GetBrands(this IRepository<Product> repository) {
            return repository.GetRepository<Brand>().Queryable()
                .Where(b => b.EndDate == null)
                .ToList()
            ;
        }
    }
}
