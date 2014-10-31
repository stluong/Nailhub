using System;
using System.Collections.Generic;
using Generic.Core.Model;


namespace CFEntity.Models
{
    public partial class AspNetUser: BaseEntity
    {
        public AspNetUser()
        {
            this.AspNetUserClaims = new List<AspNetUserClaim>();
            this.AspNetUserLogins = new List<AspNetUserLogin>();
            this.SITEs = new List<SITE>();
            this.USER_DETAIL = new List<USER_DETAIL>();
            this.AspNetRoles = new List<AspNetRole>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public Nullable<System.DateTime> LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string UserName { get; set; }
        public virtual ICollection<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual ICollection<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual ICollection<SITE> SITEs { get; set; }
        public virtual ICollection<USER_DETAIL> USER_DETAIL { get; set; }
        public virtual ICollection<AspNetRole> AspNetRoles { get; set; }
    }
}
