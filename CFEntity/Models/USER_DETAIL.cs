using System;
using System.Collections.Generic;

namespace CFEntity.Models
{
    public partial class USER_DETAIL
    {
        public int ID_USER { get; set; }
        public short ID_USERTYPE { get; set; }
        public string FIRSTNAME { get; set; }
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual USER_TYPE USER_TYPE { get; set; }
    }
}
