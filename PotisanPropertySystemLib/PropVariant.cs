using System.Buffers.Binary;

namespace Potisan.Windows.PropertySystem;

/// <summary>
/// プロパティの値。
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public sealed class PropVariant : IDisposable
{
	public VarType vt;
#pragma warning disable IDE0044, RCS1213, RCS1139
	private ushort _reserved1;
	private ushort _reserved2;
	private ushort _reserved3;
	private int _dummy1;
	private nint _dummy2;
#pragma warning restore IDE0044, RCS1213

	public bool IsNull => vt == VarType.Null;
	public bool IsEmpty => vt == VarType.Empty;
	public VarType Type => (VarType)((uint)vt & 0xfff);
	public bool IsArray => (vt & VarType.Array) != 0;
	public bool IsVector => (vt & VarType.Vector) != 0;

	~PropVariant()
	{
		Clear();
	}

	public void Dispose()
	{
		Clear();
		GC.SuppressFinalize(this);
	}

	public ComResult ClearNoThrow()
	{
		[DllImport("ole32.dll")]
		static extern int PropVariantClear([In, Out] PropVariant propvar);

		return new(PropVariantClear(this));
	}

	public void Clear()
		=> ClearNoThrow().ThrowIfError();

	public ComResult<string> ToStringNoThrow()
	{
		[DllImport("propsys.dll")]
		static extern int PropVariantToStringAlloc(PropVariant propvar, [MarshalAs(UnmanagedType.LPWStr)] out string? ppszOut);

		return new(PropVariantToStringAlloc(this, out var x), x!);
	}

	public override string ToString()
		=> ToStringNoThrow().Or("") ?? "";

	public ComResult<object> ToObjectNoThrow()
	{
		[DllImport("propsys.dll")]
		static extern int PropVariantToVariant(PropVariant pPropVar, out object pVar);

		return new(PropVariantToVariant(this, out var x), x);
	}

	public object ToObject()
		=> ToObjectNoThrow().Value;

	public Span<byte> DataSpan
		=> MemoryMarshal.AsBytes(MemoryMarshal.CreateSpan(
			ref _dummy1, Marshal.SizeOf<PropVariant>() - (int)Marshal.OffsetOf<PropVariant>(nameof(_dummy1))));

	public static PropVariant InitNoData(VarType vt)
	{
		return new PropVariant { vt = vt };
	}

	public static PropVariant InitNull() => InitNoData(VarType.Null);
	public static PropVariant InitEmpty() => InitNoData(VarType.Empty);

	public static PropVariant InitUnmanaged<T>(VarType vt, in T value) where T : unmanaged
	{
		var pv = new PropVariant { vt = vt };
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
	public static PropVariant InitStringAnsi(string value) => InitUnmanaged(VarType.LPStr, Marshal.StringToCoTaskMemUni(value));
	public static PropVariant InitStringUni(string value) => InitUnmanaged(VarType.LPWStr, Marshal.StringToCoTaskMemAnsi(value));
	public static PropVariant InitBSTR(string value) => InitUnmanaged(VarType.BStr, Marshal.StringToBSTR(value));
	public static PropVariant InitBool(bool value) => InitUnmanaged(VarType.Bool, value ? -1 : 0);
	public static PropVariant InitFloat(float value) => InitUnmanaged(VarType.R4, value);
	public static PropVariant InitDouble(double value) => InitUnmanaged(VarType.R8, value);
	public static PropVariant InitCLSID(in Guid value) => InitUnmanaged(VarType.Clsid, ComHelper.StructureToPtr(value));
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
	ILLEGAL = 0xffff,
	*/

	private T GetUnmanaged<T>(VarType vt) where T : unmanaged
	{
		if (Type != vt) throw new InvalidCastException();
		return MemoryMarshal.Cast<byte, T>(DataSpan)[0];
	}

	private T GetUnmanagedWithDefault<T>(VarType vt, in T defaultValue) where T : unmanaged
	{
		if (Type != vt) return defaultValue;
		return MemoryMarshal.Cast<byte, T>(DataSpan)[0];
	}

	public object GetEmpty()
	{
		if (Type != VarType.Empty) throw new InvalidCastException();
		return new();
	}
	public static object GetEmptyWithDefault() => new();

	public object? GetNull()
	{
		if (Type != VarType.Null) throw new InvalidCastException();
		return null;
	}
	public static object? GetNullWithDefault() => null;

	public sbyte GetInt8() => GetUnmanaged<sbyte>(VarType.I1);
	public sbyte GetInt8WithDefault(sbyte defaultValue) => GetUnmanagedWithDefault(VarType.I1, defaultValue);
	public short GetInt16() => GetUnmanaged<short>(VarType.I2);
	public short GetInt16WithDefault(short defaultValue) => GetUnmanagedWithDefault(VarType.I2, defaultValue);
	public int GetInt32() => GetUnmanaged<int>(VarType.I4);
	public int GetInt32WithDefault(int defaultValue) => GetUnmanagedWithDefault(VarType.I4, defaultValue);
	public long GetInt64() => GetUnmanaged<long>(VarType.I8);
	public long GetInt64WithDefault(long defaultValue) => GetUnmanagedWithDefault(VarType.I8, defaultValue);

	public byte GetUInt8() => GetUnmanaged<byte>(VarType.UI1);
	public byte GetUInt8WithDefault(byte defaultValue) => GetUnmanagedWithDefault(VarType.UI1, defaultValue);
	public ushort GetUInt16() => GetUnmanaged<ushort>(VarType.UI2);
	public ushort GetUInt16WithDefault(ushort defaultValue) => GetUnmanagedWithDefault(VarType.UI2, defaultValue);
	public uint GetUInt32() => GetUnmanaged<uint>(VarType.UI4);
	public uint GetUInt32WithDefault(uint defaultValue) => GetUnmanagedWithDefault(VarType.UI4, defaultValue);
	public ulong GetUInt64() => GetUnmanaged<ulong>(VarType.UI8);
	public ulong GetUInt64WithDefault(ulong defaultValue) => GetUnmanagedWithDefault(VarType.UI8, defaultValue);

	public int GetInt() => GetUnmanaged<int>(VarType.Int);
	public int GetIntWithDefault(int defaultValue) => GetUnmanagedWithDefault(VarType.Int, defaultValue);
	public uint GetUInt() => GetUnmanaged<uint>(VarType.UInt);
	public uint GetUIntWithDefault(uint defaultValue) => GetUnmanagedWithDefault(VarType.UInt, defaultValue);

	public nint GetIntPtr() => GetUnmanaged<nint>(VarType.IntPtr);
	public nint GetIntPtrWithDefault(nint defaultValue) => GetUnmanagedWithDefault(VarType.IntPtr, defaultValue);
	public nuint GetUIntPtr() => GetUnmanaged<nuint>(VarType.UIntPtr);
	public nuint GetUIntPtrWithDefault(nuint defaultValue) => GetUnmanagedWithDefault(VarType.UIntPtr, defaultValue);

	public string GetStringUni() => Marshal.PtrToStringUni(GetUnmanaged<nint>(VarType.LPWStr)) ?? "";
	public string GetStringUniWithDefault(string defaultValue)
	{
		if (Type == VarType.LPWStr && BinaryPrimitives.ReadUIntPtrLittleEndian(DataSpan) != 0)
			return GetStringUni();
		return defaultValue;
	}

	public string GetStringAnsi() => Marshal.PtrToStringAnsi(GetUnmanaged<nint>(VarType.LPStr)) ?? "";
	public string GetStringAnsiWithDefault(string defaultValue)
	{
		if (Type == VarType.LPStr && BinaryPrimitives.ReadUIntPtrLittleEndian(DataSpan) != 0)
			return GetStringAnsi();
		return defaultValue;
	}

	public float GetFloat() => GetUnmanaged<float>(VarType.R4);
	public float GetFloatWithDefault(float defaultValue) => GetUnmanagedWithDefault(VarType.R4, defaultValue);

	public double GetDouble() => GetUnmanaged<double>(VarType.R8);
	public double GetDoubleWithDefault(double defaultValue) => GetUnmanagedWithDefault(VarType.R8, defaultValue);

	public string GetBSTR() => Marshal.PtrToStringBSTR(GetUnmanaged<nint>(VarType.BStr));
	public string GetBSTRWithDefault(string defaultValue)
	{
		if (Type == VarType.BStr && BinaryPrimitives.ReadUIntPtrLittleEndian(DataSpan) != 0)
			return GetBSTR();
		return defaultValue;
	}

	public bool GetBool() => GetUnmanaged<int>(VarType.Bool) != 0;
	public bool GetBoolWithDefault(bool defaultValue) => GetUnmanagedWithDefault(VarType.Bool, defaultValue ? 1 : 0) != 0;
	//CY = 6,
	//Date = 7,
	//Dispatch = 9,
	//Error = 10,
	//Variant = 12,
	//Unknown = 13,
	//Decimal = 14,
	//Void = 24,
	//HResult = 25,
	//Ptr = 26,
	//SafeArray = 27,
	//CArray = 28,
	//UserDefined = 29,
	//LPSTR = 30,
	//LPWSTR = 31,
	//Record = 36,
	//FileTime = 64,
	//Blob = 65,
	//Stream = 66,
	//Storage = 67,
	//StreamedObject = 68,
	//StoredObject = 69,
	//BlobObject = 70,
	//CF = 71,
	//CLSID = 72,
	//VersionedStream = 73,
	//BSTRBlob = 0xfff,
	//Vector = 0x1000,
	//Array = 0x2000,
	//ByRef = 0x4000,
	//Reserved = 0x8000,
	//Illegal = 0xffff,
}
