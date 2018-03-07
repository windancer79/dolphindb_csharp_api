using System;

namespace com.xxdb.data
{
	/// 
	/// <summary>
	/// Interface for DolphinDB data form: ARRAY, BIGARRAY
	/// 
	/// </summary>

	public interface Vector : Entity
	{

		bool isNull(int index);
		int Null {set;}
		Scalar get(int index);
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: void set(int index, Scalar value) throws Exception;
		void set(int index, Scalar value);
		Type ElementClass {get;}
	}

	public static class Vector_Fields
	{
		public const int DISPLAY_ROWS = 10;
	}

}