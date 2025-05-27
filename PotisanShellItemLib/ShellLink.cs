using Potisan.Windows.Com.ClrExtensions;
using Potisan.Windows.Com.ComTypes;
using Potisan.Windows.Com.SafeHandles;
using Potisan.Windows.Shell.ComTypes;

namespace Potisan.Windows.Shell;

/// <summary>
/// ショートカットファイルの操作機能。
/// </summary>
/// <param name="o">RCWオブジェクト。</param>
/// <example>
/// <code>
///	<![CDATA[using Potisan.Windows.Shell;
///	
///	var link = ShellLink.LoadFile(Path.Combine(
///		Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "test.lnk"));
///	
///	Console.WriteLine(link.DefaultPath);]]>
/// </code>
/// </example>
public sealed class ShellLink(object? o) : ComUnknownWrapperBase<IShellLinkW>(o)
{
	public static ComResult<ShellLink> CreateNoThrow()
	{
		Guid CLSID_ShellLink = new("00021401-0000-0000-C000-000000000046");
		return ComHelper.CreateInstanceNoThrow<ShellLink, IShellLinkW>(
			CLSID_ShellLink, ComClassContext.InProcServer);
	}

	public static ShellLink Create()
		=> CreateNoThrow().Value;

	public static ComResult<ShellLink> LoadFileNoThrow(string path)
	{
		var link = CreateNoThrow();
		if (!link) return link;
		if (link.Value.AsPersistFile is not { } persistFile)
			return new(CommonHResults.ENoInterface, new(null));
		var cr = persistFile.LoadNoThrow(path, ComStorageMode.Read);
		if (!cr) return new(cr.HResult, new(null));
		return link;
	}

	public static ShellLink LoadFile(string path)
		=> LoadFileNoThrow(path).Value;

	public static ComResult<ShellLink> LoadStreamNoThrow(ComStream stream)
	{
		var link = CreateNoThrow();
		if (!link) return link;
		if (link.Value.AsPersistStream is not { } persistStream)
			return new(CommonHResults.ENoInterface, new(null));
		var cr = persistStream.LoadNoThrow(stream);
		if (!cr) return new(cr.HResult, new(null));
		return link;
	}

	public static ShellLink LoadFile(ComStream stream)
		=> LoadStreamNoThrow(stream).Value;

	public static ComResult<ShellLink> LoadStreamNoThrow(Stream stream)
	{
		using var wrapper = ComStream.CreateOnStream(stream);
		return LoadStreamNoThrow(wrapper);
	}

	public static ShellLink LoadStream(Stream stream)
		=> LoadStreamNoThrow(stream).Value;

	public ComResult<string> GetPathNoThrow(int length, ShellLinkGetPath flags)
	{
		var buffer = GC.AllocateUninitializedArray<char>(length);
		var hr = _obj.GetPath(ref MemoryMarshal.GetArrayDataReference(buffer), length, 0, (uint)flags);
		if (hr == 1) return new(1, "");
		return new(hr, new(buffer));
	}

	public ComResult<string> GetPathNoThrow(ShellLinkGetPath flags)
	{
		for (uint l = 0xff; l < int.MaxValue; l += 0xff)
		{
			var buffer = GC.AllocateUninitializedArray<char>((int)l);
			var hr = _obj.GetPath(ref MemoryMarshal.GetArrayDataReference(buffer), (int)l, 0, (uint)flags);
			if (hr == 1) return new(1, "");
			if (hr == 0) return new(0, buffer.ToStringAsNullTerminated());
			if (hr == 122) continue;
			return new(hr, "");
		}
		return new(CommonHResults.SFalse, "");
	}

	public string GetPath(ShellLinkGetPath flags)
		=> GetPathNoThrow(flags).Value;

	public ComResult SetPathNoThrow(string value)
		=> new(_obj.SetPath(value));

	public string DefaultPath
	{
		get => GetPath(0);
	}

	public string Path
	{
		set => SetPathNoThrow(value).ThrowIfError();
	}

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<SafeCoTaskMemHandle> IDListNoThrow
		=> new(_obj.GetIDList(out var x), x);

	public ComResult SetIDListNoThrow(SafeCoTaskMemHandle pidl)
		=> new(_obj.SetIDList(pidl));

	public SafeCoTaskMemHandle IDList
	{
		get => IDListNoThrow.Value;
		set => SetIDListNoThrow(value).ThrowIfError();
	}

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<string> DescriptionNoThrow
	{
		get
		{
			for (uint l = 0xff; l < int.MaxValue; l += 0xff)
			{
				var buffer = GC.AllocateUninitializedArray<char>((int)l);
				var hr = _obj.GetDescription(ref MemoryMarshal.GetArrayDataReference(buffer), (int)l);
				if (hr == 1) return new(1, "");
				if (hr == 0) return new(0, buffer.ToStringAsNullTerminated());
				if (hr == 122) continue;
				return new(hr, "");
			}
			return new(CommonHResults.SFalse, "");
		}
	}

	public ComResult SetDescriptionNoThrow(string value)
		=> new(_obj.SetDescription(value));

