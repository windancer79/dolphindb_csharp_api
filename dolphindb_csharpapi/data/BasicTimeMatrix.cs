using System;
using System.Collections.Generic;

namespace com.xxdb.data
{


	using ExtendedDataInput = com.xxdb.io.ExtendedDataInput;

	/// 
	/// <summary>
	/// Corresponds to DolphinDB time matrix.
	/// 
	/// </summary>

	public class BasicTimeMatrix : BasicIntMatrix
	{
		public BasicTimeMatrix(int rows, int columns) : base(rows, columns)
		{
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public BasicTimeMatrix(int rows, int columns, java.util.List<int[]> listOfArrays) throws Exception
		public BasicTimeMatrix(int rows, int columns, IList<int[]> listOfArrays) : base(rows,columns, listOfArrays)
		{
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public BasicTimeMatrix(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
		public BasicTimeMatrix(ExtendedDataInput @in) : base(@in)
		{
		}

		public virtual void setTime(int row, int column, LocalTime value)
		{
			setInt(row, column, Utils.countMilliseconds(value));
		}

		public virtual LocalTime getTime(int row, int column)
		{
			return Utils.parseTime(getInt(row, column));
		}

		public override Scalar get(int row, int column)
		{
			return new BasicTime(getInt(row, column));
		}

		public override DATA_CATEGORY DataCategory
		{
			get
			{
				return DATA_CATEGORY.TEMPORAL;
			}
		}

		public override DATA_TYPE DataType
		{
			get
			{
				return DATA_TYPE.DT_TIME;
			}
		}

		public override Type ElementClass
		{
			get
			{
				return typeof(BasicTime);
			}
		}
	}

}