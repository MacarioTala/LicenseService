using System;
using System.Collections.Generic;
using LicenseService.Entities;
using LicenseService.Enums;

namespace LicenseService
{
    public class License
    {
        public virtual Int32 LicenseId { get; set; }

        public virtual Boolean Active { get; set; }
        public virtual bool? EulaAccepted { get; set; }
        public virtual DateTime ExpirationDate { get; set; }
        public virtual Boolean Internal { get; set; }
        public virtual String LicenseKey { get; set; }
        public virtual int? LineNumber { get; set; }
        public virtual DateTime? ModifiedDateTime { get; set; }
        public virtual Int32 NumberOfUsers { get; set; }
        public virtual String OrderNumber { get; set; }
        public virtual Boolean Used { get; set; }
        public virtual Company Company { get; set; }
        public virtual LicenseModeEnum LicenseMode { get; set; }
        
        public virtual IList<Feature> LicenseModules { get; set; }
        
            
        public License()
        {
            LicenseModules = new List<Feature>();
        }
    }
}
