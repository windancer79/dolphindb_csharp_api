using System;
using System.Collections.Generic;

namespace com.xxdb.data
{


	using ExtendedDataInput = com.xxdb.io.ExtendedDataInput;

	/// 
	/// <summary>
	/// Corresponds to DolphinDB datetime matrix
	/// 
	/// </summary>

	public class BasicDateTimeMatrix : BasicIntMatrix
	{
		public BasicDateTimeMatrix(int rows, int columns) : base(rows, columns)
		{
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public BasicDateTimeMatrix(int rows, int columns, java.util.List<int[]> listOfArrays) throws Exception
		public BasicDateTimeMatrix(int rows, int columns, IList<int[]> listOfArrays) : base(rows,columns, listOfArrays)
		{
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public BasicDateTimeMatrix(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
		public BasicDateTimeMatrix(ExtendedDataInput @in) : base(@in)
		{
		}

		public virtual void setDateTime(int row, int column, DateTime value)
		{
			setInt(row, column, Utils.countSeconds(value));
		}

		public virtual DateTime getDateTime(int row, int column)
		{
			return Utils.parseDateTime(getInt(row, column));
		}

		public override Scalar get(int row, int column)
		{
			return new BasicDateTime(getInt(row, column));
		}

		public override Type ElementClass
		{
			get
			{
				return typeof(BasicDateTime);
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
				return DATA_TYPE.DT_DATETIME;
			}
		}
	}

}