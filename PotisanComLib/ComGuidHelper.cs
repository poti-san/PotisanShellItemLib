namespace Potisan.Windows.Com;

/// <summary>
/// System.Guid型のCOM変換機能を提供します。
/// </summary>
public static class ComGuidHelper
{
	/// <summary>
	/// CLSIDからProgIDを取得します。
	/// </summary>
	public static ComResult<string> ClsidToProgIDNoThrow(in Guid clsid)
	{
		[DllImport("ole32.dll")]
		static extern int ProgIDFromCLSID(in Guid clsid, [MarshalAs(UnmanagedType.LPWStr)] out string lplpszProgID);
		return new(ProgIDFromCLSID(clsid, out var s), s);
	}

	/// <inheritdoc cref="ClsidToProgIDNoThrow(in Guid)"/>
	public static string ClsidToProgID(in Guid clsid)
		=> ClsidToProgIDNoThrow(clsid).Value;

	/// <summary>
	/// ProgIDからCLSIDを取得します。
	/// </summary>
	public static ComResult<Guid> ProgIDToClsidNoThrow(ReadOnlySpan<char> progId)
	{
		[DllImport("ole32.dll", CharSet = CharSet.Unicode)]
		static extern int CLSIDFromProgID(in char lpszProgID, out Guid lpclsid);
		return new(CLSIDFromProgID(MemoryMarshal.GetReference(progId), out var x), x);
	}

	/// <inheritdoc cref="ProgIDToClsidNoThrow(ReadOnlySpan{char})"/>
	public static Guid ProgIDToClsid(ReadOnlySpan<char> progId)
		=> ProgIDToClsidNoThrow(progId).Value;
}
