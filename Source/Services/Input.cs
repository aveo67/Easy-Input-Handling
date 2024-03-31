using System.Collections.Generic;

namespace EasyInputHandling
{
	internal class Input : IInput
	{
		private readonly List<IInput> _holders;

		public Input(List<IInput> holders)
		{
			_holders = holders;
		}

		public void Enable()
		{
			foreach (var holder in _holders)
			{
				holder.Enable();
			}
		}

		public void Disable()
		{
			foreach (var holder in _holders)
			{
				holder.Disable();
			}
		}

		public void Dispose()
		{
			foreach (var holder in _holders)
			{
				holder.Dispose();
			}
		}
	}
}
