using System;
using System.Collections.Generic;

namespace com.xxdb.data
{


	using ExtendedDataInput = com.xxdb.io.ExtendedDataInput;
	using ExtendedDataOutput = com.xxdb.io.ExtendedDataOutput;

	/// 
	/// <summary>
	/// Corresponds to DolphinDB short vector
	/// 
	/// </summary>

	public class BasicShortVector : AbstractVector
	{
		private short[] values;

		public BasicShortVector(int size) : this(DATA_FORM.DF_VECTOR, size)
		{
		}

		public BasicShortVector(IList<short?> list) : base(DATA_FORM.DF_VECTOR)
		{
			if (list != null)
			{
				values = new short[list.Count];
				for (int i = 0; i < list.Count; ++i)
				{
					values[i] = list[i].Value;
				}
			}
		}

		public BasicShortVector(short[] array) : base(DATA_FORM.DF_VECTOR)
		{
			values = array.Clone();
		}

		protected internal BasicShortVector(DATA_FORM df, int size) : base(df)
		{
			values = new short[size];
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: protected BasicShortVector(Entity_DATA_FORM df, com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
		protected internal BasicShortVector(DATA_FORM df, ExtendedDataInput @in) : base(df)
		{
			int rows = @in.readInt();
			int cols = @in.readInt();
			int size = rows * cols;
			values = new short[size];
			for (int i = 0; i < size; ++i)
			{
				values[i] = @in.readShort();
			}
		}

		public virtual short getShort(int index)
		{
			return values[index];
		}

		public override Scalar get(int index)
		{
			return new BasicShort(values[index]);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void set(int index, Scalar value) throws Exception
		public override void set(int index, Scalar value)
		{
			values[index] = value.Number.shortValue();
		}

		public virtual void setShort(int index, short value)
		{
			values[index] = value;
		}

		public override bool isNull(int index)
		{
			return values[index] == short.MinValue;
		}

		public override int Null
		{
			set
			{
				values[value] = short.MinValue;
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
				return DATA_TYPE.DT_SHORT;
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
				return typeof(BasicShort);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override protected void writeVectorToOutputStream(com.xxdb.io.ExtendedDataOutput out) throws java.io.IOException
		protected internal override void writeVectorToOutputStream(ExtendedDataOutput @out)
		{
			@out.writeShortArray(values);
		}
	}

}