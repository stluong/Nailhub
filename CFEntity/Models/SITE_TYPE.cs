using System;
using System.Collections.Generic;

namespace CFEntity.Models
{
    public partial class SITE_TYPE
    {
        public SITE_TYPE()
        {
            this.SITEs = new List<SITE>();
        }

        public short id { get; set; }
        public string NAME { get; set; }
        public string DESCRIPTION { get; set; }
        public string NOTE { get; set; }
        public virtual ICollection<SITE> SITEs { get; set; }
    }
}
