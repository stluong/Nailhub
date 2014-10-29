using System;
using System.Collections.Generic;

namespace CFEntity.Models
{
    public partial class EMPLOYEE
    {
        public string id { get; set; }
        public Nullable<short> TITLE_ID { get; set; }
        public string NAME { get; set; }
        public string DESCRIPTION { get; set; }
        public virtual TITLE TITLE { get; set; }
    }
}
