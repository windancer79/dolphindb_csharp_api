using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace com.xxdb
{


	using BasicEntityFactory = com.xxdb.data.BasicEntityFactory;
	using Entity = com.xxdb.data.Entity;
	using EntityFactory = com.xxdb.data.EntityFactory;
	using Void = com.xxdb.data.Void;
	using AbstractExtendedDataOutputStream = com.xxdb.io.AbstractExtendedDataOutputStream;
	using BigEndianDataInputStream = com.xxdb.io.BigEndianDataInputStream;
	using BigEndianDataOutputStream = com.xxdb.io.BigEndianDataOutputStream;
	using ExtendedDataInput = com.xxdb.io.ExtendedDataInput;
	using ExtendedDataOutput = com.xxdb.io.ExtendedDataOutput;
	using LittleEndianDataInputStream = com.xxdb.io.LittleEndianDataInputStream;
	using LittleEndianDataOutputStream = com.xxdb.io.LittleEndianDataOutputStream;
	using ProgressListener = com.xxdb.io.ProgressListener;

	/// <summary>
	/// Sets up a connection to DolphinDB server through TCP/IP protocol
	/// Executes DolphinDB scripts
	/// 
	/// Example:
	/// 
	/// import com.xxdb;
	/// DBConnection conn = new DBConnection();
	/// boolean success = conn.connect("localhost", 8080);
	/// conn.run("sum(1..100)");
	/// 
	/// </summary>

	public class DBConnection
	{
		private static readonly int MAX_FORM_VALUE = Enum.GetValues(typeof(com.xxdb.data.DATA_FORM)).Length - 1;
		private static readonly int MAX_TYPE_VALUE = Enum.GetValues(typeof(com.xxdb.data.DATA_TYPE)).Length - 1;

        private static readonly object threadLock = new object();
        private string sessionID;
		private Socket socket;
		private bool remoteLittleEndian;
		private ExtendedDataOutput @out;
		private EntityFactory factory;
		private string hostName;
		private int port;

		public DBConnection()
		{
			factory = new BasicEntityFactory();
			sessionID = "";
		}

		public bool isBusy()
		{
            return !Monitor.TryEnter(threadLock);
		}

		public bool connect(string hostName, int port)
		{
            lock (threadLock)
            {
                try
                {
                    if (sessionID.Length > 0)
                    {
                        return true;
                    }

                    this.hostName = hostName;
                    this.port = port;
                    socket = new Socket(AddressFamily.InterNetwork ,SocketType.Stream, ProtocolType.Tcp);
                    socket.Connect(hostName, port);
                    socket.NoDelay = true;
                    @out = new LittleEndianDataOutputStream(new BufferedOutputStream(socket.OutputStream));
                    //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
                    //ORIGINAL LINE: @SuppressWarnings("resource") com.xxdb.io.ExtendedDataInput in = new com.xxdb.io.LittleEndianDataInputStream(new java.io.BufferedInputStream(socket.getInputStream()));
                    ExtendedDataInput @in = new LittleEndianDataInputStream(new BufferedInputStream(socket.InputStream));
                    string body = "connect\n";
                    @out.writeBytes("API 0 ");
                    @out.writeBytes(body.Length.ToString());
                    @out.writeByte('\n');
                    @out.writeBytes(body);
                    @out.flush();


                    string line = @in.readLine();
                    int endPos = line.IndexOf(' ');
                    if (endPos <= 0)
                    {
                        close();
                        return false;
                    }
                    sessionID = line.Substring(0, endPos);

                    int startPos = endPos + 1;
                    endPos = line.IndexOf(' ', startPos);
                    if (endPos != line.Length - 2)
                    {
                        close();
                        return false;
                    }

                    if (line[endPos + 1] == '0')
                    {
                        remoteLittleEndian = false;
                        @out = new BigEndianDataOutputStream(new BufferedOutputStream(socket.OutputStream));
                    }
                    else
                    {
                        remoteLittleEndian = true;
                    }

                    return true;
                }
            }
        }

		public virtual bool RemoteLittleEndian
		{
			get
			{
				return this.remoteLittleEndian;
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public com.xxdb.data.Entity tryRun(String script) throws java.io.IOException
		public virtual Entity tryRun(string script)
		{
			if (!mutex.tryLock())
			{
				return null;
			}
			try
			{
				return run(script);
			}
			finally
			{
				mutex.unlock();
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public com.xxdb.data.Entity run(String script) throws java.io.IOException
		public virtual Entity run(string script)
		{
			return run(script, (ProgressListener)null);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public boolean tryReconnect() throws java.io.IOException
		public virtual bool tryReconnect()
		{
			socket = new Socket(hostName, port);
			@out = new LittleEndianDataOutputStream(new BufferedOutputStream(socket.OutputStream));
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @SuppressWarnings("resource") com.xxdb.io.ExtendedDataInput in = new com.xxdb.io.LittleEndianDataInputStream(new java.io.BufferedInputStream(socket.getInputStream()));
			ExtendedDataInput @in = new LittleEndianDataInputStream(new BufferedInputStream(socket.InputStream));
			string body = "connect\n";
			@out.writeBytes("API 0 ");
			@out.writeBytes(body.Length.ToString());
			@out.writeByte('\n');
			@out.writeBytes(body);
			@out.flush();


			string line = @in.readLine();
			int endPos = line.IndexOf(' ');
			if (endPos <= 0)
			{
				close();
				return false;
			}
			sessionID = line.Substring(0, endPos);

			int startPos = endPos + 1;
			endPos = line.IndexOf(' ', startPos);
			if (endPos != line.Length - 2)
			{
				close();
				return false;
			}

			if (line[endPos + 1] == '0')
			{
				remoteLittleEndian = false;
				@out = new BigEndianDataOutputStream(new BufferedOutputStream(socket.OutputStream));
			}
			else
			{
				remoteLittleEndian = true;
			}

			return true;
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public com.xxdb.data.Entity run(String script, com.xxdb.io.ProgressListener listener) throws java.io.IOException
		public virtual Entity run(string script, ProgressListener listener)
		{
			mutex.@lock();
			try
			{
				bool reconnect = false;
				if (socket == null || !socket.Connected)
				{
					if (sessionID.Length == 0)
					{
						throw new IOException("Database connection is not established yet.");
					}
					else
					{
						socket = new Socket(hostName, port);
						@out = new LittleEndianDataOutputStream(new BufferedOutputStream(socket.OutputStream));
					}
				}

				string body = "script\n" + script;
				ExtendedDataInput @in = null;
				string header = null;
				try
				{
					@out.writeBytes((listener != null ? "API2 " : "API ") + sessionID + " ");
					@out.writeBytes(AbstractExtendedDataOutputStream.getUTFlength(body, 0, 0).ToString());
					@out.writeByte('\n');
					@out.writeBytes(body);
					@out.flush();

					@in = remoteLittleEndian ? new LittleEndianDataInputStream(new BufferedInputStream(socket.InputStream)) : new BigEndianDataInputStream(new BufferedInputStream(socket.InputStream));
					header = @in.readLine();
				}
				catch (IOException ex)
				{
					if (reconnect)
					{
						socket = null;
						throw ex;
					}

					try
					{
						tryReconnect();
						@out.writeBytes((listener != null ? "API2 " : "API ") + sessionID + " ");
						@out.writeBytes(AbstractExtendedDataOutputStream.getUTFlength(body, 0, 0).ToString());
						@out.writeByte('\n');
						@out.writeBytes(body);
						@out.flush();

						@in = remoteLittleEndian ? new LittleEndianDataInputStream(new BufferedInputStream(socket.InputStream)) : new BigEndianDataInputStream(new BufferedInputStream(socket.InputStream));
						header = @in.readLine();
						reconnect = true;
					}
					catch (Exception e)
					{
						socket = null;
						throw e;
					}
				}
                string msg;

                while (header.Equals("MSG"))
				{
					//read intermediate message to indicate the progress
					msg = @in.readString();
					if (listener != null)
					{
						listener.progress(msg);
					}
					header = @in.readLine();
				}

				string[] headers = header.Split(" ", true);
				if (headers.Length != 3)
				{
					socket = null;
					throw new IOException("Received invalid header: " + header);
				}

				if (reconnect)
				{
					sessionID = headers[0];
				}
				int numObject = int.Parse(headers[1]);

				msg = @in.readLine();
				if (!msg.Equals("OK"))
				{
					throw new IOException(msg);
				}

				if (numObject == 0)
				{
					return new Void();
				}
				try
				{
					short flag = @in.readShort();
					int form = flag >> 8;
					int type = flag & 0xff;

					if (form < 0 || form > MAX_FORM_VALUE)
					{
						throw new IOException("Invalid form value: " + form);
					}
					if (type < 0 || type > MAX_TYPE_VALUE)
					{
						throw new IOException("Invalid type value: " + type);
					}

					com.xxdb.data.DATA_FORM df = Enum.GetValues(typeof(com.xxdb.data.DATA_FORM));
					com.xxdb.data.DATA_TYPE dt = Enum.GetValues(typeof(com.xxdb.data.DATA_TYPE))[type];

					return factory.createEntity(df, dt, @in);
				}
				catch (IOException ex)
				{
					socket = null;
					throw ex;
				}
			}
			finally
			{
				mutex.unlock();
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public com.xxdb.data.Entity tryRun(String function, java.util.List<com.xxdb.data.Entity> arguments) throws java.io.IOException
		public virtual Entity tryRun(string function, IList<Entity> arguments)
		{
			if (!mutex.tryLock())
			{
				return null;
			}
			try
			{
				return run(function, arguments);
			}
			finally
			{
				mutex.unlock();
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public com.xxdb.data.Entity run(String function, java.util.List<com.xxdb.data.Entity> arguments) throws java.io.IOException
		public virtual Entity run(string function, IList<Entity> arguments)
		{
			mutex.@lock();
			try
			{
				bool reconnect = false;
				if (socket == null || !socket.Connected || socket.Closed)
				{
					if (sessionID.Length == 0)
					{
						throw new IOException("Database connection is not established yet.");
					}
					else
					{
						socket = new Socket(hostName, port);
						@out = new LittleEndianDataOutputStream(new BufferedOutputStream(socket.OutputStream));
					}
				}

				string body = "function\n" + function;
				body += ("\n" + arguments.Count + "\n");
				body += remoteLittleEndian ? "1" : "0";

				ExtendedDataInput @in = null;
				string[] headers = null;
				try
				{
					@out.writeBytes("API " + sessionID + " ");
					@out.writeBytes(body.Length.ToString());
					@out.writeByte('\n');
					@out.writeBytes(body);
					for (int i = 0; i < arguments.Count; ++i)
					{
						arguments[i].write(@out);
					}
					@out.flush();

					@in = remoteLittleEndian ? new LittleEndianDataInputStream(new BufferedInputStream(socket.InputStream)) : new BigEndianDataInputStream(new BufferedInputStream(socket.InputStream));
					headers = @in.readLine().Split(" ");
				}
				catch (IOException ex)
				{
					if (reconnect)
					{
						socket = null;
						throw ex;
					}

					try
					{
						tryReconnect();
						@out = new LittleEndianDataOutputStream(new BufferedOutputStream(socket.OutputStream));
						@out.writeBytes("API " + sessionID + " ");
						@out.writeBytes(body.Length.ToString());
						@out.writeByte('\n');
						@out.writeBytes(body);
						for (int i = 0; i < arguments.Count; ++i)
						{
							arguments[i].write(@out);
						}
						@out.flush();

						@in = remoteLittleEndian ? new LittleEndianDataInputStream(new BufferedInputStream(socket.InputStream)) : new BigEndianDataInputStream(new BufferedInputStream(socket.InputStream));
						headers = @in.readLine().Split(" ");
						reconnect = true;
					}
					catch (Exception e)
					{
						socket = null;
						throw e;
					}
				}

				if (headers.Length != 3)
				{
					socket = null;
					throw new IOException("Received invalid header.");
				}

				if (reconnect)
				{
					sessionID = headers[0];
				}
				int numObject = int.Parse(headers[1]);

				string msg = @in.readLine();
				if (!msg.Equals("OK"))
				{
					throw new IOException(msg);
				}

				if (numObject == 0)
				{
					return new Void();
				}

				try
				{
					short flag = @in.readShort();
					int form = flag >> 8;
					int type = flag & 0xff;

					if (form < 0 || form > MAX_FORM_VALUE)
					{
						throw new IOException("Invalid form value: " + form);
					}
					if (type < 0 || type > MAX_TYPE_VALUE)
					{
						throw new IOException("Invalid type value: " + type);
					}

					com.xxdb.data.DATA_FORM df = Enum.GetValues(typeof(com.xxdb.data.DATA_FORM))[form];
					com.xxdb.data.DATA_TYPE dt = Enum.GetValues(typeof(com.xxdb.data.DATA_TYPE))[type];

					return factory.createEntity(df, dt, @in);
				}
				catch (IOException ex)
				{
					socket = null;
					throw ex;
				}
			}
			finally
			{
				mutex.unlock();
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void tryUpload(final java.util.Map<String, com.xxdb.data.Entity> variableObjectMap) throws java.io.IOException
//JAVA TO C# CONVERTER WARNING: 'final' parameters are not available in .NET:
		public virtual void tryUpload(IDictionary<string, Entity> variableObjectMap)
		{
			if (!mutex.tryLock())
			{
				throw new IOException("The connection is in use.");
			}
			try
			{
				upload(variableObjectMap);
			}
			finally
			{
				mutex.unlock();
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void upload(final java.util.Map<String, com.xxdb.data.Entity> variableObjectMap) throws java.io.IOException
//JAVA TO C# CONVERTER WARNING: 'final' parameters are not available in .NET:
		public virtual void upload(IDictionary<string, Entity> variableObjectMap)
		{
			if (variableObjectMap == null || variableObjectMap.Count == 0)
			{
				return;
			}

			mutex.@lock();
			try
			{
				bool reconnect = false;
				if (socket == null || !socket.Connected || socket.Closed)
				{
					if (sessionID.Length == 0)
					{
						throw new IOException("Database connection is not established yet.");
					}
					else
					{
						reconnect = true;
						socket = new Socket(hostName, port);
						@out = new LittleEndianDataOutputStream(new BufferedOutputStream(socket.OutputStream));
					}
				}

				IList<Entity> objects = new List<Entity>();

				string body = "variable\n";
				foreach (string key in variableObjectMap.Keys)
				{
					if (!isVariableCandidate(key))
					{
						throw new System.ArgumentException("'" + key + "' is not a good variable name.");
					}
					body += key + ",";
					objects.Add(variableObjectMap[key]);
				}
				body = body.Substring(0, body.Length - 1);
				body += ("\n" + objects.Count + "\n");
				body += remoteLittleEndian ? "1" : "0";

				try
				{
					@out.writeBytes("API " + sessionID + " ");
					@out.writeBytes(body.Length.ToString());
					@out.writeByte('\n');
					@out.writeBytes(body);
					for (int i = 0; i < objects.Count; ++i)
					{
						objects[i].write(@out);
					}
					@out.flush();
				}
				catch (IOException ex)
				{
					if (reconnect)
					{
						socket = null;
						throw ex;
					}

					try
					{
						socket = new Socket(hostName, port);
						@out = new LittleEndianDataOutputStream(new BufferedOutputStream(socket.OutputStream));
						@out.writeBytes("API " + sessionID + " ");
						@out.writeBytes(body.Length.ToString());
						@out.writeByte('\n');
						@out.writeBytes(body);
						for (int i = 0; i < objects.Count; ++i)
						{
							objects[i].write(@out);
						}
						@out.flush();
						reconnect = true;
					}
					catch (Exception e)
					{
						socket = null;
						throw e;
					}
				}

				ExtendedDataInput @in = remoteLittleEndian ? new LittleEndianDataInputStream(new BufferedInputStream(socket.InputStream)) : new BigEndianDataInputStream(new BufferedInputStream(socket.InputStream));

				string[] headers = @in.readLine().Split(" ");
				if (headers.Length != 3)
				{
					socket = null;
					throw new IOException("Received invalid header.");
				}

				if (reconnect)
				{
					sessionID = headers[0];
				}
				string msg = @in.readLine();
				if (!msg.Equals("OK"))
				{
					throw new IOException(msg);
				}
			}
			finally
			{
				mutex.unlock();
			}
		}

		public virtual void close()
		{
			mutex.@lock();
			try
			{
				if (socket != null)
				{
					socket.close();
					socket = null;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				Console.Write(ex.StackTrace);
			}
			finally
			{
				mutex.unlock();
			}
		}

		private bool isVariableCandidate(string word)
		{
			char cur = word[0];
			if ((cur < 'a' || cur>'z') && (cur < 'A' || cur>'Z'))
			{
				return false;
			}
			for (int i = 1;i < word.Length;i++)
			{
				cur = word[i];
				if ((cur < 'a' || cur>'z') && (cur < 'A' || cur>'Z') && (cur < '0' || cur>'9') && cur != '_')
				{
					return false;
				}
			}
			return true;
		}

		public virtual string HostName
		{
			get
			{
				return hostName;
			}
		}

		public virtual int Port
		{
			get
			{
				return port;
			}
		}
	}

}