using System;

namespace com.xxdb.data
{


	using ExtendedDataInput = com.xxdb.io.ExtendedDataInput;
	using ExtendedDataOutput = com.xxdb.io.ExtendedDataOutput;

	/// 
	/// <summary>
	/// Corresponds to DolphinDB int scalar
	/// 
	/// </summary>

	public class BasicInt : AbstractScalar, IComparable<BasicInt>
	{
		private int value;

		public BasicInt(int value)
		{
			this.value = value;
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public BasicInt(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
		public BasicInt(ExtendedDataInput @in)
		{
			value = @in.readInt();
		}

		public virtual int Int
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
				return value == int.MinValue;
			}
		}

		public override void setNull()
		{
			value = int.MinValue;
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
				return DATA_TYPE.DT_INT;
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
					return new int?(value);
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
			if (!(o is BasicInt) || o == null)
			{
				return false;
			}
			else
			{
				return value == ((BasicInt)o).value;
			}
		}

		public override int GetHashCode()
		{
			return (new int?(value)).GetHashCode();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: protected void writeScalarToOutputStream(com.xxdb.io.ExtendedDataOutput out) throws java.io.IOException
		protected internal override void WriteScalarToOutputStream(ExtendedDataOutput @out)
		{
			@out.writeInt(value);
		}

		public virtual int CompareTo(BasicInt o)
		{
			return Integer.compare(value, o.value);
		}
	}

}