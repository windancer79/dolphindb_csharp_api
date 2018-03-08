using com.xxdb.jobjects;

namespace com.xxdb.data
{

	public interface Scalar : Entity
	{
		bool Null {get;}
		void setNull();

        Number getNumber();
        Temporal getTemporal();
	}

}