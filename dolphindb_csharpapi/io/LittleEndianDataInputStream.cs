using System.IO;
namespace dolphindb.io
{


	public class LittleEndianDataInputStream : AbstractExtendedDataInputStream
	{

		public LittleEndianDataInputStream(Stream @in) : base(@in)
		{
		}

		public override int readInt()
		{
			sbyte b1 = readAndCheckByte();
			sbyte b2 = readAndCheckByte();
			sbyte b3 = readAndCheckByte();
			sbyte b4 = readAndCheckByte();
			return fromBytes(b4, b3, b2, b1);
		}

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

		public override int readUnsignedShort()
		{
			sbyte b1 = readAndCheckByte();
			sbyte b2 = readAndCheckByte();
			return fromBytes((sbyte)0, (sbyte)0, b2, b1);
		}
	}

}