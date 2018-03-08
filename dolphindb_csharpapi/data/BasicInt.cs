using com.xxdb.jobjects;
using System;
using com.xxdb.io;
namespace com.xxdb.data
{
	/// 
	/// <summary>
	/// Corresponds to DolphinDB int scalar
	/// 
	/// </summary>

	public class BasicInt : AbstractScalar, IComparable<BasicInt>
	{
		private int value;

		public BasicInt(int value)
		{
			this.value = value;
		}

		public BasicInt(ExtendedDataInput @in)
		{
			value = @in.readInt();
		}

		public virtual int Int
		{
			get
			{
				return value;
			}
		}

		public bool isNull
		{
			get
			{
				return value == int.MinValue;
			}
		}

		public void setNull()
		{
			value = int.MinValue;
		}

		public  DATA_CATEGORY DataCategory
		{
			get
			{
				return DATA_CATEGORY.INTEGRAL;
			}
		}

		public  DATA_TYPE DataType
		{
			get
			{
				return DATA_TYPE.DT_INT;
			}
		}
		public int getNumber()
		{
				if (isNull)
				{
					return int.MinValue;
				}
				else
				{
					return value;
				}
		}

		public Temporal getTemporal()
		{
			throw new Exception("Imcompatible data type");
		}

		public override bool Equals(object o)
		{
			if (!(o is BasicInt) || o == null)
			{
				return false;
			}
			else
			{
				return value == ((BasicInt)o).value;
			}
		}

		public override int GetHashCode()
		{
			return (new int?(value)).GetHashCode();
		}

		protected internal void WriteScalarToOutputStream(ExtendedDataOutput @out)
		{
			@out.writeInt(value);
		}

		public int CompareTo(BasicInt o)
		{
			return value.CompareTo(o.value);
		}
	}

}