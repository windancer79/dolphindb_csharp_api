using System;
using System.Collections.Generic;

namespace com.xxdb.data
{


	using ExtendedDataInput = com.xxdb.io.ExtendedDataInput;


	/// 
	/// <summary>
	/// Corresponds to DolphinDB month vector
	/// 
	/// </summary>

	public class BasicMonthVector : BasicIntVector
	{

		public BasicMonthVector(int size) : base(DATA_FORM.DF_VECTOR, size)
		{
		}

		public BasicMonthVector(IList<int?> list) : base(list)
		{
		}

		public BasicMonthVector(int[] array) : base(array)
		{
		}

		protected internal BasicMonthVector(DATA_FORM df, int size) : base(df, size)
		{
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: protected BasicMonthVector(Entity_DATA_FORM df, com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
		protected internal BasicMonthVector(DATA_FORM df, ExtendedDataInput @in) : base(df, @in)
		{
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

		public override Scalar get(int index)
		{
			return new BasicMonth(getInt(index));
		}

		public virtual YearMonth getMonth(int index)
		{
			return Utils.parseMonth(getInt(index));
		}

		public virtual void setMonth(int index, YearMonth month)
		{
			setInt(index, Utils.countMonths(month));
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