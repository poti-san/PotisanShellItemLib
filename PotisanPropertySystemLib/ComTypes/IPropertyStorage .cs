namespace Potisan.Windows.PropertySystem.ComTypes;

[ComImport]
[Guid("00000138-0000-0000-C000-000000000046")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IPropertyStorage
{
	[PreserveSig]
	int ReadMultiple(
		uint cpspec,
		[MarshalAs(UnmanagedType.LPArray), Out] ComPropSpec[] rgpspec,
		[MarshalAs(UnmanagedType.LPArray), Out] PropVariant[] rgpropvar);

	[PreserveSig]
	int WriteMultiple(
		uint cpspec,
		[MarshalAs(UnmanagedType.LPArray)] ComPropSpec[] rgpspec,
		[MarshalAs(UnmanagedType.LPArray)] PropVariant[] rgpropvar,
		uint propidNameFirst);

	[PreserveSig]
	int DeleteMultiple(
		uint cpspec,
		[MarshalAs(UnmanagedType.LPArray)] ComPropSpec[] rgpspec);

	[PreserveSig]
	int ReadPropertyNames(
		uint cpropid,
		[MarshalAs(UnmanagedType.LPArray)] uint[] rgpropid,
		[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr)] out string[] rglpwstrName);

	[PreserveSig]
	int WritePropertyNames(
		uint cpropid,
		[MarshalAs(UnmanagedType.LPArray)] uint[] rgpropid,
		[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr)] string[] rglpwstrName);

	[PreserveSig]
	int DeletePropertyNames(
		uint cpropid,
		[MarshalAs(UnmanagedType.LPArray)] uint[] rgpropid);

	[PreserveSig]
	int Commit(
		uint grfCommitFlags);

	[PreserveSig] int Revert();

	[PreserveSig]
	int Enum(
		out IEnumSTATPROPSTG ppenum);

	[PreserveSig]
	int SetTimes(
		in FileTime pctime,
		in FileTime patime,
		in FileTime pmtime);

	[PreserveSig]
	int SetClass(
		in Guid clsid);

	[PreserveSig]
	int Stat(
		out ComStatPropSetStorage pstatpsstg);
}