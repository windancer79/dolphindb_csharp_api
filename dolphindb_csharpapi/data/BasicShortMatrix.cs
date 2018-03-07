using System;
using System.Collections.Generic;

namespace com.xxdb.data
{


	using ExtendedDataInput = com.xxdb.io.ExtendedDataInput;
	using ExtendedDataOutput = com.xxdb.io.ExtendedDataOutput;

	/// 
	/// <summary>
	/// Corresponds to DolphinDB short matrix
	/// 
	/// </summary>

	public class BasicShortMatrix : AbstractMatrix
	{
		private short[] values;

		public BasicShortMatrix(int rows, int columns) : base(rows, columns)
		{
			values = new short[rows * columns];
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public BasicShortMatrix(int rows, int columns, java.util.List<short[]> list) throws Exception
		public BasicShortMatrix(int rows, int columns, IList<short[]> list) : base(rows,columns)
		{
			values = new short[rows * columns];
			if (list == null || list.Count != columns)
			{
				throw new Exception("input list of arrays does not have " + columns + " columns");
			}
			for (int i = 0; i < columns; ++i)
			{
				short[] array = list[i];
				if (array == null || array.Length != rows)
				{
					throw new Exception("The length of array " + (i + 1) + " doesn't have " + rows + " elements");
				}
				Array.Copy(array, 0, values, i * rows, rows);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public BasicShortMatrix(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
		public BasicShortMatrix(ExtendedDataInput @in) : base(@in)
		{
		}

		public virtual void setShort(int row, int column, short value)
		{
			values[getIndex(row, column)] = value;
		}

		public virtual short getShort(int row, int column)
		{
			return values[getIndex(row, column)];
		}

		public override bool isNull(int row, int column)
		{
			return values[getIndex(row, column)] == short.MinValue;
		}

		public override void setNull(int row, int column)
		{
			values[getIndex(row, column)] = short.MinValue;
		}

		public override Scalar get(int row, int column)
		{
			return new BasicShort(values[getIndex(row, column)]);
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

		public override Type ElementClass
		{
			get
			{
				return typeof(BasicShort);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override protected void readMatrixFromInputStream(int rows, int columns, com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
		protected internal override void readMatrixFromInputStream(int rows, int columns, ExtendedDataInput @in)
		{
			int size = rows * columns;
			values = new short[size];
			for (int i = 0; i < size; ++i)
			{
				values[i] = @in.readShort();
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: protected void writeVectorToOutputStream(com.xxdb.io.ExtendedDataOutput out) throws java.io.IOException
		protected internal override void writeVectorToOutputStream(ExtendedDataOutput @out)
		{
			foreach (short value in values)
			{
				@out.writeInt(value);
			}
		}
	}

}