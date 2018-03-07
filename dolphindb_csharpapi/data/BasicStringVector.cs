using System;
using System.Collections.Generic;

namespace com.xxdb.data
{


	using ExtendedDataInput = com.xxdb.io.ExtendedDataInput;
	using ExtendedDataOutput = com.xxdb.io.ExtendedDataOutput;

	/// 
	/// <summary>
	/// Corresponds to DolphinDB string vector
	/// 
	/// </summary>

	public class BasicStringVector : AbstractVector
	{
		private string[] values;
		private bool isSymbol;

		public BasicStringVector(int size) : this(DATA_FORM.DF_VECTOR, size, false)
		{
		}

		public BasicStringVector(IList<string> list) : base(DATA_FORM.DF_VECTOR)
		{
			if (list != null)
			{
				values = new string[list.Count];
				for (int i = 0; i < list.Count; ++i)
				{
					values[i] = list[i];
				}
			}
			this.isSymbol = false;
		}

		public BasicStringVector(string[] array) : base(DATA_FORM.DF_VECTOR)
		{
			values = array.Clone();
			this.isSymbol = false;
		}

		protected internal BasicStringVector(DATA_FORM df, int size, bool isSymbol) : base(df)
		{
			values = new string[size];
			this.isSymbol = isSymbol;
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: protected BasicStringVector(Entity_DATA_FORM df, com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
		protected internal BasicStringVector(DATA_FORM df, ExtendedDataInput @in) : base(df)
		{
			int rows = @in.readInt();
			int columns = @in.readInt();
			int size = rows * columns;
			values = new string[size];
			for (int i = 0; i < size; ++i)
			{
				values[i] = @in.readString();
			}
		}

		public override Scalar get(int index)
		{
			return new BasicString(values[index]);
		}

		public virtual string getString(int index)
		{
			return values[index];
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void set(int index, Scalar value) throws Exception
		public override void set(int index, Scalar value)
		{
			values[index] = value.String;
		}

		public virtual void setString(int index, string value)
		{
			values[index] = value;
		}

		public override bool isNull(int index)
		{
			return string.ReferenceEquals(values[index], null) || values[index].Length == 0;
		}

		public override int Null
		{
			set
			{
				values[value] = "";
			}
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
				return isSymbol ? DATA_TYPE.DT_SYMBOL : DATA_TYPE.DT_STRING;
			}
		}

		public override Type ElementClass
		{
			get
			{
				return typeof(BasicString);
			}
		}

		public override int rows()
		{
			return values.Length;
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: protected void writeVectorToOutputStream(com.xxdb.io.ExtendedDataOutput out) throws java.io.IOException
		protected internal override void writeVectorToOutputStream(ExtendedDataOutput @out)
		{
			foreach (string str in values)
			{
				@out.writeString(str);
			}
		}
	}

}