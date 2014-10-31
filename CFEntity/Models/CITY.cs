using System;
using System.Collections.Generic;

namespace CFEntity.Models
{
    public partial class CITY
    {
        public CITY()
        {
            this.LOCATIONs = new List<LOCATION>();
        }

        public short id { get; set; }
        public string NAME { get; set; }
        public string DESCRIPTION { get; set; }
        public string NOTE { get; set; }
        public virtual ICollection<LOCATION> LOCATIONs { get; set; }
    }
}
