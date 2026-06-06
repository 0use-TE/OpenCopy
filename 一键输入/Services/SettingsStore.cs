using System;
using System.IO;
using 一键输入.Native;

namespace 一键输入.Services
{
	internal static class SettingsStore
	{
		private static readonly string SettingsDirectory = Path.Combine(
			Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
			"OpenCopy");

		private static readonly string SettingsFile = Path.Combine(SettingsDirectory, "settings.txt");

		public static void SaveHotKey(int modifiers, int virtualKey)
		{
			Directory.CreateDirectory(SettingsDirectory);
			File.WriteAllText(SettingsFile, modifiers + Environment.NewLine + virtualKey);
		}

		public static bool TryLoadHotKey(out int modifiers, out int virtualKey)
		{
			modifiers = (int)NativeMethods.ModControl;
			virtualKey = 0x56;

			if (!File.Exists(SettingsFile))
				return false;

			try
			{
				var lines = File.ReadAllLines(SettingsFile);
				if (lines.Length < 2)
					return false;

				if (!int.TryParse(lines[0].Trim(), out modifiers))
					return false;

				if (!int.TryParse(lines[1].Trim(), out virtualKey))
					return false;

				return virtualKey > 0;
			}
			catch
			{
				return false;
			}
		}
	}
}
