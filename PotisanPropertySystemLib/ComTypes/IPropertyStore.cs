namespace Potisan.Windows.PropertySystem.ComTypes;

[ComImport]
[Guid("886d8eeb-8cf2-4446-8d02-cdba1dbdcf99")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IPropertyStore
{
	[PreserveSig]
	int GetCount(
		out uint cProps);

	[PreserveSig]
	int GetAt(
		uint iProp,
		[Out] PropertyKey pkey);

	[PreserveSig]
	int GetValue(
		PropertyKey key,
		[Out] PropVariant pv);

	[PreserveSig]
	int SetValue(
		PropertyKey key,
		PropVariant propvar);

	[PreserveSig]
	int Commit();
}