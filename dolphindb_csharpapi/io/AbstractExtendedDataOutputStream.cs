using com.xxdb.jobjects;
using System.IO;

namespace com.xxdb.io
{


	public abstract class AbstractExtendedDataOutputStream : FilterOutputStream,ExtendedDataOutput
	{
		public abstract void writeLongArray(long[] A, int startIdx, int len);
		public abstract void writeIntArray(int[] A, int startIdx, int len);
		public abstract void writeShortArray(short[] A, int startIdx, int len);
		private const int UTF8_STRING_LIMIT = 65535;
		protected internal const int BUF_SIZE = 4096;
		protected internal sbyte[] buf;
		private static readonly int longBufSize = BUF_SIZE / 8;
		private static readonly int intBufSize = BUF_SIZE / 4;
		private int[] intBuf;
		private long[] longBuf;

        private Stream outStream;
		public AbstractExtendedDataOutputStream(Stream @out)
		{
            outStream = @out;

        }

		public void flush()
		{
            try
            {
                outStream.Flush();
            }
            catch (IOException ex)
            {
                throw ex;
            }
           
        }

		public void writeBoolean(bool v)
		{
            try
            {
                outStream.Write(v ? 1 : 0);
            }
            catch (IOException ex)
            {
                throw ex;
            }
            
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void writeByte(int v) throws java.io.IOException
		public override void writeByte(int v)
		{
            try
            {

            }
            catch (IOException ex)
            {
                throw ex;
            }
            write(v & 0xff);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void writeChar(int v) throws java.io.IOException
		public override void writeChar(int v)
		{
            try
            {

            }
            catch (IOException ex)
            {
                throw ex;
            }
            writeShort(v);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void writeFloat(float v) throws java.io.IOException
		public override void writeFloat(float v)
		{
            try
            {

            }
            catch (IOException ex)
            {
                throw ex;
            }
            writeInt(Float.floatToIntBits(v));
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void writeDouble(double v) throws java.io.IOException
		public override void writeDouble(double v)
		{
            try
            {

            }
            catch (IOException ex)
            {
                throw ex;
            }
            writeLong(Double.doubleToLongBits(v));
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void writeBytes(String s) throws java.io.IOException
		public override void writeBytes(string s)
		{
            try
            {

            }
            catch (IOException ex)
            {
                throw ex;
            }
            int len = s.Length;
			int i = 0;
			int pos = 0;

			if (buf == null)
			{
				buf = new sbyte[BUF_SIZE];
			}
			do
			{
				while (i < len && pos < buf.Length - 4)
				{
					char c = s[i++];
					if (c >= '\u0001' && c <= '\u007f')
					{
						buf[pos++] = (sbyte) c;
					}
					else if (c == '\u0000' || (c >= '\u0080' && c <= '\u07ff'))
					{
						buf[pos++] = unchecked((sbyte)(0xc0 | (0x1f & (c >> 6))));
						buf[pos++] = unchecked((sbyte)(0x80 | (0x3f & c)));
					}
					else
					{
						buf[pos++] = unchecked((sbyte)(0xe0 | (0x0f & (c >> 12))));
						buf[pos++] = unchecked((sbyte)(0x80 | (0x3f & (c >> 6))));
						buf[pos++] = unchecked((sbyte)(0x80 | (0x3f & c)));
					}
				}
				write(buf, 0, pos);
				pos = 0;
			}while (i < len);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void writeChars(String s) throws java.io.IOException
		public override void writeChars(string s)
		{
            try
            {

            }
            catch (IOException ex)
            {
                throw ex;
            }
            int len = s.Length;
			for (int i = 0; i < len; ++i)
			{
				writeChar(s[i]);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void writeUTF(String value) throws java.io.IOException
		public override void writeUTF(string value)
		{
            try
            {

            }
            catch (IOException ex)
            {
                throw ex;
            }
            int len = value.Length;
			int i = 0;
			int pos = 0;
			bool lengthWritten = false;
			if (buf == null)
			{
				buf = new sbyte[BUF_SIZE];
			}
			do
			{
				while (i < len && pos < buf.Length - 3)
				{
					char c = value[i++];
					if (c >= '\u0001' && c <= '\u007f')
					{
						buf[pos++] = (sbyte) c;
					}
					else if (c == '\u0000' || (c >= '\u0080' && c <= '\u07ff'))
					{
						buf[pos++] = unchecked((sbyte)(0xc0 | (0x1f & (c >> 6))));
						buf[pos++] = unchecked((sbyte)(0x80 | (0x3f & c)));
					}
					else
					{
						buf[pos++] = unchecked((sbyte)(0xe0 | (0x0f & (c >> 12))));
						buf[pos++] = unchecked((sbyte)(0x80 | (0x3f & (c >> 6))));
						buf[pos++] = unchecked((sbyte)(0x80 | (0x3f & c)));
					}
				}
				if (!lengthWritten)
				{
					if (i == len)
					{
						writeShort(pos);
					}
					else
					{
						writeShort(getUTFlength(value, i, pos));
					}
					lengthWritten = true;
				}
				write(buf, 0, pos);
				pos = 0;
			}while (i < len);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void writeString(String value) throws java.io.IOException
		public virtual void writeString(string value)
		{
            try
            {

            }
            catch (IOException ex)
            {
                throw ex;
            }
            int len = value.Length;
			int i = 0;
			int pos = 0;
			if (buf == null)
			{
				buf = new sbyte[BUF_SIZE];
			}
			do
			{
				while (i < len && pos < buf.Length - 4)
				{
					char c = value[i++];
					if (c >= '\u0001' && c <= '\u007f')
					{
						buf[pos++] = (sbyte) c;
					}
					else if (c == '\u0000' || (c >= '\u0080' && c <= '\u07ff'))
					{
						buf[pos++] = unchecked((sbyte)(0xc0 | (0x1f & (c >> 6))));
						buf[pos++] = unchecked((sbyte)(0x80 | (0x3f & c)));
					}
					else
					{
						buf[pos++] = unchecked((sbyte)(0xe0 | (0x0f & (c >> 12))));
						buf[pos++] = unchecked((sbyte)(0x80 | (0x3f & (c >> 6))));
						buf[pos++] = unchecked((sbyte)(0x80 | (0x3f & c)));
					}
				}
				if (i >= len)
				{
					buf[pos++] = 0;
				}
				write(buf, 0, pos);
				pos = 0;
			}while (i < len);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public static int getUTFlength(String value, int start, int sum) throws java.io.IOException
		public static int getUTFlength(string value, int start, int sum)
		{
            try
            {

            }
            catch (IOException ex)
            {
                throw ex;
            }
            int len = value.Length;
			for (int i = start; i < len && sum <= 65535; ++i)
			{
				char c = value[i];
				if (c >= '\u0001' && c <= '\u007f')
				{
					sum += 1;
				}
				else if (c == '\u0000' || (c >= '\u0080' && c <= '\u07ff'))
				{
					sum += 2;
				}
				else
				{
					sum += 3;
				}
			}

			if (sum > UTF8_STRING_LIMIT)
			{
				throw new UTFDataFormatException();
			}
			return sum;
		}
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void writeShortArray(short[] A) throws java.io.IOException
		public virtual void writeShortArray(short[] A)
		{
            try
            {

            }
            catch (IOException ex)
            {
                throw ex;
            }
            writeShortArray(A, 0, A.Length);
		}
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void writeIntArray(int[] A) throws java.io.IOException
		public virtual void writeIntArray(int[] A)
		{
            try
            {

            }
            catch (IOException ex)
            {
                throw ex;
            }
            writeIntArray(A, 0, A.Length);
		}
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void writeLongArray(long[] A) throws java.io.IOException
		public virtual void writeLongArray(long[] A)
		{
            try
            {

            }
            catch (IOException ex)
            {
                throw ex;
            }
            writeLongArray(A, 0, A.Length);
		}
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void writeDoubleArray(double[] A) throws java.io.IOException
		public virtual void writeDoubleArray(double[] A)
		{
            try
            {

            }
            catch (IOException ex)
            {
                throw ex;
            }
            writeDoubleArray(A, 0, A.Length);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void writeDoubleArray(double[] A, int startIdx, int len) throws java.io.IOException
		public virtual void writeDoubleArray(double[] A, int startIdx, int len)
		{
            try
            {

            }
            catch (IOException ex)
            {
                throw ex;
            }
            if (longBuf == null)
			{
				longBuf = new long[longBufSize];
			}
			int end = startIdx + len;
			int pos = 0;
			for (int i = startIdx; i < end; ++i)
			{
				if (pos >= longBufSize)
				{
					writeLongArray(longBuf,0, pos);
					pos = 0;
				}
				longBuf[pos++] = Double.doubleToLongBits(A[i]);
			}
			if (pos > 0)
			{
				writeLongArray(longBuf, 0, pos);
			}
		}



//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void writeFloatArray(float[] A) throws java.io.IOException
		public virtual void writeFloatArray(float[] A)
		{
            try
            {

            }
            catch (IOException ex)
            {
                throw ex;
            }
            writeFloatArray(A, 0, A.Length);
		}
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void writeFloatArray(float[] A, int startIdx, int len) throws java.io.IOException
		public virtual void writeFloatArray(float[] A, int startIdx, int len)
		{
            try
            {

            }
            catch (IOException ex)
            {
                throw ex;
            }
            if (intBuf == null)
			{
				intBuf = new int[intBufSize];
			}
			int end = startIdx + len;
			int pos = 0;
			for (int i = startIdx; i < end; ++i)
			{
				if (pos >= intBufSize)
				{
					writeIntArray(intBuf,0, pos);
					pos = 0;
				}
				intBuf[pos++] = Float.floatToIntBits(A[i]);
			}
			if (pos > 0)
			{
				writeIntArray(intBuf,0, pos);
			}
		}
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void writeStringArray(String[] A) throws java.io.IOException
		public virtual void writeStringArray(string[] A)
		{
            try
            {

            }
            catch (IOException ex)
            {
                throw ex;
            }
            writeStringArray(A, 0, A.Length);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void writeStringArray(String[] A, int startIdx, int len) throws java.io.IOException
		public virtual void writeStringArray(string[] A, int startIdx, int len)
		{
            try
            {

            }
            catch (IOException ex)
            {
                throw ex;
            }
            if (buf == null)
			{
				buf = new sbyte[BUF_SIZE];
			}
			int end = startIdx + len;
			int pos = 0;
			for (int j = startIdx; j < end; ++j)
			{
				string value = A[j];
				int valueLen = value.Length;
				int i = 0;
				do
				{
					while (i < valueLen && pos < buf.Length - 4)
					{
						char c = value[i++];
						if (c >= '\u0001' && c <= '\u007f')
						{
							buf[pos++] = (sbyte) c;
						}
						else if (c == '\u0000' || (c >= '\u0080' && c <= '\u07ff'))
						{
							buf[pos++] = unchecked((sbyte)(0xc0 | (0x1f & (c >> 6))));
							buf[pos++] = unchecked((sbyte)(0x80 | (0x3f & c)));
						}
						else
						{
							buf[pos++] = unchecked((sbyte)(0xe0 | (0x0f & (c >> 12))));
							buf[pos++] = unchecked((sbyte)(0x80 | (0x3f & (c >> 6))));
							buf[pos++] = unchecked((sbyte)(0x80 | (0x3f & c)));
						}
					}
					if (i >= valueLen)
					{
						buf[pos++] = 0;
					}
					if (pos + 4 >= buf.Length)
					{
						write(buf, 0, pos);
						pos = 0;
					}
				}while (i < valueLen);
			}
			if (pos > 0)
			{
				write(buf, 0, pos);
			}
		}
	}

}