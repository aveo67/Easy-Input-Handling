using System;

namespace EasyInputHandling
{
	/// <summary>
	/// Interface of input handler
	/// </summary>
	public interface IInput : IDisposable
	{
		/// <summary>
		/// Enables input handling for this instance
		/// </summary>
		void Enable();

		/// <summary>
		/// Disables input handling for this instance
		/// </summary>
		void Disable();
	}
}
