namespace com.xxdb.io
{


	public class LittleEndianDataInputStream : AbstractExtendedDataInputStream
	{

		public LittleEndianDataInputStream(System.IO.Stream @in) : base(@in)
		{
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public int readInt() throws java.io.IOException
		public override int readInt()
		{
			sbyte b1 = readAndCheckByte();
			sbyte b2 = readAndCheckByte();
			sbyte b3 = readAndCheckByte();
			sbyte b4 = readAndCheckByte();
			return fromBytes(b4, b3, b2, b1);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public long readLong() throws java.io.IOException
		public override long readLong()
		{
			sbyte b1 = readAndCheckByte();
			sbyte b2 = readAndCheckByte();
			sbyte b3 = readAndCheckByte();
			sbyte b4 = readAndCheckByte();
			sbyte b5 = readAndCheckByte();
			sbyte b6 = readAndCheckByte();
			sbyte b7 = readAndCheckByte();
			sbyte b8 = readAndCheckByte();
			return fromBytes(b8, b7, b6, b5, b4, b3, b2, b1);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public int readUnsignedShort() throws java.io.IOException
		public override int readUnsignedShort()
		{
			sbyte b1 = readAndCheckByte();
			sbyte b2 = readAndCheckByte();
			return fromBytes((sbyte)0, (sbyte)0, b2, b1);
		}
	}

}