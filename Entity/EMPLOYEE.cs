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
    
    public partial class EMPLOYEE
    {
        public string ID_EMPLYEE { get; set; }
        public Nullable<short> ID_TITLE { get; set; }
        public string NAME { get; set; }
        public string DESCRIPTION { get; set; }
    
        public virtual Title Title { get; set; }
    }
}
