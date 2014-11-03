using System;
using System.Collections.Generic;

namespace CFEntity.Models
{
    public partial class Title
    {
        public Title()
        {
            this.EMPLOYEEs = new List<EMPLOYEE>();
        }

        public short id { get; set; }
        public string NAME { get; set; }
        public string DESCRIPTION { get; set; }
        public string NOTE { get; set; }
        public virtual ICollection<EMPLOYEE> EMPLOYEEs { get; set; }
    }
}
