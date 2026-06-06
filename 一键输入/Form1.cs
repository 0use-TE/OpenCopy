using System;
using System.Drawing;
using System.Windows.Forms;
using 一键输入.Models;
using 一键输入.Services;
using 一键输入.UI;

namespace 一键输入
{
	public partial class Form1 : Form
	{
		private const string DefaultHint = "点击上方录制快捷键";

		private readonly HotKeyService _hotKeyService;
		private readonly TextInputSimulator _textInputSimulator = new TextInputSimulator();
		private readonly Timer _feedbackTimer = new Timer();
		private HotKeyBinding _pendingBinding;
		private HotKeyBinding _activeBinding;
		private bool _isRecording;

		public Form1()
		{
			InitializeComponent();
			_hotKeyService = new HotKeyService(this);
			_hotKeyService.HotKeyPressed += OnHotKeyPressed;

			_feedbackTimer.Interval = 3000;
			_feedbackTimer.Tick += FeedbackTimer_Tick;

			applyHotKeyButton.Click += ApplyHotKeyButton_Click;
			resetHotKeyButton.Click += ResetHotKeyButton_Click;
			hotKeyTextBox.Click += HotKeyTextBox_Click;
			hotKeyTextBox.KeyDown += HotKeyTextBox_KeyDown;
			hotKeyTextBox.Leave += HotKeyTextBox_Leave;

			Shown += Form1_Shown;

			LoadSavedHotKey();
		}

		private void Form1_Shown(object sender, EventArgs e)
		{
			UpdateHotKeyDisplay();
			applyHotKeyButton.Focus();
		}

		private void LoadSavedHotKey()
		{
			_activeBinding = HotKeyBinding.LoadSaved();
			_pendingBinding = _activeBinding;
			UpdateHotKeyDisplay();

			if (!TryApplyBinding(_activeBinding, saveSettings: false))
				ShowFeedback("启动失败，快捷键可能被占用", FeedbackType.Error);
		}

		private void OnHotKeyPressed(object sender, EventArgs e)
		{
			try
			{
				if (!Clipboard.ContainsText())
					return;

				var clipboardText = Clipboard.GetText();
				if (string.IsNullOrEmpty(clipboardText))
					return;

				_textInputSimulator.Simulate(clipboardText);
			}
			catch (Exception ex)
			{
				ShowFeedback("输入失败 · " + ex.Message, FeedbackType.Error);
			}
		}

		private void HotKeyTextBox_Click(object sender, EventArgs e)
		{
			_isRecording = true;
			hotKeyTextBox.BackColor = AppTheme.SurfaceHover;
			hotKeyTextBox.ForeColor = AppTheme.SecondaryText;
			hotKeyTextBox.Text = "···";
		}

		private void HotKeyTextBox_Leave(object sender, EventArgs e)
		{
			AppTheme.StyleHotKeyBox(hotKeyTextBox);
			if (_isRecording)
			{
				_isRecording = false;
				UpdateHotKeyDisplay();
			}
		}

		private void HotKeyTextBox_KeyDown(object sender, KeyEventArgs e)
		{
			e.SuppressKeyPress = true;

			if (HotKeyBinding.IsModifierKey(e.KeyCode))
				return;

			var binding = HotKeyBinding.FromKeyEvent(e);
			if (binding == null)
			{
				_isRecording = false;
				hotKeyTextBox.ForeColor = AppTheme.Error;
				hotKeyTextBox.Text = "需组合键";
				ShowFeedback("请使用 Ctrl / Alt / Shift 组合键", FeedbackType.Error);
				return;
			}

			_isRecording = false;
			_pendingBinding = binding;
			hotKeyTextBox.ForeColor = AppTheme.Accent;
			hotKeyTextBox.Text = binding.ToDisplayString();
			ShowFeedback("已录制 · 点击应用保存", FeedbackType.Info);
		}

		private void ApplyHotKeyButton_Click(object sender, EventArgs e)
		{
			if (_pendingBinding == null)
			{
				ShowFeedback("请先录制快捷键", FeedbackType.Error);
				return;
			}

			if (_pendingBinding.Equals(_activeBinding))
			{
				ShowFeedback("快捷键未变更", FeedbackType.Info);
				UpdateHotKeyDisplay();
				return;
			}

			var previous = _activeBinding;
			if (TryApplyBinding(_pendingBinding, saveSettings: true))
			{
				ShowFeedback("应用成功 · " + _activeBinding.ToDisplayString(), FeedbackType.Success);
				return;
			}

			TryApplyBinding(previous, saveSettings: false);
			ShowFeedback("应用失败，快捷键可能被占用", FeedbackType.Error);
		}

		private void ResetHotKeyButton_Click(object sender, EventArgs e)
		{
			_pendingBinding = HotKeyBinding.Default;
			UpdateHotKeyDisplay();

			if (_pendingBinding.Equals(_activeBinding))
			{
				ShowFeedback("已是默认快捷键", FeedbackType.Info);
				return;
			}

			var previous = _activeBinding;
			if (TryApplyBinding(_pendingBinding, saveSettings: true))
			{
				ShowFeedback("已恢复默认 · Ctrl + V", FeedbackType.Success);
				return;
			}

			TryApplyBinding(previous, saveSettings: false);
			ShowFeedback("重置失败，快捷键可能被占用", FeedbackType.Error);
		}

		private bool TryApplyBinding(HotKeyBinding binding, bool saveSettings)
		{
			if (!_hotKeyService.TryRegister(binding))
			{
				SetStatus(false);
				return false;
			}

			_activeBinding = binding;
			_pendingBinding = binding;

			if (saveSettings)
				_activeBinding.SaveToSettings();

			UpdateHotKeyDisplay();
			SetStatus(true);
			return true;
		}

		private void UpdateHotKeyDisplay()
		{
			hotKeyTextBox.ForeColor = AppTheme.PrimaryText;
			hotKeyTextBox.Text = (_pendingBinding ?? _activeBinding).ToDisplayString();
		}

		private void SetStatus(bool isRunning)
		{
			statusDot.BackColor = isRunning ? AppTheme.Success : AppTheme.Error;
			statusLabel.Text = isRunning ? "就绪" : "异常";
			statusLabel.ForeColor = isRunning ? AppTheme.SecondaryText : AppTheme.Error;
		}

		private enum FeedbackType
		{
			Success,
			Error,
			Info
		}

		private void ShowFeedback(string message, FeedbackType type)
		{
			_feedbackTimer.Stop();
			hintLabel.Text = message;
			hintLabel.ForeColor = type == FeedbackType.Success
				? AppTheme.Success
				: type == FeedbackType.Error
					? AppTheme.Error
					: AppTheme.SecondaryText;
			_feedbackTimer.Start();
		}

		private void FeedbackTimer_Tick(object sender, EventArgs e)
		{
			_feedbackTimer.Stop();
			hintLabel.ForeColor = AppTheme.SecondaryText;
			hintLabel.Text = DefaultHint;
		}

		protected override void WndProc(ref Message m)
		{
			_hotKeyService.ProcessWindowMessage(ref m);
			base.WndProc(ref m);
		}

		protected override void OnFormClosed(FormClosedEventArgs e)
		{
			_feedbackTimer.Dispose();
			_hotKeyService.Dispose();
			base.OnFormClosed(e);
		}
	}
}
