using System;
using System.Collections.Generic;
using System.Text;

namespace com.xxdb.data
{


	using ExtendedDataInput = com.xxdb.io.ExtendedDataInput;
	using ExtendedDataOutput = com.xxdb.io.ExtendedDataOutput;

	/// 
	/// <summary>
	/// Corresponds to DolphinDB table object
	/// 
	/// </summary>

	public class BasicTable : AbstractEntity, Table
	{
		private IList<Vector> columns_ = new List<Vector>();
		private IList<string> names_ = new List<string>();
		private IDictionary<string, int?> name2index_ = new Dictionary<string, int?>();

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public BasicTable(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
		public BasicTable(ExtendedDataInput @in)
		{
			int rows = @in.readInt();
			int cols = @in.readInt();
			string tableName = @in.readString();

			//read column names
			for (int i = 0; i < cols; ++i)
			{
				string name = @in.readString();
				name2index_[name] = name2index_.Count;
				names_.Add(name);
			}

			BasicEntityFactory factory = new BasicEntityFactory();
			//read columns
			for (int i = 0; i < cols; ++i)
			{
				short flag = @in.readShort();
				int form = flag >> 8;
				int type = flag & 0xff;

				DATA_FORM df = Enum.GetValues(typeof(DATA_FORM))[form];
				DATA_TYPE dt = Enum.GetValues(typeof(DATA_TYPE))[type];
				if (df != DATA_FORM.DF_VECTOR)
				{
					throw new IOException("Invalid form for column [" + names_[i] + "] for table " + tableName);
				}
				Vector vector = (Vector)factory.createEntity(df, dt, @in);
				if (vector.rows() != rows && vector.rows() != 1)
				{
					throw new IOException("The number of rows for column " + names_[i] + " is not consistent with other columns");
				}
				columns_.Add(vector);
			}
		}

//JAVA TO C# CONVERTER WARNING: 'final' parameters are not available in .NET:
//ORIGINAL LINE: public BasicTable(final java.util.List<String> colNames, final java.util.List<Vector> cols)
		public BasicTable(IList<string> colNames, IList<Vector> cols)
		{
			this.ColName = colNames;
			this.Columns = cols;
		}

//JAVA TO C# CONVERTER WARNING: 'final' parameters are not available in .NET:
//ORIGINAL LINE: public void setColName(final java.util.List<String> colNames)
		public virtual IList<string> ColName
		{
			set
			{
				names_.Clear();
				foreach (string name in value)
				{
					names_.Add(name);
				}
			}
		}

//JAVA TO C# CONVERTER WARNING: 'final' parameters are not available in .NET:
//ORIGINAL LINE: public void setColumns(final java.util.List<Vector> cols)
		public virtual IList<Vector> Columns
		{
			set
			{
				columns_.Clear();
				// this is a shallow copy!
				foreach (Vector vector in value)
				{
					columns_.Add(vector);
				}
    
			}
		}

		public virtual DATA_CATEGORY DataCategory
		{
			get
			{
				return DATA_CATEGORY.MIXED;
			}
		}

		public virtual DATA_TYPE DataType
		{
			get
			{
				return DATA_TYPE.DT_DICTIONARY;
			}
		}

		public override DATA_FORM DataForm
		{
			get
			{
				return DATA_FORM.DF_TABLE;
			}
		}

		public virtual int rows()
		{
			if (columns() <= 0)
			{
				return 0;
			}
			else
			{
				return columns_[0].rows();
			}
		}

		public virtual int columns()
		{
			return columns_.Count;
		}

		public virtual Vector getColumn(int index)
		{
			return columns_[index];
		}

		public virtual Vector getColumn(string name)
		{
			int? index = name2index_[name];
			if (index == null)
			{
				return null;
			}
			else
			{
				return getColumn(index.Value);
			}
		}

		public virtual string getColumnName(int index)
		{
			return names_[index];
		}

		public virtual string String
		{
			get
			{
				int rows = Math.Min(Utils.DISPLAY_ROWS,rows());
				int strColMaxWidth = Utils.DISPLAY_WIDTH / Math.Min(columns(),Utils.DISPLAY_COLS) + 5;
				int length = 0;
				int curCol = 0;
				int maxColWidth;
				StringBuilder[] list = new StringBuilder[rows + 1];
				StringBuilder separator = new StringBuilder();
				string[] listTmp = new string[rows + 1];
				int i, curSize;
    
				for (i = 0; i < list.Length; ++i)
				{
					list[i] = new StringBuilder();
				}
    
				while (length < Utils.DISPLAY_WIDTH && curCol < columns())
				{
					listTmp[0] = getColumnName(curCol);
					maxColWidth = 0;
					for (i = 0;i < rows;i++)
					{
						listTmp[i + 1] = getColumn(curCol).get(i).String;
						if (listTmp[i + 1].Length > maxColWidth)
						{
							maxColWidth = listTmp[i + 1].Length;
						}
					}
					if (maxColWidth > strColMaxWidth && getColumn(curCol).DataCategory == DATA_CATEGORY.LITERAL)
					{
						maxColWidth = strColMaxWidth;
					}
					if ((int)listTmp[0].Length > maxColWidth)
					{
						maxColWidth = Math.Min(strColMaxWidth,(int)listTmp[0].Length);
					}
    
					if (length + maxColWidth > Utils.DISPLAY_WIDTH && curCol + 1 < columns())
					{
						break;
					}
    
					for (int k = 0; k < maxColWidth; ++k)
					{
						separator.Append('-');
					}
					if (curCol < columns() - 1)
					{
						maxColWidth++;
						separator.Append(' ');
					}
    
					for (i = 0;i <= rows;i++)
					{
						curSize = listTmp[i].Length;
						if (curSize <= maxColWidth)
						{
							list[i].Append(listTmp[i]);
							if (curSize < maxColWidth)
							{
								for (int j = 0; j < (maxColWidth - curSize); j++)
								{
									list[i].Append(' ');
								}
							}
						}
						else
						{
							if (maxColWidth > 3)
							{
								list[i].Append(listTmp[i].Substring(0,maxColWidth - 3));
							}
							list[i].Append("...");
							separator.Append("---");
						}
					}
					length += maxColWidth;
					curCol++;
				}
    
				if (curCol < columns())
				{
					for (i = 0;i <= rows;i++)
					{
						list[i].Append("...");
					}
				}
    
				StringBuilder resultStr = new StringBuilder(list[0]);
				resultStr.Append("\n");
				resultStr.Append(separator);
				resultStr.Append("\n");
				for (i = 1;i <= rows;i++)
				{
					resultStr.Append(list[i]);
					resultStr.Append("\n");
				}
				if (rows < rows())
				{
					resultStr.Append("...\n");
				}
				return resultStr.ToString();
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void write(com.xxdb.io.ExtendedDataOutput out) throws java.io.IOException
		public virtual void write(ExtendedDataOutput @out)
		{
			int flag = ((int)DATA_FORM.DF_TABLE << 8) + DataType.ordinal();
			@out.writeShort(flag);
			@out.writeInt(rows());
			@out.writeInt(columns());
			@out.writeString(""); //table name
			foreach (string colName in names_)
			{
				@out.writeString(colName);
			}
			foreach (Vector vector in columns_)
			{
				vector.write(@out);
			}
		}
	}

}