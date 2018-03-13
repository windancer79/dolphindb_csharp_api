using dolphindb.data;
using System;

namespace dolphindb.data
{

	/// 
	/// <summary>
	/// Interface for matrix object
	/// 
	/// </summary>

	public interface IMatrix : IEntity
	{
		bool isNull(int row, int column);
		void setNull(int row, int column);
		IScalar getRowLabel(int row);
        IScalar getColumnLabel(int column);
        IScalar get(int row, int column);
		IVector RowLabels {get;}
        IVector ColumnLabels {get;}
		bool hasRowLabel();
		bool hasColumnLabel();
		Type ElementClass {get;}
	}

}