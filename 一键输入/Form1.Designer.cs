using System.Drawing;
using System.Windows.Forms;
using 一键输入.UI;

namespace 一键输入
{
	partial class Form1
	{
		private System.ComponentModel.IContainer components = null;

		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.titleLabel = new System.Windows.Forms.Label();
			this.statusDot = new System.Windows.Forms.Panel();
			this.statusLabel = new System.Windows.Forms.Label();
			this.hotKeyTextBox = new System.Windows.Forms.TextBox();
			this.hintLabel = new System.Windows.Forms.Label();
			this.applyHotKeyButton = new System.Windows.Forms.Button();
			this.resetHotKeyButton = new System.Windows.Forms.Button();
			this.footerLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// titleLabel
			// 
			this.titleLabel.AutoSize = true;
			this.titleLabel.Font = AppTheme.TitleFont;
			this.titleLabel.ForeColor = AppTheme.PrimaryText;
			this.titleLabel.Location = new System.Drawing.Point(36, 32);
			this.titleLabel.Name = "titleLabel";
			this.titleLabel.Size = new System.Drawing.Size(133, 37);
			this.titleLabel.TabIndex = 0;
			this.titleLabel.Text = "OpenCopy";
			// 
			// statusDot
			// 
			this.statusDot.BackColor = AppTheme.Success;
			this.statusDot.Location = new System.Drawing.Point(318, 48);
			this.statusDot.Name = "statusDot";
			this.statusDot.Size = new System.Drawing.Size(8, 8);
			this.statusDot.TabIndex = 1;
			// 
			// statusLabel
			// 
			this.statusLabel.AutoSize = true;
			this.statusLabel.Font = AppTheme.SmallFont;
			this.statusLabel.ForeColor = AppTheme.SecondaryText;
			this.statusLabel.Location = new System.Drawing.Point(332, 44);
			this.statusLabel.Name = "statusLabel";
			this.statusLabel.Size = new System.Drawing.Size(32, 15);
			this.statusLabel.TabIndex = 2;
			this.statusLabel.Text = "就绪";
			// 
			// hotKeyTextBox
			// 
			this.hotKeyTextBox.Cursor = System.Windows.Forms.Cursors.Hand;
			this.hotKeyTextBox.Location = new System.Drawing.Point(36, 96);
			this.hotKeyTextBox.Name = "hotKeyTextBox";
			this.hotKeyTextBox.ReadOnly = true;
			this.hotKeyTextBox.Size = new System.Drawing.Size(328, 36);
			this.hotKeyTextBox.TabIndex = 3;
			this.hotKeyTextBox.TabStop = false;
			this.hotKeyTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			AppTheme.StyleHotKeyBox(this.hotKeyTextBox);
			// 
			// hintLabel
			// 
			this.hintLabel.AutoSize = true;
			this.hintLabel.Font = AppTheme.SmallFont;
			this.hintLabel.ForeColor = AppTheme.SecondaryText;
			this.hintLabel.Location = new System.Drawing.Point(36, 142);
			this.hintLabel.Name = "hintLabel";
			this.hintLabel.Size = new System.Drawing.Size(92, 15);
			this.hintLabel.TabIndex = 4;
			this.hintLabel.Text = "点击上方录制快捷键";
			// 
			// applyHotKeyButton
			// 
			this.applyHotKeyButton.Location = new System.Drawing.Point(36, 176);
			this.applyHotKeyButton.Name = "applyHotKeyButton";
			this.applyHotKeyButton.Size = new System.Drawing.Size(156, 34);
			this.applyHotKeyButton.TabIndex = 5;
			this.applyHotKeyButton.Text = "应用";
			this.applyHotKeyButton.UseVisualStyleBackColor = false;
			AppTheme.StylePrimaryButton(this.applyHotKeyButton);
			// 
			// resetHotKeyButton
			// 
			this.resetHotKeyButton.Location = new System.Drawing.Point(208, 176);
			this.resetHotKeyButton.Name = "resetHotKeyButton";
			this.resetHotKeyButton.Size = new System.Drawing.Size(156, 34);
			this.resetHotKeyButton.TabIndex = 6;
			this.resetHotKeyButton.Text = "重置";
			this.resetHotKeyButton.UseVisualStyleBackColor = false;
			AppTheme.StyleGhostButton(this.resetHotKeyButton);
			// 
			// footerLabel
			// 
			this.footerLabel.AutoSize = true;
			this.footerLabel.Font = AppTheme.SmallFont;
			this.footerLabel.ForeColor = AppTheme.SecondaryText;
			this.footerLabel.Location = new System.Drawing.Point(36, 232);
			this.footerLabel.Name = "footerLabel";
			this.footerLabel.Size = new System.Drawing.Size(187, 15);
			this.footerLabel.TabIndex = 7;
			this.footerLabel.Text = "Ouse & Touken · github.com/0use-TE/OpenCopy";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = AppTheme.Background;
			this.ClientSize = new System.Drawing.Size(400, 268);
			this.Controls.Add(this.footerLabel);
			this.Controls.Add(this.resetHotKeyButton);
			this.Controls.Add(this.applyHotKeyButton);
			this.Controls.Add(this.hintLabel);
			this.Controls.Add(this.hotKeyTextBox);
			this.Controls.Add(this.statusLabel);
			this.Controls.Add(this.statusDot);
			this.Controls.Add(this.titleLabel);
			this.Font = AppTheme.BodyFont;
			this.ForeColor = AppTheme.PrimaryText;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "OpenCopy";
			this.ResumeLayout(false);
			this.PerformLayout();
		}

		private Label titleLabel;
		private Panel statusDot;
		private Label statusLabel;
		private TextBox hotKeyTextBox;
		private Label hintLabel;
		private Button applyHotKeyButton;
		private Button resetHotKeyButton;
		private Label footerLabel;
	}
}
