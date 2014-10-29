using System;
using System.Collections.Generic;

namespace CFEntity.Models
{
    public partial class AspNetRole
    {
        public AspNetRole()
        {
            this.AspNetUsers = new List<AspNetUser>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<AspNetUser> AspNetUsers { get; set; }
    }
}
