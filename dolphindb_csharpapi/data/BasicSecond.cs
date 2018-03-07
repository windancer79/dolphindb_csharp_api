namespace com.xxdb.data
{


	using ExtendedDataInput = com.xxdb.io.ExtendedDataInput;


	/// 
	/// <summary>
	/// Corresponds to DolphinDB second scalar
	/// 
	/// </summary>

	public class BasicSecond : BasicInt
	{
		private static DateTimeFormatter format = DateTimeFormatter.ofPattern("HH:mm:ss");

		public BasicSecond(LocalTime value) : base(Utils.countSeconds(value))
		{
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public BasicSecond(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
		public BasicSecond(ExtendedDataInput @in) : base(@in)
		{
		}

		protected internal BasicSecond(int value) : base(value)
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

		public virtual LocalTime Second
		{
			get
			{
				if (Null)
				{
					return null;
				}
				else
				{
					return Utils.parseSecond(Int);
				}
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public java.time.temporal.Temporal getTemporal() throws Exception
		public override Temporal Temporal
		{
			get
			{
				return Second;
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
					return Second.format(format);
				}
			}
		}

		public override bool Equals(object o)
		{
			if (!(o is BasicSecond) || o == null)
			{
				return false;
			}
			else
			{
				return Int == ((BasicSecond)o).Int;
			}
		}
	}

}