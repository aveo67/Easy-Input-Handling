using System;
using System.Collections.Generic;
using UnityEngine.InputSystem;

namespace EasyInputHandling
{
	internal class InputFactoryBuilder<TContext> : IInputFactoryBuilder<TContext>
	{
		private readonly List<Func<TContext, IInput>> _expressions
			= new List<Func<TContext, IInput>>();

		private readonly Dictionary<Type, object> _builders = new Dictionary<Type, object>();

		public InputFactoryBuilder(Action<IInputFactoryBuilder<TContext>>[] actions)
		{
			foreach (var a in actions)
			{
				a(this);
			}
		}

		public void Add(Func<TContext, IInput> expression)
		{
			_expressions.Add(expression);
		}

		public IInputFactory<TContext> Create()
		{
			return new InputFactory<TContext>(_expressions);
		}

		public IInputHandlerBuilder<TActions, TContext> GetBuilder<TActions>()
			where TActions : IInputActionCollection, IDisposable, new()
		{
			if (_builders.TryGetValue(typeof(TActions), out var b))
			{
				return (IInputHandlerBuilder<TActions, TContext>)b;
			}

			var builder = new InputHandlerBuilder<TActions, TContext>();

			_builders.Add(typeof(TActions), builder);

			_expressions.Add(builder.Build);

			return builder;
		}
	}
}
