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
    
    public partial class THEME_DETAIL
    {
        public int id { get; set; }
        public Nullable<int> THEME_ID { get; set; }
        public string NAME { get; set; }
        public string DESCRIPT { get; set; }
        public string IMAGE { get; set; }
        public string STYLE { get; set; }
    
        public virtual THEME THEME { get; set; }
    }
}