	public string Description
	{
		get => DescriptionNoThrow.Value;
		set => SetDescriptionNoThrow(value).ThrowIfError();
	}

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<string> WorkingDirectoryNoThrow
	{
		get
		{
			for (uint l = 0xff; l < int.MaxValue; l += 0xff)
			{
				var buffer = GC.AllocateUninitializedArray<char>((int)l);
				var hr = _obj.GetWorkingDirectory(ref MemoryMarshal.GetArrayDataReference(buffer), (int)l);
				if (hr == 1) return new(1, "");
				if (hr == 0) return new(0, buffer.ToStringAsNullTerminated());
				if (hr == 122) continue;
				return new(hr, "");
			}
			return new(CommonHResults.SFalse, "");
		}
	}

	public ComResult SetWorkingDirectoryNoThrow(string value)
		=> new(_obj.SetWorkingDirectory(value));

	public string WorkingDirectory
	{
		get => WorkingDirectoryNoThrow.Value;
		set => SetWorkingDirectoryNoThrow(value).ThrowIfError();
	}

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<string> ArgumentsNoThrow
	{
		get
		{
			for (uint l = 0xff; l < int.MaxValue; l += 0xff)
			{
				var buffer = GC.AllocateUninitializedArray<char>((int)l);
				var hr = _obj.GetArguments(ref MemoryMarshal.GetArrayDataReference(buffer), (int)l);
				if (hr == 1) return new(1, "");
				if (hr == 0) return new(0, buffer.ToStringAsNullTerminated());
				if (hr == 122) continue;
				return new(hr, "");
			}
			return new(CommonHResults.SFalse, "");
		}
	}

	public ComResult SetArgumentsNoThrow(string value)
		=> new(_obj.SetArguments(value));

	public string Arguments
	{
		get => ArgumentsNoThrow.Value;
		set => SetArgumentsNoThrow(value).ThrowIfError();
	}

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<ushort> HotkeyNoThrow
		=> new(_obj.GetHotkey(out var x), x);

	public ComResult SetHotkeyNoThrow(ushort value)
		=> new(_obj.SetHotkey(value));

	public ushort Hotkey
	{
		get => HotkeyNoThrow.Value;
		set => SetHotkeyNoThrow(value).ThrowIfError();
	}

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<int> ShowCommandNoThrow
		=> new(_obj.GetShowCmd(out var x), x);

	public ComResult SetShowCommandNoThrow(int value)
		=> new(_obj.SetShowCmd(value));

	public int ShowCommand
	{
		get => ShowCommandNoThrow.Value;
		set => SetShowCommandNoThrow(value).ThrowIfError();
	}

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<(string Path, int IconIndex)> IconLocationNoThrow
	{
		get
		{
			for (uint l = 0xff; l < int.MaxValue; l += 0xff)
			{
				var buffer = GC.AllocateUninitializedArray<char>((int)l);
				var hr = _obj.GetIconLocation(ref MemoryMarshal.GetArrayDataReference(buffer), (int)l, out var x2);
				if (hr == 1) return new(1, ("", 0));
				if (hr == 0) return new(0, (buffer.ToStringAsNullTerminated(), x2));
				if (hr == 122) continue;
				return new(hr, ("", 0));
			}
			return new(CommonHResults.SFalse, ("", 0));
		}
	}

	public ComResult SetIconLocationNoThrow(string Path, int IconIndex)
		=> new(_obj.SetIconLocation(Path, IconIndex));

	public (string Path, int IconIndex) IconLocation
	{
		get => IconLocationNoThrow.Value;
		set => SetIconLocationNoThrow(value.Path, value.IconIndex).ThrowIfError();
	}

	public ComResult SetRelativePathNoThrow(string path)
		=> new(_obj.SetRelativePath(path, 0));

	public void SetRelativePath(string path)
		=> SetRelativePathNoThrow(path).ThrowIfError();

	public ComResult ResolveNoThrow(ShellLinkResolve flags, nint windowHandle = 0)
		=> new(_obj.Resolve(windowHandle, (uint)flags));

	public void Resolve(ShellLinkResolve flags, nint windowHandle = 0)
		=> ResolveNoThrow(flags, windowHandle).ThrowIfError();

	public PersistFile? AsPersistFile
		=> this.As<PersistFile, IPersistFile>();

	public PersistStream? AsPersistStream
		=> this.As<PersistStream, IPersistStream>();

	public ShellLinkDataList? AsShellLinkDataList
		=> this.As<ShellLinkDataList, IShellLinkDataList>();
}

/// <summary>
/// 
/// </summary>
/// <remarks><c>SLGP_FLAGS</c></remarks>
[Flags]
public enum ShellLinkGetPath : uint
{
	ShortPath = 0x1,
	//UncPriority = 0x2,
	RawPath = 0x4,
	RelativePriority = 0x8,
}

/// <summary>
/// 
/// </summary>
/// <remarks><c>SLR_FLAGS</c></remarks>
[Flags]
public enum ShellLinkResolve : uint
{
	None = 0,
	NoUI = 0x1,
	AnyMatch = 0x2,
	Update = 0x4,
	NoUpdate = 0x8,
	NoSearch = 0x10,
	NoTrack = 0x20,
	NoLinkInfo = 0x40,
	InvokeMsi = 0x80,
	NoUIWithMessagePump = 0x101,
	OfferDeleteWithoutFile = 0x200,
	KnownFolder = 0x400,
	MachineInLocalTarget = 0x800,
	UpdateMachineAndSid = 0x1000,
	NoObjectID = 0x2000,
}