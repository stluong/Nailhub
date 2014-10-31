using System;
using System.Collections.Generic;

namespace CFEntity.Models
{
    public partial class THEME_DETAIL
    {
        public int id { get; set; }
        public Nullable<int> THEME_ID { get; set; }
        public string NAME { get; set; }
        public string DESCRIPT { get; set; }
        public string IMAGE { get; set; }
        public string STYLE { get; set; }
        public virtual THEME THEME { get; set; }
    }
}
