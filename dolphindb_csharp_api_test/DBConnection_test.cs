using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using dolphindb;
using dolphindb.data;
using System.IO;
using System.Net.Sockets;
using dolphindb.io;
using System.Text;

namespace dolphindb_csharp_api_test
{
    [TestClass]
    public class DBConnection_test
    {
        [TestMethod]
        public void Test_MyDemo()
        {

            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            socket.Connect("192.168.1.61", 8702);
            StreamWriter @out = new StreamWriter(new NetworkStream(socket));

            StreamReader @in = new StreamReader(new NetworkStream(socket),Encoding.Default);
            string body = "connect\n";
            @out.Write("API 0 ");
            @out.Write(body.Length.ToString());
            @out.Write('\n');
            @out.Write(body);
            @out.Flush();

            string line = @in.ReadLine();
            int endPos = line.IndexOf(' ');
            string sessionID = line.Substring(0, endPos);

            BinaryReader binreader = new BinaryReader(new NetworkStream(socket), Encoding.Default);
            string script = "129";
            body = "script\n" + script;
            //string header = null;
            try
            {
                @out.Write("API2 " + sessionID + " ");
                @out.Write(AbstractExtendedDataOutputStream.getUTFlength(body, 0, 0).ToString());
                @out.Write('\n');
                @out.Write(body);
                @out.Flush();

                //header = @in.ReadLine();
                //string session = @in.ReadLine();
                //string msg = @in.ReadLine();
                //string type = @in.ReadLine();
                //string res = @in.ReadLine();
                //long len = @in.BaseStream.Length;

                byte[] result = binreader.ReadBytes(20);

                //int i = 0;
                //do
                //{
                //    if (i>23) break;
                //    int j = @in.BaseStream.Read(result, i, 1);
                //    i++;
                //    //Console.Out.WriteLine(i.ToString() + " : " + Convert.ToString((int)result[i]));
                //} while (true);

                Assert.AreEqual(129, (int)result[23]);
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
           // Console.Out.WriteLine(db.connect());
        }

        [TestMethod]
        public void Test_run_return_scalar_int()
        {
            DBConnection db = new DBConnection();
            db.connect("localhost", 8900);
            Assert.AreEqual(63, ((BasicInt)db.run("63")).getValue());
            Assert.AreEqual(129, ((BasicInt)db.run("129")).getValue());
            Assert.AreEqual(255, ((BasicInt)db.run("255")).getValue());
            Assert.AreEqual(1023, ((BasicInt)db.run("1023")).getValue());
            Assert.AreEqual(2047, ((BasicInt)db.run("2047")).getValue());
            Assert.AreEqual(-2047, ((BasicInt)db.run("-2047")).getValue());
            Assert.AreEqual(-129, ((BasicInt)db.run("-129")).getValue());
            Assert.ThrowsException<InvalidCastException>(()=>{ ((BasicInt)db.run("129123456456")).getValue(); });
        }

        [TestMethod]
        public void Test_run_return_scalar_long()
        {
            DBConnection db = new DBConnection();
            db.connect("localhost", 8900);
            BasicLong re = (BasicLong)db.run("1l");
            Assert.AreEqual(1, re.getValue());
        }

        [TestMethod]
        public void Test_run_return_scalar_double()
        {
            DBConnection db = new DBConnection();
            db.connect("localhost", 8900);
            Assert.AreEqual(3, ((BasicDouble)db.run("1.0+2.0")).getValue());
            Assert.AreEqual(129.1, ((BasicDouble)db.run("127.1+2.0")).getValue());
            Assert.IsTrue(Math.Abs(1114.4-((BasicDouble)db.run("1127.1-12.7")).getValue())<0.000001);
        }

        [TestMethod]
        public void Test_run_return_scalar_float()
        {
            DBConnection db = new DBConnection();
            db.connect("localhost", 8900);
            Assert.AreEqual(3, ((BasicFloat)db.run("1.0f+2.0f")).getValue());
            Assert.AreEqual(Math.Round(129.1,1), Math.Round(((BasicFloat)db.run("127.1f+2.0f")).getValue(),1));
        }

        [TestMethod]
        public void Test_run_return_scalar_bool()
        {
            DBConnection db = new DBConnection();
            db.connect("localhost", 8900);
            Assert.IsTrue(((BasicBoolean)db.run("true")).getValue());
            Assert.IsFalse(((BasicBoolean)db.run("false")).getValue());
            Assert.IsFalse(((BasicBoolean)db.run("1==2")).getValue());
            Assert.IsTrue(((BasicBoolean)db.run("2==2")).getValue());
        }

        [TestMethod]
        public void Test_run_return_scalar_byte()
        {
            DBConnection db = new DBConnection();
            db.connect("localhost", 8900);
            //Assert.AreEqual(1,((BasicByte)db.run("true")).getValue());
            //Assert.AreEqual(0, ((BasicByte)db.run("false")).getValue());
        }

        [TestMethod]
        public void Test_run_return_scalar_short()
        {
            DBConnection db = new DBConnection();
            db.connect("localhost", 8900);
            Assert.AreEqual(1,((BasicShort)db.run("1h")).getValue());
            Assert.AreEqual(256,((BasicShort)db.run("256h")).getValue());
            Assert.AreEqual(1024,((BasicShort)db.run("1024h")).getValue());
            Assert.ThrowsException<InvalidCastException>(() => { ((BasicShort)db.run("100h+5000h")).getValue(); });
        }
    }
}
