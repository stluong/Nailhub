using System;
using System.Collections.Generic;

namespace CFEntity.Models
{
    public partial class CURRENT_SERVICE
    {
        public int ID_CURRENT_SERVICE { get; set; }
        public Nullable<int> ID_SERVICE { get; set; }
        public Nullable<int> ID_SITE { get; set; }
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
