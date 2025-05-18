namespace Potisan.Windows.Text.Tsf.ComTypes;

[ComImport]
[Guid("F0C0F8DD-CF38-44E1-BB0F-68CF0D551C78")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IEnumTfContextViews
{
	[PreserveSig]
	int Clone(
		out IEnumTfContextViews ppEnum);

	[PreserveSig]
	int Next(
		uint ulCount,
		out ITfContextView rgViews,
		out uint pcFetched);

	[PreserveSig]
	int Reset();

	[PreserveSig]
	int Skip(
		uint ulCount);
}