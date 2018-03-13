using System;

namespace dolphindb.data
{

	public interface IVector : IEntity
	{

		bool isNull(int index);
		int Null {set;}
		IScalar get(int index);
		void set(int index, IScalar value);
		Type ElementClass {get;}
	}

	public static class Vector_Fields
	{
		public const int DISPLAY_ROWS = 10;
	}

}