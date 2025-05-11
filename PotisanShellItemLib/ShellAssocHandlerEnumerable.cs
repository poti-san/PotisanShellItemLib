using System.Collections;

using Potisan.Windows.Com.ComTypes;
using Potisan.Windows.Shell.ComTypes;

namespace Potisan.Windows.Shell;

/// <summary>
/// 関連付け呼び出し処理。IAssocHandlerInvoker COMインターフェイスのラッパーです。
/// </summary>
/// <remarks>
/// 作成には<see cref="ShellAssocHandler"/>を使用します。
/// </remarks>
public class ShellAssocHandlerInvoker(object? o) : ComUnknownWrapperBase<IAssocHandlerInvoker>(o)
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<bool> SupportsSelectionNoThrow
		=> ComResult.HRSuccess(_obj.SupportsSelection());

	public bool SupportsSelection
		=> SupportsSelectionNoThrow.Value;

	public ComResult InvokeNoThrow()
		=> new(_obj.Invoke());

	public void Invoke()
		=> InvokeNoThrow().ThrowIfError();
}

/// <summary>
/// 関連付けハンドラ。IAssocHandler COMインターフェイスのラッパーです。
/// </summary>
/// <remarks>
/// 作成には<see cref="ShellAssocHandlerEnumerable"/>を使用します。
/// </remarks>
public class ShellAssocHandler(object? o) : ComUnknownWrapperBase<IAssocHandler>(o)
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<string> NameNoThrow
		=> new(_obj.GetName(out var x), x);

	public string Name
		=> NameNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<string> UINameNoThrow
		=> new(_obj.GetUIName(out var x), x);

	public string UIName
		=> UINameNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<(string Path, int Index)> IconLocationNoThrow
		=> new(_obj.GetIconLocation(out var x1, out var x2), (x1, x2));

	public (string Path, int Index) IconLocation
		=> IconLocationNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<bool> IsRecommendedNoThrow
		=> ComResult.HRSuccess(_obj.IsRecommended());

	public bool IsRecommended
		=> IsRecommendedNoThrow.Value;

	public ComResult MakeDefaultNoThrow(string description)
		=> new(_obj.MakeDefault(description));

	public void MakeDefault(string description)
		=> MakeDefaultNoThrow(description).ThrowIfError();

	public ComResult InvokeNoThrow(ComDataObject dataobj)
		=> new(_obj.Invoke((IDataObject)dataobj.WrappedObject!));

	public void Invoke(ComDataObject dataobj)
		=> InvokeNoThrow(dataobj).ThrowIfError();

	public ComResult<ShellAssocHandlerInvoker> CreateInvokerNoThrow(ComDataObject dataobj)
		=> new(_obj.CreateInvoker((IDataObject)dataobj.WrappedObject!, out var x), new(x));

	public ShellAssocHandlerInvoker CreateInvoker(ComDataObject dataobj)
		=> CreateInvokerNoThrow(dataobj).Value;
}

/// <summary>
/// 関連付けハンドラ列挙子。IEnumAssocHandlers COMインターフェイスのラッパーです。
/// </summary>
/// <example>
/// <code><![CDATA[
///using Potisan.Windows.Shell;
///
///Console.WriteLine($"UI名, 名前, アイコンの場所");
///foreach (var assoc in ShellAssocHandlerEnumerable.Create(".txt", ShellAssocFilter.None))
///{
///    Console.WriteLine($"{assoc.UIName}, {assoc.Name}, {assoc.IconLocation}");
///}
///]]></code>
/// </example>
public class ShellAssocHandlerEnumerable(object? o) :
	ComUnknownWrapperBase<IEnumAssocHandlers>(o),
	IEnumerable<ShellAssocHandler>
{
	public IEnumerator<ShellAssocHandler> GetEnumerator()
	{
		int hr;
		while ((hr = _obj.Next(1, out var x, out _)) == 0)
		{
			yield return new(x);
		}
		Marshal.ThrowExceptionForHR(hr);
	}

	IEnumerator IEnumerable.GetEnumerator()
		=> GetEnumerator();

	public static ComResult<ShellAssocHandlerEnumerable> CreateNoThrow(string dotExtension, ShellAssocFilter filter)
	{
		[DllImport("shell32.dll", CharSet = CharSet.Unicode)]
		static extern int SHAssocEnumHandlers(string? pszExtra, ShellAssocFilter afFilter, out IEnumAssocHandlers ppEnumHandler);

		return new(SHAssocEnumHandlers(dotExtension, filter, out var x), new(x));
	}

	public static ShellAssocHandlerEnumerable Create(string dotExtension, ShellAssocFilter filter)
		=> CreateNoThrow(dotExtension, filter).Value;

	public static ComResult<ShellAssocHandlerEnumerable> CreateForProtocolByApplicationNoThrow(string protocol)
	{
		[DllImport("shell32.dll", CharSet = CharSet.Unicode)]
		static extern int SHAssocEnumHandlersForProtocolByApplication(
			string protocol, in Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object enumHandlers);

		return new(SHAssocEnumHandlersForProtocolByApplication(protocol, typeof(IEnumAssocHandlers).GUID, out var x), new(x));
	}

	public static ShellAssocHandlerEnumerable CreateForProtocolByApplication(string protocol)
		=> CreateForProtocolByApplicationNoThrow(protocol).Value;
}

/// <summary>
/// ASSOC_FILTER
/// </summary>
[Flags]
public enum ShellAssocFilter : uint
{
	None = 0,
	Recommended = 0x1,
}

/// <summary>
/// AHTYPE
/// </summary>
[Flags]
public enum ShellAssocHandlerType : uint
{
	Undefined = 0,
	UserApplication = 0x8,
	AnyApplication = 0x10,
	MachineDefault = 0x20,
	ProgID = 0x40,
	Application = 0x80,
	ClassApplication = 0x100,
	AnyProgID = 0x200,
}
