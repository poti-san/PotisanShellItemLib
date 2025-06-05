using System.Text;

using Potisan.Windows.MSIme.ComTypes;

namespace Potisan.Windows.MSIme;

/// <summary>
/// IME基本機能の制御。
/// </summary>
/// <param name="o">RCWオブジェクト。</param>
/// <remarks><see cref="MSIme"/>クラスを使って作成できます。</remarks>
public class FECommon(object? o) : ComUnknownWrapperBase<IFECommon>(o)
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<bool> IsDefaultImeNoThrow
		=> ComResult.HRSuccess(_obj.IsDefaultIME(null, 0));

	public ComResult SetIsDefaultImeNoThrow(bool value)
	{
		if (value) return new(_obj.SetDefaultIME());
		else return new(CommonHResults.SOK);
	}

	public bool IsDefaultIme
	{
		get => IsDefaultImeNoThrow.Value;
		set => SetIsDefaultImeNoThrow(value).ThrowIfError();
	}

	public ComResult<bool> CheckDefaultImeNoThrow(string keyboardLayoutName)
		=> ComResult.HRSuccess(_obj.IsDefaultIME(keyboardLayoutName, Encoding.ASCII.GetByteCount(keyboardLayoutName)));

	public bool CheckDefaultIme(string keyboardLayoutName)
		=> CheckDefaultImeNoThrow(keyboardLayoutName).Value;

	public ComResult<bool> InvokeWordRegisterDialogNoThrow(string? word = null, nint windowHandle = 0, int tabId = 0)
		=> ComResult.HRSuccess(_obj.InvokeWordRegDialog(new() { cbIMEDLG = Marshal.SizeOf<IMEDLG>(), hwnd = windowHandle, lpwstrWord = word, nTabId = tabId }));

	public bool InvokeWordRegisterDialog(string? word = null, nint windowHandle = 0, int tabId = 0)
		=> InvokeWordRegisterDialogNoThrow(word, windowHandle, tabId);

	public ComResult<bool> InvokeDictionaryToolDialogNoThrow(string? word = null, nint windowHandle = 0, int tabId = 0)
		=> ComResult.HRSuccess(_obj.InvokeDictToolDialog(new() { cbIMEDLG = Marshal.SizeOf<IMEDLG>(), hwnd = windowHandle, lpwstrWord = word, nTabId = tabId }));

	public bool InvokeDictionaryToolDialog(string? word = null, nint windowHandle = 0, int tabId = 0)
		=> InvokeDictionaryToolDialogNoThrow(word, windowHandle, tabId);

	[UnmanagedFunctionPointer(CallingConvention.StdCall)]
	private delegate int CreateIFECommonInstance([MarshalAs(UnmanagedType.IUnknown)] out object? ppv);
}
