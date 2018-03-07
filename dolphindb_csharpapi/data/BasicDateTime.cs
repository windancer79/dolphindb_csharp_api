using System;

namespace com.xxdb.data
{


	using ExtendedDataInput = com.xxdb.io.ExtendedDataInput;

	/// 
	/// <summary>
	/// Corresponds to DolphinDB datetime scalar
	/// 
	/// </summary>

	public class BasicDateTime : BasicInt
	{
		private static DateTimeFormatter format = DateTimeFormatter.ofPattern("yyyy.MM.dd'T'HH:mm:ss");

		public BasicDateTime(DateTime value) : base(Utils.countSeconds(value))
		{
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public BasicDateTime(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
		public BasicDateTime(ExtendedDataInput @in) : base(@in)
		{
		}

		protected internal BasicDateTime(int value) : base(value)
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

		public virtual DateTime DateTime
		{
			get
			{
				if (Null)
				{
					return null;
				}
				else
				{
					return Utils.parseDateTime(Int);
				}
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public java.time.temporal.Temporal getTemporal() throws Exception
		public override Temporal Temporal
		{
			get
			{
				return DateTime;
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
					return DateTime.format(format);
				}
			}
		}

		public override bool Equals(object o)
		{
			if (!(o is BasicDateTime) || o == null)
			{
				return false;
			}
			else
			{
				return Int == ((BasicDateTime)o).Int;
			}
		}
	}

}