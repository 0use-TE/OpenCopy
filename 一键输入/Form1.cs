using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using WindowsInput;
using WindowsInput.Native;


namespace 一键输入
{
	public partial class Form1 : Form
	{

		// 引入 Windows API
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

		[DllImport("user32.dll")]
		public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

		// 定义快捷键
		private const int HOTKEY_ID = 1;
		private const uint MOD_CONTROL = 0x0002; // Ctrl 键
		private const uint VK_V = 0x56; // V 键（Ctrl+V）
		public Form1()
		{
			InitializeComponent();
			var stateLable = State;
			// 注册 Ctrl+V 快捷键
			if (RegisterHotKey(this.Handle, HOTKEY_ID, MOD_CONTROL, VK_V))
			{
				stateLable.Text = "运行";
				stateLable.BackColor = Color.Green;
			}
			else
			{
				MessageBox.Show("快捷键注册失败！");
				stateLable.BackColor = Color.Red;
				stateLable.Text = "失败";
			}
		}
		protected override void WndProc(ref Message m)
		{
			// 检查是否为我们注册的快捷键
			if (m.Msg == 0x0312) // WM_HOTKEY
			{
				// 快捷键被按下时调用模拟输入的方法
				string clipboardText = Clipboard.GetText(); // 获取剪切板的内容
				if (!string.IsNullOrEmpty(clipboardText))
				{
					SimulateTextInput(clipboardText); // 模拟输入剪切板的文本
				}
			}

			base.WndProc(ref m);
		}
		public void SimulateTextInput(string text)
		{
			InputSimulator simulator = new InputSimulator();

			// 遍历文本中的每个字符
			foreach (var c in text)
			{
				if (c == '\n' || c == '\r')
				{
					// 如果是换行符，模拟回车
					simulator.Keyboard.KeyPress(VirtualKeyCode.RETURN);
				}
				else
				{
					// 输入文本中的每个字符
					simulator.Keyboard.TextEntry(c);
				}
				Thread.Sleep(10); // 模拟自然的打字速度（可调节）
			}
		}
		protected override void OnFormClosed(FormClosedEventArgs e)
		{
			UnregisterHotKey(this.Handle, HOTKEY_ID); // 注销快捷键
			base.OnFormClosed(e);
		}

		private void label1_Click(object sender, EventArgs e)
		{

		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{

		}

		private void button1_Click(object sender, EventArgs e)
		{
			
		}

		private void tabPage1_Click(object sender, EventArgs e)
		{

		}

		private void Form1_Load(object sender, EventArgs e)
		{

		}

		private void textBox1_TextChanged_1(object sender, EventArgs e)
		{

		}

		private void richTextBox1_TextChanged(object sender, EventArgs e)
		{

		}

		private void richTextBox2_TextChanged(object sender, EventArgs e)
		{

		}

		private void label2_Click(object sender, EventArgs e)
		{

		}

		private void label3_Click(object sender, EventArgs e)
		{

		}

		private void label4_Click(object sender, EventArgs e)
		{

		}

		private void label6_Click(object sender, EventArgs e)
		{

		}
	}
}
