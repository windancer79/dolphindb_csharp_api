using com.xxdb.jobjects;

namespace com.xxdb.data
{

	/// 
	/// <summary>
	/// Interface for scalar object
	/// 
	/// </summary>

	public interface Scalar : Entity
	{
		bool Null {get;}
		void setNull();
        //JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        //ORIGINAL LINE: Number getNumber() throws Exception;
        Number getNumber();
        //JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        //ORIGINAL LINE: java.time.temporal.Temporal getTemporal() throws Exception;
        Temporal getTemporal();
	}

}