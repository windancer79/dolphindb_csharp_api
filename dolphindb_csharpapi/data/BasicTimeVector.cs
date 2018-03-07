using System;
using System.Collections.Generic;

namespace com.xxdb.data
{

	using ExtendedDataInput = com.xxdb.io.ExtendedDataInput;

	/// 
	/// <summary>
	/// Corresponds to DolphinDB time vector
	/// 
	/// </summary>

	public class BasicTimeVector : BasicIntVector
	{

		public BasicTimeVector(int size) : base(DATA_FORM.DF_VECTOR, size)
		{
		}

		public BasicTimeVector(IList<int?> list) : base(list)
		{
		}

		public BasicTimeVector(int[] array) : base(array)
		{
		}

		protected internal BasicTimeVector(DATA_FORM df, int size) : base(df, size)
		{
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: protected BasicTimeVector(Entity_DATA_FORM df, com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
		protected internal BasicTimeVector(DATA_FORM df, ExtendedDataInput @in) : base(df, @in)
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
				return DATA_TYPE.DT_TIME;
			}
		}

		public override Scalar get(int index)
		{
			return new BasicTime(getInt(index));
		}

		public virtual LocalTime getTime(int index)
		{
			if (isNull(index))
			{
				return null;
			}
			else
			{
				return Utils.parseTime(getInt(index));
			}
		}

		public virtual void setTime(int index, LocalTime time)
		{
			setInt(index, Utils.countMilliseconds(time));
		}

		public override Type ElementClass
		{
			get
			{
				return typeof(BasicTime);
			}
		}
	}

}