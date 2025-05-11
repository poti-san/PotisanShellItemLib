using Potisan.Windows.Com.ComTypes;

namespace Potisan.Windows.Com;

/// <summary>
/// COMエラーログ。IErrorLog COMインターフェイスのラッパーです。
/// </summary>
public class ComErrorLog(object? o) : ComUnknownWrapperBase<IErrorLog>(o)
{
	public ComResult AddErrorNoThrow(string propName, ComExceptionInfo excepInfo)
		=> new(_obj.AddError(propName, excepInfo));

	public void AddError(string propName, ComExceptionInfo excepInfo)
		=> AddErrorNoThrow(propName, excepInfo).ThrowIfError();
}

/// <summary>
/// EXCEPINFO
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public sealed class ComExceptionInfo
{
	public ushort Code;
	public ushort Reserved1;
	[MarshalAs(UnmanagedType.BStr)]
	public string? Source;
	[MarshalAs(UnmanagedType.BStr)]
	public string? Description;
	[MarshalAs(UnmanagedType.BStr)]
	public string? HelpFile;
	public ushort HelpContext;
	public nuint Reserved2;
	public nuint DeferredFillIn;
	public int SCode;
}