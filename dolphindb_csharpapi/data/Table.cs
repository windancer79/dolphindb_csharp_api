namespace com.xxdb.data
{

	/// 
	/// <summary>
	/// Interface for table object
	/// 
	/// </summary>

	public interface Table : Entity
	{
		Vector getColumn(int index);
		Vector getColumn(string name);
		string getColumnName(int index);
	}

}