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
    class CustomerPropsTests
    {
        CustomerProps c;

        [SetUp]
        public void SetUp()
        {
            c = new CustomerProps();
            c.customerID = 100;
            c.name = "Mike";
            c.address = "my test product";
            c.city = "Orlando";
            c.state = "FL";
            c.zipCode = "11111";
        }

        [Test]
        public void TestGetState()
        {
            string xml = c.GetState();
            Assert.Greater(xml.Length, 0);
            Assert.IsTrue(xml.Contains(c.name));
            Console.WriteLine(xml);
        }

        [Test]
        public void TestSetStateXML()
        {
            CustomerProps newC = new CustomerProps();
            string xml = c.GetState();
            newC.SetState(xml);
            Assert.AreEqual(newC.customerID, c.customerID);
            Assert.AreEqual(newC.name, c.name);
        }

        [Test]
        public void TestClone()
        {
            CustomerProps newC = (CustomerProps)c.Clone();
            Assert.IsTrue(c.name == newC.name);
        }
    }
}
