namespace Potisan.Windows.PropertySystem.ComTypes;

[ComImport]
[Guid("11e1fbf9-2d56-4a6b-8db3-7cd193a471f2")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IPropertyEnumType
{
	[PreserveSig]
	int GetEnumType(
		out PropEnumType penumtype);

	[PreserveSig]
	int GetValue(
		[Out] PropVariant ppropvar);

	[PreserveSig]
	int GetRangeMinValue(
		[Out] PropVariant ppropvarMin);

	[PreserveSig]
	int GetRangeSetValue(
		[Out] PropVariant ppropvarSet);

	[PreserveSig]
	int GetDisplayText(
		[MarshalAs(UnmanagedType.LPWStr)] out string ppszDisplay);
}