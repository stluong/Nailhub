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
    
    public partial class CITY
    {
        public CITY()
        {
            this.LOCATIONs = new HashSet<LOCATION>();
        }
    
        public short id { get; set; }
        public string NAME { get; set; }
        public string DESCRIPTION { get; set; }
        public string NOTE { get; set; }
    
        public virtual ICollection<LOCATION> LOCATIONs { get; set; }
    }
}