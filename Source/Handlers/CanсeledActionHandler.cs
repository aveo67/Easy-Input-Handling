using System;
using UnityEngine.InputSystem;

namespace EasyInputHandling
{
	internal class CanсeledActionHandler<TContext, TActions> : CanсeledActionHandlerBase<Action<TContext>, TContext, TActions>
	{
		public CanсeledActionHandler(
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

	internal class CanсeledActionHandler<TContext, TActions, TPayload> : CanсeledActionHandlerBase<Action<TContext, TPayload>, TContext, TActions>
	{
		private readonly Func<InputAction.CallbackContext, TPayload> _payloadExpression;

		public CanсeledActionHandler(
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
