using System;
using System.Text;

namespace com.xxdb.data
{

	using ExtendedDataInput = com.xxdb.io.ExtendedDataInput;
	using ExtendedDataOutput = com.xxdb.io.ExtendedDataOutput;

	public abstract class AbstractMatrix : AbstractEntity, Matrix
	{
		public abstract DATA_TYPE DataType {get;}
		public abstract DATA_CATEGORY DataCategory {get;}
		public abstract Type ElementClass {get;}
		public abstract Scalar get(int row, int column);
		public abstract void setNull(int row, int column);
		public abstract bool isNull(int row, int column);
		private Vector rowLabels = null;
		private Vector columnLabels = null;
//JAVA TO C# CONVERTER NOTE: Fields cannot have the same name as methods:
		protected internal int rows_Renamed;
//JAVA TO C# CONVERTER NOTE: Fields cannot have the same name as methods:
		protected internal int columns_Renamed;

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: protected abstract void readMatrixFromInputStream(int rows, int columns, com.xxdb.io.ExtendedDataInput in) throws java.io.IOException;
		protected internal abstract void readMatrixFromInputStream(int rows, int columns, ExtendedDataInput @in);
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: protected abstract void writeVectorToOutputStream(com.xxdb.io.ExtendedDataOutput out) throws java.io.IOException;
		protected internal abstract void writeVectorToOutputStream(ExtendedDataOutput @out);

