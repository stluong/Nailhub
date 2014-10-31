using System;
using System.Collections.Generic;

namespace CFEntity.Models
{
    public partial class SERVICE
    {
        public SERVICE()
        {
            this.CURRENT_SERVICE = new List<CURRENT_SERVICE>();
            this.SERVICE_DETAIL = new List<SERVICE_DETAIL>();
        }

        public int ID_SERVICE { get; set; }
        public Nullable<int> ID_TYPE { get; set; }
        public string NAME { get; set; }
        public string DESCRIPTION { get; set; }
        public string NOTE { get; set; }
        public virtual ICollection<CURRENT_SERVICE> CURRENT_SERVICE { get; set; }
        public virtual TYPE TYPE { get; set; }
        public virtual ICollection<SERVICE_DETAIL> SERVICE_DETAIL { get; set; }
    }
}
