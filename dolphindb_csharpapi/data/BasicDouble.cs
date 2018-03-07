using System;

namespace com.xxdb.data
{


	using ExtendedDataInput = com.xxdb.io.ExtendedDataInput;
	using ExtendedDataOutput = com.xxdb.io.ExtendedDataOutput;

	/// 
	/// <summary>
	/// Corresponds to DolphinDB double scalar
	/// 
	/// </summary>

	public class BasicDouble : AbstractScalar, IComparable<BasicDouble>
	{
		private static readonly DecimalFormat df1 = new DecimalFormat("0.######");
		private static readonly DecimalFormat df2 = new DecimalFormat("0.######E0");
		private double value;

		public BasicDouble(double value)
		{
			this.value = value;
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public BasicDouble(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
		public BasicDouble(ExtendedDataInput @in)
		{
			value = @in.readDouble();
		}

		public virtual double Double
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
				return value == -double.MaxValue;
			}
		}

		public override void setNull()
		{
			value = -double.MaxValue;
		}

		public override DATA_CATEGORY DataCategory
		{
			get
			{
				return DATA_CATEGORY.FLOATING;
			}
		}

		public override DATA_TYPE DataType
		{
			get
			{
				return DATA_TYPE.DT_DOUBLE;
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
					return new double?(value);
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
				else if (double.IsNaN(value) || double.IsInfinity(value))
				{
					return value.ToString();
				}
				else
				{
					double absVal = Math.Abs(value);
					if ((absVal > 0 && absVal <= 0.000001) || absVal >= 1000000.0)
					{
						return df2.format(value);
					}
					else
					{
						return df1.format(value);
					}
				}
			}
		}

		public override bool Equals(object o)
		{
			if (!(o is BasicDouble) || o == null)
			{
				return false;
			}
			else
			{
				return value == ((BasicDouble)o).value;
			}
		}

		public override int GetHashCode()
		{
			return (new double?(value)).GetHashCode();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: protected void writeScalarToOutputStream(com.xxdb.io.ExtendedDataOutput out) throws java.io.IOException
		protected internal override void WriteScalarToOutputStream(ExtendedDataOutput @out)
		{
			@out.writeDouble(value);
		}

		public virtual int CompareTo(BasicDouble o)
		{
			return value.CompareTo(o.value);
		}
	}

}