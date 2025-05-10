using System.Collections.Immutable;

using Potisan.Windows.Shell.ComTypes;

namespace Potisan.Windows.Shell;

public class ShellWindows(object? o) : ComUnknownWrapperBase<IShellWindows>(o)
{
	public static ComResult<ShellWindows> CreateNoThrow()
	{
		Guid CLSID_ShellWindows = new("9BA05972-F6A8-11CF-A442-00A0C90A8F39");
		return ComHelper.CreateInstanceNoThrow<ShellWindows>(CLSID_ShellWindows);
	}

	public static ShellWindows Create()
		=> CreateNoThrow().Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<int> CountNoThrow
		=> new(_obj.get_Count(out var x), x);

	public int Count
		=> CountNoThrow.Value;

	public ComResult<ShellWindowDispatch> ItemNoThrow(object index)
		=> new(_obj.Item(index, out var x), new(x));

	public ShellWindowDispatch Item(object index)
		=> ItemNoThrow(index).Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public IEnumerable<ShellWindowDispatch> ItemEnumerable
	{
		get
		{
			var c = Count;
			for (int i = 0; i < c; i++)
				yield return Item(i);
		}
	}

	public ImmutableArray<ShellWindowDispatch> Items
		=> [.. ItemEnumerable];

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public IEnumerable<ShellBrowser> ShellBrowserEnumerable
	{
		get
		{
			var c = Count;
			for (int i = 0; i < c; i++)
			{
				var item = Item(i);
				if (Item(i).AsTopLevelShellBrowserNoThrow is { Succeeded: true, ValueUnchecked: var value })
					yield return value;
			}
		}
	}

	public ImmutableArray<ShellBrowser> ShellBrowsers
		=> [.. ShellBrowserEnumerable];

	//[PreserveSig]
	//int _NewEnum(
	//	[MarshalAs(UnmanagedType.IUnknown)] out object? ppunk);

	//[PreserveSig]
	//int Register(
	//	[MarshalAs(UnmanagedType.IDispatch)] object pid,
	//	int hwnd,
	//	int swClass,
	//	out int plCookie);

	//[PreserveSig]
	//int RegisterPending(
	//	int lThreadId,
	//	[MarshalAs(UnmanagedType.Struct)] in object pvarloc,
	//	[MarshalAs(UnmanagedType.Struct)] in object pvarlocRoot,
	//	/* [in] */ int swClass,
	//	out int plCookie);

	//[PreserveSig]
	//int Revoke(
	//	int lCookie);

	//[PreserveSig]
	//int OnNavigate(
	//	int lCookie,
	//	[MarshalAs(UnmanagedType.Struct)] in object pvarLoc);

	//[PreserveSig]
	//int OnActivated(
	//	int lCookie,
	//	[MarshalAs(UnmanagedType.VariantBool)] bool fActive);

	//[PreserveSig]
	//int FindWindowSW(
	//	[MarshalAs(UnmanagedType.Struct)] in object pvarLoc,
	//	[MarshalAs(UnmanagedType.Struct)] in object pvarLocRoot,
	//	/* [in] */ int swClass,
	//	out int phwnd,
	//	/* [in] */ int swfwOptions,
	//	[MarshalAs(UnmanagedType.IDispatch)] out object? ppdispOut);

	//[PreserveSig]
	//int OnCreated(
	//	int lCookie,
	//	[MarshalAs(UnmanagedType.IUnknown)] object punk);

	//[PreserveSig]
	//int ProcessAttachDetach(
	//	[MarshalAs(UnmanagedType.VariantBool)] bool fAttach);
}
