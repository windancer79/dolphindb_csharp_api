using com.xxdb.jobjects;
using System;

namespace com.xxdb.data
{


	using ExtendedDataInput = com.xxdb.io.ExtendedDataInput;
	using ExtendedDataOutput = com.xxdb.io.ExtendedDataOutput;

	/// 
	/// <summary>
	/// Corresponds to DolphinDB char scalar
	/// 
	/// </summary>

	public class BasicByte : AbstractScalar, IComparable<BasicByte>
	{
		private sbyte value;

		public BasicByte(sbyte value)
		{
			this.value = value;
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public BasicByte(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
		public BasicByte(ExtendedDataInput @in)
		{
			value = @in.readByte();
		}

		public virtual sbyte Byte
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
				return value == sbyte.MinValue;
			}
		}

		public override void setNull()
		{
			value = sbyte.MinValue;
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
				return DATA_TYPE.DT_BYTE;
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Number getNumber() throws Exception
		public override Number getNumber()
		{
			
					return new sbyte?(value);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public java.time.temporal.Temporal getTemporal() throws Exception
		public override Temporal getTemporal()
		{
				throw new Exception("Imcompatible data type");
		}

		public override string String
		{
			get
			{
				if (Null)
				{
					return "";
				}
				else if (value > 31 && value < 127)
				{
					return "'" + ((char)value).ToString() + "'";
				}
				else
				{
					return value.ToString();
				}
			}
		}

		public override bool Equals(object o)
		{
			if (!(o is BasicByte) || o == null)
			{
				return false;
			}
			else
			{
				return value == ((BasicByte)o).value;
			}
		}

		public override int GetHashCode()
		{
			return (new sbyte?(value)).GetHashCode();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: protected void writeScalarToOutputStream(com.xxdb.io.ExtendedDataOutput out) throws java.io.IOException
		protected internal override void WriteScalarToOutputStream(ExtendedDataOutput @out)
		{
			@out.writeByte(value);
		}

		public virtual int CompareTo(BasicByte o)
		{
			return Byte.compare(value, o.value);
		}
	}

}