namespace com.xxdb.data
{


	using ExtendedDataInput = com.xxdb.io.ExtendedDataInput;

	/// 
	/// <summary>
	/// Corresponds to DolphinDB time scalar.
	/// 
	/// </summary>

	public class BasicTime : BasicInt
	{
		private static DateTimeFormatter format = DateTimeFormatter.ofPattern("HH:mm:ss.SSS");

		public BasicTime(LocalTime value) : base(Utils.countMilliseconds(value))
		{
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public BasicTime(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
		public BasicTime(ExtendedDataInput @in) : base(@in)
		{
		}

		protected internal BasicTime(int value) : base(value)
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

		public virtual LocalTime Time
		{
			get
			{
				if (Null)
				{
					return null;
				}
				else
				{
					return Utils.parseTime(Int);
				}
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public java.time.temporal.Temporal getTemporal() throws Exception
		public override Temporal Temporal
		{
			get
			{
				return Time;
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
					return Time.format(format);
				}
			}
		}

		public override bool Equals(object o)
		{
			if (!(o is BasicTime) || o == null)
			{
				return false;
			}
			else
			{
				return Int == ((BasicTime)o).Int;
			}
		}
	}

}