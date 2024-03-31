using System;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

namespace EasyInputHandling
{
	internal class ContinousActionHandler<TPayload, TContext, TActions> : InputActionHandler<Action<TContext, TPayload>, TContext, TActions>
	{
		private readonly Func<InputAction.CallbackContext, TPayload> _payloadExpression;

		private readonly CancellationToken _token;

		private bool _isPressed = false;

		public ContinousActionHandler(
			Action<TContext, TPayload> expression,
			Func<TActions, InputAction> targetActionExpression,
			Func<InputAction.CallbackContext, TPayload> payloadExpression,
			TContext context,
			TActions targetinputActionExpression,
			CancellationToken token)
			: base(expression, targetActionExpression, context, targetinputActionExpression)
		{
			_targetAction.performed += async (c) => await OnPerformed(c);
			_payloadExpression = payloadExpression;
			_token = token;
		}

		private async Awaitable OnPerformed(InputAction.CallbackContext context)
		{
			if (!_isPressed)
			{
				_isPressed = true;

				await Loop(context);

				_isPressed = false;
			}
		}

		private async Awaitable Loop(InputAction.CallbackContext context)
		{
			while (_targetAction.IsPressed())
			{
				Process(context);

				await Awaitable.FixedUpdateAsync(_token);
			}
		}

		protected void Process(InputAction.CallbackContext context)
		{
			var payload = _payloadExpression(context);

			_expression(_context, payload);
		}
	}
}
