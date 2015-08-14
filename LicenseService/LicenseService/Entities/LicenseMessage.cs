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
        /// Values to be inserted into message
        /// </summary>
        public string[] MessageValues { get; set; }

        /// <summary>
        /// Unique Code for Error
        /// </summary>
        public Guid Code { get; set; }
    }
}
