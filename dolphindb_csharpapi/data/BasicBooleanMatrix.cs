using System;
using System.Collections.Generic;

namespace com.xxdb.data
{


	using ExtendedDataInput = com.xxdb.io.ExtendedDataInput;
	using ExtendedDataOutput = com.xxdb.io.ExtendedDataOutput;

	/// 
	/// <summary>
	/// Corresponds to DolphinDB bool matrix
	/// 
	/// </summary>

	public class BasicBooleanMatrix : AbstractMatrix
	{
	private sbyte[] values;

		public BasicBooleanMatrix(int rows, int columns) : base(rows, columns)
		{
			values = new sbyte[rows * columns];
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public BasicBooleanMatrix(int rows, int columns, java.util.List<byte[]> listOfArrays) throws Exception
		public BasicBooleanMatrix(int rows, int columns, IList<sbyte[]> listOfArrays) : base(rows,columns)
		{
			values = new sbyte[rows * columns];
			if (listOfArrays == null || listOfArrays.Count != columns)
			{
				throw new Exception("input list of arrays does not have " + columns + " columns");
			}
			for (int i = 0; i < columns; ++i)
			{
				sbyte[] array = listOfArrays[i];
				if (array == null || array.Length != rows)
				{
					throw new Exception("The length of array " + (i + 1) + " doesn't have " + rows + " elements");
				}
				Array.Copy(array, 0, values, i * rows, rows);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public BasicBooleanMatrix(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
		public BasicBooleanMatrix(ExtendedDataInput @in) : base(@in)
		{
		}

		public virtual void setBoolean(int row, int column, bool value)
		{
			values[getIndex(row, column)] = value ? (sbyte)1 : (sbyte)0;
		}

		public virtual bool getBoolean(int row, int column)
		{
			return values[getIndex(row, column)] == 1;
		}

		public override bool isNull(int row, int column)
		{
			return values[getIndex(row, column)] == sbyte.MinValue;
		}

		public override void setNull(int row, int column)
		{
			values[getIndex(row, column)] = sbyte.MinValue;
		}

		public override Scalar get(int row, int column)
		{
			return new BasicBoolean(values[getIndex(row, column)]);
		}

		public override DATA_CATEGORY DataCategory
		{
			get
			{
				return DATA_CATEGORY.LOGICAL;
			}
		}

		public override DATA_TYPE DataType
		{
			get
			{
				return DATA_TYPE.DT_BOOL;
			}
		}

		public override Type ElementClass
		{
			get
			{
				return typeof(BasicBoolean);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override protected void readMatrixFromInputStream(int rows, int columns, com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
		protected internal override void readMatrixFromInputStream(int rows, int columns, ExtendedDataInput @in)
		{
			int size = rows * columns;
			values = new sbyte[size];
			for (int i = 0; i < size; ++i)
			{
				values[i] = @in.readByte();
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: protected void writeVectorToOutputStream(com.xxdb.io.ExtendedDataOutput out) throws java.io.IOException
		protected internal override void writeVectorToOutputStream(ExtendedDataOutput @out)
		{
			foreach (sbyte value in values)
			{
				@out.writeByte(value);
			}
		}
	}

}