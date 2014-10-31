using System;
using System.Collections.Generic;

namespace CFEntity.Models
{
    public partial class LOCATION
    {
        public short id { get; set; }
        public string NAME { get; set; }
        public string ADDRESS { get; set; }
        public string ADDRESS2 { get; set; }
        public Nullable<short> STATE_ID { get; set; }
        public Nullable<short> CITY_ID { get; set; }
        public string ZIP { get; set; }
        public Nullable<short> COUNTRY_ID { get; set; }
        public virtual CITY CITY { get; set; }
        public virtual COUNTRY COUNTRY { get; set; }
        public virtual STATE STATE { get; set; }
    }
}
