using System;
using System.Collections.Generic;

namespace CFEntity.Models
{
    public partial class COUNTRY
    {
        public COUNTRY()
        {
            this.LOCATIONs = new List<LOCATION>();
        }

        public short ID_COUNTRY { get; set; }
        public string NAME { get; set; }
        public string NATIVELANGUAGE { get; set; }
        public string DESCRIPTION { get; set; }
        public string NOTE { get; set; }
        public virtual ICollection<LOCATION> LOCATIONs { get; set; }
    }
}
