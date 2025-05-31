namespace Potisan.Windows.PropertySystem.ComTypes;

[ComImport]
[Guid("0000013A-0000-0000-C000-000000000046")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IPropertySetStorage
{
	[PreserveSig]
	int Create(
		in Guid rfmtid,
		in Guid pclsid,
		uint grfFlags,
		uint grfMode,
		out IPropertyStorage ppprstg);

	[PreserveSig]
	int Open(
		in Guid rfmtid,
		uint grfMode,
		out IPropertyStorage ppprstg);

	[PreserveSig]
	int Delete(
		in Guid rfmtid);

	[PreserveSig]
	int Enum(
		out IEnumSTATPROPSETSTG ppenum);
}