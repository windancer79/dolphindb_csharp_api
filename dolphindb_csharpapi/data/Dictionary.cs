namespace com.xxdb.data
{

	/*
	 * Interface for dictionary object
	 */

	public interface Dictionary : Entity
	{
		DATA_TYPE KeyDataType {get;}
		Entity get(Scalar key);
		bool put(Scalar key, Entity value);
	}

}