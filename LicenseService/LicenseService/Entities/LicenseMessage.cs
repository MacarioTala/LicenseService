using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseService
{
    public class LicenseMessage
    {
        /// <summary>
        /// Severity 1 = Error 2 = Warning 3 = Info
        /// </summary>
        public int Severity { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// This is where we put the license keys and what products they belong to.
        /// The format is:
        /// string LicenseKey
        /// string ProductName
        /// string URL
        /// TODO: 20150825: When we have leeway over changing Nav, we should change this to the LicensePackage class
        /// and remove the extension method there that changes things into a string array.
        /// </summary>
        public string[] MessageValues { get; set; }

        /// <summary>
        /// Unique Code for Error
        /// </summary>
        public Guid Code { get; set; }
    }
}
