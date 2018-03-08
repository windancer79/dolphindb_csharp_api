using System;

namespace com.xxdb.data
{


	using ExtendedDataInput = com.xxdb.io.ExtendedDataInput;
	using ExtendedDataOutput = com.xxdb.io.ExtendedDataOutput;

	/// 
	/// <summary>
	/// Corresponds to DolphindB long scalar
	/// 
	/// </summary>

	public class BasicLong : AbstractScalar, IComparable<BasicLong>
	{
		private long value;

		public BasicLong(long value)
		{
			this.value = value;
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public BasicLong(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
		public BasicLong(ExtendedDataInput @in)
		{
			value = @in.readLong();
		}

		public virtual long Long
		{
			get
			{
				return value;
			}
		}

		public override bool Null
		{
			get
			{
				return value == long.MinValue;
			}
		}

		public override void setNull()
		{
			value = long.MinValue;
		}

		public override DATA_CATEGORY DataCategory
		{
			get
			{
				return DATA_CATEGORY.INTEGRAL;
			}
		}

		public override DATA_TYPE DataType
		{
			get
			{
				return DATA_TYPE.DT_LONG;
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Number getNumber() throws Exception
		public override Number Number
		{
			get
			{
				if (Null)
				{
					return null;
				}
				else
				{
					return new long?(value);
				}
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public java.time.temporal.Temporal getTemporal() throws Exception
		public override Temporal Temporal
		{
			get
			{
				throw new Exception("Imcompatible data type");
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
					return value.ToString();
				}
			}
		}

		public override bool Equals(object o)
		{
			if (!(o is BasicLong) || o == null)
			{
				return false;
			}
			else
			{
				return value == ((BasicLong)o).value;
			}
		}

		public override int GetHashCode()
		{
			return (new long?(value)).GetHashCode();
		}

		protected internal override void WriteScalarToOutputStream(ExtendedDataOutput @out)
		{
			@out.writeLong(value);
		}

		public virtual int CompareTo(BasicLong o)
		{
			return Long.compare(value, o.value);
		}
	}

}