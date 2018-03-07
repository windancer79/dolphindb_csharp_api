using System;
using System.Collections.Generic;

namespace com.xxdb.data
{


	using ExtendedDataInput = com.xxdb.io.ExtendedDataInput;

	/// 
	/// <summary>
	/// Corresponds to DolphinDB second vector
	/// 
	/// </summary>

	public class BasicSecondVector : BasicIntVector
	{

		public BasicSecondVector(int size) : base(DATA_FORM.DF_VECTOR, size)
		{
		}

		public BasicSecondVector(IList<int?> list) : base(list)
		{
		}

		public BasicSecondVector(int[] array) : base(array)
		{
		}

		protected internal BasicSecondVector(DATA_FORM df, int size) : base(df, size)
		{
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: protected BasicSecondVector(Entity_DATA_FORM df, com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
		protected internal BasicSecondVector(DATA_FORM df, ExtendedDataInput @in) : base(df, @in)
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
				return DATA_TYPE.DT_SECOND;
			}
		}

		public override Scalar get(int index)
		{
			return new BasicSecond(getInt(index));
		}

		public virtual LocalTime getSecond(int index)
		{
			if (isNull(index))
			{
				return null;
			}
			else
			{
				return Utils.parseSecond(getInt(index));
			}
		}

		public virtual void setSecond(int index, LocalTime time)
		{
			setInt(index, Utils.countSeconds(time));
		}

		public override Type ElementClass
		{
			get
			{
				return typeof(BasicSecond);
			}
		}
	}

}