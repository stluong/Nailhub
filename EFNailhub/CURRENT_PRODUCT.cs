//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EFNailhub
{
    using System;
    using System.Collections.Generic;
    
    public partial class CURRENT_PRODUCT
    {
        public int CURRENT_PRODUCT_ID { get; set; }
        public Nullable<int> PRODUCT_ID { get; set; }
        public Nullable<int> SITE_ID { get; set; }
        public string NAME { get; set; }
        public Nullable<decimal> PRICE { get; set; }
        public Nullable<decimal> TAX { get; set; }
        public Nullable<decimal> DELIVERY { get; set; }
        public Nullable<decimal> DISCOUNT { get; set; }
        public Nullable<int> ENTERBY { get; set; }
        public Nullable<System.DateTime> ENTERDATE { get; set; }
        public Nullable<int> MODIFYBY { get; set; }
        public Nullable<System.DateTime> MODIFYDATE { get; set; }
        public Nullable<System.DateTime> ENDDATE { get; set; }
    
        public virtual PRODUCT PRODUCT { get; set; }
        public virtual SITE SITE { get; set; }
    }
}