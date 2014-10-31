using System;
using System.Collections.Generic;

namespace CFEntity.Models
{
    public partial class USER_TYPE
    {
        public USER_TYPE()
        {
            this.USER_DETAIL = new List<USER_DETAIL>();
        }

        public short id { get; set; }
        public string NAME { get; set; }
        public string NOTE { get; set; }
        public virtual ICollection<USER_DETAIL> USER_DETAIL { get; set; }
    }
}
