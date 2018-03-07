namespace com.xxdb.data
{


	using ExtendedDataInput = com.xxdb.io.ExtendedDataInput;

	/// 
	/// <summary>
	/// Corresponds to DolphinDB minute scalar
	/// 
	/// </summary>

	public class BasicMinute : BasicInt
	{
		private static DateTimeFormatter format = DateTimeFormatter.ofPattern("HH:mm'm'");

		public BasicMinute(LocalTime value) : base(Utils.countMinutes(value))
		{
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public BasicMinute(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
		public BasicMinute(ExtendedDataInput @in) : base(@in)
		{
		}

		protected internal BasicMinute(int value) : base(value)
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

		public virtual LocalTime Minute
		{
			get
			{
				if (Null)
				{
					return null;
				}
				else
				{
					return Utils.parseMinute(Int);
				}
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public java.time.temporal.Temporal getTemporal() throws Exception
		public override Temporal Temporal
		{
			get
			{
				return Minute;
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
					return Minute.format(format);
				}
			}
		}

		public override bool Equals(object o)
		{
			if (!(o is BasicMinute) || o == null)
			{
				return false;
			}
			else
			{
				return Int == ((BasicMinute)o).Int;
			}
		}
	}

}