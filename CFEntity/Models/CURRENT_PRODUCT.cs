using System;
using System.Collections.Generic;

namespace CFEntity.Models
{
    public partial class CURRENT_PRODUCT
    {
        public int id { get; set; }
        public Nullable<int> PRODUCT_ID { get; set; }
        public Nullable<int> SITE_ID { get; set; }
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
