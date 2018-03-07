using System;
using System.Collections.Generic;

namespace com.xxdb.data
{


	using ExtendedDataInput = com.xxdb.io.ExtendedDataInput;
	using ExtendedDataOutput = com.xxdb.io.ExtendedDataOutput;

	/// 
	/// <summary>
	/// Corresponds to DolphinDB int vector
	/// 
	/// </summary>

	public class BasicIntVector : AbstractVector
	{
		private int[] values;

		public BasicIntVector(int size) : this(DATA_FORM.DF_VECTOR, size)
		{
		}

		public BasicIntVector(IList<int?> list) : base(DATA_FORM.DF_VECTOR)
		{
			if (list != null)
			{
				values = new int[list.Count];
				for (int i = 0; i < list.Count; ++i)
				{
					values[i] = list[i].Value;
				}
			}
		}

		public BasicIntVector(int[] array) : base(DATA_FORM.DF_VECTOR)
		{
			values = array.Clone();
		}

		protected internal BasicIntVector(DATA_FORM df, int size) : base(df)
		{
			values = new int[size];
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: protected BasicIntVector(Entity_DATA_FORM df, com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
		protected internal BasicIntVector(DATA_FORM df, ExtendedDataInput @in) : base(df)
		{
			int rows = @in.readInt();
			//if (rows != 1024)
				//assert(rows == 1024);
			int cols = @in.readInt();
			int size = rows * cols;
			values = new int[size];
			for (int i = 0; i < size; ++i)
			{
				values[i] = @in.readInt();
			}
		}

		public override Scalar get(int index)
		{
			return new BasicInt(values[index]);
		}

		public virtual int getInt(int index)
		{
			return values[index];
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void set(int index, Scalar value) throws Exception
		public override void set(int index, Scalar value)
		{
			values[index] = value.Number.intValue();
		}

		public virtual void setInt(int index, int value)
		{
			values[index] = value;
		}

		public override bool isNull(int index)
		{
			return values[index] == int.MinValue;
		}

		public override int Null
		{
			set
			{
				values[value] = int.MinValue;
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
				return DATA_TYPE.DT_INT;
			}
		}

		public override Type ElementClass
		{
			get
			{
				return typeof(BasicInt);
			}
		}

		public override int rows()
		{
			return values.Length;
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: protected void writeVectorToOutputStream(com.xxdb.io.ExtendedDataOutput out) throws java.io.IOException
		protected internal override void writeVectorToOutputStream(ExtendedDataOutput @out)
		{
			@out.writeIntArray(values);
		}
	}

}