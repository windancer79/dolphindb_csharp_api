using System;
using System.Collections.Generic;

namespace com.xxdb.data
{


	using ExtendedDataInput = com.xxdb.io.ExtendedDataInput;

	/// 
	/// <summary>
	/// Corresponds to DolphinDB timestamp vector
	/// 
	/// </summary>

	public class BasicTimestampVector : BasicLongVector
	{

		public BasicTimestampVector(int size) : base(size)
		{
		}

		public BasicTimestampVector(IList<long?> list) : base(list)
		{
		}

		public BasicTimestampVector(long[] array) : base(array)
		{
		}

		protected internal BasicTimestampVector(DATA_FORM df, int size) : base(df, size)
		{
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: protected BasicTimestampVector(Entity_DATA_FORM df, com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
		protected internal BasicTimestampVector(DATA_FORM df, ExtendedDataInput @in) : base(df, @in)
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
				return DATA_TYPE.DT_TIMESTAMP;
			}
		}

		public override Scalar get(int index)
		{
			return new BasicTimestamp(getLong(index));
		}

		public virtual DateTime getTimestamp(int index)
		{
			if (isNull(index))
			{
				return null;
			}
			else
			{
				return Utils.parseTimestamp(getLong(index));
			}
		}

		public virtual void setTimestamp(int index, DateTime dt)
		{
			setLong(index, Utils.countMilliseconds(dt));
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