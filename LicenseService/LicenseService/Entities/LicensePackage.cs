using System;

namespace LicenseService.Entities
{
    /// <summary>
    /// This is how we return to the user their license keys and which products they correspond to.
    /// This is an unmapped class.
    /// </summary>
    public class LicensePackage
    {
        public string LicenseKey { get; set; }
        public string ProductName { get; set; }
        public string Url { get; set; }//todo 20150825: this should be a url

        public string[] ConvertToMessageValueArray()
        {
            return new string[] {LicenseKey, ProductName, Url};
        }
    }
}
