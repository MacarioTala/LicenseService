using System;

namespace LicenseService
{
    public class ProductOrder
    {
        public string LicenseKey { get; set; }

        /// <summary>
        /// Exact match to DGIS tblProduct Table
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// License Expiration Date
        /// </summary>
        public DateTime ExpirationDate { get; set; }

        /// <summary>
        /// Number of Users (seats) or Concurrent
        /// </summary>
        public int NumberOfUsers { get; set; }

        /// <summary>
        /// 0 = Per Seat license, 1 = Concurrent License
        /// </summary>
        public int LicenseMode { get; set; }

    }
}
