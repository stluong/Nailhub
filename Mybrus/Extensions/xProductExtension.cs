using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EFColuc;

namespace Mybrus.Extensions
{
    public static class xProductExtension
    {
        /// <summary>
        /// Get default image
        /// </summary>
        /// <param name="xprod"></param>
        /// <returns></returns>
        public static string GetImageSet(this xProduct xprod, int? prodId = null) {
            using (var co = new CoLucEntities(TNT.App.EFConnection.ToString()))
            {
                var myPi = co.Products
                    .Join(co.Images
                        , pk => pk.ProductId
                        , fk => fk.productId
                        , (p, i) => new { p, i }
                    //{
                    //    SetImageId = p.SetImageId,
                    //    Path = i.Path,
                    //    Endate = i.EndDate
                    //}
                    )
                    .Where(pi => pi.p.EndDate == null && pi.i.EndDate == null)
                    .Where(pi => 
                        pi.i.productId == (prodId ?? xprod.productid)
                        && pi.i.imageId == pi.p.SetImageId
                    )
                    .ToList()
                ;

                return myPi
                    .Select(pi => pi.i.Path)
                    .SingleOrDefault()
                ;
            }
        }
        /// <summary>
        /// Get all images of product. Return its images if prodId is null
        /// </summary>
        /// <param name="xprod"></param>
        /// <param name="prodId"></param>
        /// <returns></returns>
        public static IEnumerable<Image> GetImages(this xProduct xprod, int? prodId = null)
        {
            using (var co = new CoLucEntities(TNT.App.EFConnection.ToString()))
            {
                return co.Images
                    .Where(i => i.productId == (prodId ?? xprod.productid) && i.EndDate == null)
                    //.Select(i => i.Path)
                    .ToList()
                ;
            }
        }
        /// <summary>
        /// Get all available size
        /// </summary>
        /// <param name="xprod"></param>
        /// <param name="prodId"></param>
        /// <returns></returns>
        public static IEnumerable<int> GetSizes(this xProduct xprod, int? prodId = null) {
            using (var co = new CoLucEntities(TNT.App.EFConnection.ToString())) {
                return co.Inventories
                    .Where(i => i.ProductId == (prodId ?? xprod.productid) && i.EndDate == null)
                    .Select(i => i.Size)
                    .OrderBy(i => i)
                    .ToList()
                ;
            }
        }
    }
}