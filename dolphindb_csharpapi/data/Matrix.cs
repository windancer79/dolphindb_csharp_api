using System;

namespace com.xxdb.data
{

	/// 
	/// <summary>
	/// Interface for matrix object
	/// 
	/// </summary>

	public interface Matrix : Entity
	{
		bool isNull(int row, int column);
		void setNull(int row, int column);
		Scalar getRowLabel(int row);
		Scalar getColumnLabel(int column);
		Scalar get(int row, int column);
		Vector RowLabels {get;}
		Vector ColumnLabels {get;}
		bool hasRowLabel();
		bool hasColumnLabel();
		Type ElementClass {get;}
	}

}