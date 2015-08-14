using System;
using System.Collections.Generic;
using System.Linq;
using LicenseService;
using LicenseService.Entities;
using LicenseService.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LicenseServiceUnitTests
{
    [TestClass]
    public class NavOrderExtensionTests
    {
        [TestMethod]
        public void IfNavOrdersContainEmptyLicenseKeyReturnMessage()
        {
            var x = new NavOrder();
            x.ProductOrders.Add(new ProductOrder{ExpirationDate = DateTime.Now});
            var expected = ErrorGuids.LicenseKeyIsEmptyGuid;
            var query = x.AreThereEmptyLicenseKeys();

            var actual = (from a in query
                where a.Code == expected
                select a);

            Assert.IsTrue(actual.Any());
        }

        [TestMethod]
        public void IfNavOrderContainsEmptyProductOrdersReturnMessage()
        {
            var x = new NavOrder();
            var expected = ErrorGuids.NoProductsInOrderGuid;

            var actual = x.IsProductOrdersEmpty().Code;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void IfLicenseKeyInOrderExistsInPassedArrayReturnMessage()
        {
            var x = new NavOrder();
            const string testLicenseKey = "testLicenseKey";
            x.ProductOrders.Add(new ProductOrder{LicenseKey = testLicenseKey});
            var expected = ErrorGuids.LicenseKeyAlreadyAssigned;
            var existingLicenseKeys = new List<string> {testLicenseKey};

            var query = x.DoesProductListContainLicenseKey(existingLicenseKeys);
            var actual = (from a in query
                          where a.Code == expected
                          select a);
            Assert.IsTrue(actual.Any());
        }

        [TestMethod]
        public void IfLicenseKeyInOrderNotExistsInPassedArrayReturnMessage()
        {
            var x = new NavOrder();
            const string testLicenseKey = "testLicenseKey";
            const string nonPresentLicenseKey = "ThisKeyIsn'tIn";
            var expected = ErrorGuids.LicenseKeyDoesntExist;
            var keysToPassIn = new List<string>() {nonPresentLicenseKey};
            x.ProductOrders.Add(new ProductOrder { LicenseKey = testLicenseKey });
            var query = x.DoesLicenseKeyExistForUpdate(keysToPassIn);
            var actual = (from a in query
                          where a.Code == expected
                          select a);
            Assert.IsTrue(actual.Any());
        }
    }
}
