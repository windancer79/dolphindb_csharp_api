using System;
using System.Collections.Generic;

namespace com.xxdb.data
{


	using ExtendedDataInput = com.xxdb.io.ExtendedDataInput;

	/// 
	/// <summary>
	/// Corresponds to DolphinDB month matrix
	/// 
	/// </summary>

	public class BasicMonthMatrix : BasicIntMatrix
	{
		public BasicMonthMatrix(int rows, int columns) : base(rows, columns)
		{
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public BasicMonthMatrix(int rows, int columns, java.util.List<int[]> listOfArrays) throws Exception
		public BasicMonthMatrix(int rows, int columns, IList<int[]> listOfArrays) : base(rows,columns, listOfArrays)
		{
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public BasicMonthMatrix(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
		public BasicMonthMatrix(ExtendedDataInput @in) : base(@in)
		{
		}

		public virtual void setMonth(int row, int column, YearMonth value)
		{
			setInt(row, column, Utils.countMonths(value));
		}

		public virtual YearMonth getMonth(int row, int column)
		{
			return Utils.parseMonth(getInt(row, column));
		}


		public override Scalar get(int row, int column)
		{
			return new BasicMonth(getInt(row, column));
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
				return DATA_TYPE.DT_MONTH;
			}
		}

		public override Type ElementClass
		{
			get
			{
				return typeof(YearMonth);
			}
		}

	}

}