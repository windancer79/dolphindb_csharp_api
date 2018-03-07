using System;
using System.Collections.Generic;

namespace com.xxdb.data
{


	using ExtendedDataInput = com.xxdb.io.ExtendedDataInput;

	/// 
	/// <summary>
	/// Corresponds to DolphinDB minute vector
	/// 
	/// </summary>

	public class BasicMinuteVector : BasicIntVector
	{

		public BasicMinuteVector(int size) : base(DATA_FORM.DF_VECTOR, size)
		{
		}

		public BasicMinuteVector(IList<int?> list) : base(list)
		{
		}

		public BasicMinuteVector(int[] array) : base(array)
		{
		}

		protected internal BasicMinuteVector(DATA_FORM df, int size) : base(df, size)
		{
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: protected BasicMinuteVector(Entity_DATA_FORM df, com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
		protected internal BasicMinuteVector(DATA_FORM df, ExtendedDataInput @in) : base(df, @in)
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
				return DATA_TYPE.DT_MINUTE;
			}
		}

		public override Scalar get(int index)
		{
			return new BasicMinute(getInt(index));
		}

		public virtual LocalTime getMinute(int index)
		{
			if (isNull(index))
			{
				return null;
			}
			else
			{
				return Utils.parseMinute(getInt(index));
			}
		}

		public virtual void setMinute(int index, LocalTime time)
		{
			setInt(index, Utils.countMinutes(time));
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