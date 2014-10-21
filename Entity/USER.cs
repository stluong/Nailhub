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
    
    public partial class USER
    {
        public USER()
        {
            this.USER_ROLE = new HashSet<USER_ROLE>();
            this.USER_DETAIL = new HashSet<USER_DETAIL>();
            this.USER_CLAIM = new HashSet<USER_CLAIM>();
            this.USER_LOGIN = new HashSet<USER_LOGIN>();
        }
    
        public int ID_USER { get; set; }
        public Nullable<short> ID_USERTYPE { get; set; }
        public string USERNAME { get; set; }
        public string PASSWORD { get; set; }
        public string SECURITY_STAMP { get; set; }
        public string EMAIL { get; set; }
        public Nullable<bool> EMAIL_CONFIRMED { get; set; }
        public string PHONE_NUMBER { get; set; }
        public Nullable<bool> PHONE_NUMBER_CONFIRMED { get; set; }
        public Nullable<bool> TWO_FACTOR_ENABLED { get; set; }
        public Nullable<bool> LOCKOUT_ENABLED { get; set; }
        public Nullable<System.DateTime> LOCKOUT_ENDDATE_UTC { get; set; }
        public Nullable<int> ACCESS_FAILED_COUNT { get; set; }
        public string NOTE { get; set; }
    
        public virtual ICollection<USER_ROLE> USER_ROLE { get; set; }
        public virtual USER_TYPE USER_TYPE { get; set; }
        public virtual ICollection<USER_DETAIL> USER_DETAIL { get; set; }
        public virtual ICollection<USER_CLAIM> USER_CLAIM { get; set; }
        public virtual ICollection<USER_LOGIN> USER_LOGIN { get; set; }
    }
}
