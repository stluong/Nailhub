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
    
    public partial class LOCATION
    {
        public short id { get; set; }
        public string NAME { get; set; }
        public string ADDRESS { get; set; }
        public string ADDRESS2 { get; set; }
        public Nullable<short> STATE_ID { get; set; }
        public Nullable<short> CITY_ID { get; set; }
        public string ZIP { get; set; }
        public Nullable<short> COUNTRY_ID { get; set; }
    
        public virtual CITY CITY { get; set; }
        public virtual COUNTRY COUNTRY { get; set; }
        public virtual STATE STATE { get; set; }
    }
}
