using System.IO;

namespace com.xxdb.io
{


	public class BigEndianDataOutputStream : AbstractExtendedDataOutputStream
	{

		public BigEndianDataOutputStream(System.IO.Stream @out) : base(@out)
		{
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void writeShort(int v) throws java.io.IOException
		public void writeShort(int v)
		{
            try
            {

            }
            catch (IOException ex)
            {
                throw ex;
            }

            write(0xff & (v >> 8));
			write(0xff & v);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void writeInt(int v) throws java.io.IOException
		public override void writeInt(int v)
		{
            try
            {

            }
            catch (IOException ex)
            {
                throw ex;
            }

            @out.write(0xFF & (v >> 24));
			@out.write(0xFF & (v >> 16));
			@out.write(0xFF & (v >> 8));
			@out.write(0xFF & v);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void writeLong(long v) throws java.io.IOException
		public override void writeLong(long v)
		{
            try
            {

            }
            catch (IOException ex)
            {
                throw ex;
            }

            @out.write((int)(0xFF & (v >> 56)));
			@out.write((int)(0xFF & (v >> 48)));
			@out.write((int)(0xFF & (v >> 40)));
			@out.write((int)(0xFF & (v >> 32)));
			@out.write((int)(0xFF & (v >> 24)));
			@out.write((int)(0xFF & (v >> 16)));
			@out.write((int)(0xFF & (v >> 8)));
			@out.write((int)(0xFF & v));
		}
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void writeShortArray(short [] A, int startIdx, int len) throws java.io.IOException
		public override void writeShortArray(short[] A, int startIdx, int len)
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
			for (int i = startIdx; i < end; ++i)
			{
				short v = A[i];
				if (pos + 2 >= BUF_SIZE)
				{
					@out.write(buf, 0, pos);
					pos = 0;
				}
				buf[pos++] = unchecked((sbyte)(0xFF & (v >> 8)));
				buf[pos++] = unchecked((sbyte)(0xFF & (v)));
			}
			if (pos > 0)
			{
				@out.write(buf, 0, pos);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void writeIntArray(int [] A, int startIdx, int len) throws java.io.IOException
		public override void writeIntArray(int[] A, int startIdx, int len)
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
			for (int i = startIdx; i < end; ++i)
			{
				int v = A[i];
				if (pos + 4 >= BUF_SIZE)
				{
					@out.write(buf, 0, pos);
					pos = 0;
				}
				buf[pos++] = unchecked((sbyte)(0xFF & (v >> 24)));
				buf[pos++] = unchecked((sbyte)(0xFF & (v >> 16)));
				buf[pos++] = unchecked((sbyte)(0xFF & (v >> 8)));
				buf[pos++] = unchecked((sbyte)(0xFF & (v)));
			}
			if (pos > 0)
			{
				@out.write(buf, 0, pos);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void writeLongArray(long[] A, int startIdx, int len) throws java.io.IOException
		public override void writeLongArray(long[] A, int startIdx, int len)
		{
            try
            {
                if (buf == null)
                {
                    buf = new sbyte[BUF_SIZE];
                }
                int end = startIdx + len;
                int pos = 0;
                for (int i = startIdx; i < end; ++i)
                {
                    long v = A[i];
                    if (pos + 8 >= BUF_SIZE)
                    {
                        @out.write(buf, 0, pos);
                        pos = 0;
                    }
                    buf[pos++] = unchecked((sbyte)(0xFF & (v >> 56)));
                    buf[pos++] = unchecked((sbyte)(0xFF & (v >> 48)));
                    buf[pos++] = unchecked((sbyte)(0xFF & (v >> 40)));
                    buf[pos++] = unchecked((sbyte)(0xFF & (v >> 32)));
                    buf[pos++] = unchecked((sbyte)(0xFF & (v >> 24)));
                    buf[pos++] = unchecked((sbyte)(0xFF & (v >> 16)));
                    buf[pos++] = unchecked((sbyte)(0xFF & (v >> 8)));
                    buf[pos++] = unchecked((sbyte)(0xFF & (v)));
                }
                if (pos > 0)
                {
                    @out.write(buf, 0, pos);
                }
            }
            catch (IOException ex)
            {
                throw ex;
            }
        }

    }

}