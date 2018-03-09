using dolphindb.jobjects;

namespace dolphindb.data
{

	public interface IScalar : IEntity
	{
        bool isNull(); 
		void setNull();

        object getNumber();
        object getTemporal();
	}

}