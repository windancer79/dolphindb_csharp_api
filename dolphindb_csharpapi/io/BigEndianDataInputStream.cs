using System.IO;

namespace dolphindb.io
{


	public class BigEndianDataInputStream : AbstractExtendedDataInputStream
	{

		public BigEndianDataInputStream(Stream @in) : base(@in)
		{

		}

		public override int readInt()
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

		public override long readLong()
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

		public override int readUnsignedShort()
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