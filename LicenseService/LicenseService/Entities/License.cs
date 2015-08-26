using System;
using System.Collections.Generic;

namespace LicenseService.Entities
{
    public class License
    {
        public virtual Int32 LicenseId { get; set; }

        public virtual Boolean Active { get; set; }
        public virtual bool? EulaAccepted { get; set; }
        public virtual Boolean Internal { get; set; }
        public virtual String LicenseKey { get; set; }
        public virtual int? LineNumber { get; set; }
        public virtual DateTime? ModifiedDateTime { get; set; }
        public virtual Int32 NumberOfUsers { get; set; }// TODO: Bug. This is the number of users allowed for this license, currently using it as the number of users ASSIGNED to the license
        public virtual String OrderNumber { get; set; }
        public virtual Boolean Used { get; set; }
        public virtual Company Company { get; set; }
        public virtual IList<UserData> Users { get; set; }
        public virtual Product Product { get; set; } 
        public virtual IList<Feature> Features { get; set; }

        
        //Changed Expiration date to validFromDate and validToDate
        public virtual DateTime ValidFromDateTime { get; set; }
        public virtual DateTime ValidToDateTime { get; set; }
            
        public License()
        {
            // ReSharper disable DoNotCallOverridableMethodsInConstructor - NHibernate's one-to-many relationship mappings do not work unless we do this.
            Features = new List<Feature>();
            Users = new List<UserData>();
            // ReSharper restore DoNotCallOverridableMethodsInConstructor
        }

        public LicensePackage GetLicensePackageFromLicense()
        {
            var returnPackage = new LicensePackage
            {
                LicenseKey = LicenseKey,
                ProductName = Product.ProductName,
                Url = string.Format(Properties.Settings.Default.LicenseActivationLink, LicenseKey)
            };
            return returnPackage;
        }

    }
}
