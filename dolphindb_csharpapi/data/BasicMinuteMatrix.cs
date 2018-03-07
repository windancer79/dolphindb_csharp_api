using System;
using System.Collections.Generic;

namespace com.xxdb.data
{


	using ExtendedDataInput = com.xxdb.io.ExtendedDataInput;

	/// 
	/// <summary>
	/// Corresponds to DolphinDB minute matrix
	/// 
	/// </summary>

	public class BasicMinuteMatrix : BasicIntMatrix
	{
		public BasicMinuteMatrix(int rows, int columns) : base(rows, columns)
		{
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public BasicMinuteMatrix(int rows, int columns, java.util.List<int[]> listOfArrays) throws Exception
		public BasicMinuteMatrix(int rows, int columns, IList<int[]> listOfArrays) : base(rows,columns, listOfArrays)
		{
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public BasicMinuteMatrix(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
		public BasicMinuteMatrix(ExtendedDataInput @in) : base(@in)
		{
		}

		public virtual void setMinute(int row, int column, LocalTime value)
		{
			setInt(row, column, Utils.countMinutes(value));
		}

		public virtual LocalTime getMinute(int row, int column)
		{
			return Utils.parseMinute(getInt(row, column));
		}


		public override Scalar get(int row, int column)
		{
			return new BasicMinute(getInt(row, column));
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
				return DATA_TYPE.DT_MINUTE;
			}
		}

		public override Type ElementClass
		{
			get
			{
				return typeof(BasicMinute);
			}
		}

	}

}