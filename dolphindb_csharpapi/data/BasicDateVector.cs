using System;
using System.Collections.Generic;

namespace com.xxdb.data
{


	using ExtendedDataInput = com.xxdb.io.ExtendedDataInput;

	/// 
	/// <summary>
	/// Corresponds to DolphinDB date vector
	/// 
	/// </summary>

	public class BasicDateVector : BasicIntVector
	{

		public BasicDateVector(int size) : base(size)
		{
		}

		public BasicDateVector(IList<int?> list) : base(list)
		{
		}

		public BasicDateVector(int[] array) : base(array)
		{
		}

		protected internal BasicDateVector(DATA_FORM df, int size) : base(df,size)
		{
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: protected BasicDateVector(Entity_DATA_FORM df, com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
		protected internal BasicDateVector(DATA_FORM df, ExtendedDataInput @in) : base(df, @in)
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
				return DATA_TYPE.DT_DATE;
			}
		}

		public override Scalar get(int index)
		{
			return new BasicDate(getInt(index));
		}

		public virtual LocalDate getDate(int index)
		{
			if (isNull(index))
			{
				return null;
			}
			else
			{
				return Utils.parseDate(getInt(index));
			}
		}

		public virtual void setDate(int index, LocalDate date)
		{
			setInt(index,Utils.countDays(date));
		}

		public override Type ElementClass
		{
			get
			{
				return typeof(BasicDate);
			}
		}
	}

}