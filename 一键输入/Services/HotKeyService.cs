using System;
using System.Windows.Forms;
using 一键输入.Models;
using 一键输入.Native;

namespace 一键输入.Services
{
	internal sealed class HotKeyService : IDisposable
	{
		private const int HotKeyId = 1;

		private readonly Form _owner;
		private HotKeyBinding _currentBinding;
		private bool _isRegistered;

		public event EventHandler HotKeyPressed;

		public HotKeyBinding CurrentBinding => _currentBinding;

		public bool IsRegistered => _isRegistered;

		public HotKeyService(Form owner)
		{
			_owner = owner ?? throw new ArgumentNullException(nameof(owner));
		}

		public bool TryRegister(HotKeyBinding binding)
		{
			if (binding == null)
				throw new ArgumentNullException(nameof(binding));

			Unregister();

			if (!NativeMethods.RegisterHotKey(_owner.Handle, HotKeyId, binding.Modifiers, binding.VirtualKey))
				return false;

			_currentBinding = binding;
			_isRegistered = true;
			return true;
		}

		public void Unregister()
		{
			if (!_isRegistered)
				return;

			NativeMethods.UnregisterHotKey(_owner.Handle, HotKeyId);
			_isRegistered = false;
		}

		public bool ProcessWindowMessage(ref Message message)
		{
			if (message.Msg != NativeMethods.WmHotKey)
				return false;

			HotKeyPressed?.Invoke(this, EventArgs.Empty);
			return true;
		}

		public void Dispose()
		{
			Unregister();
		}
	}
}
