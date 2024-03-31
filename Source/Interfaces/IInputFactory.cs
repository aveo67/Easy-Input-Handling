namespace EasyInputHandling
{
	/// <summary>
	/// Factory interface that create an instance of input handler for a context 
	/// </summary>
	/// <typeparam name="TContext">Type of context</typeparam>
	public interface IInputFactory<in TContext>
	{
		/// <summary>
		/// Create an instance of input handler
		/// </summary>
		/// <param name="context">Context for handling</param>
		IInput Create(TContext context);
	}
}
