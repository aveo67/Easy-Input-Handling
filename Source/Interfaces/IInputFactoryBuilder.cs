using System;
using UnityEngine.InputSystem;

namespace EasyInputHandling
{
	/// <summary>
	/// Factory builder interface that collect creation expressions and create an instance of input handler factory
	/// </summary>
	/// <typeparam name="TContext">Context type to input handling</typeparam>
	public interface IInputFactoryBuilder<TContext>
	{
		/// <summary>
		/// Returns input handler builder
		/// </summary>
		/// <typeparam name="TActions">Action type of builder expression</typeparam>
		IInputHandlerBuilder<TActions, TContext> GetBuilder<TActions>()
			where TActions : IInputActionCollection, IDisposable, new();

		/// <summary>
		/// Add a builder expression for creating an instance of input handler
		/// </summary>
		/// <param name="expression">Builder expression</param>
		void Add(Func<TContext, IInput> expression);

		/// <summary>
		/// Creates an input handler factory instance
		/// </summary>
		/// <returns></returns>
		IInputFactory<TContext> Create();
	}
}
