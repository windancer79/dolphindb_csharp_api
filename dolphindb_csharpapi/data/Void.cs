using com.xxdb.jobjects;
using System;

namespace com.xxdb.data
{


	using ExtendedDataOutput = com.xxdb.io.ExtendedDataOutput;

	public class Void : AbstractScalar
	{

		public bool isNull()
		{
				return true;
		}

		public void setNull()
		{
		}

		public DATA_CATEGORY getDataCategory()
		{
			return DATA_CATEGORY.NOTHING;
		}

		public DATA_TYPE getDataType()
		{
			return DATA_TYPE.DT_VOID;
		}


		public  Number getNumber()
        {
				throw new Exception("Imcompatible data type");
		}

 
		public  Temporal getTemporal()
        {
				throw new Exception("Imcompatible data type");
		}

		public  string getString()
		{
				return "";
		}

		public bool Equals(object o)
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

		public int GetHashCode()
		{
			return 0;
		}


		protected internal void WriteScalarToOutputStream(ExtendedDataOutput @out)
		{
			@out.writeBoolean(true); //explicit null value
		}
	}

}