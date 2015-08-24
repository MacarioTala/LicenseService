using System;
using System.Collections.Generic;

namespace LicenseService
{
    public class UserData//Was:UserDataEntity
    {
        public virtual Guid UserId { get; set; }

        public virtual bool? Active { get; set; }
        public virtual DateTime? DateCreated { get; set; }
        public virtual String Email { get; set; }
        public virtual bool? EulaAccepted { get; set; }
        public virtual String FirstName { get; set; }
        public virtual Guid? InternalLoginId { get; set; }
        public virtual Boolean InternalUser { get; set; }
        public virtual Boolean IsSingleUser { get; set; }
        public virtual String LastName { get; set; }
        public virtual DateTime? LastRequestedAt { get; set; }
        public virtual Guid? LocationId { get; set; }
        public virtual DateTime? ModifiedDateTime { get; set; }
        public virtual String Phone { get; set; }
        public virtual bool? SecurityAdministrator { get; set; }
        public virtual bool? SharedUser { get; set; }
        public virtual String UserName { get; set; }
        public virtual Company Company { get; set; }
        public virtual Location Locations { get; set; }
        public virtual IList<License> Licenses { get; set; } //Note: This is new, no more licenseUser Entity mapping 

        public UserData()
        {
            // ReSharper disable DoNotCallOverridableMethodsInConstructor - NHibernate's one-to-many relationship mappings do not work unless we do this.
            Licenses = new List<License>();
            // ReSharper restore DoNotCallOverridableMethodsInConstructor
        }
    }
}