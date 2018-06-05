using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using DBDataReader = System.Data.SqlClient.SqlDataReader;
using System.IO;
using ToolsCSharp;

namespace Lab_6_PropsClasses
{
    public class ProductProps : IBaseProps
    {
        #region Instance Variables

        /// <summary>
        /// 
        /// </summary>
        public int productID;

        /// <summary>
        /// 
        /// </summary>
        public string productCode = "";

        /// <summary>
        /// 
        /// </summary>
        public string description = "";

        /// <summary>
        /// 
        /// </summary>
        public decimal price;

        public int onHandQuantity;

        /// <summary>
        /// ConcurrencyID. See main docs, don't manipulate directly
        /// </summary>
        public int ConcurrencyID = 0;

        #endregion instance Variables

        #region Methods

        /// <summary>
        /// Serializes this props object to XML, and writes the key-value pairs to a string.
        /// </summary>
        /// <returns>String containing key-value pairs</returns>	
        public string GetState()
        {
            XmlSerializer serializer = new XmlSerializer(this.GetType());
            StringWriter writer = new StringWriter();
            serializer.Serialize(writer, this);
            return writer.GetStringBuilder().ToString();
        }

        // I don't always want to generate xml in the db class so the 
        // props class can read in from xml
        public void SetState(DBDataReader dr)
        {
            this.productID = (int)dr["ProductID"];
            this.productCode = ((string)dr["ProductCode"]).Trim();
            this.description = (string)dr["Description"];
            this.price = (decimal)dr["UnitPrice"];
            this.onHandQuantity = (int)dr["OnHandQuantity"];
            this.ConcurrencyID = (int)dr["ConcurrencyID"];
        }

        /// <summary>
        /// 
        /// </summary>
        public void SetState(string xml)
        {
            XmlSerializer serializer = new XmlSerializer(this.GetType());
            StringReader reader = new StringReader(xml);
            ProductProps p = (ProductProps)serializer.Deserialize(reader);
            this.productID = p.productID;
            this.productCode = p.productCode;
            this.description = p.description;
            this.price = p.price;
            this.onHandQuantity = p.onHandQuantity;
            this.ConcurrencyID = p.ConcurrencyID;
        }

        /// <summary>
        /// Clones this object.
        /// </summary>
        /// <returns>A clone of this object.</returns>
        public Object Clone()
        {
            ProductProps p = new ProductProps();
            p.productID = this.productID;
            p.productCode = this.productCode;
            p.description = this.description;
            p.price = this.price;
            p.onHandQuantity = this.onHandQuantity;
            p.ConcurrencyID = this.ConcurrencyID;
            return p;
        }

        #endregion Methods
    }
}
