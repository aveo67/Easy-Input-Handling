using System;
using System.Collections.Generic;
using UnityEngine.InputSystem;

namespace EasyInputHandling
{
	internal class InputHandlerBuilder<TActions, TContext> : IInputHandlerBuilder<TActions, TContext>
		where TActions : IInputActionCollection, IDisposable, new()
	{
		private readonly List<Func<TContext, TActions, IInput>> _buildingExpressions = new();

		public void Add(Func<TContext, TActions, IInput> expression)
		{
			_buildingExpressions.Add(expression);
		}

		public IInput Build(TContext context)
		{
			var actions = new TActions();

			List<IInput> handlers = new List<IInput>();

			foreach (var expression in _buildingExpressions)
			{
				var handler = expression(context, actions);

				handlers.Add(handler);
			}

			return new InputHandler<TContext, TActions>(handlers, actions);
		}
	}
}
