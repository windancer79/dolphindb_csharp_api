namespace com.xxdb.data
{

	using ExtendedDataInput = com.xxdb.io.ExtendedDataInput;


	/// <summary>
	/// Corresponds to DolphinDB nanotime scalar.
	/// 
	/// </summary>

	public class BasicNanoTime : BasicLong
	{
		private static DateTimeFormatter format = DateTimeFormatter.ofPattern("HH:mm:ss.SSSSSSSSS");

		public BasicNanoTime(LocalTime value) : base(Utils.countNanoseconds(value))
		{
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public BasicNanoTime(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
		public BasicNanoTime(ExtendedDataInput @in) : base(@in)
		{
		}

		protected internal BasicNanoTime(long value) : base(value)
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
				return DATA_TYPE.DT_NANOTIME;
			}
		}

		public virtual LocalTime NanoTime
		{
			get
			{
				if (Null)
				{
					return null;
				}
				else
				{
					return Utils.parseNanoTime(Long);
				}
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public java.time.temporal.Temporal getTemporal() throws Exception
		public override Temporal Temporal
		{
			get
			{
				return NanoTime;
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
					return NanoTime.format(format);
				}
			}
		}

		public override bool Equals(object o)
		{
			if (!(o is BasicNanoTime) || o == null)
			{
				return false;
			}
			else
			{
				return Long == ((BasicNanoTime)o).Long;
			}
		}
	}

}