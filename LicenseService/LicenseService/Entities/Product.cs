using System;
using System.Collections.Generic;

namespace LicenseService.Entities
{
    public class Product
    {
        public virtual Int32 ProductId { get; protected set; }

        public virtual String Description { get; set; }
        public virtual Int32 DistributionType { get; set; }
        public virtual Boolean IsSingleUser { get; set; }
        public virtual Int32 LicenseMode { get; set; }
        public virtual DateTime? ModifiedDateTime { get; set; }
        public virtual Int32 NumberOfMonths { get; set; }
        public virtual String ProductName { get; set; }

        public virtual IList<License> Licenses { get; set; }

        public Product()
        {
            Licenses = new List<License>();
        }
     }
}
