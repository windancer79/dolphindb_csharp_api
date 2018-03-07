using System;

namespace com.xxdb.data
{


	using ExtendedDataInput = com.xxdb.io.ExtendedDataInput;
	using ExtendedDataOutput = com.xxdb.io.ExtendedDataOutput;

	/// 
	/// <summary>
	/// Corresponds to DolphinDB string scalar
	/// 
	/// </summary>

	public class BasicString : AbstractScalar, IComparable<BasicString>
	{
		private string value;

		public BasicString(string value)
		{
			this.value = value;
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public BasicString(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
		public BasicString(ExtendedDataInput @in)
		{
			value = @in.readString();
		}

		public override string String
		{
			get
			{
				return value;
			}
			set
			{
				this.value = value;
			}
		}


		public override bool Null
		{
			get
			{
				return value.Length == 0;
			}
		}

		public override void setNull()
		{
			value = "";
		}

		public override DATA_CATEGORY DataCategory
		{
			get
			{
				return DATA_CATEGORY.LITERAL;
			}
		}

		public override DATA_TYPE DataType
		{
			get
			{
				return DATA_TYPE.DT_STRING;
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Number getNumber() throws Exception
		public override Number Number
		{
			get
			{
				throw new Exception("Imcompatible data type");
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

		public override bool Equals(object o)
		{
			if (!(o is BasicString) || o == null)
			{
				return false;
			}
			else
			{
				return value.Equals(((BasicString)o).value);
			}
		}

		public override int GetHashCode()
		{
			return value.GetHashCode();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: protected void writeScalarToOutputStream(com.xxdb.io.ExtendedDataOutput out) throws java.io.IOException
		protected internal override void WriteScalarToOutputStream(ExtendedDataOutput @out)
		{
			@out.writeString(value);
		}

		public virtual int CompareTo(BasicString o)
		{
			return value.CompareTo(o.value);
		}
	}

}