using System;
using System.Collections.Generic;
using UnityEngine.InputSystem;

namespace EasyInputHandling
{
	internal class InputHandler<TContext, TActions> : IInput
		where TActions : IInputActionCollection, IDisposable, new()
	{
		private readonly List<IInput> _handlers;

		private readonly TActions _actions;

		public InputHandler(List<IInput> handlers, TActions actions)
		{
			_handlers = handlers;
			_actions = actions;
		}

		public void Enable()
		{
			foreach (var handlers in _handlers)
			{
				handlers.Enable();
			}

			_actions.Enable();
		}

		public void Disable()
		{
			foreach (var handlers in _handlers)
			{
				handlers.Disable();
			}

			_actions.Disable();
		}

		public void Dispose()
		{
			foreach (IDisposable handler in _handlers)
			{
				handler.Dispose();
			}

			_actions.Dispose();
		}
	}
}
