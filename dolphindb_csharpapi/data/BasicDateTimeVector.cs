using System;
using System.Collections.Generic;

namespace com.xxdb.data
{


	using ExtendedDataInput = com.xxdb.io.ExtendedDataInput;

	/// 
	/// <summary>
	/// Corresponds to DolphinDB datetime vector
	/// 
	/// </summary>

	public class BasicDateTimeVector : BasicIntVector
	{

		public BasicDateTimeVector(int size) : base(size)
		{
		}

		public BasicDateTimeVector(IList<int?> list) : base(list)
		{
		}

		public BasicDateTimeVector(int[] array) : base(array)
		{
		}

		protected internal BasicDateTimeVector(DATA_FORM df, int size) : base(df,size)
		{
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: protected BasicDateTimeVector(Entity_DATA_FORM df, com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
		protected internal BasicDateTimeVector(DATA_FORM df, ExtendedDataInput @in) : base(df, @in)
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
				return DATA_TYPE.DT_DATETIME;
			}
		}

		public override Scalar get(int index)
		{
			return new BasicDateTime(getInt(index));
		}

		public virtual DateTime getDateTime(int index)
		{
			if (isNull(index))
			{
				return null;
			}
			else
			{
				return Utils.parseDateTime(getInt(index));
			}
		}

		public virtual void setDateTime(int index, DateTime dt)
		{
			setInt(index,Utils.countSeconds(dt));
		}

		public override Type ElementClass
		{
			get
			{
				return typeof(BasicDateTime);
			}
		}

	}

}