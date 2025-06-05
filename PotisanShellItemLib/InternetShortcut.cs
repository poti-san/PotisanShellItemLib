using Potisan.Windows.Com.ComTypes;
using Potisan.Windows.PropertySystem;
using Potisan.Windows.PropertySystem.ComTypes;
using Potisan.Windows.Shell.ComTypes;

namespace Potisan.Windows.Shell;

/// <summary>
/// インターネットショートカット(*.url)の操作機能。
/// </summary>
/// <param name="o">RCWオブジェクト。</param>
/// <example>
/// <code>
/// <![CDATA[using Potisan.Windows.Shell;
/// 
/// Thread.CurrentThread.SetApartmentState(ApartmentState.Unknown);
/// Thread.CurrentThread.SetApartmentState(ApartmentState.STA);
/// 
/// var link = InternetShortcut.LoadFile(Path.Combine(
/// 	Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "test.url"));
/// 
/// Console.WriteLine(link.Url);
/// ]]>
/// </code>
/// </example>
public sealed class InternetShortcut(object? o) : ComUnknownWrapperBase<IUniformResourceLocatorW>(o)
{
	public static ComResult<InternetShortcut> CreateNoThrow()
	{
		// 分かりにくい原因なのでアサートを発生させます。
		Debug.Assert(Thread.CurrentThread.GetApartmentState() == ApartmentState.STA,
			"シングルスレッドモデルでのみ作成できます。");

		// {fbf23b40-e3f0-101b-8488-00aa003e56f8}
		Guid CLSID_InternetShortcut = new(0xFBF23B40, 0xE3F0, 0x101B, 0x84, 0x88, 0x00, 0xAA, 0x00, 0x3E, 0x56, 0xF8);
		return ComHelper.CreateInstanceNoThrow<InternetShortcut, IUniformResourceLocatorW>(
			CLSID_InternetShortcut, ComClassContext.InProcServer);
	}

	public static InternetShortcut Create()
		=> CreateNoThrow().Value;

	public static ComResult<InternetShortcut> LoadFileNoThrow(string path)
	{
		var link = CreateNoThrow();
		if (!link) return link;
		if (link.Value.AsPersistFile is not { } persistFile)
			return new(CommonHResults.ENoInterface, new(null));
		var cr = persistFile.LoadNoThrow(path, ComStorageMode.Read);
		if (!cr) return new(cr.HResult, new(null));
		return link;
	}

	public static InternetShortcut LoadFile(string path)
		=> LoadFileNoThrow(path).Value;

	public static ComResult<InternetShortcut> LoadStreamNoThrow(ComStream stream)
	{
		var link = CreateNoThrow();
		if (!link) return link;
		if (link.Value.AsPersistStream is not { } persistStream)
			return new(CommonHResults.ENoInterface, new(null));
		var cr = persistStream.LoadNoThrow(stream);
		if (!cr) return new(cr.HResult, new(null));
		return link;
	}

	public static InternetShortcut LoadFile(ComStream stream)
		=> LoadStreamNoThrow(stream).Value;

	public static ComResult<InternetShortcut> LoadStreamNoThrow(Stream stream)
	{
		using var wrapper = ComStream.CreateOnStream(stream);
		return LoadStreamNoThrow(wrapper);
	}

	public static InternetShortcut LoadStream(Stream stream)
		=> LoadStreamNoThrow(stream).Value;

	public ComResult SetUrlNoThrow(string url, InternetShortcutSetUrl flags)
		=> new(_obj.SetURL(url, (uint)flags));

	public void SetUrl(string url, InternetShortcutSetUrl flags)
		=> SetUrlNoThrow(url, flags).ThrowIfError();

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<string> UrlNoThrow
		=> new(_obj.GetURL(out var x), x);

	public string Url
		=> UrlNoThrow.Value;

	public ComResult InvokeCommandNoThrow(
		InternetShortcutInvokeCommand flags,
		string? verb = null,
		nint parentWindowHandle = 0)
	{
		var uici = new URLINVOKECOMMANDINFOW
		{
			dwcbSize = (uint)Marshal.SizeOf<URLINVOKECOMMANDINFOW>(),
			dwFlags = (uint)flags,
			hwndParent = parentWindowHandle,
			pcszVerb = verb,
		};
		return new(_obj.InvokeCommand(uici));
	}

	public void InvokeCommand(
		InternetShortcutInvokeCommand flags,
		string? verb = null,
		nint parentWindowHandle = 0)
		=> InvokeCommandNoThrow(flags, verb, parentWindowHandle).ThrowIfError();

	public void InvokeOpen(InternetShortcutInvokeCommand flags = 0, nint parentWindowHandle = 0)
		=> InvokeCommand(flags, "open", parentWindowHandle);

	public void InvokeEdit(InternetShortcutInvokeCommand flags = 0, nint parentWindowHandle = 0)
		=> InvokeCommand(flags, "edit", parentWindowHandle);

	public PersistFile? AsPersistFile
		=> this.As<PersistFile, IPersistFile>();

	public PersistStream? AsPersistStream
		=> this.As<PersistStream, IPersistStream>();

	public ComDataObject? AsDataObject
		=> this.As<ComDataObject, IDataObject>();

	public ShellLink? AsShellLink
		=> this.As<ShellLink, IShellLinkW>();

	public PropertySetStorage? AsPropertySetStorage
		=> this.As<PropertySetStorage, IPropertySetStorage>();
}

[Flags]
public enum InternetShortcutSetUrl : uint
{
	GuessProtocol = 0x0001,
	UseDefaultProtocol = 0x0002,
}

[Flags]
public enum InternetShortcutInvokeCommand : uint
{
	AllowUI = 0x0001,
	UseDefaultVerb = 0x0002,
	DdeWait = 0x0004,
	AsyncOK = 0x0008,
	LogUsage = 0x0010,
}

public struct URLINVOKECOMMANDINFOW
{
	public uint dwcbSize;
	public uint dwFlags;
	public nint hwndParent;
	[MarshalAs(UnmanagedType.LPWStr)]
	public string? pcszVerb;
}