namespace com.xxdb.data
{

	using ExtendedDataInput = com.xxdb.io.ExtendedDataInput;
	using ExtendedDataOutput = com.xxdb.io.ExtendedDataOutput;

	public class BasicSystemEntity : BasicString
	{
		private DATA_TYPE type;

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public BasicSystemEntity(com.xxdb.io.ExtendedDataInput in, Entity_DATA_TYPE type) throws java.io.IOException
		public BasicSystemEntity(ExtendedDataInput @in, DATA_TYPE type) : base("")
		{
			this.type = type;
			if (type == DATA_TYPE.DT_FUNCTIONDEF)
			{
				@in.readByte();
			}
			String = @in.readString();
		}

		public override DATA_CATEGORY DataCategory
		{
			get
			{
				return DATA_CATEGORY.SYSTEM;
			}
		}

		public override DATA_TYPE DataType
		{
			get
			{
				return type;
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: protected void writeScalarToOutputStream(com.xxdb.io.ExtendedDataOutput out) throws java.io.IOException
		protected internal override void WriteScalarToOutputStream(ExtendedDataOutput @out)
		{
			throw new IOException("System entity is not supposed to serialize.");
		}
	}

}