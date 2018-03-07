using System;

namespace com.xxdb.data
{


	using ExtendedDataInput = com.xxdb.io.ExtendedDataInput;
	using ExtendedDataOutput = com.xxdb.io.ExtendedDataOutput;

	/// 
	/// <summary>
	/// Corresponds to DolphinDB short scalar
	/// 
	/// </summary>

	public class BasicShort : AbstractScalar, IComparable<BasicShort>
	{
		private short value;

		public BasicShort(short value)
		{
			this.value = value;
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public BasicShort(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
		public BasicShort(ExtendedDataInput @in)
		{
			value = @in.readShort();
		}

		public virtual short Short
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
				return value == short.MinValue;
			}
		}

		public override void setNull()
		{
			value = short.MinValue;
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
				return DATA_TYPE.DT_SHORT;
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
					return new short?(value);
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
			if (!(o is BasicShort) || o == null)
			{
				return false;
			}
			else
			{
				return value == ((BasicShort)o).value;
			}
		}

		public override int GetHashCode()
		{
			return (new short?(value)).GetHashCode();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: protected void writeScalarToOutputStream(com.xxdb.io.ExtendedDataOutput out) throws java.io.IOException
		protected internal override void WriteScalarToOutputStream(ExtendedDataOutput @out)
		{
			@out.writeShort(value);
		}

		public virtual int CompareTo(BasicShort o)
		{
			return Short.compare(value, o.value);
		}
	}

}