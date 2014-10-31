using System;
using System.Collections.Generic;

namespace CFEntity.Models
{
    public partial class STATE
    {
        public STATE()
        {
            this.LOCATIONs = new List<LOCATION>();
        }

        public short ID_STATE { get; set; }
        public string NAME { get; set; }
        public string SHORTNAME { get; set; }
        public string DESCRIPTION { get; set; }
        public string NOTE { get; set; }
        public virtual ICollection<LOCATION> LOCATIONs { get; set; }
    }
}
