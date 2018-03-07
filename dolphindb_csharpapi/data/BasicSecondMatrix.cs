using System;
using System.Collections.Generic;

namespace com.xxdb.data
{


	using ExtendedDataInput = com.xxdb.io.ExtendedDataInput;


	/// 
	/// <summary>
	/// Corresponds to DolphinDB second matrix
	/// 
	/// </summary>

	public class BasicSecondMatrix : BasicIntMatrix
	{
		public BasicSecondMatrix(int rows, int columns) : base(rows, columns)
		{
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public BasicSecondMatrix(int rows, int columns, java.util.List<int[]> listOfArrays) throws Exception
		public BasicSecondMatrix(int rows, int columns, IList<int[]> listOfArrays) : base(rows,columns, listOfArrays)
		{
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public BasicSecondMatrix(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
		public BasicSecondMatrix(ExtendedDataInput @in) : base(@in)
		{
		}

		public virtual void setSecond(int row, int column, LocalTime value)
		{
			setInt(row, column, Utils.countSeconds(value));
		}

		public virtual LocalTime getSecond(int row, int column)
		{
			return Utils.parseSecond(getInt(row, column));
		}

		public override Scalar get(int row, int column)
		{
			return new BasicSecond(getInt(row, column));
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
				return DATA_TYPE.DT_SECOND;
			}
		}

		public override Type ElementClass
		{
			get
			{
				return typeof(BasicSecond);
			}
		}
	}

}