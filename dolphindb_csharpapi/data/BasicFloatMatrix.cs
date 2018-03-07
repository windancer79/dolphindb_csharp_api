using System;
using System.Collections.Generic;

namespace com.xxdb.data
{


	using ExtendedDataInput = com.xxdb.io.ExtendedDataInput;
	using ExtendedDataOutput = com.xxdb.io.ExtendedDataOutput;

	/// 
	/// <summary>
	/// Corresponds to DolphinDB float matrix
	/// 
	/// </summary>

	public class BasicFloatMatrix : AbstractMatrix
	{
		private float[] values;

		public BasicFloatMatrix(int rows, int columns) : base(rows, columns)
		{
			values = new float[rows * columns];
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public BasicFloatMatrix(int rows, int columns, java.util.List<float[]> listOfArrays) throws Exception
		public BasicFloatMatrix(int rows, int columns, IList<float[]> listOfArrays) : base(rows,columns)
		{
			values = new float[rows * columns];
			if (listOfArrays == null || listOfArrays.Count != columns)
			{
				throw new Exception("input list of arrays does not have " + columns + " columns");
			}
			for (int i = 0; i < columns; ++i)
			{
				float[] array = listOfArrays[i];
				if (array == null || array.Length != rows)
				{
					throw new Exception("The length of array " + (i + 1) + " doesn't have " + rows + " elements");
				}
				Array.Copy(array, 0, values, i * rows, rows);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public BasicFloatMatrix(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
		public BasicFloatMatrix(ExtendedDataInput @in) : base(@in)
		{
		}

		public virtual void setFloat(int row, int column, float value)
		{
			values[getIndex(row, column)] = value;
		}

		public virtual float getFloat(int row, int column)
		{
			return values[getIndex(row, column)];
		}

		public override bool isNull(int row, int column)
		{
			return values[getIndex(row, column)] == -float.MaxValue;
		}

		public override void setNull(int row, int column)
		{
			values[getIndex(row, column)] = -float.MaxValue;
		}

		public override Scalar get(int row, int column)
		{
			return new BasicFloat(values[getIndex(row, column)]);
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

		public override Type ElementClass
		{
			get
			{
				return typeof(BasicFloat);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override protected void readMatrixFromInputStream(int rows, int columns, com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
		protected internal override void readMatrixFromInputStream(int rows, int columns, ExtendedDataInput @in)
		{
			int size = rows * columns;
			values = new float[size];
			for (int i = 0; i < size; ++i)
			{
				values[i] = @in.readFloat();
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: protected void writeVectorToOutputStream(com.xxdb.io.ExtendedDataOutput out) throws java.io.IOException
		protected internal override void writeVectorToOutputStream(ExtendedDataOutput @out)
		{
			foreach (float value in values)
			{
				@out.writeFloat(value);
			}
		}
	}

}