using System;

namespace EasyInputHandling
{
	public abstract class InputExtensionProfile<TContext>
	{
		public abstract Action<IInputFactoryBuilder<TContext>> Configure();
	}
}
