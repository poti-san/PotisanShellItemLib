namespace Potisan.Windows.Com.ComTypes;

[ComImport]
[Guid("000001ce-0000-0000-C000-000000000046")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IComThreadingInfo
{
	[PreserveSig]
	int GetCurrentApartmentType(
		out ComApartmentType pAptType);

	[PreserveSig]
	int GetCurrentThreadType(
		out ComThreadType pThreadType);

	[PreserveSig]
	int GetCurrentLogicalThreadId(
		out Guid pguidLogicalThreadId);

	[PreserveSig]
	int SetCurrentLogicalThreadId(
		in Guid rguid);
}