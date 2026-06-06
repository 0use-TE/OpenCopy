using System.Drawing;
using System.Windows.Forms;

namespace 一键输入.UI
{
	internal static class AppTheme
	{
		public static readonly Color Background = Color.FromArgb(17, 17, 27);
		public static readonly Color Surface = Color.FromArgb(30, 30, 46);
		public static readonly Color SurfaceHover = Color.FromArgb(39, 39, 58);
		public static readonly Color Border = Color.FromArgb(49, 50, 68);
		public static readonly Color PrimaryText = Color.FromArgb(205, 214, 244);
		public static readonly Color SecondaryText = Color.FromArgb(108, 112, 134);
		public static readonly Color Accent = Color.FromArgb(137, 180, 250);
		public static readonly Color AccentMuted = Color.FromArgb(116, 143, 252);
		public static readonly Color Success = Color.FromArgb(166, 227, 161);
		public static readonly Color Error = Color.FromArgb(243, 139, 168);

		public static readonly Font TitleFont = new Font("Segoe UI Light", 22F, FontStyle.Regular);
		public static readonly Font HotKeyFont = new Font("Segoe UI Semibold", 16F, FontStyle.Bold);
		public static readonly Font BodyFont = new Font("Segoe UI", 9.5F);
		public static readonly Font SmallFont = new Font("Segoe UI", 8.5F);

		public static void StylePrimaryButton(Button button)
		{
			button.FlatStyle = FlatStyle.Flat;
			button.FlatAppearance.BorderSize = 0;
			button.BackColor = AccentMuted;
			button.ForeColor = Background;
			button.Font = BodyFont;
			button.Cursor = Cursors.Hand;
			button.Height = 34;
		}

		public static void StyleGhostButton(Button button)
		{
			button.FlatStyle = FlatStyle.Flat;
			button.FlatAppearance.BorderColor = Border;
			button.FlatAppearance.BorderSize = 1;
			button.BackColor = Surface;
			button.ForeColor = SecondaryText;
			button.Font = BodyFont;
			button.Cursor = Cursors.Hand;
			button.Height = 34;
		}

		public static void StyleHotKeyBox(TextBox textBox)
		{
			textBox.BackColor = Surface;
			textBox.ForeColor = PrimaryText;
			textBox.BorderStyle = BorderStyle.FixedSingle;
			textBox.Font = HotKeyFont;
		}
	}
}
