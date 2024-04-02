


# Easy Input Handling

## Introduction

Easy Input Handling is a code-only tool for managing an input with New Input System. You can easily configure and perform input without typical boilerplate. Highly recommended to use this tool with input context that is state of finite state machine. Moreover, tool was designed in mindset that you would be able to use it with DI-container like Zenject.

## Requirements

- Unity 2023.1 or newer;
- New Input System package;

## Getting started

### Step 1. Create Input Action Map

First of all you just need to create an action map asset. In "Project" tab click right mouse button on free space and choose `Create`>`Input Actions`. Setup your action map in a regular way. Tool uses a code-only approach so the main thing is setting `C# Class Name` property in asset editor and ticking `Generate C# Class`. This class name will be used as a generic type when you configure input handler profile.

### Step 2. Create Input Handler Profile

For configuring input handling you need to inherit an `InputProfile` class and set Input Action Map class name as a generic type.

Override `Configure` method. Configure `_builder` field by using `UseHandling` method.
Next you need to setup input handler expression in a fluent way.

``` CSharp
internal class ExampleProfile : InputProfile<SomeContext>
{
	public ExampleProfile(Action<IInputFactoryBuilder<SomeContext>>[] builderExpressions)
		: base(builderExpressions)
	{
	}

	public override void Configure()
	{
		_builder
			.UseHandling<InputActions, SomeContext>(b => b.
				HandleInputActionPerformed(inputMap => inputMap.Mouse.Click, context => context.Shoot()).
				HandleInputActionPerformed(inputMap => inputMap.Mouse.Rotate, callbackContext => callbackContext.ReadValue<Vector2>(), (context, payload) => context.Rotate(payload)).
				HandleInputContinuousAction(inputMap => inputMap.Keyboard.Movement, callbackContext => callbackContext.ReadValue<Vector2>(), (context , payload) => context.Move(payload), (context) => context.DestroyCancellationToken).
				HandleInputActionCanceled(inputMap => inputMap.Keyboard.Space, context => context.Jump()).
				HandleInputActionCanceled(inputMap => inputMap.Mouse.Scroll, callbackContext => callbackContext.ReadValue<int>(), (context, payload) => context.Scroll(payload))
			);
	}
}
```

You can combine action maps handling by multiple calling of `UseHandling` method.

### Step 3. Use Input Handler

When profile is written you can create an instance of this profile. Invoke `Configure()` method and next `Build()`. You will get an instance of Input Handler Factory. Invoke `Create()` method with an instance of context as an argument to get instance of input handler. To start handling you need to call `Enable()` method. If you want to suspend handling call `Disable()` method. In the end you should call `Dispose()` method.

``` CSharp
// Create profile instance
ExampleProfile profile = new(null);

// Configure builder
profile.Configure();

// Build factory
IInputFactory<SomeContext> factory = profile.Build();

// Create input handler
IInput inputHandler = factory.Create(new SomeContext());

// Enable input handling
inputHandler.Enable();

// ...

// Suspend handling
inputHandler.Disable();

// ...

// Resume handling again
inputHandler.Enable();

// ...

// Disposing handler in the end
inputHandler.Dispose();
```

### Step 4.

...

### Step 5. Profit

😁

## Profile Extensions

You can extend handling by additional actions if you inherit `InputExtensionProfile` class. It is helpful when you want to handle input for additional context for example. `Configure` method returns an array of builder expressions which you can share with common profile.

``` CSharp
internal class ExampleInputExtensionProfile : InputExtensionProfile<SomeContext>
{
	private readonly AdditionalContext _additionalContext;

	public ExampleInputExtensionProfile(AdditionalContext additionalContext)
	{
		_additionalContext = additionalContext;
	}

	public override Action<IInputFactoryBuilder<SomeContext>> Configure()
	{
		return Foo;
	}

	private void Foo(IInputFactoryBuilder<SomeContext> builder)
	{
		builder.
			UseHandling<InputActions, SomeContext>(b => b.
				HandleInputActionPerformed(inputMap => inputMap.Mouse.Click, context => _additionalContext.SomeAction()));
	}
}
```

Then create an instance of extension profile class and share the array by calling `Configure()` method.

``` CSharp
ExampleInputExtensionProfile extensionProfile = new (new AdditionalContext());

ExampleProfile profile = new (extensionProfile.Configure());
```

## Input Handler Factory Builder Extension Methods

If you need to reuse Input Actions Map you can simplify it by using extension methods. For example you can create `UseMouseActions()` instead of `UseHandling()` or create `LeftMouseButtonClick()` method as a wrapper for `HandleInputActionPerformed()` method.

``` CSharp
public static IInputFactoryBuilder<TContext> UseDefaultMouseHandling<TContext>(
	this IInputFactoryBuilder<TContext> builder,
	Action<IInputHandlerBuilder<MouseActions, TContext>> expression)
{
	return builder.UseHandling(expression);
}

public static IInputHandlerBuilder<MouseActions, TContext> HandleLeftMouseButtonClick<TContext>(
	this IInputHandlerBuilder<MouseActions, TContext> builder,
	Action<TContext> expression)
{
	return builder.HandleInputActionPerformed(x => x.MouseActionsMap.LeftButtomClick, expression);
}
```

Use those methods instead of original when configuring builder in profile.

``` CSharp
public override void Configure()
{
	_builder.
		UseDefaultMouseHandling(b => b.
			HandleLeftMouseButtonClick(context => context.Shoot()));
}
```

## Zenject Extensions

[Link](https://github.com/aveo67/Easy-Input-Handling-Zenject-Extensions)

## Installation

Open the `Package Manager` window and click `+`. Choose `Install package from git URL...` and paste "https://github.com/aveo67/Easy-Input-Handling.git". Ckick `Install` button.

## Examples

Soon

## Licence

This library is under the MIT License.
