//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DFEntity
{
    using System;
    using System.Collections.Generic;
    
    public partial class CURRENT_SERVICE
    {
        public int id { get; set; }
        public Nullable<int> SERVICE_ID { get; set; }
        public Nullable<int> SITE_ID { get; set; }
        public string NAME { get; set; }
        public Nullable<decimal> PRICE { get; set; }
        public string DESCRIPTION { get; set; }
        public string NOTE { get; set; }
        public Nullable<int> ENTERBY { get; set; }
        public Nullable<System.DateTime> ENTERDATE { get; set; }
        public Nullable<int> MODIFYBY { get; set; }
        public Nullable<System.DateTime> MODIFYDATE { get; set; }
        public Nullable<int> DELETEBY { get; set; }
        public Nullable<System.DateTime> DELETEDATE { get; set; }
    
        public virtual SERVICE SERVICE { get; set; }
        public virtual SITE SITE { get; set; }
    }
}
