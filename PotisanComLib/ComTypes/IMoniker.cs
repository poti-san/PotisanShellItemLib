namespace Potisan.Windows.Com.ComTypes;

[ComImport]
[Guid("0000000f-0000-0000-C000-000000000046")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public partial interface IMoniker : IPersistStream
{
	[PreserveSig]
	int BindToObject(
		IBindCtx pbc,
		IMoniker? pmkToLeft,
		in Guid riidResult,
		[MarshalAs(UnmanagedType.IUnknown)] out object? ppvResult);

	[PreserveSig]
	int BindToStorage(
		IBindCtx pbc,
		IMoniker? pmkToLeft,
		in Guid riid,
		[MarshalAs(UnmanagedType.IUnknown)] out object? ppvObj);

	[PreserveSig]
	int Reduce(
		IBindCtx? pbc,
		uint dwReduceHowFar,
		IMoniker? ppmkToLeft,
		out IMoniker? ppmkReduced);

	[PreserveSig]
	int ComposeWith(
		IMoniker? pmkRight,
		[MarshalAs(UnmanagedType.Bool)] bool fOnlyIfNotGeneric,
		out IMoniker? ppmkComposite);

	[PreserveSig]
	int Enum(
		[MarshalAs(UnmanagedType.Bool)] bool fForward,
		out IEnumMoniker ppenumMoniker);

	[PreserveSig]
	int IsEqual(
		IMoniker? pmkOtherMoniker);

	[PreserveSig]
	int Hash(
		out uint pdwHash);

	[PreserveSig]
	int IsRunning(
		IBindCtx pbc,
		IMoniker? pmkToLeft,
		IMoniker? pmkNewlyRunning);

	[PreserveSig]
	int GetTimeOfLastChange(
		IBindCtx? pbc,
		IMoniker? pmkToLeft,
		out FileTime pFileTime);

	[PreserveSig]
	int Inverse(
		out IMoniker? ppmk);

	[PreserveSig]
	int CommonPrefixWith(
		IMoniker? pmkOther,
		out IMoniker? ppmkPrefix);

	[PreserveSig]
	int RelativePathTo(
		IMoniker? pmkOther,
		out IMoniker? ppmkRelPath);

	[PreserveSig]
	int GetDisplayName(
		IBindCtx? pbc,
		IMoniker? pmkToLeft,
		[MarshalAs(UnmanagedType.LPWStr)] out string ppszDisplayName);

	[PreserveSig]
	int ParseDisplayName(
		IBindCtx? pbc,
		IMoniker? pmkToLeft,
		[MarshalAs(UnmanagedType.LPWStr)] string pszDisplayName,
		out uint pchEaten,
		out IMoniker? ppmkOut);

	[PreserveSig]
	int IsSystemMoniker(
		out uint pdwMksys);
}