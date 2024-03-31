using System;

namespace EasyInputHandling
{
	public sealed class InputImplicitProfile<TContext>
	{
		private readonly IInputFactoryBuilder<TContext> _builder;

		public InputImplicitProfile(Action<IInputFactoryBuilder<TContext>>[] builderExpressions)
		{
			_builder = new InputFactoryBuilder<TContext>(builderExpressions);
		}

		public void Configure(Action<IInputFactoryBuilder<TContext>> expression)
		{
			if (expression == null)
				throw new ArgumentNullException("Builder expression is null");

			expression(_builder);
		}

		public IInputFactory<TContext> Build()
		{
			return _builder.Create();
		}
	}
}
