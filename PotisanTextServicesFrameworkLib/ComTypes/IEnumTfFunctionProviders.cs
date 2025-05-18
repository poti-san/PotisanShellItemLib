namespace Potisan.Windows.Text.Tsf.ComTypes;

[ComImport]
[Guid("e4b24db0-0990-11d3-8df0-00105a2799b5")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IEnumTfFunctionProviders
{
	[PreserveSig]
	int Clone(
		out IEnumTfFunctionProviders ppEnum);

	[PreserveSig]
	int Next(
		uint ulCount,
		out ITfFunctionProvider ppCmdobj,
		out uint pcFetch);

	[PreserveSig]
	int Reset();

	[PreserveSig]
	int Skip(
		uint ulCount);
}