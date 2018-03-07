using System;

namespace com.xxdb.data
{


	using ExtendedDataOutput = com.xxdb.io.ExtendedDataOutput;

	public class Void : AbstractScalar
	{

		public override bool Null
		{
			get
			{
				return true;
			}
		}

		public override void setNull()
		{
		}

		public override DATA_CATEGORY DataCategory
		{
			get
			{
				return DATA_CATEGORY.NOTHING;
			}
		}

		public override DATA_TYPE DataType
		{
			get
			{
				return DATA_TYPE.DT_VOID;
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

		public override string String
		{
			get
			{
				return "";
			}
		}

		public override bool Equals(object o)
		{
			if (!(o is Void) || o == null)
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		public override int GetHashCode()
		{
			return 0;
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: protected void writeScalarToOutputStream(com.xxdb.io.ExtendedDataOutput out) throws java.io.IOException
		protected internal override void WriteScalarToOutputStream(ExtendedDataOutput @out)
		{
			@out.writeBoolean(true); //explicit null value
		}
	}

}