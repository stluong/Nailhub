using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNT.Core.Model;
using TNT.Infrastructure.Repositories;

namespace EFColuc
{
    public partial class CoLucEntities : MyContext
    {
        public CoLucEntities(string nameOrConnectionString) 
            : base(nameOrConnectionString) 
        { 
        }

    }
    public partial class AspNetRole : BaseEntity
    {
    }
    public partial class AspNetUser : BaseEntity
    {
    }
    public partial class AspNetUserClaim : BaseEntity
    {
    }
    public partial class AspNetUserLogin : BaseEntity
    {
    }
    public partial class Brand : BaseEntity
    {
    }
    public partial class Customer : BaseEntity
    {
    }
    public partial class Inventory : BaseEntity
    {
    }
    public partial class Language : BaseEntity
    {
    }
    public partial class Order : BaseEntity
    {
    }
    public partial class OrderDetail : BaseEntity
    {
    }
    public partial class Product : BaseEntity
    {
    }
    public partial class ProductDetail : BaseEntity
    {
    }
    public partial class SpecialEvent : BaseEntity
    {
    }
    public partial class Image: BaseEntity
    { }

    public partial class xProduct {
        public IEnumerable<int> Sizes { get; set; }
        public string Ten { get; set; }
        public string MieuTa { get; set; }
    }
}
