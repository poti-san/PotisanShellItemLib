using PotisanShellItemLib.Core;

namespace PotisanShellItemLib.PropertySystem;

public enum VarType : ushort
{
	Empty = 0,
	Null = 1,
	I2 = 2,
	I4 = 3,
	R4 = 4,
	R8 = 5,
	CY = 6,
	Date = 7,
	BSTR = 8,
	Dispatch = 9,
	Error = 10,
	Bool = 11,
	Variant = 12,
	Unknown = 13,
	Decimal = 14,
	I1 = 16,
	UI1 = 17,
	UI2 = 18,
	UI4 = 19,
	I8 = 20,
	UI8 = 21,
	Int = 22,
	UInt = 23,
	Void = 24,
	HResult = 25,
	Ptr = 26,
	SafeArray = 27,
	CArray = 28,
	UserDefined = 29,
	LPSTR = 30,
	LPWSTR = 31,
	Record = 36,
	IntPtr = 37,
	UIntPtr = 38,
	FileTime = 64,
	Blob = 65,
	Stream = 66,
	Storage = 67,
	StreamedObject = 68,
	StoredObject = 69,
	BlobObject = 70,
	CF = 71,
	CLSID = 72,
	VersionedStream = 73,
	BSTRBlob = 0xfff,
	Vector = 0x1000,
	Array = 0x2000,
	ByRef = 0x4000,
	Reserved = 0x8000,
	Illegal = 0xffff,
}

#if TARGET_64BIT
[StructLayout(LayoutKind.Sequential, Size = 24)]
#else
[StructLayout(LayoutKind.Sequential, Size = 16)]
#endif
public sealed class PropVariant
{
	public VarType vt;
	private ushort _reserved1;
	private ushort _reserved2;
	private ushort _reserved3;
	private byte _dummy;

	public bool IsNull => vt == VarType.Null;
	public bool IsEmpty => vt == VarType.Empty;
	public VarType Type => (VarType)((uint)vt & 0xfff);
	public bool IsArray => (vt & VarType.Array) != 0;
	public bool IsVector => (vt & VarType.Vector) != 0;

	~PropVariant()
	{
		Clear();
	}

	public ComResult ClearNoThrow()
	{
		[DllImport("propsys.dll")]
		static extern int PropVariantClear([In, Out] PropVariant propvar);

		return new(PropVariantClear(this));
	}
	public void Clear() => ClearNoThrow().ThrowIfError();

	public ComResult<string> ToStringNoThrow()
	{
		[DllImport("propsys.dll")]
		static extern int PropVariantToStringAlloc(PropVariant propvar, [MarshalAs(UnmanagedType.LPWStr)] out string? ppszOut);

		return new(PropVariantToStringAlloc(this, out var x), x);
	}
	public override string ToString() => ToStringNoThrow().Or("");

	public ComResult<object> ToObjectNoThrow()
	{
		[DllImport("propsys.dll")]
		static extern int PropVariantToVariant(PropVariant pPropVar, out object pVar);

		return new(PropVariantToVariant(this, out var x), x);
	}
	public object ToObject() => ToObjectNoThrow().Value;

	public Span<byte> DataSpan => MemoryMarshal.CreateSpan(ref _dummy, Marshal.SizeOf<PropVariant>() - (int)Marshal.OffsetOf<PropVariant>(nameof(_dummy)));

	public static PropVariant InitNoData(VarType vt)
	{
		var pv = new PropVariant();
		pv.vt = vt;
		return pv;
	}

	public static PropVariant InitNull() => InitNoData(VarType.Null);
	public static PropVariant InitEmpty() => InitNoData(VarType.Empty);

	public static PropVariant InitUnmanaged<T>(VarType vt, in T value) where T : unmanaged
	{
		var pv = new PropVariant();
		pv.vt = vt;
		MemoryMarshal.Cast<byte, T>(pv.DataSpan)[0] = value;
		return pv;
	}

	public static PropVariant InitInt8(sbyte value) => InitUnmanaged(VarType.I1, value);
	public static PropVariant InitInt16(short value) => InitUnmanaged(VarType.I2, value);
	public static PropVariant InitInt32(int value) => InitUnmanaged(VarType.I4, value);
	public static PropVariant InitInt64(long value) => InitUnmanaged(VarType.I8, value);
	public static PropVariant InitUInt8(byte value) => InitUnmanaged(VarType.UI1, value);
	public static PropVariant InitUInt16(ushort value) => InitUnmanaged(VarType.UI2, value);
	public static PropVariant InitUInt32(uint value) => InitUnmanaged(VarType.UI4, value);
	public static PropVariant InitUInt64(ulong value) => InitUnmanaged(VarType.UI8, value);
	public static PropVariant InitInt(int value) => InitUnmanaged(VarType.Int, value);
	public static PropVariant InitUInt(uint value) => InitUnmanaged(VarType.UInt, value);
	public static PropVariant InitIntPtr(nint value) => InitUnmanaged(VarType.IntPtr, value);
	public static PropVariant InitUIntPtr(nuint value) => InitUnmanaged(VarType.UIntPtr, value);
	public static PropVariant InitStringAnsi(string value) => InitUnmanaged(VarType.LPSTR, Marshal.StringToCoTaskMemUni(value));
	public static PropVariant InitStringUni(string value) => InitUnmanaged(VarType.LPWSTR, Marshal.StringToCoTaskMemAnsi(value));
	public static PropVariant InitBSTR(string value) => InitUnmanaged(VarType.BSTR, Marshal.StringToBSTR(value));
	public static PropVariant InitBool(bool value) => InitUnmanaged(VarType.Bool, value ? -1 : 0);
	public static PropVariant InitFloat(float value) => InitUnmanaged(VarType.R4, value);
	public static PropVariant InitDouble(double value) => InitUnmanaged(VarType.R8, value);
	public static PropVariant InitCLSID(in Guid value) => InitUnmanaged(VarType.CLSID, CoTaskMemHelper.CoTaskMemAllocWithStructure(value));
	public static PropVariant InitHResult(int value) => InitUnmanaged(VarType.HResult, value);
	/*
	CY = 6,
	DATE = 7,
	DISPATCH = 9,
	ERROR = 10,
	VARIANT = 12,
	UNKNOWN = 13,
	DECIMAL = 14,
	VOID = 24,
	PTR = 26,
	SAFEARRAY = 27,
	CARRAY = 28,
	USERDEFINED = 29,
	RECORD = 36,
	FILETIME = 64,
	BLOB = 65,
	STREAM = 66,
	STORAGE = 67,
	STREAMED_OBJECT = 68,
	STORED_OBJECT = 69,
	BLOB_OBJECT = 70,
	CF = 71,
	VERSIONED_STREAM = 73,
	BSTR_BLOB = 0xfff,
	VECTOR = 0x1000,
	ARRAY = 0x2000,
	BYREF = 0x4000,
	RESERVED = 0x8000,
	ILLEGAL = 0xffff,*/
}