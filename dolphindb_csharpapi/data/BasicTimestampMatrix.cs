using System;
using System.Collections.Generic;

namespace com.xxdb.data
{


	using ExtendedDataInput = com.xxdb.io.ExtendedDataInput;
	/// 
	/// <summary>
	/// Corresponds to DolphinDB timestamp matrix
	/// 
	/// </summary>

	public class BasicTimestampMatrix : BasicLongMatrix
	{
		public BasicTimestampMatrix(int rows, int columns) : base(rows, columns)
		{
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public BasicTimestampMatrix(int rows, int columns, java.util.List<long[]> listOfArrays) throws Exception
		public BasicTimestampMatrix(int rows, int columns, IList<long[]> listOfArrays) : base(rows,columns, listOfArrays)
		{
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public BasicTimestampMatrix(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
		public BasicTimestampMatrix(ExtendedDataInput @in) : base(@in)
		{
		}

		public virtual void setTimestamp(int row, int column, DateTime value)
		{
			setLong(row, column, Utils.countMilliseconds(value));
		}

		public virtual DateTime getTimestamp(int row, int column)
		{
			return Utils.parseTimestamp(getLong(row, column));
		}


		public override Scalar get(int row, int column)
		{
			return new BasicTimestamp(getLong(row, column));
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
				return DATA_TYPE.DT_TIMESTAMP;
			}
		}

		public override Type ElementClass
		{
			get
			{
				return typeof(BasicTimestamp);
			}
		}

	}

}