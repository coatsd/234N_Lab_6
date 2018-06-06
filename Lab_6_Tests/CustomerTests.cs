using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lab6Classes;
using Lab_6_PropsClasses;
using Lab_6_DBClasses;
using DBDataReader = System.Data.SqlClient.SqlDataReader;
using DBConnection = System.Data.SqlClient.SqlConnection;
using DBCommand = System.Data.SqlClient.SqlCommand;
using DBParameter = System.Data.SqlClient.SqlParameter;
using DBDataAdapter = System.Data.SqlClient.SqlDataAdapter;
using NUnit.Framework;

namespace Lab_6_Tests
{
    [TestFixture]
    class CustomerTests
    {
        Customer c = new Customer(dataSource);
        private static string dataSource = "Data Source=1912843-C20243;Initial Catalog=MMABooksUpdated;Integrated Security=True";
        static DBConnection connection = new DBConnection(dataSource);
        static CustomerDB cdb = new CustomerDB(connection);
        static int test = 0;

        [SetUp]
        public void SetUpTests()
        {

        }

        [Test]
        public void TestLoad()
        {
            Customer cust = new Customer(dataSource);
            cust.Load(1);
            Assert.IsTrue(cust.Name.Trim() == "Molunguri, A");
        }

        [Test, Order(1)]
        public void TestSave()
        {
            c.Name = "XYZ";
            c.Address = "100 street";
            c.City = "Blehtown";
            c.State = "NJ";
            c.ZipCode = "11111";
            c.Save();
        }

        [Test, Order(2)]
        public void TestUpdate()
        {
            List<CustomerProps> customers = (List<CustomerProps>)cdb.RetrieveAll(test.GetType());
            c = new Customer(customers[customers.Count - 1].customerID, dataSource);
            c.ZipCode = "22222";
            try
            {
                c.Save();
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.IsTrue(false);
            }
        }

        /*
        This Test is for undo changes, but I might not understand how it works.
        
        [Test, Order(3)]
        public void TestUndo()
        {
            List<ProductProps> products = (List<ProductProps>)pdb.RetrieveAll(test.GetType());
            Product oldP = new Product(products[products.Count - 1].productID, dataSource);

            p.UndoChanges();
            p.Save();
            Assert.IsTrue(p.Price != oldP.Price);
        }
        */

        [Test, Order(3)]
        public void TestDelete()
        {
            c.Delete();
            try
            {
                c.Save();
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.IsTrue(false);
            }
        }
    }
}
