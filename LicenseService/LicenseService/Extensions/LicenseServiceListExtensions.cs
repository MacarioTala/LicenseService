using System.Collections.Generic;
using System.Linq;

namespace LicenseService.Extensions
{
    public static class LicenseServiceListExtensions
    {
        public static void AddIfNotNull<TListType>(this List<TListType> listToModify, TListType listItem) where TListType : class
        {
            if (listItem != null)
            { listToModify.Add(listItem); }
        }

        public static List<string> GetLicenseKeysFromLicenseList(this List<License> licenses)
        {
            return licenses.Select(license => license.LicenseKey).ToList();
        }
    }
}
