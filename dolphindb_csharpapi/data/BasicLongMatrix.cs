using System;
using System.Collections.Generic;

namespace com.xxdb.data
{


	using ExtendedDataInput = com.xxdb.io.ExtendedDataInput;
	using ExtendedDataOutput = com.xxdb.io.ExtendedDataOutput;

	/// 
	/// <summary>
	/// Corresponds to DolphinDB long matrix
	/// 
	/// </summary>

	public class BasicLongMatrix : AbstractMatrix
	{
		private long[] values;

		public BasicLongMatrix(int rows, int columns) : base(rows, columns)
		{
			values = new long[rows * columns];
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public BasicLongMatrix(int rows, int columns, java.util.List<long[]> list) throws Exception
		public BasicLongMatrix(int rows, int columns, IList<long[]> list) : base(rows,columns)
		{
			values = new long[rows * columns];
			if (list == null || list.Count != columns)
			{
				throw new Exception("input list of arrays does not have " + columns + " columns");
			}
			for (int i = 0; i < columns; ++i)
			{
				long[] array = list[i];
				if (array == null || array.Length != rows)
				{
					throw new Exception("The length of array " + (i + 1) + " doesn't have " + rows + " elements");
				}
				Array.Copy(array, 0, values, i * rows, rows);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public BasicLongMatrix(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
		public BasicLongMatrix(ExtendedDataInput @in) : base(@in)
		{
		}

		public virtual void setLong(int row, int column, long value)
		{
			values[getIndex(row, column)] = value;
		}

		public virtual long getLong(int row, int column)
		{
			return values[getIndex(row, column)];
		}

		public override bool isNull(int row, int column)
		{
			return values[getIndex(row, column)] == long.MinValue;
		}

		public override void setNull(int row, int column)
		{
			values[getIndex(row, column)] = long.MinValue;
		}

		public override Scalar get(int row, int column)
		{
			return new BasicLong(values[getIndex(row, column)]);
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

		public override Type ElementClass
		{
			get
			{
				return typeof(BasicLong);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override protected void readMatrixFromInputStream(int rows, int columns, com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
		protected internal override void readMatrixFromInputStream(int rows, int columns, ExtendedDataInput @in)
		{
			int size = rows * columns;
			values = new long[size];
			for (int i = 0; i < size; ++i)
			{
				values[i] = @in.readLong();
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: protected void writeVectorToOutputStream(com.xxdb.io.ExtendedDataOutput out) throws java.io.IOException
		protected internal override void writeVectorToOutputStream(ExtendedDataOutput @out)
		{
			foreach (long value in values)
			{
				@out.writeLong(value);
			}
		}

	}

}