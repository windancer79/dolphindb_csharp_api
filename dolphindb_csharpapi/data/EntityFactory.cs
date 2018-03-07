namespace com.xxdb.data
{

	using ExtendedDataInput = com.xxdb.io.ExtendedDataInput;

	public interface EntityFactory
	{
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: Entity createEntity(Entity_DATA_FORM form, Entity_DATA_TYPE type, com.xxdb.io.ExtendedDataInput in) throws java.io.IOException;
		Entity createEntity(DATA_FORM form, DATA_TYPE type, ExtendedDataInput @in);
		Matrix createMatrixWithDefaultValue(DATA_TYPE type, int rows, int columns);
		Vector createVectorWithDefaultValue(DATA_TYPE type, int size);
		Vector createPairWithDefaultValue(DATA_TYPE type);
		Scalar createScalarWithDefaultValue(DATA_TYPE type);
	}

}