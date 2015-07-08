//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EFColuc
{
    using System;
    using System.Collections.Generic;
    
    public partial class Brand
    {
        public Brand()
        {
            this.Products = new HashSet<Product>();
        }
    
        public int BrandId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string MadeOf { get; set; }
        public string MadeFrom { get; set; }
        public string MadeBy { get; set; }
        public int EnteredBy { get; set; }
        public System.DateTime EnteredDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
    
        public virtual ICollection<Product> Products { get; set; }
    }
}
