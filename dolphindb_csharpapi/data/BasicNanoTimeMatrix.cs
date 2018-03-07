using System;
using System.Collections.Generic;

namespace com.xxdb.data
{

	using ExtendedDataInput = com.xxdb.io.ExtendedDataInput;


	/// 
	/// <summary>
	/// Corresponds to DolphinDB nanotime matrix.
	/// 
	/// </summary>

	public class BasicNanoTimeMatrix : BasicLongMatrix
	{
		public BasicNanoTimeMatrix(int rows, int columns) : base(rows, columns)
		{
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public BasicNanoTimeMatrix(int rows, int columns, java.util.List<long[]> listOfArrays) throws Exception
		public BasicNanoTimeMatrix(int rows, int columns, IList<long[]> listOfArrays) : base(rows,columns, listOfArrays)
		{
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public BasicNanoTimeMatrix(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
		public BasicNanoTimeMatrix(ExtendedDataInput @in) : base(@in)
		{
		}

		public virtual void setNanoTime(int row, int column, LocalTime value)
		{
			setLong(row, column, Utils.countNanoseconds(value));
		}

		public virtual LocalTime getNanoTime(int row, int column)
		{
			return Utils.parseNanoTime(getLong(row, column));
		}

		public override Scalar get(int row, int column)
		{
			return new BasicNanoTime(getLong(row, column));
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
				return DATA_TYPE.DT_NANOTIME;
			}
		}

		public override Type ElementClass
		{
			get
			{
				return typeof(BasicNanoTime);
			}
		}
	}

}