//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class CURRENT_PRODUCT
    {
        public int ID_CURRENT_PRODUCT { get; set; }
        public Nullable<int> ID_PRODUCT { get; set; }
        public Nullable<int> ID_SITE { get; set; }
        public string NAME { get; set; }
        public string PRICE { get; set; }
        public Nullable<int> ENTERBY { get; set; }
        public Nullable<System.DateTime> ENTERDATE { get; set; }
        public Nullable<int> MODIFYBY { get; set; }
        public Nullable<System.DateTime> MODIFYDATE { get; set; }
        public Nullable<int> DELETEBY { get; set; }
        public Nullable<System.DateTime> DELETEDATE { get; set; }
    
        public virtual PRODUCT PRODUCT { get; set; }
        public virtual SITE SITE { get; set; }
    }
}
