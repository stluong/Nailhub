using System;
using System.Collections.Generic;

namespace CFEntity.Models
{
    public partial class THEME
    {
        public THEME()
        {
            this.THEME_DETAIL = new List<THEME_DETAIL>();
        }

        public int ID_THEME { get; set; }
        public Nullable<int> ID_SITE { get; set; }
        public string BACKGROUND { get; set; }
        public string BACKGROUNDIMAGE { get; set; }
        public string LOGO { get; set; }
        public string SITENAME { get; set; }
        public string DESCRIPTION { get; set; }
        public string NOTE { get; set; }
        public virtual SITE SITE { get; set; }
        public virtual ICollection<THEME_DETAIL> THEME_DETAIL { get; set; }
    }
}
