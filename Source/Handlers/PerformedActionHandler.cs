using System;
using UnityEngine.InputSystem;

namespace EasyInputHandling
{
	internal class PerformedActionHandler<TContext, TActions> : PerformedActionHandlerBase<Action<TContext>, TContext, TActions>
	{
		public PerformedActionHandler(
			Action<TContext> expression,
			Func<TActions, InputAction> targetActionExpression,
			TContext context,
			TActions actions)
			: base(expression, targetActionExpression, context, actions)
		{
		}

		protected override void Process(InputAction.CallbackContext context)
		{
			_expression(_context);
		}
	}

	internal class PerformedActionHandler<TContext, TActions, TPayload> : PerformedActionHandlerBase<Action<TContext, TPayload>, TContext, TActions>
	{
		private readonly Func<InputAction.CallbackContext, TPayload> _payloadExpression;

		public PerformedActionHandler(
			Action<TContext, TPayload> expression,
			Func<TActions, InputAction> targetActionExpression,
			Func<InputAction.CallbackContext, TPayload> payloadExpression,
			TContext context,
			TActions actions)
			: base(expression, targetActionExpression, context, actions)
		{
			_payloadExpression = payloadExpression;
		}

		protected override void Process(InputAction.CallbackContext context)
		{
			var payload = _payloadExpression(context);

			_expression(_context, payload);
		}
	}
}
