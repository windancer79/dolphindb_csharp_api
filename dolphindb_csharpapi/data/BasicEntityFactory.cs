namespace com.xxdb.data
{

	using ExtendedDataInput = com.xxdb.io.ExtendedDataInput;

	public class BasicEntityFactory : EntityFactory
	{
		private TypeFactory[] factories;

		public BasicEntityFactory()
		{
			factories = new TypeFactory[Enum.GetValues(typeof(DATA_TYPE)).length];
			factories[(int)DATA_TYPE.DT_BOOL] = new BooleanFactory(this);
			factories[(int)DATA_TYPE.DT_BYTE] = new ByteFactory(this);
			factories[(int)DATA_TYPE.DT_SHORT] = new ShortFactory(this);
			factories[(int)DATA_TYPE.DT_INT] = new IntFactory(this);
			factories[(int)DATA_TYPE.DT_LONG] = new LongFactory(this);
			factories[(int)DATA_TYPE.DT_FLOAT] = new FloatFactory(this);
			factories[(int)DATA_TYPE.DT_DOUBLE] = new DoubleFactory(this);
			factories[(int)DATA_TYPE.DT_MINUTE] = new MinuteFactory(this);
			factories[(int)DATA_TYPE.DT_SECOND] = new SecondFactory(this);
			factories[(int)DATA_TYPE.DT_TIME] = new TimeFactory(this);
			factories[(int)DATA_TYPE.DT_NANOTIME] = new NanoTimeFactory(this);
			factories[(int)DATA_TYPE.DT_DATE] = new DateFactory(this);
			factories[(int)DATA_TYPE.DT_MONTH] = new MonthFactory(this);
			factories[(int)DATA_TYPE.DT_DATETIME] = new DateTimeFactory(this);
			factories[(int)DATA_TYPE.DT_TIMESTAMP] = new TimestampFactory(this);
			factories[(int)DATA_TYPE.DT_NANOTIMESTAMP] = new NanoTimestampFactory(this);
			factories[(int)DATA_TYPE.DT_SYMBOL] = new SymbolFactory(this);
			factories[(int)DATA_TYPE.DT_STRING] = new StringFactory(this);
			factories[(int)DATA_TYPE.DT_FUNCTIONDEF] = new FunctionDefFactory(this);
			factories[(int)DATA_TYPE.DT_HANDLE] = new SystemHandleFactory(this);
			factories[(int)DATA_TYPE.DT_CODE] = new MetaCodeFactory(this);
			factories[(int)DATA_TYPE.DT_DATASOURCE] = new DataSourceFactory(this);
			factories[(int)DATA_TYPE.DT_RESOURCE] = new ResourceFactory(this);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public Entity createEntity(Entity_DATA_FORM form, Entity_DATA_TYPE type, com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
		public virtual Entity createEntity(DATA_FORM form, DATA_TYPE type, ExtendedDataInput @in)
		{
			if (form == DATA_FORM.DF_TABLE)
			{
				return new BasicTable(@in);
			}
			else if (form == DATA_FORM.DF_CHART)
			{
				return new BasicChart(@in);
			}
			else if (form == DATA_FORM.DF_DICTIONARY)
			{
				return new BasicDictionary(type, @in);
			}
			else if (form == DATA_FORM.DF_SET)
			{
				return new BasicSet(type, @in);
			}
			else if (form == DATA_FORM.DF_CHUNK)
			{
				return new BasicChunkMeta(@in);
			}
			else if (type == DATA_TYPE.DT_ANY && form == DATA_FORM.DF_VECTOR)
			{
				return new BasicAnyVector(@in);
			}
			else if (type == DATA_TYPE.DT_VOID && form == DATA_FORM.DF_SCALAR)
			{
				@in.readBoolean();
				return new Void();
			}
			else
			{
				int index = type.ordinal();
				if (factories[index] == null)
				{
					throw new IOException("Data type " + type.name() + " is not supported yet.");
				}
				else if (form == DATA_FORM.DF_VECTOR)
				{
					return factories[index].createVector(@in);
				}
				else if (form == DATA_FORM.DF_SCALAR)
				{
					return factories[index].createScalar(@in);
				}
				else if (form == DATA_FORM.DF_MATRIX)
				{
					return factories[index].createMatrix(@in);
				}
				else if (form == DATA_FORM.DF_PAIR)
				{
					return factories[index].createPair(@in);
				}
				else
				{
					throw new IOException("Data form " + form.name() + " is not supported yet.");
				}
			}
		}

		public virtual Matrix createMatrixWithDefaultValue(DATA_TYPE type, int rows, int columns)
		{
			int index = type.ordinal();
			if (factories[index] == null)
			{
				return null;
			}
			else
			{
				return factories[index].createMatrixWithDefaultValue(rows, columns);
			}
		}

		public virtual Vector createVectorWithDefaultValue(DATA_TYPE type, int size)
		{
			int index = type.ordinal();
			if (factories[index] == null)
			{
				return null;
			}
			else
			{
				return factories[index].createVectorWithDefaultValue(size);
			}
		}

		public virtual Vector createPairWithDefaultValue(DATA_TYPE type)
		{
			int index = type.ordinal();
			if (factories[index] == null)
			{
				return null;
			}
			else
			{
				return factories[index].createPairWithDefaultValue();
			}
		}

		public virtual Scalar createScalarWithDefaultValue(DATA_TYPE type)
		{
			int index = type.ordinal();
			if (factories[index] == null)
			{
				return null;
			}
			else
			{
				return factories[index].createScalarWithDefaultValue();
			}
		}

		private interface TypeFactory
		{
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: Scalar createScalar(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException;
			Scalar createScalar(ExtendedDataInput @in);
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: Vector createVector(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException;
			Vector createVector(ExtendedDataInput @in);
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: Vector createPair(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException;
			Vector createPair(ExtendedDataInput @in);
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: Matrix createMatrix(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException;
			Matrix createMatrix(ExtendedDataInput @in);
			Scalar createScalarWithDefaultValue();
			Vector createVectorWithDefaultValue(int size);
			Vector createPairWithDefaultValue();
			Matrix createMatrixWithDefaultValue(int rows, int columns);
		}

		private class BooleanFactory : TypeFactory
		{
			private readonly BasicEntityFactory outerInstance;

			public BooleanFactory(BasicEntityFactory outerInstance)
			{
				this.outerInstance = outerInstance;
			}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Scalar createScalar(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
			public virtual Scalar createScalar(ExtendedDataInput @in)
			{
				return new BasicBoolean(@in);
			}
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Vector createVector(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
			public virtual Vector createVector(ExtendedDataInput @in)
			{
				return new BasicBooleanVector(DATA_FORM.DF_VECTOR, @in);
			}
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Vector createPair(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
			public virtual Vector createPair(ExtendedDataInput @in)
			{
				return new BasicBooleanVector(DATA_FORM.DF_PAIR, @in);
			}
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Matrix createMatrix(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
			public virtual Matrix createMatrix(ExtendedDataInput @in)
			{
				return new BasicBooleanMatrix(@in);
			}
			public virtual Scalar createScalarWithDefaultValue()
			{
				return new BasicBoolean(false);
			}
			public virtual Vector createVectorWithDefaultValue(int size)
			{
				return new BasicBooleanVector(size);
			}
			public virtual Vector createPairWithDefaultValue()
			{
				return new BasicBooleanVector(DATA_FORM.DF_PAIR, 2);
			}
			public virtual Matrix createMatrixWithDefaultValue(int rows, int columns)
			{
				return new BasicBooleanMatrix(rows, columns);
			}
		}

		private class ByteFactory : TypeFactory
		{
			private readonly BasicEntityFactory outerInstance;

			public ByteFactory(BasicEntityFactory outerInstance)
			{
				this.outerInstance = outerInstance;
			}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Scalar createScalar(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
			public virtual Scalar createScalar(ExtendedDataInput @in)
			{
				return new BasicByte(@in);
			}
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Vector createVector(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
			public virtual Vector createVector(ExtendedDataInput @in)
			{
				return new BasicByteVector(DATA_FORM.DF_VECTOR, @in);
			}
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Vector createPair(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
			public virtual Vector createPair(ExtendedDataInput @in)
			{
				return new BasicByteVector(DATA_FORM.DF_PAIR, @in);
			}
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Matrix createMatrix(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
			public virtual Matrix createMatrix(ExtendedDataInput @in)
			{
				return new BasicByteMatrix(@in);
			}
			public virtual Scalar createScalarWithDefaultValue()
			{
				return new BasicByte((sbyte)0);
			}
			public virtual Vector createVectorWithDefaultValue(int size)
			{
				return new BasicByteVector(size);
			}
			public virtual Vector createPairWithDefaultValue()
			{
				return new BasicByteVector(DATA_FORM.DF_PAIR, 2);
			}
			public virtual Matrix createMatrixWithDefaultValue(int rows, int columns)
			{
				return new BasicByteMatrix(rows, columns);
			}
		}

		private class ShortFactory : TypeFactory
		{
			private readonly BasicEntityFactory outerInstance;

			public ShortFactory(BasicEntityFactory outerInstance)
			{
				this.outerInstance = outerInstance;
			}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Scalar createScalar(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
			public virtual Scalar createScalar(ExtendedDataInput @in)
			{
				return new BasicShort(@in);
			}
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Vector createVector(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
			public virtual Vector createVector(ExtendedDataInput @in)
			{
				return new BasicShortVector(DATA_FORM.DF_VECTOR, @in);
			}
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Vector createPair(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
			public virtual Vector createPair(ExtendedDataInput @in)
			{
				return new BasicShortVector(DATA_FORM.DF_PAIR, @in);
			}
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Matrix createMatrix(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
			public virtual Matrix createMatrix(ExtendedDataInput @in)
			{
				return new BasicShortMatrix(@in);
			}
			public virtual Scalar createScalarWithDefaultValue()
			{
				return new BasicShort((short)0);
			}
			public virtual Vector createVectorWithDefaultValue(int size)
			{
				return new BasicShortVector(size);
			}
			public virtual Vector createPairWithDefaultValue()
			{
				return new BasicShortVector(DATA_FORM.DF_PAIR, 2);
			}
			public virtual Matrix createMatrixWithDefaultValue(int rows, int columns)
			{
				return new BasicShortMatrix(rows, columns);
			}
		}

		private class IntFactory : TypeFactory
		{
			private readonly BasicEntityFactory outerInstance;

			public IntFactory(BasicEntityFactory outerInstance)
			{
				this.outerInstance = outerInstance;
			}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Scalar createScalar(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
			public virtual Scalar createScalar(ExtendedDataInput @in)
			{
				return new BasicInt(@in);
			}
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Vector createVector(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
			public virtual Vector createVector(ExtendedDataInput @in)
			{
				return new BasicIntVector(DATA_FORM.DF_VECTOR, @in);
			}
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Vector createPair(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
			public virtual Vector createPair(ExtendedDataInput @in)
			{
				return new BasicIntVector(DATA_FORM.DF_PAIR, @in);
			}
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Matrix createMatrix(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
			public virtual Matrix createMatrix(ExtendedDataInput @in)
			{
				return new BasicIntMatrix(@in);
			}
			public virtual Scalar createScalarWithDefaultValue()
			{
				return new BasicInt(0);
			}
			public virtual Vector createVectorWithDefaultValue(int size)
			{
				return new BasicIntVector(size);
			}
			public virtual Vector createPairWithDefaultValue()
			{
				return new BasicIntVector(DATA_FORM.DF_PAIR, 2);
			}
			public virtual Matrix createMatrixWithDefaultValue(int rows, int columns)
			{
				return new BasicIntMatrix(rows, columns);
			}
		}

		private class LongFactory : TypeFactory
		{
			private readonly BasicEntityFactory outerInstance;

			public LongFactory(BasicEntityFactory outerInstance)
			{
				this.outerInstance = outerInstance;
			}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Scalar createScalar(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
			public virtual Scalar createScalar(ExtendedDataInput @in)
			{
				return new BasicLong(@in);
			}
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Vector createVector(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
			public virtual Vector createVector(ExtendedDataInput @in)
			{
				return new BasicLongVector(DATA_FORM.DF_VECTOR, @in);
			}
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Vector createPair(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
			public virtual Vector createPair(ExtendedDataInput @in)
			{
				return new BasicLongVector(DATA_FORM.DF_PAIR, @in);
			}
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Matrix createMatrix(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
			public virtual Matrix createMatrix(ExtendedDataInput @in)
			{
				return new BasicLongMatrix(@in);
			}
			public virtual Scalar createScalarWithDefaultValue()
			{
				return new BasicLong(0);
			}
			public virtual Vector createVectorWithDefaultValue(int size)
			{
				return new BasicLongVector(size);
			}
			public virtual Vector createPairWithDefaultValue()
			{
				return new BasicLongVector(DATA_FORM.DF_PAIR, 2);
			}
			public virtual Matrix createMatrixWithDefaultValue(int rows, int columns)
			{
				return new BasicLongMatrix(rows, columns);
			}
		}

		private class FloatFactory : TypeFactory
		{
			private readonly BasicEntityFactory outerInstance;

			public FloatFactory(BasicEntityFactory outerInstance)
			{
				this.outerInstance = outerInstance;
			}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Scalar createScalar(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
			public virtual Scalar createScalar(ExtendedDataInput @in)
			{
				return new BasicFloat(@in);
			}
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Vector createVector(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
			public virtual Vector createVector(ExtendedDataInput @in)
			{
				return new BasicFloatVector(DATA_FORM.DF_VECTOR, @in);
			}
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Vector createPair(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
			public virtual Vector createPair(ExtendedDataInput @in)
			{
				return new BasicFloatVector(DATA_FORM.DF_PAIR, @in);
			}
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Matrix createMatrix(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
			public virtual Matrix createMatrix(ExtendedDataInput @in)
			{
				return new BasicFloatMatrix(@in);
			}
			public virtual Scalar createScalarWithDefaultValue()
			{
				return new BasicFloat(0);
			}
			public virtual Vector createVectorWithDefaultValue(int size)
			{
				return new BasicFloatVector(size);
			}
			public virtual Vector createPairWithDefaultValue()
			{
				return new BasicFloatVector(DATA_FORM.DF_PAIR, 2);
			}
			public virtual Matrix createMatrixWithDefaultValue(int rows, int columns)
			{
				return new BasicFloatMatrix(rows, columns);
			}
		}

		private class DoubleFactory : TypeFactory
		{
			private readonly BasicEntityFactory outerInstance;

			public DoubleFactory(BasicEntityFactory outerInstance)
			{
				this.outerInstance = outerInstance;
			}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Scalar createScalar(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
			public virtual Scalar createScalar(ExtendedDataInput @in)
			{
				return new BasicDouble(@in);
			}
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Vector createVector(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
			public virtual Vector createVector(ExtendedDataInput @in)
			{
				return new BasicDoubleVector(DATA_FORM.DF_VECTOR, @in);
			}
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Vector createPair(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
			public virtual Vector createPair(ExtendedDataInput @in)
			{
				return new BasicDoubleVector(DATA_FORM.DF_PAIR, @in);
			}
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Matrix createMatrix(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
			public virtual Matrix createMatrix(ExtendedDataInput @in)
			{
				return new BasicDoubleMatrix(@in);
			}
			public virtual Scalar createScalarWithDefaultValue()
			{
				return new BasicDouble(0);
			}
			public virtual Vector createVectorWithDefaultValue(int size)
			{
				return new BasicDoubleVector(size);
			}
			public virtual Vector createPairWithDefaultValue()
			{
				return new BasicDoubleVector(DATA_FORM.DF_PAIR, 2);
			}
			public virtual Matrix createMatrixWithDefaultValue(int rows, int columns)
			{
				return new BasicDoubleMatrix(rows, columns);
			}
		}

		private class MinuteFactory : TypeFactory
		{
			private readonly BasicEntityFactory outerInstance;

			public MinuteFactory(BasicEntityFactory outerInstance)
			{
				this.outerInstance = outerInstance;
			}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Scalar createScalar(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
			public virtual Scalar createScalar(ExtendedDataInput @in)
			{
				return new BasicMinute(@in);
			}
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Vector createVector(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
			public virtual Vector createVector(ExtendedDataInput @in)
			{
				return new BasicMinuteVector(DATA_FORM.DF_VECTOR, @in);
			}
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Vector createPair(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
			public virtual Vector createPair(ExtendedDataInput @in)
			{
				return new BasicMinuteVector(DATA_FORM.DF_PAIR, @in);
			}
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Matrix createMatrix(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
			public virtual Matrix createMatrix(ExtendedDataInput @in)
			{
				return new BasicMinuteMatrix(@in);
			}
			public virtual Scalar createScalarWithDefaultValue()
			{
				return new BasicMinute(0);
			}
			public virtual Vector createVectorWithDefaultValue(int size)
			{
				return new BasicMinuteVector(size);
			}
			public virtual Vector createPairWithDefaultValue()
			{
				return new BasicMinuteVector(DATA_FORM.DF_PAIR, 2);
			}
			public virtual Matrix createMatrixWithDefaultValue(int rows, int columns)
			{
				return new BasicMinuteMatrix(rows, columns);
			}
		}

		private class SecondFactory : TypeFactory
		{
			private readonly BasicEntityFactory outerInstance;

			public SecondFactory(BasicEntityFactory outerInstance)
			{
				this.outerInstance = outerInstance;
			}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Scalar createScalar(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
			public virtual Scalar createScalar(ExtendedDataInput @in)
			{
				return new BasicSecond(@in);
			}
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Vector createVector(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
			public virtual Vector createVector(ExtendedDataInput @in)
			{
				return new BasicSecondVector(DATA_FORM.DF_VECTOR, @in);
			}
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Vector createPair(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
			public virtual Vector createPair(ExtendedDataInput @in)
			{
				return new BasicSecondVector(DATA_FORM.DF_PAIR, @in);
			}
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Matrix createMatrix(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
			public virtual Matrix createMatrix(ExtendedDataInput @in)
			{
				return new BasicSecondMatrix(@in);
			}
			public virtual Scalar createScalarWithDefaultValue()
			{
				return new BasicInt(0);
			}
			public virtual Vector createVectorWithDefaultValue(int size)
			{
				return new BasicSecondVector(size);
			}
			public virtual Vector createPairWithDefaultValue()
			{
				return new BasicSecondVector(DATA_FORM.DF_PAIR, 2);
			}
			public virtual Matrix createMatrixWithDefaultValue(int rows, int columns)
			{
				return new BasicSecondMatrix(rows, columns);
			}
		}

		private class TimeFactory : TypeFactory
		{
			private readonly BasicEntityFactory outerInstance;

			public TimeFactory(BasicEntityFactory outerInstance)
			{
				this.outerInstance = outerInstance;
			}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Scalar createScalar(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
			public virtual Scalar createScalar(ExtendedDataInput @in)
			{
				return new BasicTime(@in);
			}
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Vector createVector(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
			public virtual Vector createVector(ExtendedDataInput @in)
			{
				return new BasicTimeVector(DATA_FORM.DF_VECTOR, @in);
			}
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Vector createPair(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
			public virtual Vector createPair(ExtendedDataInput @in)
			{
				return new BasicTimeVector(DATA_FORM.DF_PAIR, @in);
			}
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Matrix createMatrix(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
			public virtual Matrix createMatrix(ExtendedDataInput @in)
			{
				return new BasicTimeMatrix(@in);
			}
			public virtual Scalar createScalarWithDefaultValue()
			{
				return new BasicTime(0);
			}
			public virtual Vector createVectorWithDefaultValue(int size)
			{
				return new BasicTimeVector(size);
			}
			public virtual Vector createPairWithDefaultValue()
			{
				return new BasicTimeVector(DATA_FORM.DF_PAIR, 2);
			}
			public virtual Matrix createMatrixWithDefaultValue(int rows, int columns)
			{
				return new BasicTimeMatrix(rows, columns);
			}
		}
		private class NanoTimeFactory : TypeFactory
		{
			private readonly BasicEntityFactory outerInstance;

			public NanoTimeFactory(BasicEntityFactory outerInstance)
			{
				this.outerInstance = outerInstance;
			}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Scalar createScalar(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
			public virtual Scalar createScalar(ExtendedDataInput @in)
			{
				return new BasicNanoTime(@in);
			}
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Vector createVector(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
			public virtual Vector createVector(ExtendedDataInput @in)
			{
				return new BasicNanoTimeVector(DATA_FORM.DF_VECTOR, @in);
			}
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Vector createPair(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
			public virtual Vector createPair(ExtendedDataInput @in)
			{
				return new BasicNanoTimeVector(DATA_FORM.DF_PAIR, @in);
			}
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Matrix createMatrix(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
			public virtual Matrix createMatrix(ExtendedDataInput @in)
			{
				return new BasicNanoTimeMatrix(@in);
			}
			public virtual Scalar createScalarWithDefaultValue()
			{
				return new BasicNanoTime(0);
			}
			public virtual Vector createVectorWithDefaultValue(int size)
			{
				return new BasicNanoTimeVector(size);
			}
			public virtual Vector createPairWithDefaultValue()
			{
				return new BasicNanoTimeVector(DATA_FORM.DF_PAIR, 2);
			}
			public virtual Matrix createMatrixWithDefaultValue(int rows, int columns)
			{
				return new BasicNanoTimeMatrix(rows, columns);
			}
		}

		private class DateFactory : TypeFactory
		{
			private readonly BasicEntityFactory outerInstance;

			public DateFactory(BasicEntityFactory outerInstance)
			{
				this.outerInstance = outerInstance;
			}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Scalar createScalar(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
			public virtual Scalar createScalar(ExtendedDataInput @in)
			{
				return new BasicDate(@in);
			}
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Vector createVector(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
			public virtual Vector createVector(ExtendedDataInput @in)
			{
				return new BasicDateVector(DATA_FORM.DF_VECTOR, @in);
			}
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Vector createPair(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
			public virtual Vector createPair(ExtendedDataInput @in)
			{
				return new BasicDateVector(DATA_FORM.DF_PAIR, @in);
			}
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Matrix createMatrix(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
			public virtual Matrix createMatrix(ExtendedDataInput @in)
			{
				return new BasicDateMatrix(@in);
			}
			public virtual Scalar createScalarWithDefaultValue()
			{
				return new BasicDate(0);
			}
			public virtual Vector createVectorWithDefaultValue(int size)
			{
				return new BasicDateVector(size);
			}
			public virtual Vector createPairWithDefaultValue()
			{
				return new BasicDateVector(DATA_FORM.DF_PAIR, 2);
			}
			public virtual Matrix createMatrixWithDefaultValue(int rows, int columns)
			{
				return new BasicDateMatrix(rows, columns);
			}
		}

		private class MonthFactory : TypeFactory
		{
			private readonly BasicEntityFactory outerInstance;

			public MonthFactory(BasicEntityFactory outerInstance)
			{
				this.outerInstance = outerInstance;
			}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Scalar createScalar(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
			public virtual Scalar createScalar(ExtendedDataInput @in)
			{
				return new BasicMonth(@in);
			}
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Vector createVector(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
			public virtual Vector createVector(ExtendedDataInput @in)
			{
				return new BasicMonthVector(DATA_FORM.DF_VECTOR, @in);
			}
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Vector createPair(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
			public virtual Vector createPair(ExtendedDataInput @in)
			{
				return new BasicMonthVector(DATA_FORM.DF_PAIR, @in);
			}
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Matrix createMatrix(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
			public virtual Matrix createMatrix(ExtendedDataInput @in)
			{
				return new BasicMonthMatrix(@in);
			}
			public virtual Scalar createScalarWithDefaultValue()
			{
				return new BasicMonth(0);
			}
			public virtual Vector createVectorWithDefaultValue(int size)
			{
				return new BasicMonthVector(size);
			}
			public virtual Vector createPairWithDefaultValue()
			{
				return new BasicMonthVector(DATA_FORM.DF_PAIR, 2);
			}
			public virtual Matrix createMatrixWithDefaultValue(int rows, int columns)
			{
				return new BasicMonthMatrix(rows, columns);
			}
		}

		private class DateTimeFactory : TypeFactory
		{
			private readonly BasicEntityFactory outerInstance;

			public DateTimeFactory(BasicEntityFactory outerInstance)
			{
				this.outerInstance = outerInstance;
			}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Scalar createScalar(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
			public virtual Scalar createScalar(ExtendedDataInput @in)
			{
				return new BasicDateTime(@in);
			}
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Vector createVector(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
			public virtual Vector createVector(ExtendedDataInput @in)
			{
				return new BasicDateTimeVector(DATA_FORM.DF_VECTOR, @in);
			}
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Vector createPair(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
			public virtual Vector createPair(ExtendedDataInput @in)
			{
				return new BasicDateTimeVector(DATA_FORM.DF_PAIR, @in);
			}
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Matrix createMatrix(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
			public virtual Matrix createMatrix(ExtendedDataInput @in)
			{
				return new BasicDateTimeMatrix(@in);
			}
			public virtual Scalar createScalarWithDefaultValue()
			{
				return new BasicDateTime(0);
			}
			public virtual Vector createVectorWithDefaultValue(int size)
			{
				return new BasicDateTimeVector(size);
			}
			public virtual Vector createPairWithDefaultValue()
			{
				return new BasicDateTimeVector(DATA_FORM.DF_PAIR, 2);
			}
			public virtual Matrix createMatrixWithDefaultValue(int rows, int columns)
			{
				return new BasicDateTimeMatrix(rows, columns);
			}
		}

		private class TimestampFactory : TypeFactory
		{
			private readonly BasicEntityFactory outerInstance;

			public TimestampFactory(BasicEntityFactory outerInstance)
			{
				this.outerInstance = outerInstance;
			}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Scalar createScalar(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
			public virtual Scalar createScalar(ExtendedDataInput @in)
			{
				return new BasicTimestamp(@in);
			}
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Vector createVector(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
			public virtual Vector createVector(ExtendedDataInput @in)
			{
				return new BasicTimestampVector(DATA_FORM.DF_VECTOR, @in);
			}
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Vector createPair(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
			public virtual Vector createPair(ExtendedDataInput @in)
			{
				return new BasicTimestampVector(DATA_FORM.DF_PAIR, @in);
			}
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Matrix createMatrix(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
			public virtual Matrix createMatrix(ExtendedDataInput @in)
			{
				return new BasicTimestampMatrix(@in);
			}
			public virtual Scalar createScalarWithDefaultValue()
			{
				return new BasicTimestamp(0);
			}
			public virtual Vector createVectorWithDefaultValue(int size)
			{
				return new BasicTimestampVector(size);
			}
			public virtual Vector createPairWithDefaultValue()
			{
				return new BasicTimestampVector(DATA_FORM.DF_PAIR, 2);
			}
			public virtual Matrix createMatrixWithDefaultValue(int rows, int columns)
			{
				return new BasicTimestampMatrix(rows, columns);
			}
		}
		private class NanoTimestampFactory : TypeFactory
		{
			private readonly BasicEntityFactory outerInstance;

			public NanoTimestampFactory(BasicEntityFactory outerInstance)
			{
				this.outerInstance = outerInstance;
			}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Scalar createScalar(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
			public virtual Scalar createScalar(ExtendedDataInput @in)
			{
				return new BasicNanoTimestamp(@in);
			}
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Vector createVector(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
			public virtual Vector createVector(ExtendedDataInput @in)
			{
				return new BasicNanoTimestampVector(DATA_FORM.DF_VECTOR, @in);
			}
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Vector createPair(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
			public virtual Vector createPair(ExtendedDataInput @in)
			{
				return new BasicNanoTimestampVector(DATA_FORM.DF_PAIR, @in);
			}
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Matrix createMatrix(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
			public virtual Matrix createMatrix(ExtendedDataInput @in)
			{
				return new BasicNanoTimestampMatrix(@in);
			}
			public virtual Scalar createScalarWithDefaultValue()
			{
				return new BasicNanoTimestamp(0);
			}
			public virtual Vector createVectorWithDefaultValue(int size)
			{
				return new BasicNanoTimestampVector(size);
			}
			public virtual Vector createPairWithDefaultValue()
			{
				return new BasicNanoTimestampVector(DATA_FORM.DF_PAIR, 2);
			}
			public virtual Matrix createMatrixWithDefaultValue(int rows, int columns)
			{
				return new BasicNanoTimestampMatrix(rows, columns);
			}
		}

		private class StringFactory : TypeFactory
		{
			private readonly BasicEntityFactory outerInstance;

			public StringFactory(BasicEntityFactory outerInstance)
			{
				this.outerInstance = outerInstance;
			}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Scalar createScalar(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
			public virtual Scalar createScalar(ExtendedDataInput @in)
			{
				return new BasicString(@in);
			}
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Vector createVector(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
			public virtual Vector createVector(ExtendedDataInput @in)
			{
				return new BasicStringVector(DATA_FORM.DF_VECTOR, @in);
			}
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Vector createPair(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
			public virtual Vector createPair(ExtendedDataInput @in)
			{
				return new BasicStringVector(DATA_FORM.DF_PAIR, @in);
			}
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Matrix createMatrix(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
			public virtual Matrix createMatrix(ExtendedDataInput @in)
			{
				return new BasicStringMatrix(@in);
			}
			public virtual Scalar createScalarWithDefaultValue()
			{
				return new BasicString("");
			}
			public virtual Vector createVectorWithDefaultValue(int size)
			{
				return new BasicStringVector(size);
			}
			public virtual Vector createPairWithDefaultValue()
			{
				return new BasicStringVector(DATA_FORM.DF_PAIR, 2, false);
			}
			public virtual Matrix createMatrixWithDefaultValue(int rows, int columns)
			{
				return new BasicStringMatrix(rows, columns);
			}
		}

		private class SymbolFactory : TypeFactory
		{
			private readonly BasicEntityFactory outerInstance;

			public SymbolFactory(BasicEntityFactory outerInstance)
			{
				this.outerInstance = outerInstance;
			}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Scalar createScalar(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
			public virtual Scalar createScalar(ExtendedDataInput @in)
			{
				return new BasicString(@in);
			}
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Vector createVector(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
			public virtual Vector createVector(ExtendedDataInput @in)
			{
				return new BasicStringVector(DATA_FORM.DF_VECTOR, @in);
			}
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Vector createPair(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
			public virtual Vector createPair(ExtendedDataInput @in)
			{
				return new BasicStringVector(DATA_FORM.DF_PAIR, @in);
			}
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Matrix createMatrix(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
			public virtual Matrix createMatrix(ExtendedDataInput @in)
			{
				return new BasicStringMatrix(@in);
			}
			public virtual Scalar createScalarWithDefaultValue()
			{
				return new BasicString("");
			}
			public virtual Vector createVectorWithDefaultValue(int size)
			{
				return new BasicStringVector(DATA_FORM.DF_VECTOR, size, true);
			}
			public virtual Vector createPairWithDefaultValue()
			{
				return new BasicStringVector(DATA_FORM.DF_PAIR, 2, true);
			}
			public virtual Matrix createMatrixWithDefaultValue(int rows, int columns)
			{
				return new BasicStringMatrix(rows, columns);
			}
		}

		private class FunctionDefFactory : StringFactory
		{
			private readonly BasicEntityFactory outerInstance;

			public FunctionDefFactory(BasicEntityFactory outerInstance) : base(outerInstance)
			{
				this.outerInstance = outerInstance;
			}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Scalar createScalar(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
			public override Scalar createScalar(ExtendedDataInput @in)
			{
				return new BasicSystemEntity(@in, DATA_TYPE.DT_FUNCTIONDEF);
			}
		}

		private class MetaCodeFactory : StringFactory
		{
			private readonly BasicEntityFactory outerInstance;

			public MetaCodeFactory(BasicEntityFactory outerInstance) : base(outerInstance)
			{
				this.outerInstance = outerInstance;
			}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Scalar createScalar(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
			public override Scalar createScalar(ExtendedDataInput @in)
			{
				return new BasicSystemEntity(@in, DATA_TYPE.DT_CODE);
			}
		}

		private class DataSourceFactory : StringFactory
		{
			private readonly BasicEntityFactory outerInstance;

			public DataSourceFactory(BasicEntityFactory outerInstance) : base(outerInstance)
			{
				this.outerInstance = outerInstance;
			}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Scalar createScalar(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
			public override Scalar createScalar(ExtendedDataInput @in)
			{
				return new BasicSystemEntity(@in, DATA_TYPE.DT_DATASOURCE);
			}
		}

		private class SystemHandleFactory : StringFactory
		{
			private readonly BasicEntityFactory outerInstance;

			public SystemHandleFactory(BasicEntityFactory outerInstance) : base(outerInstance)
			{
				this.outerInstance = outerInstance;
			}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Scalar createScalar(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
			public override Scalar createScalar(ExtendedDataInput @in)
			{
				return new BasicSystemEntity(@in, DATA_TYPE.DT_HANDLE);
			}
		}

		private class ResourceFactory : StringFactory
		{
			private readonly BasicEntityFactory outerInstance;

			public ResourceFactory(BasicEntityFactory outerInstance) : base(outerInstance)
			{
				this.outerInstance = outerInstance;
			}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Scalar createScalar(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
			public override Scalar createScalar(ExtendedDataInput @in)
			{
				return new BasicSystemEntity(@in, DATA_TYPE.DT_RESOURCE);
			}
		}


	}

}