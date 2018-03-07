using System;
using System.Text;

namespace com.xxdb.data
{

	using ExtendedDataOutput = com.xxdb.io.ExtendedDataOutput;

	public abstract class AbstractVector : AbstractEntity, Vector
	{
		public abstract int rows();
		public abstract DATA_TYPE DataType {get;}
		public abstract DATA_CATEGORY DataCategory {get;}
		public abstract Type ElementClass {get;}
		public abstract void set(int index, Scalar value);
		public abstract Scalar get(int index);
		public abstract int Null {set;}
		public abstract bool isNull(int index);
		private DATA_FORM df_;

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: protected abstract void writeVectorToOutputStream(com.xxdb.io.ExtendedDataOutput out) throws java.io.IOException;
		protected internal abstract void writeVectorToOutputStream(ExtendedDataOutput @out);

		public AbstractVector(DATA_FORM df)
		{
			df_ = df;
		}

		public override DATA_FORM DataForm
		{
			get
			{
				return df_;
			}
		}

		public virtual int columns()
		{
			return 1;
		}

		public virtual string String
		{
			get
			{
				StringBuilder sb = new StringBuilder("[");
				int size = Math.Min(Vector_Fields.DISPLAY_ROWS, rows());
				if (size > 0)
				{
					sb.Append(get(0).String);
				}
				for (int i = 1; i < size; ++i)
				{
					sb.Append(',');
					sb.Append(get(i).String);
				}
				if (size < rows())
				{
					sb.Append(",...");
				}
				sb.Append("]");
				return sb.ToString();
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void write(com.xxdb.io.ExtendedDataOutput out) throws java.io.IOException
		public virtual void write(ExtendedDataOutput @out)
		{
			int flag = (df_.ordinal() << 8) + DataType.ordinal();
			@out.writeShort(flag);
			@out.writeInt(rows());
			@out.writeInt(columns());
			writeVectorToOutputStream(@out);
		}
	}

}