using System;
using System.Text;

namespace com.xxdb.data
{

	using ExtendedDataInput = com.xxdb.io.ExtendedDataInput;
	using ExtendedDataOutput = com.xxdb.io.ExtendedDataOutput;

	/// 
	/// <summary>
	/// Corresponds to DolphinDB tuple(any vector)
	/// 
	/// </summary>

	public class BasicAnyVector : AbstractVector
	{
		private Entity[] values;


		public BasicAnyVector(int size) : base(DATA_FORM.DF_VECTOR)
		{
			values = new Entity[size];
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: protected BasicAnyVector(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
		protected internal BasicAnyVector(ExtendedDataInput @in) : base(DATA_FORM.DF_VECTOR)
		{
			int rows = @in.readInt();
			int cols = @in.readInt();
			int size = rows * cols;
			values = new Entity[size];
			assert(rows <= 1024);
			BasicEntityFactory factory = new BasicEntityFactory();
			for (int i = 0; i < size; ++i)
			{
				short flag = @in.readShort();
				int form = flag >> 8;
				int type = flag & 0xff;
				//if (form != 1)
					//assert (form == 1);
				//if (type != 4)
					//assert(type == 4);
				Entity obj = factory.createEntity(Enum.GetValues(typeof(DATA_FORM))[form], Enum.GetValues(typeof(DATA_TYPE))[type], @in);
				values[i] = obj;
			}

		}

		public virtual Entity getEntity(int index)
		{
			return values[index];
		}

		public override Scalar get(int index)
		{
			if (values[index].Scalar)
			{
				return (Scalar)values[index];
			}
			else
			{
				throw new Exception("The element of the vector is not a scalar object.");
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void set(int index, Scalar value) throws Exception
		public override void set(int index, Scalar value)
		{
			values[index] = value;
		}

		public virtual void setEntity(int index, Entity value)
		{
			values[index] = value;
		}

		public override bool isNull(int index)
		{
			return values[index] == null || (values[index].Scalar && ((Scalar)values[index]).Null);
		}

		public override int Null
		{
			set
			{
				values[value] = new Void();
			}
		}

		public override DATA_CATEGORY DataCategory
		{
			get
			{
				return DATA_CATEGORY.MIXED;
			}
		}

		public override DATA_TYPE DataType
		{
			get
			{
				return DATA_TYPE.DT_ANY;
			}
		}

		public override int rows()
		{
			return values.Length;
		}

		public override string String
		{
			get
			{
				StringBuilder sb = new StringBuilder("(");
				int size = Math.Min(10, rows());
				if (size > 0)
				{
					sb.Append(getEntity(0).String);
				}
				for (int i = 1; i < size; ++i)
				{
					sb.Append(',');
					sb.Append(getEntity(i).String);
				}
				if (size < rows())
				{
					sb.Append(",...");
				}
				sb.Append(")");
				return sb.ToString();
			}
		}

		public override Type ElementClass
		{
			get
			{
				return typeof(Entity);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override protected void writeVectorToOutputStream(com.xxdb.io.ExtendedDataOutput out) throws java.io.IOException
		protected internal override void writeVectorToOutputStream(ExtendedDataOutput @out)
		{
			foreach (Entity value in values)
			{
				value.write(@out);
			}
		}
	}

}