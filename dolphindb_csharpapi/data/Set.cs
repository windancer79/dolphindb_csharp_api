namespace com.xxdb.data
{

	/*
	 * Interface for set object
	 */

	public interface Set : Entity
	{
		bool contains(Scalar key);
		bool add(Scalar key);
	}

}