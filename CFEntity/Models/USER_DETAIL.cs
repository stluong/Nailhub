using System;
using System.Collections.Generic;

namespace CFEntity.Models
{
    public partial class USER_DETAIL
    {
        public int userId { get; set; }
        public short UserTypeId { get; set; }
        public string FIRSTNAME { get; set; }
        public string LASTNAME { get; set; }
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual USER_TYPE USER_TYPE { get; set; }
    }
}
