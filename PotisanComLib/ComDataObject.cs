using System.ComponentModel;

using Potisan.Windows.Com.ComTypes;

namespace Potisan.Windows.Com;

// TODO
/// <summary>
/// COMデータオブジェクト。
/// </summary>
/// <param name="o">RCWオブジェクト。</param>
/// <remarks>
/// <c>IDataObject</c> COMインターフェイスのラッパーです。
/// </remarks>
public class ComDataObject(object? o) : ComUnknownWrapperBase<IDataObject>(o)
{
	public ComResult<ComStorageMedium> GetDataNoThrow(ComFormatEtc fmtetc)
	{
		var x = new ComStorageMedium();
		return new(_obj.GetData(fmtetc, x), x);
	}

	public ComStorageMedium GetData(ComFormatEtc fmtetc)
		=> GetDataNoThrow(fmtetc).Value;

	public ComResult<ComStorageMedium> GetDataHereNoThrow(ComFormatEtc fmtetc)
	{
		var x = new ComStorageMedium();
		return new(_obj.GetDataHere(fmtetc, x), x);
	}

	public ComStorageMedium GetDataHere(ComFormatEtc fmtetc)
		=> GetDataHereNoThrow(fmtetc).Value;

	public ComResult<bool> QueryGetDataNoThrow(ComFormatEtc fmtetc)
		=> ComResult.HRSuccess(_obj.QueryGetData(fmtetc));

	public bool QueryGetData(ComFormatEtc fmtetc)
		=> QueryGetDataNoThrow(fmtetc).Value;

	//[PreserveSig]
	//int GetCanonicalFormatEtc(
	//	FORMATETC pformatectIn,
	//	[Out] FORMATETC pformatetcOut);

	//[PreserveSig]
	//int SetData(
	//	FORMATETC pformatetc,
	//	STGMEDIUM pmedium,
	//	[MarshalAs(UnmanagedType.Bool)] bool fRelease);

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public ComResult<ComFormatEtcEnumerable> GetFormatEtcEnumerableNoThrow(ComDataDirection direction)
		=> new(_obj.EnumFormatEtc((uint)direction, out var x), new(x));

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public ComFormatEtcEnumerable GetFormatEtcEnumerable(ComDataDirection direction)
		=> GetFormatEtcEnumerableNoThrow(direction).Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<ComFormatEtcEnumerable> FormatEtcGetterEnumerableNoThrow
		=> GetFormatEtcEnumerableNoThrow(ComDataDirection.Get);

	public ComFormatEtcEnumerable FormatEtcGetterEnumerable
		=> FormatEtcGetterEnumerableNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<ComFormatEtcEnumerable> FormatEtcSetterEnumerableNoThrow
		=> GetFormatEtcEnumerableNoThrow(ComDataDirection.Set);

	public ComFormatEtcEnumerable FormatEtcSetterEnumerable
		=> FormatEtcSetterEnumerableNoThrow.Value;

	//[PreserveSig]
	//int DAdvise(
	//	FORMATETC pformatetc,
	//	uint advf,
	//	IAdviseSink pAdvSink,
	//	out uint pdwConnection);

	//[PreserveSig]
	//int DUnadvise(
	//	uint dwConnection);

	//[PreserveSig]
	//int EnumDAdvise(
	//	out IEnumSTATDATA ppenumAdvise);

	public static ComResult<ComDataObject> GetClipboardNoThrow()
	{
		[DllImport("ole32.dll")]
		static extern int OleGetClipboard(out IDataObject ppDataObj);

		return new(OleGetClipboard(out var x), new(x));
	}

	public static ComDataObject GetClipboard()
		=> GetClipboardNoThrow().Value;

	public ComResult SetClipboardNoThrow()
	{
		[DllImport("ole32.dll")]
		static extern int OleSetClipboard(IDataObject ppDataObj);

		return new(OleSetClipboard((IDataObject)WrappedObject!));
	}

	public void SetClipboard()
		=> SetClipboardNoThrow().ThrowIfError();
}

/// <summary>
/// DATADIR
/// </summary>
public enum ComDataDirection : uint
{
	Get = 1,
	Set = 2,
}

/// <summary>
/// DVTARGETDEVICEの可変長配列サイズ1の実装です。
/// 実際のサイズとは異なります。
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct DVTargetDevice0
{
	public uint Size;
	public ushort DriverNameOffset;
	public ushort DeviceNameOffset;
	public ushort PortNameOffset;
	public ushort ExtDevmodeOffset;
	public byte Data0;
}

/// <summary>
/// DVASPECT
/// </summary>
public enum DVAspect : uint
{
	Content = 1,
	Thumbnail = 2,
	Icon = 4,
	DocPrint = 8
}