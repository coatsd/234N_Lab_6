using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

using Lab_6_DBClasses;
using Lab6Classes;
using Lab_6_PropsClasses;

using DBConnection = System.Data.SqlClient.SqlConnection;
using DBCommand = System.Data.SqlClient.SqlCommand;
using DBParameter = System.Data.SqlClient.SqlParameter;
using DBDataReader = System.Data.SqlClient.SqlDataReader;
using DBDataAdapter = System.Data.SqlClient.SqlDataAdapter;

namespace Lab_6_Tests
{
    [TestFixture]
    class CustomerDBTests
    {
        private static string dataSource = "Data Source=1912843-C20243;Initial Catalog=MMABooksUpdated;Integrated Security=True";
        static DBConnection connection = new DBConnection(dataSource);
        CustomerDB cdb = new CustomerDB(connection);
        CustomerProps c = new CustomerProps();

        [SetUp]
        public void SetUpTests()
        {
            c.name = "Cody";
            c.address = "100 Flarp Drive";
            c.city = "Gretchen";
            c.state = "MA";
            c.zipCode = "11111";
        }

        [Test, Order(2)]
        public void TestRetrieve()
        {
            CustomerProps createdc = (CustomerProps)cdb.Retrieve(c.customerID);
            Assert.AreEqual(createdc.customerID, c.customerID);
        }

        [Test]
        public void TestRetrieveAll()
        {
            int test = 0;
            List<CustomerProps> customers = (List<CustomerProps>)cdb.RetrieveAll(test.GetType());
            Assert.IsTrue(customers.Count > 1);
        }

        [Test, Order(1)]
        public void TestCreate()
        {
            cdb.Create(c);
            CustomerProps createdC = (CustomerProps)cdb.Retrieve(c.customerID);
            Assert.AreEqual(createdC.name, c.name);
        }

        [Test, Order(4)]
        public void TestDelete()
        {
            bool isDeleted = cdb.Delete(c);
            Assert.IsTrue(isDeleted);
        }

        [Test, Order(3)]
        public void TestUpdate()
        {
            c.address = "101 Meh Street";
            c.city = "Gulag";
            c.zipCode = "12121";
            cdb.Update(c);
            CustomerProps updatedC = (CustomerProps)cdb.Retrieve(c.customerID);
            Assert.AreEqual(updatedC.address, c.address);
            Assert.AreEqual(updatedC.city, c.city);
            Assert.AreEqual(updatedC.zipCode, c.zipCode);
        }
    }
}
