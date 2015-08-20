using System.Collections.Generic;
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
    }

   
}
