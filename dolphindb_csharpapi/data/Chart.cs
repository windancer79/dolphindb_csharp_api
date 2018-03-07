namespace com.xxdb.data
{

	/// 
	/// <summary>
	/// Interface for chart object
	/// 
	/// </summary>

	public interface Chart : Dictionary
	{

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: Chart_CHART_TYPE getChartType() throws Exception;
		Chart_CHART_TYPE ChartType {get;}
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: Matrix getData() throws Exception;
		Matrix Data {get;}
		string Title {get;}
		string XAxisName {get;}
		string YAxisName {get;}

	}

	public enum Chart_CHART_TYPE
	{
		CT_AREA,
		CT_BAR,
		CT_COLUMN,
		CT_HISTOGRAM,
		CT_LINE,
		CT_PIE,
		CT_SCATTER,
		CT_TREND
	}

}