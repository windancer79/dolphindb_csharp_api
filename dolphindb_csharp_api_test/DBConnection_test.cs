using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using dolphindb;
using dolphindb.data;
using System.IO;
using System.Net.Sockets;
using dolphindb.io;

namespace dolphindb_csharp_api_test
{
    [TestClass]
    public class DBConnection_test
    {
        [TestMethod]
        public void Test_MyDemo()
        {

            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect("localhost", 8900);
            StreamWriter @out = new StreamWriter(new NetworkStream(socket));

            StreamReader @in = new StreamReader(new NetworkStream(socket));
            string body = "connect\n";
            @out.Write("API 0 ");
            @out.Write(body.Length.ToString());
            @out.Write('\n');
            @out.Write(body);
            @out.Flush();

            string line = @in.ReadLine();
            int endPos = line.IndexOf(' ');
            string sessionID = line.Substring(0, endPos);

            string script = "129";
            body = "script\n" + script;
            string header = null;
            try
            {
                @out.Write("API2 " + sessionID + " ");
                @out.Write(AbstractExtendedDataOutputStream.getUTFlength(body, 0, 0).ToString());
                @out.Write('\n');
                @out.Write(body);
                @out.Flush();

                header = @in.ReadLine();

                string msg1 = @in.ReadLine();
                string msg2 = @in.ReadLine();
                string msg3 = @in.ReadLine();
            }
            catch
            {
                throw;
            }
        }

        [TestMethod]
        public void Test_Connect()
        {
            DBConnection db = new DBConnection();
            Assert.AreEqual(true,db.connect("localhost", 8900));
        }

        [TestMethod]
        public void Test_Connect_demo()
        {
            DBConnection db = new DBConnection();
            Console.Out.WriteLine(db.connect());
        }

        [TestMethod]
        public void Test_run_return_scalar_int()
        {
            DBConnection db = new DBConnection();
            db.connect("localhost", 8900);
            Assert.AreEqual(63, ((BasicInt)db.run("63")).getInt());
            Assert.AreEqual(129, ((BasicInt)db.run("129")).getInt());
            Assert.AreEqual(255, ((BasicInt)db.run("255")).getInt());
            Assert.AreEqual(1023, ((BasicInt)db.run("1023")).getInt());
            Assert.AreEqual(2047, ((BasicInt)db.run("2047")).getInt());
        }

        [TestMethod]
        public void Test_run_return_scalar_long()
        {
            DBConnection db = new DBConnection();
            db.connect("localhost", 8900);
            BasicLong re = (BasicLong)db.run("1l");
            Assert.AreEqual(1, re.getLong());
        }

        [TestMethod]
        public void Test_run_return_scalar_double()
        {
            DBConnection db = new DBConnection();
            db.connect("localhost", 8900);
            BasicInt re = (BasicInt)db.run("1.0+2.0");
            Assert.AreEqual(3, re.getInt());
        }

        [TestMethod]
        public void Test_run_return_scalar_float()
        {
            DBConnection db = new DBConnection();
            db.connect("localhost", 8900);
            BasicInt re = (BasicInt)db.run("1.0f+2.0f");
            Assert.AreEqual(3, re.getInt());
        }
    }
}
