using System;

namespace EasyInputHandling
{
	/// <summary>
	/// Input handler builder interface
	/// </summary>
	/// <typeparam name="TActions">Input action type</typeparam>
	/// <typeparam name="TContext">Input handling context type</typeparam>
	public interface IInputHandlerBuilder<TActions, TContext>
	{
		/// <summary>
		/// Builds an instance of input handler
		/// </summary>
		/// <param name="context">An instance of context for input handling</param>
		IInput Build(TContext context);

		/// <summary>
		/// Adds input handling expression
		/// </summary>
		/// <param name="expression">Выражение которое выполнится при создании экземпляра обработчика ввода</param>
		void Add(Func<TContext, TActions, IInput> expression);
	}
}