		protected internal AbstractMatrix(int rows, int columns)
		{
			this.rows_Renamed = rows;
			this.columns_Renamed = columns;
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: protected AbstractMatrix(com.xxdb.io.ExtendedDataInput in) throws java.io.IOException
		protected internal AbstractMatrix(ExtendedDataInput @in)
		{
			byte hasLabels = @in.readByte();

			BasicEntityFactory factory = null;
			DATA_TYPE[] types = Enum.GetValues(typeof(DATA_TYPE));
			if (hasLabels > 0)
			{
				factory = new BasicEntityFactory();
			}

			if ((hasLabels & 1) == 1)
			{
				//contain row labels
				short flag = @in.readShort();
				int form = flag >> 8;
				int type = flag & 0xff;
				if (form != (int)DATA_FORM.DF_VECTOR)
				{
					throw new IOException("The form of matrix row labels must be vector");
				}
				if (type < 0 || type >= types.Length)
				{
					throw new IOException("Invalid data type for matrix row labels: " + type);
				}
				rowLabels = (Vector)factory.createEntity(DATA_FORM.DF_VECTOR, types[type], @in);
			}

			if ((hasLabels & 2) == 2)
			{
				//contain columns labels
				short flag = @in.readShort();
				int form = flag >> 8;
				int type = flag & 0xff;
				if (form != (int)DATA_FORM.DF_VECTOR)
				{
					throw new IOException("The form of matrix columns labels must be vector");
				}
				if (type < 0 || type >= types.Length)
				{
					throw new IOException("Invalid data type for matrix column labels: " + type);
				}
				columnLabels = (Vector)factory.createEntity(DATA_FORM.DF_VECTOR, types[type], @in);
			}

			short flag = @in.readShort();
			int type = flag & 0xff;
			if (type < 0 || type >= types.Length)
			{
				throw new IOException("Invalid data type for matrix: " + type);
			}
			rows_Renamed = @in.readInt();
			columns_Renamed = @in.readInt();
			readMatrixFromInputStream(rows_Renamed, columns_Renamed, @in);
		}

		protected internal virtual int getIndex(int row, int column)
		{
			return column * rows_Renamed + row;
		}

		public virtual bool hasRowLabel()
		{
			return rowLabels != null;
		}

		public virtual bool hasColumnLabel()
		{
			return columnLabels != null;
		}

		public virtual Scalar getRowLabel(int index)
		{
			return rowLabels.get(index);
		}

		public virtual Scalar getColumnLabel(int index)
		{
			return columnLabels.get(index);
		}

		public virtual Vector RowLabels
		{
			get
			{
				return rowLabels;
			}
			set
			{
				if (value.rows() != rows_Renamed)
				{
					throw new System.ArgumentException("the row label size doesn't equal to the row number of the matrix.");
				}
				rowLabels = value;
			}
		}


		public virtual Vector ColumnLabels
		{
			get
			{
				return columnLabels;
			}
			set
			{
				if (value.rows() != columns_Renamed)
				{
					throw new System.ArgumentException("the column label size doesn't equal to the column number of the matrix.");
				}
				columnLabels = value;
			}
		}


		public virtual string String
		{
			get
			{
				int rows = Math.Min(Utils.DISPLAY_ROWS,rows());
				int limitColMaxWidth = 25;
				int length = 0;
				int curCol = 0;
				int maxColWidth;
				StringBuilder[] list = new StringBuilder[rows + 1];
				string[] listTmp = new string[rows + 1];
				int i, curSize;
    
				for (i = 0; i < list.Length; ++i)
				{
					list[i] = new StringBuilder();
				}
    
				//display row label
				if (rowLabels != null)
				{
					listTmp[0] = "";
					maxColWidth = 0;
					for (i = 0;i < rows;i++)
					{
						listTmp[i + 1] = rowLabels.get(i).getString();
						if (listTmp[i + 1].Length > maxColWidth)
						{
							maxColWidth = listTmp[i + 1].Length;
						}
					}
					maxColWidth++;
					for (i = 0;i <= rows;i++)
					{
						curSize = listTmp[i].Length;
						if (curSize <= maxColWidth)
						{
							list[i].Append(listTmp[i]);
							if (curSize < maxColWidth)
							{
								for (int j = 0; j < maxColWidth - curSize; ++j)
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
						}
					}
					length += maxColWidth;
				}
    
				while (length < Utils.DISPLAY_WIDTH && curCol < columns())
				{
					listTmp[0] = columnLabels == null ?"#" + curCol : columnLabels.get(curCol).String;
					maxColWidth = 0;
					for (i = 0;i < rows;i++)
					{
						listTmp[i + 1] = get(i, curCol).getString();
						if (listTmp[i + 1].Length > maxColWidth)
						{
							maxColWidth = listTmp[i + 1].Length;
						}
					}
					if (maxColWidth > limitColMaxWidth)
					{
						maxColWidth = limitColMaxWidth;
					}
					if ((int)listTmp[0].Length > maxColWidth)
					{
						maxColWidth = Math.Min(limitColMaxWidth, listTmp[0].Length);
					}
					if (curCol < columns() - 1)
					{
						maxColWidth++;
					}
    
					if (length + maxColWidth > Utils.DISPLAY_WIDTH && curCol + 1 < columns())
					{
						break;
					}
    
					for (i = 0;i <= rows;i++)
					{
						curSize = listTmp[i].Length;
						if (curSize <= maxColWidth)
						{
							list[i].Append(listTmp[i]);
							if (curSize < maxColWidth)
							{
								for (int j = 0; j < maxColWidth - curSize; ++j)
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
						}
					}
					length += maxColWidth;
					curCol++;
				}
    
				if (curCol < columns_Renamed)
				{
					for (i = 0;i <= rows;i++)
					{
						list[i].Append("...");
					}
				}
    
				StringBuilder resultStr = new StringBuilder();
				for (i = 0;i <= rows;i++)
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

		public override DATA_FORM DataForm
		{
			get
			{
				return DATA_FORM.DF_MATRIX;
			}
		}

		public virtual int rows()
		{
			return rows_Renamed;
		}

		public virtual int columns()
		{
			return columns_Renamed;
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void write(com.xxdb.io.ExtendedDataOutput out) throws java.io.IOException
		public virtual void write(ExtendedDataOutput @out)
		{
			int flag = ((int)DATA_FORM.DF_MATRIX << 8) + DataType.ordinal();
			@out.writeShort(flag);
			sbyte labelFlag = (sbyte)((hasRowLabel() ? 1 : 0) + (hasColumnLabel() ? 2 : 0));
			@out.writeByte(labelFlag);
			if (hasRowLabel())
			{
				rowLabels.write(@out);
			}
			if (hasColumnLabel())
			{
				columnLabels.write(@out);
			}
			@out.writeShort(flag);
			@out.writeInt(rows());
			@out.writeInt(columns());
			writeVectorToOutputStream(@out);
		}
	}

}