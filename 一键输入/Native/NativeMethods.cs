using System;
using System.Runtime.InteropServices;

namespace 一键输入.Native
{
	internal static class NativeMethods
	{
		public const int WmHotKey = 0x0312;

		public const uint ModAlt = 0x0001;
		public const uint ModControl = 0x0002;
		public const uint ModShift = 0x0004;
		public const uint ModWin = 0x0008;

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

		[DllImport("user32.dll")]
		public static extern bool UnregisterHotKey(IntPtr hWnd, int id);
	}
}
