using System;

namespace LicenseService
{
    public static class ErrorGuids
    {
        public static Guid OrderNumberMissingGuid = new Guid("{1227536E-532E-4839-BB7F-78D3D9EFB59D}");
        public static Guid OrderNumberIsTooLongGuid = new Guid("{091AAD11-BD6F-4E8A-9537-286D9731AD49}");
        public static Guid InvalidActionGuid = new Guid("{5F22A145-EA5F-4E8A-B81B-214A1CA92469}");
        public static Guid NoProductsInOrderGuid = new Guid("{DE78F76D-69F0-4A36-90A6-04E7E8C73D39}");
        public static Guid LicenseKeyIsEmptyGuid = new Guid("{AA18FB18-884D-4E85-B927-616790D48DFA}");
        public static Guid LicenseKeyAlreadyAssigned = new Guid("{C6C39FF2-5F29-4F81-9E1C-962B523C0553}");
        public static Guid LicenseKeyDoesntExist = new Guid("{3F30D63A-5D6B-4D69-926B-88F173A3937A}");
        public static Guid NumberOfAssignedUsersExceedsLicensedUsersInOrderGuid = new Guid("{88DAC014-5B19-4A8D-AFB3-E1EDD691ACCF}");
        public static Guid OrderNumberDoesntMatchGuid = new Guid("{C9F1B833-8E93-421E-971F-EAA6D8859D5B}");
        public static Guid ProductDoesNotExistGuid = new Guid("{BE7E05C4-D410-45F5-953D-75F8AED0C548}");
        public static Guid InvalidExpireDateGuid = new Guid("{D285BD31-A1A6-4D9F-90AA-DB08538FC600}");
        public static Guid InvalidLicenseModeGuid = new Guid("{35E551B9-467C-4E43-AE9F-E53DF5B5196E}");
        public static Guid NumberOfUsersIsInvalid = new Guid("{8FDA843E-9E8B-4911-9251-5C1EE427687C}");
    }
}
