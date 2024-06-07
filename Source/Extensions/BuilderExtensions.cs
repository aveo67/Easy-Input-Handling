using System;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

namespace EasyInputHandling
{
	public static class BuilderExtensions
	{
		/// <summary>
		/// Указывает что обработка ввода должна также обрабатывать события также для набора событий типа <typeparamref name="TInputActions"/>
		/// </summary>
		/// <typeparam name="TInputActions">Тип набора событий ввода</typeparam>
		/// <typeparam name="TContext">Контекст обработки ввода</typeparam>
		/// <param name="builder">Строитель общего обработчика ввода</param>
		/// <param name="expression">Действие которое будет выполнено при создании экземпляра фабрики создания обработчика ввода для контекста <typeparamref name="TContext"/></param>
		public static IInputFactoryBuilder<TContext> UseHandling<TInputActions, TContext>(this IInputFactoryBuilder<TContext> builder, Action<IInputHandlerBuilder<TInputActions, TContext>> expression)
			where TInputActions : IInputActionCollection, IDisposable, new()
		{
			var b = builder.GetBuilder<TInputActions>();
			expression(b);

			return builder;
		}

		/// <summary>
		/// Добавляет обработку события performe
		/// </summary>
		/// <typeparam name="TInputActions">Тип набора событий ввода</typeparam>
		/// <typeparam name="TContext">Контекст обработки ввода</typeparam>
		/// <param name="builder">Строитель общего обработчика ввода</param>
		/// <param name="targetActionExpression">Выражение которое определяет событие ввода которое будет обрабатываться выражением <paramref name="expression"/></param>
		/// <param name="expression">Выражение которое будет выполнено при целевом событии ввода</param>
		public static IInputHandlerBuilder<TInputActions, TContext> HandleInputActionPerformed<TInputActions, TContext>(
			this IInputHandlerBuilder<TInputActions, TContext> builder,
			Func<TInputActions, InputAction> targetActionExpression,
			Action<TContext> expression)
		{
			Func<TContext, TInputActions, IInput> exp = (context, actions) =>
			{
				return new PerformedActionHandler<TContext, TInputActions>(expression, targetActionExpression, context, actions);
			};

			builder.Add(exp);

			return builder;
		}

		/// <summary>
		/// Добавляет обработку события performe
		/// </summary>
		/// <typeparam name="TInputActions">Тип набора событий ввода</typeparam>
		/// <typeparam name="TContext">Контекст обработки ввода</typeparam>
		/// <typeparam name="TPayload">Тип полезной нагрузки ввода</typeparam>
		/// <param name="builder">Строитель общего обработчика ввода</param>
		/// <param name="targetActionExpression">Выражение которое определяет событие ввода которое будет обрабатываться выражением <paramref name="expression"/></param>
		/// <param name="payloadExpression">Выражение которое определяет значение полезной нагрузки</param>
		/// <param name="expression">Выражение которое будет выполнено при целевом событии ввода</param>
		public static IInputHandlerBuilder<TInputActions, TContext> HandleInputActionPerformed<TInputActions, TContext, TPayload>(
			this IInputHandlerBuilder<TInputActions, TContext> builder,
			Func<TInputActions, InputAction> targetActionExpression,
			Func<InputAction.CallbackContext, TPayload> payloadExpression,
			Action<TContext, TPayload> expression)
		{
			Func<TContext, TInputActions, IInput> exp = (context, actions) =>
			{
				return new PerformedActionHandler<TContext, TInputActions, TPayload>(expression, targetActionExpression, payloadExpression, context, actions);
			};

			builder.Add(exp);

			return builder;
		}

		/// <summary>
		/// Добавляет обработку события canceled
		/// </summary>
		/// <typeparam name="TInputActions">Тип набора событий ввода</typeparam>
		/// <typeparam name="TContext">Контекст обработки ввода</typeparam>
		/// <param name="builder">Строитель общего обработчика ввода</param>
		/// <param name="targetActionExpression">Выражение которое определяет событие ввода которое будет обрабатываться выражением <paramref name="expression"/></param>
		/// <param name="expression">Выражение которое будет выполнено при целевом событии ввода</param>
		public static IInputHandlerBuilder<TInputActions, TContext> HandleInputActionCanceled<TInputActions, TContext>(
			this IInputHandlerBuilder<TInputActions, TContext> builder,
			Func<TInputActions, InputAction> targetActionExpression,
			Action<TContext> expression)
		{
			Func<TContext, TInputActions, IInput> exp = (context, actions) =>
			{
				return new CanсeledActionHandler<TContext, TInputActions>(expression, targetActionExpression, context, actions);
			};

			builder.Add(exp);

			return builder;
		}

		/// <summary>
		/// Добавляет обработку события canceled
		/// </summary>
		/// <typeparam name="TInputActions">Тип набора событий ввода</typeparam>
		/// <typeparam name="TContext">Контекст обработки ввода</typeparam>
		/// <typeparam name="TPayload">Тип полезной нагрузки ввода</typeparam>
		/// <param name="builder">Строитель общего обработчика ввода</param>
		/// <param name="targetActionExpression">Выражение которое определяет событие ввода которое будет обрабатываться выражением <paramref name="expression"/></param>
		/// <param name="payloadExpression">Выражение которое определяет значение полезной нагрузки</param>
		/// <param name="expression">Выражение которое будет выполнено при целевом событии ввода</param>
		public static IInputHandlerBuilder<TInputActions, TContext> HandleInputActionCanceled<TInputActions, TContext, TPayload>(
			this IInputHandlerBuilder<TInputActions, TContext> builder,
			Func<TInputActions, InputAction> targetActionExpression,
			Func<InputAction.CallbackContext, TPayload> payloadExpression,
			Action<TContext, TPayload> expression)
		{
			Func<TContext, TInputActions, IInput> exp = (context, actions) =>
			{
				return new CanсeledActionHandler<TContext, TInputActions, TPayload>(expression, targetActionExpression, payloadExpression, context, actions);
			};

			builder.Add(exp);

			return builder;
		}

		public static IInputHandlerBuilder<TInputActions, TContext> HandleInputContinuousAction<TInputActions, TContext, TPayload>(
			this IInputHandlerBuilder<TInputActions, TContext> builder,
			Func<TInputActions, InputAction> targetActionExpression,
			Func<InputAction.CallbackContext, TPayload> payloadExpression,
			Action<TContext, TPayload> expression,
			Action endAction = null,
			Func<TContext, CancellationToken> tokenExpression = default)
		{
			Func<TContext, TInputActions, IInput> exp = (context, actions) =>
			{
				var token = tokenExpression?.Invoke(context) ?? default;

				return new ContinuousActionHandler<TPayload, TContext, TInputActions>(expression, targetActionExpression, payloadExpression, context, actions, endAction, token);
			};

			builder.Add(exp);

			return builder;
		}

		public static IInputHandlerBuilder<TInputActions, TContext> HandleInputContinuousAction<TInputActions, TContext, TPayload>(
			this IInputHandlerBuilder<TInputActions, TContext> builder,
			Func<TInputActions, InputAction> targetActionExpression,
			Func<InputAction.CallbackContext, TPayload> payloadExpression,
			Action<TContext, TPayload> expression,
			Action endAction = null)
			where TContext : MonoBehaviour
		{
			Func<TContext, TInputActions, IInput> exp = (context, actions) =>
			{
				var token = context.destroyCancellationToken;

				return new ContinuousActionHandler<TPayload, TContext, TInputActions>(expression, targetActionExpression, payloadExpression, context, actions, endAction, token);
			};

			builder.Add(exp);

			return builder;
		}
	}
}
