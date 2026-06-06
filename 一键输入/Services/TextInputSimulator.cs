using System.Threading;
using WindowsInput;
using WindowsInput.Native;

namespace 一键输入.Services
{
	internal sealed class TextInputSimulator
	{
		private const int DefaultDelayMs = 500;

		private readonly InputSimulator _simulator = new InputSimulator();

		public void Simulate(string text, int delayMs = DefaultDelayMs)
		{
			if (string.IsNullOrEmpty(text))
				return;

			Thread.Sleep(delayMs);

			for (var i = 0; i < text.Length; i++)
			{
				var character = text[i];

				if (character == '\r')
				{
					if (i + 1 < text.Length && text[i + 1] == '\n')
						continue;

					_simulator.Keyboard.KeyPress(VirtualKeyCode.RETURN);
					continue;
				}

				if (character == '\n')
				{
					_simulator.Keyboard.KeyPress(VirtualKeyCode.RETURN);
					continue;
				}

				_simulator.Keyboard.TextEntry(character);
			}
		}
	}
}
