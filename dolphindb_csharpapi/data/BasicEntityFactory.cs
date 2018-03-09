﻿using dolphindb.data;
using dolphindb.io;

namespace dolphindb.data
{
    using System;
    using System.IO;
    using ExtendedDataInput = dolphindb.io.ExtendedDataInput;

    public class BasicEntityFactory : IEntityFactory
    {
        private TypeFactory[] factories;

        public BasicEntityFactory()
        {
            factories = new TypeFactory[Enum.GetValues(typeof(DATA_TYPE)).Length];
            //factories[(int)DATA_TYPE.DT_BOOL] = new BooleanFactory(this);
            //factories[(int)DATA_TYPE.DT_BYTE] = new ByteFactory(this);
            //factories[(int)DATA_TYPE.DT_SHORT] = new ShortFactory(this);
            factories[(int)DATA_TYPE.DT_INT] = new IntFactory();
            factories[(int)DATA_TYPE.DT_LONG] = new LongFactory();
            //factories[(int)DATA_TYPE.DT_FLOAT] = new FloatFactory(this);
            //factories[(int)DATA_TYPE.DT_DOUBLE] = new DoubleFactory(this);
            //factories[(int)DATA_TYPE.DT_MINUTE] = new MinuteFactory(this);
            //factories[(int)DATA_TYPE.DT_SECOND] = new SecondFactory(this);
            //factories[(int)DATA_TYPE.DT_TIME] = new TimeFactory(this);
            //factories[(int)DATA_TYPE.DT_NANOTIME] = new NanoTimeFactory(this);
            //factories[(int)DATA_TYPE.DT_DATE] = new DateFactory(this);
            //factories[(int)DATA_TYPE.DT_MONTH] = new MonthFactory(this);
            //factories[(int)DATA_TYPE.DT_DATETIME] = new DateTimeFactory(this);
            //factories[(int)DATA_TYPE.DT_TIMESTAMP] = new TimestampFactory(this);
            //factories[(int)DATA_TYPE.DT_NANOTIMESTAMP] = new NanoTimestampFactory(this);
            //factories[(int)DATA_TYPE.DT_SYMBOL] = new SymbolFactory(this);
            //factories[(int)DATA_TYPE.DT_STRING] = new StringFactory(this);
            //factories[(int)DATA_TYPE.DT_FUNCTIONDEF] = new FunctionDefFactory(this);
            //factories[(int)DATA_TYPE.DT_HANDLE] = new SystemHandleFactory(this);
            //factories[(int)DATA_TYPE.DT_CODE] = new MetaCodeFactory(this);
            //factories[(int)DATA_TYPE.DT_DATASOURCE] = new DataSourceFactory(this);
            //factories[(int)DATA_TYPE.DT_RESOURCE] = new ResourceFactory(this);
        }

        public IEntity createEntity(DATA_FORM form, DATA_TYPE type, ExtendedDataInput @in)
        {
            //if (form == DATA_FORM.DF_TABLE)
            //{
            //	return new BasicTable(@in);
            //}
            //else if (form == DATA_FORM.DF_CHART)
            //{
            //	return new BasicChart(@in);
            //}
            //else if (form == DATA_FORM.DF_DICTIONARY)
            //{
            //	return new BasicDictionary(type, @in);
            //}
            //else if (form == DATA_FORM.DF_SET)
            //{
            //	//return new BasicSet(type, @in);
            //}
            //else if (form == DATA_FORM.DF_CHUNK)
            //{
            //	//return new BasicChunkMeta(@in);
            //}
            //else if (type == DATA_TYPE.DT_ANY && form == DATA_FORM.DF_VECTOR)
            //{
            //	//return new BasicAnyVector(@in);
            //}
            //else if (type == DATA_TYPE.DT_VOID && form == DATA_FORM.DF_SCALAR)
            //{
            //	@in.readBoolean();
            //	return new Void();
            //}
            //else
            //{
            int index = (int)type;
            //	if (factories[index] == null)
            //	{
            //		throw new IOException("Data type " + type.ToString() + " is not supported yet.");
            //	}
            //	else if (form == DATA_FORM.DF_VECTOR)
            //	{
            //		return factories[index].createVector(@in);
            //	}
            //	else if (form == DATA_FORM.DF_SCALAR)
            //	{
            return factories[index].createScalar(@in);
            //}
            //else if (form == DATA_FORM.DF_MATRIX)
            //{
            //	return factories[index].createMatrix(@in);
            //}
            //else if (form == DATA_FORM.DF_PAIR)
            //{
            //	return factories[index].createPair(@in);
            //}
            //else
            //{
            //	throw new IOException("Data form " + form.ToString() + " is not supported yet.");
            //}
        }

        public IScalar createScalarWithDefaultValue(DATA_TYPE type)
        {
            int index = (int)type;
            if (factories[index] == null)
            {
                return null;
            }
            else
            {
                return factories[index].createScalarWithDefaultValue();
            }
        }

        private class IntFactory : TypeFactory
        {
            public virtual IScalar createScalar(ExtendedDataInput @in)
            {
                return new BasicInt(@in);
            }

            public virtual IScalar createScalarWithDefaultValue()
            {
                return new BasicInt(0);
            }
        }

        private class LongFactory : TypeFactory
        {

            public IScalar createScalar(ExtendedDataInput @in)
                { return new BasicLong(@in);}
            //      public Vector createVector(ExtendedDataInput @in)
            //          { return new BasicLongVector(DATA_FORM.DF_VECTOR, @in);}
            //      public Vector createPair(ExtendedDataInput @in)
            //          { return new BasicLongVector(Entity.DATA_FORM.DF_PAIR, @in);}
            //public Matrix createMatrix(ExtendedDataInput @in)
            //          { return new BasicLongMatrix(@in);}
            public IScalar createScalarWithDefaultValue()
            { return new BasicLong(0); }
            //      public Vector createVectorWithDefaultValue(int size)
            //          { return new BasicLongVector(size); }
            //      public Vector createPairWithDefaultValue()
            //          { return new BasicLongVector(Entity.DATA_FORM.DF_PAIR, 2); }
            //      public Matrix createMatrixWithDefaultValue(int rows, int columns)
            //          { return new BasicLongMatrix(rows, columns); }
        }
    }





    public interface TypeFactory
    {
        IScalar createScalar(ExtendedDataInput @in);
        IScalar createScalarWithDefaultValue();
    }
}