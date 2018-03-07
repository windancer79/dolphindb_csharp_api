using System;
using System.Collections.Generic;

namespace com.xxdb.data
{


	using ExtendedDataInput = com.xxdb.io.ExtendedDataInput;
	using ExtendedDataOutput = com.xxdb.io.ExtendedDataOutput;

	/// 
	/// <summary>
	/// Corresponds to DolphinDB float vector
	/// 
	/// </summary>

	public class BasicFloatVector : AbstractVector
	{
		private float[] values;

		public BasicFloatVector(int size) : this(DATA_FORM.DF_VECTOR, size)
		{
		}

		public BasicFloatVector(IList<float?> list) : base(DATA_FORM.DF_VECTOR)
		{
			if (list != null)
			{
				values = new float[list.Count];
				for (int i = 0; i < list.Count; ++i)
				{
					values[i] = list[i].Value;
				}
			}
		}

		public BasicFloatVector(float[] array) : base(DATA_FORM.DF_VECTOR)
		{
			values = array.Clone();
		}

		protected internal BasicFloatVector(DATA_FORM df, int size) : base(df)
		{
			values = new float[size];
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: protected BasicFloatVector(Entity_DATA_FORM df, com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
		protected internal BasicFloatVector(DATA_FORM df, ExtendedDataInput @in) : base(df)
		{
			int rows = @in.readInt();
			int cols = @in.readInt();
			int size = rows * cols;
			values = new float[size];
			for (int i = 0; i < size; ++i)
			{
				values[i] = @in.readFloat();
			}
		}

		public override Scalar get(int index)
		{
			return new BasicFloat(values[index]);
		}

		public virtual float getFloat(int index)
		{
			return values[index];
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void set(int index, Scalar value) throws Exception
		public override void set(int index, Scalar value)
		{
			values[index] = value.Number.floatValue();
		}

		public virtual void setFloat(int index, float value)
		{
			values[index] = value;
		}

		public override bool isNull(int index)
		{
			return values[index] == -float.MaxValue;
		}

		public override int Null
		{
			set
			{
				values[value] = -float.MaxValue;
			}
		}

		public override DATA_CATEGORY DataCategory
		{
			get
			{
				return DATA_CATEGORY.FLOATING;
			}
		}

		public override DATA_TYPE DataType
		{
			get
			{
				return DATA_TYPE.DT_FLOAT;
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
				return typeof(BasicFloat);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: protected void writeVectorToOutputStream(com.xxdb.io.ExtendedDataOutput out) throws java.io.IOException
		protected internal override void writeVectorToOutputStream(ExtendedDataOutput @out)
		{
			@out.writeFloatArray(values);
		}
	}

}