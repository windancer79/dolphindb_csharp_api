using System;
using System.Collections.Generic;

namespace com.xxdb.data
{


	using ExtendedDataInput = com.xxdb.io.ExtendedDataInput;
	using ExtendedDataOutput = com.xxdb.io.ExtendedDataOutput;

	/// 
	/// <summary>
	/// Corresponds to DolphinDB double vector
	/// 
	/// </summary>

	public class BasicDoubleVector : AbstractVector
	{
		private double[] values;

		public BasicDoubleVector(int size) : this(DATA_FORM.DF_VECTOR, size)
		{
		}

		public BasicDoubleVector(IList<double?> list) : base(DATA_FORM.DF_VECTOR)
		{
			if (list != null)
			{
				values = new double[list.Count];
				for (int i = 0; i < list.Count; ++i)
				{
					values[i] = list[i].Value;
				}
			}
		}

		public BasicDoubleVector(double[] array) : base(DATA_FORM.DF_VECTOR)
		{
			values = array.Clone();
		}

		protected internal BasicDoubleVector(DATA_FORM df, int size) : base(df)
		{
			values = new double[size];
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: protected BasicDoubleVector(Entity_DATA_FORM df, com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
		protected internal BasicDoubleVector(DATA_FORM df, ExtendedDataInput @in) : base(df)
		{
			int rows = @in.readInt();
			int cols = @in.readInt();
			int size = rows * cols;
			values = new double[size];
			for (int i = 0; i < size; ++i)
			{
				values[i] = @in.readDouble();
			}
		}

		public override Scalar get(int index)
		{
			return new BasicDouble(values[index]);
		}

		public virtual double getDouble(int index)
		{
			return values[index];
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void set(int index, Scalar value) throws Exception
		public override void set(int index, Scalar value)
		{
			values[index] = value.Number.doubleValue();
		}

		public virtual void setDouble(int index, double value)
		{
			values[index] = value;
		}

		public override bool isNull(int index)
		{
			return values[index] == -double.MaxValue;
		}

		public override int Null
		{
			set
			{
				values[value] = -double.MaxValue;
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
				return DATA_TYPE.DT_DOUBLE;
			}
		}

		public override Type ElementClass
		{
			get
			{
				return typeof(BasicDouble);
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
			@out.writeDoubleArray(values);
		}

	}

}