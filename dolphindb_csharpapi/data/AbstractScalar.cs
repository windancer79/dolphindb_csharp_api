namespace com.xxdb.data
{
    using System;
    using com.xxdb.io;
    using com.xxdb.jobjects;
    using ExtendedDataOutput = com.xxdb.io.ExtendedDataOutput;

    public abstract class AbstractScalar : AbstractEntity, Scalar
	{
        public bool Null => throw new NotImplementedException();

        public override DATA_FORM getDataForm()
        {
            return DATA_FORM.DF_SCALAR;
        }

        public void write(ExtendedDataOutput output)
        {
            //int flag = ((int)DATA_FORM.DF_SCALAR << 8) + getDataType().ordinal();
            //output.writeShort(flag);
            //writeScalarToOutputStream(output);
        }
        public int rows()
        {
            return 1;
        }

        public int columns()
        {
            return 1;
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

        bool Entity.isScalar()
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
    }
}