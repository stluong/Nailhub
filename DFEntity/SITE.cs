//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DFEntity
{
    using System;
    using System.Collections.Generic;
    
    public partial class SITE
    {
        public SITE()
        {
            this.CURRENT_PRODUCT = new HashSet<CURRENT_PRODUCT>();
            this.CURRENT_SERVICE = new HashSet<CURRENT_SERVICE>();
            this.THEMEs = new HashSet<THEME>();
        }
    
        public int id { get; set; }
        public Nullable<short> SITE_TYPE_ID { get; set; }
        public Nullable<int> USER_ID { get; set; }
        public string NAME { get; set; }
        public string DESCRIPTION { get; set; }
        public string NOTE { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual ICollection<CURRENT_PRODUCT> CURRENT_PRODUCT { get; set; }
        public virtual ICollection<CURRENT_SERVICE> CURRENT_SERVICE { get; set; }
        public virtual SITE_TYPE SITE_TYPE { get; set; }
        public virtual ICollection<THEME> THEMEs { get; set; }
    }
}
