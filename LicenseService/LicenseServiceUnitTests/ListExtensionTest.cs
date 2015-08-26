using System.Collections.Generic;
using LicenseService;
using LicenseService.Entities;
using LicenseService.Extensions;
using NUnit.Framework;

namespace LicenseServiceUnitTests
{
    [TestFixture]
    public class ListExtensionTest
    {
        private class TypeUnderTest
        {
            public string Name;
            public string Address;
        }
        [Test]
        public void IfItemIsNotNullAddItToTheList()
        {
            var itemUnderTest = new TypeUnderTest{Name="Name", Address= "Address"};
            var listUnderTest = new List<TypeUnderTest>();

            listUnderTest.AddIfNotNull(itemUnderTest);

            Assert.IsTrue(listUnderTest.Contains(itemUnderTest));
        }

        [Test]
        public void IfItemIsNullDoNotAddItToTheList()
        {
            TypeUnderTest itemUnderTest = null;
            var listUnderTest = new List<TypeUnderTest>();

            listUnderTest.AddIfNotNull(itemUnderTest);

            Assert.IsFalse(listUnderTest.Contains(itemUnderTest));
        }

        [Test]
        public void GetLicenseKeysFromLicenseListReturnsListOfLicenseKeys()
        {
            const string existingLicenseKey = "existingLicenseKey";
            const string nextLicenseKey = "nextLicenseKey";
            const string lastLicenseKey = "lastLicenseKey";

            var expected = new List<string> {existingLicenseKey, nextLicenseKey, lastLicenseKey};

            var listUnderTest = new List<License>
            {
                new License{LicenseKey = existingLicenseKey},
                new License{LicenseKey = nextLicenseKey},
                new License{LicenseKey = lastLicenseKey},
            };

            var actual = listUnderTest.GetLicenseKeysFromLicenseList();

            Assert.AreEqual(expected,actual);
        }
    }

   
}
