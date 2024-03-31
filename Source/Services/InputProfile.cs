using System;

namespace EasyInputHandling
{
	public abstract class InputProfile<TContext>
	{
		protected readonly IInputFactoryBuilder<TContext> _builder;

		public InputProfile(Action<IInputFactoryBuilder<TContext>>[] builderExpressions)
		{
			_builder = new InputFactoryBuilder<TContext>(builderExpressions);
		}

		public abstract void Configure();

		public IInputFactory<TContext> Build()
		{
			return _builder.Create();
		}
	}
}
