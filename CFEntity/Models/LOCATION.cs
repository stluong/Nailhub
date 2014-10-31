using System;
using System.Collections.Generic;

namespace CFEntity.Models
{
    public partial class LOCATION
    {
        public short ID_LOCATION { get; set; }
        public string NAME { get; set; }
        public string ADDRESS { get; set; }
        public string ADDRESS2 { get; set; }
        public Nullable<short> ID_STATE { get; set; }
        public Nullable<short> ID_CITY { get; set; }
        public string ZIP { get; set; }
        public Nullable<short> ID_COUNTRY { get; set; }
        public virtual CITY CITY { get; set; }
        public virtual COUNTRY COUNTRY { get; set; }
        public virtual STATE STATE { get; set; }
    }
}
