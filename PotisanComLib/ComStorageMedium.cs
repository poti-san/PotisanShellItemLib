using System.ComponentModel;
using System.Text;

using Potisan.Windows.Com.ComTypes;

namespace Potisan.Windows.Com;

/// <summary>
/// COMストレージ媒体容情報。STGMEDIUM。
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public class ComStorageMedium
{
	/// <summary>
	/// 通常は使用しません。
	/// 配列の最大長を超えた場合などに直接使用できるように公開します。
	/// </summary>
	[StructLayout(LayoutKind.Explicit)]
	public struct DummyUnion1
	{
		/// <summary>
		/// HBITMAP
		/// </summary>
		[FieldOffset(0)]
		public nint hBitmap;
		/// <summary>
		/// HMETAFILEPICT
		/// </summary>
		[FieldOffset(0)]
		public nint hMetaFilePict;
		/// <summary>
		/// HENHMETAFILE
		/// </summary>
		[FieldOffset(0)]
		public nint hEnhMetaFile;
		/// <summary>
		/// HGLOBAL
		/// </summary>
		[FieldOffset(0)]
		public nint hGlobal;
		/// <summary>
		/// LPOLESTR
		/// </summary>
		[FieldOffset(0)]
		public nint lpszFileName;
		/// <summary>
		/// IStream*
		/// </summary>
		[FieldOffset(0)]
		public nint pstm;
		/// <summary>
		/// IStorage*
		/// </summary>
		[FieldOffset(0)]
		public nint pstg;
	}

	public MediumType Tymed;
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public DummyUnion1 u;
	public nint UnknownPointerForRelease;

	~ComStorageMedium()
	{
		[DllImport("ole32.dll")]
		static extern int ReleaseStgMedium(ComStorageMedium pMedium);

		if (ReleaseStgMedium(this) != 0)
			Debug.WriteLine($"{nameof(ComStorageMedium)}のデストラクタでメモリ解放に失敗しました。");
	}

	public static ComStorageMedium CreateHGlobal(ReadOnlySpan<byte> data)
	{
		var p = ComHelper.ByteArrayToPtr(data);
		return new() { Tymed = MediumType.HGbloal, u = { hGlobal = p } };
	}

	public ComResult<nuint> HGlobalDataSizeNoThrow
	{
		get
		{
			if (Tymed != MediumType.HGbloal)
				return new(CommonHResults.EFail, 0);
			return new(0, HGlobalHelper.SizeOf(u.hGlobal));
		}
	}

	public nuint HGlobalDataSize
		=> HGlobalDataSizeNoThrow.Value;

	public ComResult<byte[]> HGlobalDataNoThrow
	{
		get
		{
			if (Tymed != MediumType.HGbloal)
				return new(CommonHResults.EFail, []);
			try
			{
				return new(0, HGlobalHelper.ReadAllBytes(u.hGlobal));
			}
			catch (Exception ex)
			{
				Debug.WriteLine("データサイズが配列の最大長を超えた可能性があります。u.HGlobalの直接操作を検討してください。");
				return new(ex.HResult, []);
			}
		}
	}
	public byte[] HGlobalData
		=> HGlobalDataNoThrow.Value;

	public static ComStorageMedium CreateFileName(string filename)
	{
		return new() { Tymed = MediumType.File, u = { lpszFileName = Marshal.StringToCoTaskMemUni(filename) } };
	}

	public ComResult<string> FileNameNoThrow
	{
		get
		{
			if (Tymed != MediumType.File)
				return new(CommonHResults.EFail, "");
			return new(0, Marshal.PtrToStringUni(u.lpszFileName)!);
		}
	}

	public string FileName
		=> FileNameNoThrow.Value;

	public static ComStorageMedium CreateStream(ReadOnlySpan<byte> data)
	{
		var stream = ComStream.CreateOnMemory(data);
		return new() { Tymed = MediumType.IStream, u = { pstm = Marshal.GetIUnknownForObject(stream) } };
	}

	public ComResult<ulong> StreamLengthNoThrow
	{
		get
		{
			if (Tymed != MediumType.IStream)
				return new(CommonHResults.EFail, 0);
			try
			{
				var stream = new ComStream(Marshal.GetObjectForIUnknown(u.pstm));
				return new(0, stream.Length);
			}
			catch (Exception ex)
			{
				return new(ex.HResult, 0);
			}
		}
	}

	public ulong StreamLength
		=> StreamLengthNoThrow.Value;

	public ComResult<byte[]> StreamDataNoThrow
	{
		get
		{
			if (Tymed != MediumType.IStream)
				return new(CommonHResults.EFail, []);
			try
			{
				var stream = new ComStream(Marshal.GetObjectForIUnknown(u.pstm));
				return new(0, stream.ReadAllBytes());
			}
			catch (Exception ex)
			{
				return new(ex.HResult, []);
			}
		}
	}

	public byte[] StreamData
		=> StreamDataNoThrow.Value;

	/// <summary>
	/// 対応する形式をバイト配列として取得します。
	/// </summary>
	public ComResult<byte[]> GetBytesNoThrow()
	{
		return Tymed switch
		{
			MediumType.HGbloal => new(0, HGlobalData!),
			MediumType.File => new(0, Encoding.Unicode.GetBytes(FileName!)),
			MediumType.IStream => new(0, StreamData!),
			MediumType.IStorage => new(CommonHResults.EFail, []),
			MediumType.GDI => new(CommonHResults.EFail, []),
			MediumType.MetaFilePicture => new(CommonHResults.EFail, []),
			MediumType.EnhMetafile => new(CommonHResults.EFail, []),
			MediumType.Null => new(0, []),
			_ => new(CommonHResults.EFail, []),
		};
	}

	/// <inheritdoc cref="GetBytesNoThrow"/>
	public byte[] GetBytes() => GetBytesNoThrow().Value;

	/// <summary>
	/// 対応する形式のバイトサイズを取得します。
	/// </summary>
	public ComResult<ulong> GetByteLengthNoThrow()
	{
		return Tymed switch
		{
			MediumType.HGbloal => new(0, HGlobalDataSize!),
			MediumType.File => new(0, (ulong)Encoding.Unicode.GetBytes(FileName!).Length),
			MediumType.IStream => new(0, StreamLength!),
			MediumType.IStorage => new(CommonHResults.EFail, 0),
			MediumType.GDI => new(CommonHResults.EFail, 0),
			MediumType.MetaFilePicture => new(CommonHResults.EFail, 0),
			MediumType.EnhMetafile => new(CommonHResults.EFail, 0),
			MediumType.Null => new(0, 0),
			_ => new(CommonHResults.EFail, 0),
		};
	}

	/// <inheritdoc cref="GetByteLengthNoThrow"/>
	public ulong GetByteLength() => GetByteLengthNoThrow().Value;
}