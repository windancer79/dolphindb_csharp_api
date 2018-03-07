using System;
using System.Collections.Generic;

namespace com.xxdb.data
{


	using ExtendedDataInput = com.xxdb.io.ExtendedDataInput;

	/// 
	/// <summary>
	/// Corresponds to DolphinDB date matrix
	/// 
	/// </summary>

	public class BasicDateMatrix : BasicIntMatrix
	{
		public BasicDateMatrix(int rows, int columns) : base(rows, columns)
		{
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public BasicDateMatrix(int rows, int columns, java.util.List<int[]> listOfArrays) throws Exception
		public BasicDateMatrix(int rows, int columns, IList<int[]> listOfArrays) : base(rows,columns, listOfArrays)
		{
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public BasicDateMatrix(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
		public BasicDateMatrix(ExtendedDataInput @in) : base(@in)
		{
		}

		public virtual void setDate(int row, int column, LocalDate value)
		{
			setInt(row, column, Utils.countDays(value));
		}

		public virtual LocalDate getDate(int row, int column)
		{
			return Utils.parseDate(getInt(row, column));
		}


		public override Scalar get(int row, int column)
		{
			return new BasicDate(getInt(row, column));
		}

		public override Type ElementClass
		{
			get
			{
				return typeof(BasicDate);
			}
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
				return DATA_TYPE.DT_DATE;
			}
		}
	}

}