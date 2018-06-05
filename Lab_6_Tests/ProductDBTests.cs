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
    class ProductDBTests
    {
        private static string dataSource = "Data Source=1912843-C20243;Initial Catalog=MMABooksUpdated;Integrated Security=True";
        static DBConnection connection = new DBConnection(dataSource);
        ProductDB pdb = new ProductDB(connection);
        ProductProps p = new ProductProps();

        [SetUp]
        public void SetUpTests()
        {
            p.productCode = "XYZ1";
            p.description = "Test product1";
            p.price = 10.00m;
            p.onHandQuantity = 15;
        }

        [Test, Order(2)]
        public void TestRetrieve()
        {
            ProductProps createdp = (ProductProps)pdb.Retrieve(p.productID);
            Assert.AreEqual(createdp.productID, p.productID);
        }

        [Test]
        public void TestRetrieveAll()
        {
            int test = 0;
            List<ProductProps> products = (List<ProductProps>)pdb.RetrieveAll(test.GetType());
            Assert.IsTrue(products.Count > 1);
        }

        [Test, Order(1)]
        public void TestCreate()
        {
            pdb.Create(p);
            ProductProps createdP = (ProductProps)pdb.Retrieve(p.productID);
            Assert.AreEqual(createdP.productCode, p.productCode);
        }

        [Test, Order(4)]
        public void TestDelete()
        {
            bool isDeleted = pdb.Delete(p);
            Assert.IsTrue(isDeleted);
        }

        [Test, Order(3)]
        public void TestUpdate()
        {
            p.price = 20.00m;
            p.onHandQuantity = 5;
            pdb.Update(p);
            ProductProps updatedP = (ProductProps)pdb.Retrieve(p.productID);
            Assert.AreEqual(updatedP.price, p.price);
            Assert.AreEqual(updatedP.onHandQuantity, p.onHandQuantity);
        }
    }
}
