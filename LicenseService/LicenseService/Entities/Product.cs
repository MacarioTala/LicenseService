using System;
using System.Collections.Generic;

namespace LicenseService
{
    public class Product
    {
        public virtual Int32 ProductId { get; protected set; }

        public virtual String Description { get; protected set; }
        public virtual Int32 DistributionType { get; protected set; }
        public virtual Boolean IsSingleUser { get; protected set; }
        public virtual Int32 LicenseMode { get; protected set; }
        public virtual DateTime? ModifiedDateTime { get; protected set; }
        public virtual Int32 NumberOfMonths { get; protected set; }
        public virtual String ProductName { get; protected set; }
     }
}
