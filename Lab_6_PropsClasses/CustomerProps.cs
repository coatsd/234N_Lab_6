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
    public class CustomerProps : IBaseProps
    {
        #region Instance Variables

        /// <summary>
        /// 
        /// </summary>
        public int customerID;

        /// <summary>
        /// 
        /// </summary>
        public string name = "";

        /// <summary>
        /// 
        /// </summary>
        public string address = "";

        /// <summary>
        /// 
        /// </summary>
        public string city = "";

        public string state = "";

        public string zipCode = "";

        /// <summary>
        /// ConcurrencyID. See main docs, don't manipulate directly
        /// </summary>
        public int concurrencyID = 0;

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
            this.customerID = (int)dr["CustomerID"];
            this.name = ((string)dr["Name"]).Trim();
            this.address = ((string)dr["Address"]).Trim();
            this.city = ((string)dr["City"]).Trim();
            this.state = (string)dr["State"];
            this.zipCode = ((string)dr["ZipCode"]).Trim();
            this.concurrencyID = (int)dr["ConcurrencyID"];
        }

        /// <summary>
        /// 
        /// </summary>
        public void SetState(string xml)
        {
            XmlSerializer serializer = new XmlSerializer(this.GetType());
            StringReader reader = new StringReader(xml);
            CustomerProps c = (CustomerProps)serializer.Deserialize(reader);
            this.customerID = c.customerID;
            this.name = c.name;
            this.address = c.address;
            this.city = c.city;
            this.state = c.state;
            this.concurrencyID = c.concurrencyID;
        }

        /// <summary>
        /// Clones this object.
        /// </summary>
        /// <returns>A clone of this object.</returns>
        public Object Clone()
        {
            CustomerProps c = new CustomerProps();
            c.customerID = this.customerID;
            c.name = this.name;
            c.address = this.address;
            c.city = this.city;
            c.state = this.state;
            c.zipCode = this.zipCode;
            c.concurrencyID = this.concurrencyID;
            return c;
        }

        #endregion Methods
    }
}
