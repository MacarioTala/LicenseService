using System;
using System.Collections.Generic;
using LicenseService.Entities;

namespace LicenseService
{
    public class Company
    {
        public virtual Guid CompanyId { get; set; }

        public virtual String CompanyName { get; set; }
        public virtual Int32 CompanyNumber { get; set; }
        public virtual String DataFolderName { get; set; }
        public virtual DateTime? DateCreated { get; set; }
        public virtual String DefaultApplicationUrl { get; set; }
        public virtual bool? IsSecurity { get; set; }
        public virtual DateTime? LicensingGracePeriodExpiration { get; set; }
        public virtual DateTime? ModifiedDateTime { get; set; }

        public virtual IList<License> Licenses { get; set; }
        public virtual IList<Location> Locations { get; set; }
        public virtual IList<UserData> UserData { get; set; }

        public Company()
        {
            // ReSharper disable DoNotCallOverridableMethodsInConstructor - NHibernate's one-to-many relationship mappings do not work unless we do this.
            CompanyId = Guid.Empty;

            Licenses = new List<License>();
            Locations = new List<Location>();
            UserData = new List<UserData>();
            // ReSharper restore DoNotCallOverridableMethodsInConstructor
        }
    }

    public class Location
    {
        Location() { throw new NotImplementedException();}
    }
}