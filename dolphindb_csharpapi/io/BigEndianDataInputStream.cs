using System.IO;

namespace com.xxdb.io
{


	public class BigEndianDataInputStream : AbstractExtendedDataInputStream
	{

		public BigEndianDataInputStream(System.IO.Stream @in) : base(@in)
		{
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public int readInt() throws java.io.IOException
		public int readInt()
		{
            try
            {
                sbyte b1 = readAndCheckByte();
                sbyte b2 = readAndCheckByte();
                sbyte b3 = readAndCheckByte();
                sbyte b4 = readAndCheckByte();
                return fromBytes(b1, b2, b3, b4);
            }
            catch(IOException ex)
            {
                throw ex;
            }

		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public long readLong() throws java.io.IOException
		public long readLong()
		{
            try
            {
                sbyte b1 = readAndCheckByte();
                sbyte b2 = readAndCheckByte();
                sbyte b3 = readAndCheckByte();
                sbyte b4 = readAndCheckByte();
                sbyte b5 = readAndCheckByte();
                sbyte b6 = readAndCheckByte();
                sbyte b7 = readAndCheckByte();
                sbyte b8 = readAndCheckByte();
                return fromBytes(b1, b2, b3, b4, b5, b6, b7, b8);
            }
            catch (IOException ex)
            {
                throw ex;
            }
        }

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public int readUnsignedShort() throws java.io.IOException
		public int readUnsignedShort()
		{
            try
            {
                sbyte b1 = readAndCheckByte();
                sbyte b2 = readAndCheckByte();
                return fromBytes(b1, b2, (sbyte)0, (sbyte)0);
            }
            catch (IOException ex)
            {
                throw ex;
            }
		}
	}

}