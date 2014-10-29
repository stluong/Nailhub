using System;
using System.Collections.Generic;

namespace CFEntity.Models
{
    public partial class SERVICE_DETAIL
    {
        public int id { get; set; }
        public Nullable<int> SERVICE_ID { get; set; }
        public string NAME { get; set; }
        public string DESCRIPTION { get; set; }
        public string NOTE { get; set; }
        public virtual SERVICE SERVICE { get; set; }
    }
}
