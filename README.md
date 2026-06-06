# OpenCopy

[中文](#中文) · [English](#english)

---

## 中文

### 简介

OpenCopy（一键输入）是一款 Windows 桌面工具，用于在 **禁止粘贴** 的网页或软件中，通过 **模拟键盘输入** 将剪贴板内容逐字打入目标窗口。

### 功能

- 全局快捷键触发，后台运行即可使用
- **自定义快捷键**（支持 Ctrl / Alt / Shift 组合）
- 暗色极简界面，设置自动保存
- 单文件便携版，无需安装

### 使用方法

1. 运行 `OpenCopy.exe`（或 `一键输入.exe`），保持窗口或最小化到后台
2. 在任意地方 **复制** 需要输入的文本
3. 切换到目标输入框，按下你设置的快捷键（默认 `Ctrl + V`）
4. 程序会自动模拟键盘输入剪贴板内容

### 自定义快捷键

1. 点击界面上的快捷键输入框
2. 按下想要的组合键（必须包含 Ctrl、Alt 或 Shift 之一）
3. 点击 **应用**，下方会显示成功或失败提示
4. 点击 **重置** 可恢复为默认 `Ctrl + V`

> 若提示「快捷键可能被占用」，说明该组合已被其他软件注册，请换一个（例如 `Ctrl + Shift + V`）。

### 系统要求

- Windows 10 / 11（或已安装 [.NET Framework 4.7.2+](https://dotnet.microsoft.com/download/dotnet-framework) 的 Windows 7/8.1）
- 无需额外安装本程序——便携版为单个 exe

### 从源码构建

```powershell
git clone https://github.com/0use-TE/OpenCopy.git
cd OpenCopy
nuget restore 一键输入.sln
msbuild 一键输入.sln /p:Configuration=Release
```

构建产物：`一键输入\bin\Release\一键输入.exe`（依赖已通过 Costura 内嵌，无需附带 DLL）

> 若未安装 `nuget` 命令，可从 https://dist.nuget.org/win-x86-commandline/latest/nuget.exe 下载，或用 Visual Studio 打开解决方案直接生成。

### 发布单文件 exe（免安装）

本项目使用 [Costura.Fody](https://github.com/Fody/Costura) 将依赖 DLL 嵌入主程序，Release 编译后即为 **单个 exe**，可直接分发，无需 ClickOnce 安装包。

**方式一：一键脚本**

```powershell
.\scripts\publish.ps1
```

输出：`publish\OpenCopy.exe`

**方式二：手动命令**

```powershell
nuget restore 一键输入.sln
msbuild 一键输入.sln /p:Configuration=Release
copy 一键输入\bin\Release\一键输入.exe publish\OpenCopy.exe
```

**说明**

| 项目 | 说明 |
|------|------|
| 是否需安装 | 否，复制 exe 即可运行 |
| 是否真正「单文件」 | 是，依赖已内嵌（Costura） |
| 是否需 .NET 运行时 | 是，目标电脑需 .NET Framework 4.7.2+（Win10/11 通常已自带） |
| 与 ClickOnce 的区别 | 不要用 VS「发布」向导的安装包；用 Release 构建或 `publish.ps1` |

### 作者

Ouse & Touken

- 源码：https://github.com/0use-TE/OpenCopy
- 反馈：2216528769@qq.com

---

## English

### Overview

OpenCopy is a lightweight Windows utility that **simulates keyboard typing** to paste clipboard text into apps or websites where **Ctrl+V is blocked**.

### Features

- Global hotkey — works while the app runs in the background
- **Customizable hotkey** (Ctrl / Alt / Shift combinations)
- Dark, minimal UI with persisted settings
- Portable **single-file** build — no installer

### How to use

1. Run `OpenCopy.exe` and keep it open (or in the background)
2. **Copy** the text you want to type
3. Focus the target input field and press your configured hotkey (default: `Ctrl + V`)
4. OpenCopy types the clipboard contents character by character

### Custom hotkey

1. Click the hotkey field on the main window
2. Press a key combination (must include Ctrl, Alt, or Shift)
3. Click **Apply** — success or failure is shown below the field
4. Click **Reset** to restore the default `Ctrl + V`

> If you see “hotkey may be in use”, another app has registered that combination. Try something else, e.g. `Ctrl + Shift + V`.

### Requirements

- Windows 10 / 11 (or Windows 7/8.1 with [.NET Framework 4.7.2+](https://dotnet.microsoft.com/download/dotnet-framework))
- No separate installer for the portable build — just the exe

### Build from source

```powershell
git clone https://github.com/0use-TE/OpenCopy.git
cd OpenCopy
nuget restore 一键输入.sln
msbuild 一键输入.sln /p:Configuration=Release
```

Output: `一键输入\bin\Release\一键输入.exe` (dependencies embedded via Costura — no extra DLLs needed)

> Download `nuget.exe` from https://dist.nuget.org/win-x86-commandline/latest/nuget.exe if the CLI is not installed, or open the solution in Visual Studio and build.

### Publish a single portable exe (no installer)

[Costura.Fody](https://github.com/Fody/Costura) embeds dependencies into the main assembly at build time. A **Release** build produces one exe you can ship without ClickOnce or an installer.

**Option A — script**

```powershell
.\scripts\publish.ps1
```

Output: `publish\OpenCopy.exe`

**Option B — manual**

```powershell
nuget restore 一键输入.sln
msbuild 一键输入.sln /p:Configuration=Release
copy 一键输入\bin\Release\一键输入.exe publish\OpenCopy.exe
```

**Notes**

| Topic | Details |
|-------|---------|
| Installer needed? | No — copy the exe and run |
| Single file? | Yes — DLLs are embedded via Costura |
| .NET runtime? | .NET Framework 4.7.2+ required on the target PC (usually preinstalled on Win10/11) |
| vs ClickOnce | Do not use the VS ClickOnce publish wizard; use Release build or `publish.ps1` instead |

### Authors

Ouse & Touken

- Repository: https://github.com/0use-TE/OpenCopy
- Contact: 2216528769@qq.com
