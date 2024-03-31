using System;
using System.Collections.Generic;

namespace EasyInputHandling
{
	internal class InputFactory<TContext> : IInputFactory<TContext>
	{
		private readonly List<Func<TContext, IInput>> _expressions = new();

		public InputFactory(List<Func<TContext, IInput>> expressions)
		{
			_expressions = expressions;
		}

		public IInput Create(TContext context)
		{
			var holders = new List<IInput>();

			foreach (var expression in _expressions)
			{
				var holder = expression(context);

				holders.Add(holder);
			}

			return new Input(holders);
		}
	}
}
