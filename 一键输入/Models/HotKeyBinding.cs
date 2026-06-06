using System;
using System.Text;
using System.Windows.Forms;
using 一键输入.Native;
using 一键输入.Services;

namespace 一键输入.Models
{
	internal sealed class HotKeyBinding : IEquatable<HotKeyBinding>
	{
		public const uint DefaultModifiers = NativeMethods.ModControl;
		public const uint DefaultVirtualKey = 0x56; // V

		public uint Modifiers { get; }
		public uint VirtualKey { get; }

		public HotKeyBinding(uint modifiers, uint virtualKey)
		{
			if (virtualKey == 0)
				throw new ArgumentException("必须指定主键。", nameof(virtualKey));

			Modifiers = modifiers;
			VirtualKey = virtualKey;
		}

		public static HotKeyBinding Default => new HotKeyBinding(DefaultModifiers, DefaultVirtualKey);

		public static HotKeyBinding LoadSaved()
		{
			int modifiers;
			int virtualKey;

			if (SettingsStore.TryLoadHotKey(out modifiers, out virtualKey))
				return FromSettings(modifiers, virtualKey);

			// 兼容旧版 Properties.Settings
			try
			{
				var legacy = Properties.Settings.Default;
				var binding = FromSettings(legacy.HotKeyModifiers, legacy.HotKeyVirtualKey);
				binding.SaveToSettings();
				return binding;
			}
			catch
			{
				return Default;
			}
		}

		public static HotKeyBinding FromSettings(int modifiers, int virtualKey)
		{
			if (virtualKey <= 0)
				return Default;

			return new HotKeyBinding((uint)modifiers, (uint)virtualKey);
		}

		public static HotKeyBinding FromKeyEvent(KeyEventArgs e)
		{
			if (IsModifierKey(e.KeyCode))
				return null;

			uint modifiers = 0;
			if (e.Control)
				modifiers |= NativeMethods.ModControl;
			if (e.Alt)
				modifiers |= NativeMethods.ModAlt;
			if (e.Shift)
				modifiers |= NativeMethods.ModShift;
			if (e.KeyCode == Keys.LWin || e.KeyCode == Keys.RWin)
				modifiers |= NativeMethods.ModWin;

			if (modifiers == 0)
				return null;

			return new HotKeyBinding(modifiers, (uint)e.KeyCode);
		}

		public static bool IsModifierKey(Keys key)
		{
			return key == Keys.ControlKey
				|| key == Keys.ShiftKey
				|| key == Keys.Menu
				|| key == Keys.LControlKey
				|| key == Keys.RControlKey
				|| key == Keys.LShiftKey
				|| key == Keys.RShiftKey
				|| key == Keys.LMenu
				|| key == Keys.RMenu
				|| key == Keys.LWin
				|| key == Keys.RWin;
		}

		public string ToDisplayString()
		{
			var builder = new StringBuilder();

			if ((Modifiers & NativeMethods.ModControl) != 0)
				builder.Append("Ctrl + ");
			if ((Modifiers & NativeMethods.ModShift) != 0)
				builder.Append("Shift + ");
			if ((Modifiers & NativeMethods.ModAlt) != 0)
				builder.Append("Alt + ");
			if ((Modifiers & NativeMethods.ModWin) != 0)
				builder.Append("Win + ");

			builder.Append(FormatVirtualKey(VirtualKey));
			return builder.ToString();
		}

		private static string FormatVirtualKey(uint virtualKey)
		{
			var key = (Keys)virtualKey;
			if (key >= Keys.D0 && key <= Keys.D9)
				return ((char)('0' + (key - Keys.D0))).ToString();

			if (key >= Keys.A && key <= Keys.Z)
				return key.ToString();

			switch (key)
			{
				case Keys.Space: return "Space";
				case Keys.Return: return "Enter";
				case Keys.Tab: return "Tab";
				case Keys.Escape: return "Esc";
				case Keys.Insert: return "Insert";
				case Keys.Delete: return "Delete";
				case Keys.Home: return "Home";
				case Keys.End: return "End";
				case Keys.PageUp: return "Page Up";
				case Keys.PageDown: return "Page Down";
				case Keys.Oemtilde: return "`";
				case Keys.OemMinus: return "-";
				case Keys.Oemplus: return "=";
				case Keys.OemOpenBrackets: return "[";
				case Keys.OemCloseBrackets: return "]";
				case Keys.OemPipe: return "\\";
				case Keys.OemSemicolon: return ";";
				case Keys.OemQuotes: return "'";
				case Keys.Oemcomma: return ",";
				case Keys.OemPeriod: return ".";
				case Keys.OemQuestion: return "/";
				default:
					if (key >= Keys.F1 && key <= Keys.F24)
						return key.ToString();
					return key.ToString();
			}
		}

		public void SaveToSettings()
		{
			SettingsStore.SaveHotKey((int)Modifiers, (int)VirtualKey);
		}

		public bool Equals(HotKeyBinding other)
		{
			if (other == null)
				return false;

			return Modifiers == other.Modifiers && VirtualKey == other.VirtualKey;
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as HotKeyBinding);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return ((int)Modifiers * 397) ^ (int)VirtualKey;
			}
		}
	}
}
