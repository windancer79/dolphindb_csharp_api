namespace com.xxdb.data
{
    using System;
    using com.xxdb.io;
    using com.xxdb.jobjects;
    using ExtendedDataOutput = com.xxdb.io.ExtendedDataOutput;

    public abstract class AbstractScalar : AbstractEntity, Scalar
	{
        public bool Null => throw new NotImplementedException();

        public abstract void write(ExtendedDataOutput output);

        public DATA_FORM getDataForm()
        {
            throw new NotImplementedException();
        }

        public DATA_CATEGORY getDataCategory()
        {
            throw new NotImplementedException();
        }

        public DATA_TYPE getDataType()
        {
            throw new NotImplementedException();
        }

        public string getString()
        {
            throw new NotImplementedException();
        }

        public bool isScalar()
        {
            throw new NotImplementedException();
        }

        public bool isVector()
        {
            throw new NotImplementedException();
        }

        public bool isPair()
        {
            throw new NotImplementedException();
        }

        public bool isTable()
        {
            throw new NotImplementedException();
        }

        public bool isMatrix()
        {
            throw new NotImplementedException();
        }

        public bool isDictionary()
        {
            throw new NotImplementedException();
        }

        public bool isChart()
        {
            throw new NotImplementedException();
        }

        public bool isChunk()
        {
            throw new NotImplementedException();
        }

        public void setNull()
        {
            throw new NotImplementedException();
        }

        public Number getNumber()
        {
            throw new NotImplementedException();
        }

        public Temporal getTemporal()
        {
            throw new NotImplementedException();
        }

        public int rows()
        {
            throw new NotImplementedException();
        }

        public int columns()
        {
            throw new NotImplementedException();
        }
    }
}