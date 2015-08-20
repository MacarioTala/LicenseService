using System;
using System.Collections.Generic;
using System.Linq;
using LicenseService;
using LicenseService.Entities;
using LicenseService.Extensions;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace LicenseServiceUnitTests
{
    [TestFixture]
    public class NavOrderExtensionTests
    {
        [Test]
        public void IfNavOrdersContainEmptyLicenseKeyReturnMessage()
        {
            var x = new NavOrder();
            x.ProductOrders.Add(new ProductOrder{ExpirationDate = DateTime.Now});
            var expected = ErrorGuids.LicenseKeyIsEmptyGuid;
            var query = x.AreThereEmptyLicenseKeys();

            var actual = IsExpectedErrorGuidInQueryResults(query, expected);

            Assert.IsTrue(actual.Any());
        }

        private static IEnumerable<LicenseMessage> IsExpectedErrorGuidInQueryResults(IEnumerable<LicenseMessage> query, Guid expected)
        {
            var actual = (from a in query
                where a.Code == expected
                select a);
            return actual;
        }

        [Test]
        public void IfNavOrderContainsEmptyProductOrdersReturnMessage()
        {
            var x = new NavOrder();
            var expected = ErrorGuids.NoProductsInOrderGuid;

            var actual = x.IsProductOrdersEmpty().Code;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void IfLicenseKeyInOrderExistsInPassedArrayReturnMessage()
        {
            var x = new NavOrder();
            const string testLicenseKey = "testLicenseKey";
            x.ProductOrders.Add(new ProductOrder{LicenseKey = testLicenseKey});
            var expected = ErrorGuids.LicenseKeyAlreadyAssigned;
            var existingLicenseKeys = new List<string> {testLicenseKey};

            var query = x.DoesProductListContainLicenseKey(existingLicenseKeys);
            var actual = IsExpectedErrorGuidInQueryResults(query, expected);

            Assert.IsTrue(actual.Any());
        }

        [Test]
        public void IfLicenseKeyInOrderNotExistsInPassedArrayReturnMessage()
        {
            var x = new NavOrder();
            const string testLicenseKey = "ExistingLicense";
            const string nonPresentLicenseKey = "NonExistingLicense";
            var expected = ErrorGuids.LicenseKeyDoesntExist;
            var keysToPassIn = new List<string>() {nonPresentLicenseKey};
            x.ProductOrders.Add(new ProductOrder { LicenseKey = testLicenseKey });
            var query = x.DoesLicenseKeyExistForUpdate(keysToPassIn);
            var actual = IsExpectedErrorGuidInQueryResults(query, expected);

            Assert.IsTrue(actual.Any());
        }

        [Test]
        public void IfNumberOfUsersExceeds5000ReturnMessage()
        {
            var navOrderUnderTest = new NavOrder();
            const int usersToPassToOrder = 5001;
            navOrderUnderTest.ProductOrders.Add(new ProductOrder{NumberOfUsers = usersToPassToOrder});
            var expected = ErrorGuids.NumberOfUsersIsInvalid;

            var query = navOrderUnderTest.DoesNumberOfUsersExceed5000();

            var actual = IsExpectedErrorGuidInQueryResults(query, expected);

            Assert.IsTrue(actual.Any());
        }

        [Test]
        public void IfNumberOfUsersDoesNotExceed5000ReturnNull()
        {
            var navOrderUnderTest = new NavOrder();
            const int usersToPassToOrder = 5000;
            navOrderUnderTest.ProductOrders.Add(new ProductOrder { NumberOfUsers = usersToPassToOrder });
            var unexpectedErrorGuid = ErrorGuids.NumberOfUsersIsInvalid;

            var query = navOrderUnderTest.DoesNumberOfUsersExceed5000();

            var actual = IsExpectedErrorGuidInQueryResults(query, unexpectedErrorGuid);
            
            Assert.IsFalse(actual.Any());
        }

        [Test]
        public void CheckParametersDoesntThrowExceptionWhenOrderNumberIsMissingAndLaterChecksAreRun()
        {
            var navOrderUnderTest = new NavOrder {Action = 5};

            navOrderUnderTest.CheckParameters();
        }

        [Test]
        public void CheckParametersChecksIfProductOrdersIsEmptyEvenIfPreviousConditionsAreNotMet()
        {
            var navOrderUnderTest = new NavOrder();
            var expected = ErrorGuids.NoProductsInOrderGuid;
            
            var query= navOrderUnderTest.CheckParameters();

            var actual = IsExpectedErrorGuidInQueryResults(query, expected);
            Assert.IsTrue(actual.Any());
        }

        [Test]
        public void ValidateOrdersFlagsMissingLicenseKeys()
        {
            var navOrderUnderTest = new NavOrder();
            var expected = ErrorGuids.LicenseKeyIsEmptyGuid;
            navOrderUnderTest.ProductOrders.Add(new ProductOrder{ExpirationDate = DateTime.Now});

            var existingLicenses = new List<string>();
            var query = navOrderUnderTest.ValidateOrdersForNewLicense(existingLicenses);

            var actual = IsExpectedErrorGuidInQueryResults(query, expected);
            Assert.IsTrue(actual.Any());

        }


        [Test]
        public void ValidateOrdersForNewLicenseFlagsMissingLicenseKeysAndAlreadyExistingOnes()
        {
            var navOrderUnderTest = new NavOrder();
            const string existingLicenseName = "ExistingLicense";
            var expectedError1 = ErrorGuids.LicenseKeyIsEmptyGuid;
            var expectedError2 = ErrorGuids.LicenseKeyAlreadyAssigned;
            navOrderUnderTest.ProductOrders.Add(new ProductOrder { ExpirationDate = DateTime.Now });
            navOrderUnderTest.ProductOrders.Add(new ProductOrder{ExpirationDate = DateTime.Now,LicenseKey =existingLicenseName});
            var existingLicenses = new List<string> {existingLicenseName};

            var query = navOrderUnderTest.ValidateOrdersForNewLicense(existingLicenses);

            var actualFirstCondition = IsExpectedErrorGuidInQueryResults(query, expectedError1);
            var actualSecondCondition = IsExpectedErrorGuidInQueryResults(query, expectedError2);
            Assert.IsTrue(actualFirstCondition.Any());
            Assert.IsTrue(actualSecondCondition.Any());
        }

        [Test]
        public void DoExistingUsersExceedUsersInOrderReturnsErrorIfAssignedUsersIsGreaterThanUsersInOrder ()
        {
            var navOrderUnderTest = new NavOrder();
            const string existingLicenseKey = "ExistingLicenseKey";
            navOrderUnderTest.ProductOrders.Add(new ProductOrder { ExpirationDate = DateTime.Now, LicenseKey = existingLicenseKey,NumberOfUsers = 10});   
            var existingLicenseDictionary = new Dictionary<string, int> {{existingLicenseKey, 111}};
            var expected = ErrorGuids.NumberOfAssignedUsersExceedsLicensedUsersInOrderGuid;

            var query=navOrderUnderTest.DoExistingUsersExceedUsersInOrder(existingLicenseDictionary);

            var actual = IsExpectedErrorGuidInQueryResults(query, expected);

            Assert.IsTrue(actual.Any());
        }

    }
}
