using System;
using System.Collections.Generic;

namespace CFEntity.Models
{
    public partial class PRODUCT_DETAIL
    {
        public int ID_PRODUCTDETAIL { get; set; }
        public Nullable<int> ID_PRODUCT { get; set; }
        public string NAME { get; set; }
        public string DESCRIPTION { get; set; }
        public string IMAGE { get; set; }
        public virtual PRODUCT PRODUCT { get; set; }
    }
}
