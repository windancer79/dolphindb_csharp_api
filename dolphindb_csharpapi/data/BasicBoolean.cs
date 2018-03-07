using com.xxdb.jobjects;
using System;

namespace com.xxdb.data
{


	using ExtendedDataInput = com.xxdb.io.ExtendedDataInput;
	using ExtendedDataOutput = com.xxdb.io.ExtendedDataOutput;

	/// 
	/// <summary>
	/// Corresponds to DolphinDB bool scalar
	/// 
	/// </summary>

	public class BasicBoolean : AbstractScalar, IComparable<BasicBoolean>
	{
		private sbyte value;

		public BasicBoolean(bool value)
		{
			this.value = value ? (sbyte)1 : (sbyte)0;
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public BasicBoolean(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
		public BasicBoolean(ExtendedDataInput @in)
		{
			value = @in.readByte();
		}

		protected internal BasicBoolean(sbyte value)
		{
			this.value = value;
		}

		public virtual bool Boolean
		{
			get
			{
				return value != 0;
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
				return DATA_CATEGORY.LOGICAL;
			}
		}

		public override DATA_TYPE DataType
		{
			get
			{
				return DATA_TYPE.DT_BOOL;
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
					return new sbyte?(value);
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
					return Boolean.ToString();
				}
			}
		}

		public override bool Equals(object o)
		{
			if (!(o is BasicBoolean) || o == null)
			{
				return false;
			}
			else
			{
				return value == ((BasicBoolean)o).value;
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

		public virtual int CompareTo(BasicBoolean o)
		{
			return Byte.compare(value, o.value);
		}
	}

}