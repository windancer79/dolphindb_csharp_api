using System.IO;

namespace com.xxdb.io
{


    public interface ExtendedDataInput
    {
        bool readBoolean();

        byte readByte();

        char readChar();

        double readDouble();

        float readFloat();

        void readFully(byte[] arg0);

        void readFully(byte[] arg0, int arg1, int arg2);

        string readLine();

        string readString();

        short readShort();

        string readUTF();
        int readUnsignedByte();

        int skipBytes(int n);
    }
}