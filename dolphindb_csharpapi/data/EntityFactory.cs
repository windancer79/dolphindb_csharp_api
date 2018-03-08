namespace com.xxdb.data
{

	using ExtendedDataInput = com.xxdb.io.ExtendedDataInput;

	public interface EntityFactory
	{
		Entity createEntity(DATA_FORM form, DATA_TYPE type, ExtendedDataInput @in);
		//Matrix createMatrixWithDefaultValue(DATA_TYPE type, int rows, int columns);
		//Vector createVectorWithDefaultValue(DATA_TYPE type, int size);
		//Vector createPairWithDefaultValue(DATA_TYPE type);
		Scalar createScalarWithDefaultValue(DATA_TYPE type);
	}

}