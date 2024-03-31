using System;
using UnityEngine.InputSystem;

namespace EasyInputHandling
{
	/// <summary>
	/// Input handler for context of type <typeparamref name="TContext"/>
	/// which handle input action which determined by expression <typeparamref name="TTargetActionExpresion"/>
	/// </summary>
	/// <typeparam name="TExpression">Type of expression which handle target input action</typeparam>
	/// <typeparam name="TContext">Type of context for input handling</typeparam>
	/// <typeparam name="TTargetActionExpresion">Type of expression which determines exact input action</typeparam>
	internal abstract class InputActionHandler<TExpression, TContext, TTargetActionExpresion> : IInput
	{
		/// <summary>
		/// Expression which handle input action
		/// </summary>
		protected readonly TExpression _expression;

		/// <summary>
		/// Reference of target input action
		/// </summary>
		protected readonly InputAction _targetAction;

		/// <summary>
		/// Context for input handling
		/// </summary>
		protected readonly TContext _context;

		/// <summary>
		/// Expression which determines exact input action
		/// </summary>
		protected readonly TTargetActionExpresion _actions;

		/// <summary>
		/// Ctor
		/// </summary>
		/// <param name="expression">Expression which handle input action</param>
		/// <param name="targetActionExpression">Выражение которое возвращает целевое событие ввода</param>
		/// <param name="context">Context for input handling</param>
		/// <param name="targetinputActionExpression">Expression which determines exact input action</param>
		public InputActionHandler(
			TExpression expression,
			Func<TTargetActionExpresion, InputAction> targetActionExpression,
			TContext context,
			TTargetActionExpresion targetinputActionExpression)
		{
			_expression = expression;
			_context = context;
			_actions = targetinputActionExpression;

			_targetAction = targetActionExpression(_actions);
		}

		/// <inheritdoc/>
		public void Enable()
		{
			_targetAction.Enable();
		}

		/// <inheritdoc/>
		public void Disable()
		{
			_targetAction.Disable();
		}

		/// <inheritdoc/>
		public void Dispose()
		{
			_targetAction.Dispose();
		}
	}
}
