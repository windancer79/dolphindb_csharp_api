using System;

namespace com.xxdb.data
{


	using ExtendedDataInput = com.xxdb.io.ExtendedDataInput;

	/// 
	/// <summary>
	/// Corresponds to DolphinDB date scalar
	/// 
	/// </summary>

	public class BasicDate : BasicInt
	{
		private static DateTimeFormatter format = DateTimeFormatter.ofPattern("yyyy.MM.dd");

		public BasicDate(LocalDate value) : base(Utils.countDays(value))
		{
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public BasicDate(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
		public BasicDate(ExtendedDataInput @in) : base(@in)
		{
		}

		protected internal BasicDate(int value) : base(value)
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
				return DATA_TYPE.DT_DATE;
			}
		}

		public virtual LocalDate Date
		{
			get
			{
				if (Null)
				{
					return null;
				}
				else
				{
					return Utils.parseDate(Int);
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
			if (!(o is BasicDate) || o == null)
			{
				return false;
			}
			else
			{
				return Int == ((BasicDate)o).Int;
			}
		}
	}

}