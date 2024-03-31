using System;
using UnityEngine.InputSystem;

namespace EasyInputHandling
{
	internal abstract class CanсeledActionHandlerBase<TExpression, TContext, TActions> : InputActionHandler<TExpression, TContext, TActions>
	{
		public CanсeledActionHandlerBase(
			TExpression action,
			Func<TActions, InputAction> targetActionExpression,
			TContext context,
			TActions actions)
			: base(action, targetActionExpression, context, actions)
		{
			_targetAction.canceled += Process;
		}

		protected abstract void Process(InputAction.CallbackContext context);
	}
}
