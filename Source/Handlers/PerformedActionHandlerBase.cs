using System;
using UnityEngine.InputSystem;

namespace EasyInputHandling
{
	internal abstract class PerformedActionHandlerBase<TExpression, TContext, TActions> : InputActionHandler<TExpression, TContext, TActions>
	{
		public PerformedActionHandlerBase(
			TExpression expression,
			Func<TActions, InputAction> targetActionExpression,
			TContext context,
			TActions actions)
			: base(expression, targetActionExpression, context, actions)
		{
			_targetAction.performed += Process;
		}

		protected abstract void Process(InputAction.CallbackContext context);
	}
}