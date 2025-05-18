namespace Potisan.Windows.Text.Tsf.ComTypes;

[ComImport]
[Guid("8ed8981b-7c10-4d7d-9fb3-ab72e9c75f72")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IEnumTfPropertyValue
{
	[PreserveSig]
	int Clone(
		out IEnumTfPropertyValue ppEnum);

	[PreserveSig]
	int Next(
		uint ulCount,
		out TFPropertyValue rgValues,
		out uint pcFetched);

	[PreserveSig]
	int Reset();

	[PreserveSig]
	int Skip(
		uint ulCount);
}