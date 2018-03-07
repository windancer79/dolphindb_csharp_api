using System;

namespace com.xxdb.data
{


	using ExtendedDataInput = com.xxdb.io.ExtendedDataInput;

	/// 
	/// <summary>
	/// Corresponds to DolphinDB timestamp scalar
	/// 
	/// </summary>

	public class BasicTimestamp : BasicLong
	{
		private static DateTimeFormatter format = DateTimeFormatter.ofPattern("yyyy.MM.dd'T'HH:mm:ss.SSS");

		public BasicTimestamp(DateTime value) : base(Utils.countMilliseconds(value))
		{
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public BasicTimestamp(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
		public BasicTimestamp(ExtendedDataInput @in) : base(@in)
		{
		}

		protected internal BasicTimestamp(long value) : base(value)
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

		public virtual DateTime Timestamp
		{
			get
			{
				if (Null)
				{
					return null;
				}
				else
				{
					return Utils.parseTimestamp(Long);
				}
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public java.time.temporal.Temporal getTemporal() throws Exception
		public override Temporal Temporal
		{
			get
			{
				return Timestamp;
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
					return Timestamp.format(format);
				}
			}
		}

		public override bool Equals(object o)
		{
			if (!(o is BasicTimestamp) || o == null)
			{
				return false;
			}
			else
			{
				return Long == ((BasicTimestamp)o).Long;
			}
		}
	}

}