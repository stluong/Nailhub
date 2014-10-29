using System;
using System.Collections.Generic;

namespace CFEntity.Models
{
    public partial class PRODUCT
    {
        public PRODUCT()
        {
            this.CURRENT_PRODUCT = new List<CURRENT_PRODUCT>();
            this.PRODUCT_DETAIL = new List<PRODUCT_DETAIL>();
        }

        public int id { get; set; }
        public Nullable<int> TYPE_ID { get; set; }
        public string NAME { get; set; }
        public string DESCRIPTION { get; set; }
        public string NOTE { get; set; }
        public virtual ICollection<CURRENT_PRODUCT> CURRENT_PRODUCT { get; set; }
        public virtual TYPE TYPE { get; set; }
        public virtual ICollection<PRODUCT_DETAIL> PRODUCT_DETAIL { get; set; }
    }
}
