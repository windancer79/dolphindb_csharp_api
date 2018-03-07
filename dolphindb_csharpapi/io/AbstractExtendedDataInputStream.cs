using com.xxdb.jobjects;
using System;

namespace com.xxdb.io
{


	public abstract class AbstractExtendedDataInputStream : FilterInputStream, ExtendedDataInput
	{
		private static readonly Charset UTF8 = Charset.forName("UTF-8");
		private const int UTF8_STRING_LIMIT = 65536;
		private sbyte[] buf_;

		protected internal AbstractExtendedDataInputStream(System.IO.Stream @in) : base(@in)
		{
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public boolean readBoolean() throws java.io.IOException
		public override bool readBoolean()
		{
			return readUnsignedByte() != 0;
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public byte readByte() throws java.io.IOException
		public override byte readByte()
		{
			return (byte)readUnsignedByte();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public char readChar() throws java.io.IOException
		public override char readChar()
		{
			return (char)readUnsignedShort();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public double readDouble() throws java.io.IOException
		public override double readDouble()
		{
			return Double.longBitsToDouble(readLong());
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public float readFloat() throws java.io.IOException
		public override float readFloat()
		{
			return Float.intBitsToFloat(readInt());
		}

		public override void readFully(byte[] arg0)
		{
			base.read(arg0);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void readFully(byte[] arg0, int arg1, int arg2) throws java.io.IOException
		public override void readFully(byte[] arg0, int arg1, int arg2)
		{
			base.read(arg0, arg1, arg2);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public String readLine() throws java.io.IOException
		public override string readLine()
		{
			return readUTF8((sbyte)'\n');
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public String readString() throws java.io.IOException
		public virtual string readString()
		{
			return readUTF8((sbyte)0);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: private String readUTF8(byte terminator) throws java.io.IOException
		private string readUTF8(sbyte terminator)
		{
			if (buf_ == null)
			{
				buf_ = new sbyte[2048];
			}
			sbyte ch = readAndCheckByte();
			int count = 0;
			while (ch != terminator)
			{
				if (count == buf_.Length)
				{
					if (count >= UTF8_STRING_LIMIT)
					{
						throw new IOException("UTF-8 string length exceeds the limit of 65536 bytes");
					}
					sbyte[] tmp = new sbyte[buf_.Length * 2];
					Array.Copy(buf_, 0, tmp, 0, buf_.Length);
					buf_ = tmp;
				}
				buf_[count++] = ch;
				ch = readAndCheckByte();
			}
			return StringHelperClass.NewString(buf_, 0, count, UTF8);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public short readShort() throws java.io.IOException
		public override short readShort()
		{
			return (short)readUnsignedShort();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public String readUTF() throws java.io.IOException
		public override string readUTF()
		{
			return (new DataInputStream(@in)).readUTF();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public int readUnsignedByte() throws java.io.IOException
		public override int readUnsignedByte()
		{
			int b1 = @in.read();
			if (0 > b1)
			{
				throw new EOFException();
			}
			return b1;
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public int skipBytes(int n) throws java.io.IOException
		public override int skipBytes(int n)
		{
			return (int) @in.skip(n);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: protected byte readAndCheckByte() throws java.io.IOException, java.io.EOFException
		protected internal virtual sbyte readAndCheckByte()
		{
			int b1 = @in.read();

			if (-1 == b1)
			{
				throw new EOFException();
			}
			return (sbyte) b1;
		}

		protected internal virtual int fromBytes(sbyte b1, sbyte b2, sbyte b3, sbyte b4)
		{
			return b1 << 24 | (b2 & 0xFF) << 16 | (b3 & 0xFF) << 8 | (b4 & 0xFF);
		}

		protected internal virtual long fromBytes(sbyte b1, sbyte b2, sbyte b3, sbyte b4, sbyte b5, sbyte b6, sbyte b7, sbyte b8)
		{
			return (b1 & 0xFFL) << 56 | (b2 & 0xFFL) << 48 | (b3 & 0xFFL) << 40 | (b4 & 0xFFL) << 32 | (b5 & 0xFFL) << 24 | (b6 & 0xFFL) << 16 | (b7 & 0xFFL) << 8 | (b8 & 0xFFL);
		}

	}

}