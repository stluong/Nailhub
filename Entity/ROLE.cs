//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class ROLE
    {
        public ROLE()
        {
            this.USER_ROLE = new HashSet<USER_ROLE>();
        }
    
        public short ID_ROLE { get; set; }
        public string NAME { get; set; }
        public string DESCRIPTION { get; set; }
        public string NOTE { get; set; }
    
        public virtual ICollection<USER_ROLE> USER_ROLE { get; set; }
    }
}
