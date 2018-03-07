using System;

namespace com.xxdb.data
{

	using ExtendedDataInput = com.xxdb.io.ExtendedDataInput;


	/// 
	/// <summary>
	/// Corresponds to DolphinDB nanotimestamp scalar
	/// 
	/// </summary>

	public class BasicNanoTimestamp : BasicLong
	{
		private static DateTimeFormatter format = DateTimeFormatter.ofPattern("yyyy.MM.dd'T'HH:mm:ss.SSSSSSSSS");

		public BasicNanoTimestamp(DateTime value) : base(Utils.countNanoseconds(value))
		{
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public BasicNanoTimestamp(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
		public BasicNanoTimestamp(ExtendedDataInput @in) : base(@in)
		{
		}

		protected internal BasicNanoTimestamp(long value) : base(value)
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
				return DATA_TYPE.DT_NANOTIMESTAMP;
			}
		}

		public virtual DateTime NanoTimestamp
		{
			get
			{
				if (Null)
				{
					return null;
				}
				else
				{
					return Utils.parseNanoTimestamp(Long);
				}
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public java.time.temporal.Temporal getTemporal() throws Exception
		public override Temporal Temporal
		{
			get
			{
				return NanoTimestamp;
			}
		}

		public override string String
		{
			get
			{
				if (Null)
				{
					return "";
				}
				else
				{
					return NanoTimestamp.format(format);
				}
			}
		}

		public override bool Equals(object o)
		{
			if (!(o is BasicNanoTimestamp) || o == null)
			{
				return false;
			}
			else
			{
				return Long == ((BasicNanoTimestamp)o).Long;
			}
		}
	}

}