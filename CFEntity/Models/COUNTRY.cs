using System;
using System.Collections.Generic;
using TNT.Core.Model;

namespace CFEntity.Models
{
    public partial class COUNTRY: BaseEntity
    {
        public COUNTRY()
        {
            this.LOCATIONs = new List<LOCATION>();
        }

        public short id { get; set; }
        public string NAME { get; set; }
        public string NATIVELANGUAGE { get; set; }
        public string DESCRIPTION { get; set; }
        public string NOTE { get; set; }
        public virtual ICollection<LOCATION> LOCATIONs { get; set; }
    }
}
