using Potisan.Windows.PropertySystem.ComTypes;

namespace Potisan.Windows.PropertySystem;

public sealed class PropertyStorage(object? o) : ComUnknownWrapperBase<IPropertyStorage>(o)
{
	//[PreserveSig]
	//int ReadMultiple(
	//	uint cpspec,
	//	[MarshalAs(UnmanagedType.LPArray), Out] ComPropSpec[] rgpspec,
	//	[MarshalAs(UnmanagedType.LPArray), Out] PropVariant[] rgpropvar);

	//[PreserveSig]
	//int WriteMultiple(
	//	uint cpspec,
	//	[MarshalAs(UnmanagedType.LPArray)] ComPropSpec[] rgpspec,
	//	[MarshalAs(UnmanagedType.LPArray)] PropVariant[] rgpropvar,
	//	uint propidNameFirst);

	//[PreserveSig]
	//int DeleteMultiple(
	//	uint cpspec,
	//	[MarshalAs(UnmanagedType.LPArray)] ComPropSpec[] rgpspec);

	//[PreserveSig]
	//int ReadPropertyNames(
	//	uint cpropid,
	//	[MarshalAs(UnmanagedType.LPArray)] uint[] rgpropid,
	//	[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr)] out string[] rglpwstrName);

	//[PreserveSig]
	//int WritePropertyNames(
	//	uint cpropid,
	//	[MarshalAs(UnmanagedType.LPArray)] uint[] rgpropid,
	//	[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr)] string[] rglpwstrName);

	//[PreserveSig]
	//int DeletePropertyNames(
	//	uint cpropid,
	//	[MarshalAs(UnmanagedType.LPArray)] uint[] rgpropid);

	//[PreserveSig]
	//int Commit(
	//	uint grfCommitFlags);

	//[PreserveSig] int Revert();

	//[PreserveSig]
	//int Enum(
	//	out IEnumSTATPROPSTG ppenum);

	//[PreserveSig]
	//int SetTimes(
	//	in FileTime pctime,
	//	in FileTime patime,
	//	in FileTime pmtime);

	//[PreserveSig]
	//int SetClass(
	//	in Guid clsid);

	//[PreserveSig]
	//int Stat(
	//	out ComStatPropSetStorage pstatpsstg);
}

/*#define	PID_DICTIONARY	( 0 )

#define	PID_CODEPAGE	( 0x1 )

#define	PID_FIRST_USABLE	( 0x2 )

#define	PID_FIRST_NAME_DEFAULT	( 0xfff )

#define	PID_LOCALE	( 0x80000000 )

#define	PID_MODIFY_TIME	( 0x80000001 )

#define	PID_SECURITY	( 0x80000002 )

#define	PID_BEHAVIOR	( 0x80000003 )

#define	PID_ILLEGAL	( 0xffffffff )

// Range which is read-only to downlevel implementations
#define	PID_MIN_READONLY	( 0x80000000 )

#define	PID_MAX_READONLY	( 0xbfffffff )
*/
[StructLayout(LayoutKind.Sequential)]
public struct ComPropSpec : IDisposable
{
	private uint _kind;
	private nint _propidOrLpwstr;

	public readonly bool IsInvalid => _kind == 0xffffffff;
	public readonly bool IsString => _kind == 0;
	public readonly bool IsProgID => _kind == 1;

	public ComPropSpec(object? value)
	{
		(_kind, _propidOrLpwstr) = value switch
		{
			not { } => (0xffffffff, 0),
			string s => (0u, Marshal.StringToCoTaskMemUni(s)),
			uint i => (1u, (nint)i),
			_ => throw new ArgumentException(),
		};
	}

	public void Dispose()
	{
		if (IsString)
		{
			Marshal.FreeCoTaskMem(_propidOrLpwstr);
		}
		_propidOrLpwstr = 0;
		_kind = 0xffffffff;
	}

	public static ComPropSpec[] CreateArray(ReadOnlySpan<object?> values)
	{
		var specs = GC.AllocateUninitializedArray<ComPropSpec>(values.Length);
		for (var i = 0; i < values.Length; i++)
		{
			specs[i] = new(values[i]);
		}
		return specs;
	}

	public static void DestroyArray(ComPropSpec[] propSpecs)
	{
		foreach (var ps in propSpecs)
			ps.Dispose();
	}
}

/// <summary>
/// 
/// </summary>
/// <remarks><c>STATPROPSETSTG</c></remarks>
public struct ComStatPropSetStorage
{
	public PropertySetFormatID FmtID;
	public Guid Clsid;
	public uint Flags;
	public FileTime ModifyTime;
	public FileTime CreationTime;
	public FileTime AccessTime;
	public uint OSVersion;
}

/// <summary>
/// 
/// </summary>
/// <remarks><c>STATPROPSTG</c></remarks>
public struct ComStatPropStorage
{
	[MarshalAs(UnmanagedType.LPWStr)]
	public string Name;
	public uint PropID;
	public VarType VT;
}