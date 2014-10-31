using System;
using System.Collections.Generic;

namespace CFEntity.Models
{
    public partial class EMPLOYEE
    {
        public string ID_EMPLYEE { get; set; }
        public Nullable<short> ID_TITLE { get; set; }
        public string NAME { get; set; }
        public string DESCRIPTION { get; set; }
        public virtual TITLE TITLE { get; set; }
    }
}
