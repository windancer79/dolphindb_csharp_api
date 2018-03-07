namespace com.xxdb.data
{


	using ExtendedDataInput = com.xxdb.io.ExtendedDataInput;

	/// 
	/// <summary>
	/// Corresponds to DolphinDB month scalar
	/// 
	/// </summary>

	public class BasicMonth : BasicInt
	{
		private static DateTimeFormatter format = DateTimeFormatter.ofPattern("yyyy.MM'M'");

		public BasicMonth(int year, Month month) : base(year * 12 + month.Value)
		{
		}
		public BasicMonth(YearMonth value) : base(value.Year * 12 + value.MonthValue - 1)
		{
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public BasicMonth(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
		public BasicMonth(ExtendedDataInput @in) : base(@in)
		{
		}

		protected internal BasicMonth(int value) : base(value)
		{
		}

		public virtual YearMonth Month
		{
			get
			{
				if (Null)
				{
					return null;
				}
				else
				{
					return Utils.parseMonth(Int);
				}
			}
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
				return DATA_TYPE.DT_MONTH;
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public java.time.temporal.Temporal getTemporal() throws Exception
		public override Temporal Temporal
		{
			get
			{
				return Month;
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
					return Month.format(format);
				}
			}
		}

		public override bool Equals(object o)
		{
			if (!(o is BasicMonth) || o == null)
			{
				return false;
			}
			else
			{
				return Int == ((BasicMonth)o).Int;
			}
		}
	}

}