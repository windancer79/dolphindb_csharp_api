using System;
using System.Collections.Generic;

namespace com.xxdb.data
{


	using ExtendedDataInput = com.xxdb.io.ExtendedDataInput;
	using ExtendedDataOutput = com.xxdb.io.ExtendedDataOutput;

	/// 
	/// <summary>
	/// Corresponds to DolphinDB char vector
	/// 
	/// </summary>

	public class BasicByteVector : AbstractVector
	{
		private sbyte[] values;

		public BasicByteVector(int size) : this(DATA_FORM.DF_VECTOR, size)
		{
		}

		public BasicByteVector(IList<sbyte?> list) : base(DATA_FORM.DF_VECTOR)
		{
			if (list != null)
			{
				values = new sbyte[list.Count];
				for (int i = 0; i < list.Count; ++i)
				{
					values[i] = list[i].Value;
				}
			}
		}

		public BasicByteVector(sbyte[] array) : base(DATA_FORM.DF_VECTOR)
		{
			values = array.Clone();
		}

		protected internal BasicByteVector(DATA_FORM df, int size) : base(df)
		{
			values = new sbyte[size];
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: protected BasicByteVector(Entity_DATA_FORM df, com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
		protected internal BasicByteVector(DATA_FORM df, ExtendedDataInput @in) : base(df)
		{
			int rows = @in.readInt();
			int cols = @in.readInt();
			int size = rows * cols;
			values = new sbyte[size];
			for (int i = 0; i < size; ++i)
			{
				values[i] = @in.readByte();
			}
		}

		public override Scalar get(int index)
		{
			return new BasicByte(values[index]);
		}

		public virtual sbyte getByte(int index)
		{
			return values[index];
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void set(int index, Scalar value) throws Exception
		public override void set(int index, Scalar value)
		{
			values[index] = value.Number.byteValue();
		}

		public virtual void setByte(int index, sbyte value)
		{
			values[index] = value;
		}

		public override bool isNull(int index)
		{
			return values[index] == sbyte.MinValue;
		}

		public override int Null
		{
			set
			{
				values[value] = sbyte.MinValue;
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
				return DATA_TYPE.DT_BYTE;
			}
		}

		public override Type ElementClass
		{
			get
			{
				return typeof(BasicByte);
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
			@out.write(values);
		}
	}

}