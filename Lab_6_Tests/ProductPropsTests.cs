using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lab_6_PropsClasses;
using DBDataReader = System.Data.SqlClient.SqlDataReader;
using NUnit.Framework;

namespace Lab_6_Tests
{
    [TestFixture]
    public class ProductPropsTests
    {
        ProductProps p;

        [SetUp]
        public void SetUp()
        {
            p = new ProductProps();
            p.productID = 100;
            p.productCode = "xxx";
            p.description = "my test product";
            p.onHandQuantity = 2;
            p.price = 99;
        }

        [Test]
        public void TestGetState()
        {
            string xml = p.GetState();
            Assert.Greater(xml.Length, 0);
            Assert.IsTrue(xml.Contains(p.description));
            Console.WriteLine(xml);
        }

        [Test]
        public void TestSetStateXML()
        {
            ProductProps newP = new ProductProps();
            string xml = p.GetState();
            newP.SetState(xml);
            Assert.AreEqual(newP.productID, p.productID);
            Assert.AreEqual(newP.description, p.description);
        }

        [Test]
        public void TestClone()
        {
            ProductProps newP = (ProductProps)p.Clone();
            Assert.IsTrue(p.productCode == newP.productCode);
        }
    }
}
