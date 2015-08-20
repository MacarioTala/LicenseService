using System.Collections.Generic;

namespace LicenseService.Extensions
{
    public static class LicenseServiceListExtensions
    {
        public static void AddIfNotNull<TListType>(this List<TListType> listToModify, TListType listItem) where TListType : class
        {
            if (listItem != null)
            { listToModify.Add(listItem); }
        }
    }
}
