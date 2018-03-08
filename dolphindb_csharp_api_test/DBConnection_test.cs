using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using com.xxdb;

namespace dolphindb_csharp_api_test
{
    [TestClass]
    public class DBConnection_test
    {
        [TestMethod]
        public void Test_Connect()
        {
            DBConnection db = new DBConnection();
            Assert.AreEqual(true,db.connect("localhost", 8900));
        }
    }
}
