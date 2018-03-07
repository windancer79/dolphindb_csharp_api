using System;
using System.Collections.Generic;

namespace com.xxdb.data
{


	using ExtendedDataInput = com.xxdb.io.ExtendedDataInput;
	using ExtendedDataOutput = com.xxdb.io.ExtendedDataOutput;

	/// 
	/// <summary>
	/// Corresponds to DolphinDB long vector
	/// 
	/// </summary>

	public class BasicLongVector : AbstractVector
	{
		private long[] values;

		public BasicLongVector(int size) : this(DATA_FORM.DF_VECTOR, size)
		{
		}

		public BasicLongVector(IList<long?> list) : base(DATA_FORM.DF_VECTOR)
		{
			if (list != null)
			{
				values = new long[list.Count];
				for (int i = 0; i < list.Count; ++i)
				{
					values[i] = list[i].Value;
				}
			}
		}

		public BasicLongVector(long[] array) : base(DATA_FORM.DF_VECTOR)
		{
			values = array.Clone();
		}

		protected internal BasicLongVector(DATA_FORM df, int size) : base(df)
		{
			values = new long[size];
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: protected BasicLongVector(Entity_DATA_FORM df, com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
		protected internal BasicLongVector(DATA_FORM df, ExtendedDataInput @in) : base(df)
		{
			int rows = @in.readInt();
			int cols = @in.readInt();
			int size = rows * cols;
			values = new long[size];
			for (int i = 0; i < size; ++i)
			{
				values[i] = @in.readLong();
			}
		}

		public override Scalar get(int index)
		{
			return new BasicLong(values[index]);
		}

		public virtual long getLong(int index)
		{
			return values[index];
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void set(int index, Scalar value) throws Exception
		public override void set(int index, Scalar value)
		{
			values[index] = value.Number.longValue();
		}

		public virtual void setLong(int index, long value)
		{
			values[index] = value;
		}

		public override bool isNull(int index)
		{
			return values[index] == long.MinValue;
		}

		public override int Null
		{
			set
			{
				values[value] = long.MinValue;
			}
		}

		public override DATA_CATEGORY DataCategory
		{
			get
			{
				return DATA_CATEGORY.INTEGRAL;
			}
		}

		public override DATA_TYPE DataType
		{
			get
			{
				return DATA_TYPE.DT_LONG;
			}
		}

		public override int rows()
		{
			return values.Length;
		}

		public override Type ElementClass
		{
			get
			{
				return typeof(BasicLong);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: protected void writeVectorToOutputStream(com.xxdb.io.ExtendedDataOutput out) throws java.io.IOException
		protected internal override void writeVectorToOutputStream(ExtendedDataOutput @out)
		{
			@out.writeLongArray(values);
		}
	}

}