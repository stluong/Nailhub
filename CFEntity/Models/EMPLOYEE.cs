using System;
using System.Collections.Generic;

namespace CFEntity.Models
{
    public partial class EMPLOYEE
    {
        public int id { get; set; }
        public Nullable<short> TITLE_ID { get; set; }
        public string NAME { get; set; }
        public string DESCRIPTION { get; set; }
        public virtual Title Title { get; set; }
    }
}
