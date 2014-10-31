using System;
using System.Collections.Generic;

namespace CFEntity.Models
{
    public partial class TYPE
    {
        public TYPE()
        {
            this.PRODUCTs = new List<PRODUCT>();
            this.SERVICEs = new List<SERVICE>();
        }

        public int id { get; set; }
        public string NAME { get; set; }
        public string DESCRIPTION { get; set; }
        public string NOTE { get; set; }
        public virtual ICollection<PRODUCT> PRODUCTs { get; set; }
        public virtual ICollection<SERVICE> SERVICEs { get; set; }
    }
}
