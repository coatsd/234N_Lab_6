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
    class ProductTests
    {
        Product p = new Product(dataSource);
        private static string dataSource = "Data Source=1912843-C20243;Initial Catalog=MMABooksUpdated;Integrated Security=True";
        static DBConnection connection = new DBConnection(dataSource);
        static ProductDB pdb = new ProductDB(connection);
        static int test = 0;

        [SetUp]
        public void SetUpTests()
        {

        }

        [Test]
        public void TestLoad()
        {
            Product prod = new Product(dataSource);
            prod.Load(1);
            Assert.IsTrue(prod.ProductCode.Trim() == "A4CS");
        }

        [Test, Order(1)]
        public void TestSave()
        {
            p.ProductCode = "XYZ1";
            p.Description = "Test product 1";
            p.Price = 15.00m;
            p.OnHandQuantity = 20;
            try
            {
                p.Save();
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.IsTrue(false);
            }
        }

        [Test, Order(2)]
        public void TestUpdate()
        {
            List<ProductProps> products = (List<ProductProps>)pdb.RetrieveAll(test.GetType());
            p = new Product(products[products.Count - 1].productID, dataSource);
            p.Price = 20.00m;
            try
            {
                p.Save();
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
            p.Delete();
            try
            {
                p.Save();
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.IsTrue(false);
            }
        }
    }
}
