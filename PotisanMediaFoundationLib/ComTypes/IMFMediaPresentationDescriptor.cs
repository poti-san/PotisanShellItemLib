namespace Potisan.Windows.MediaFoundation.ComTypes;

[ComImport]
[Guid("03cb2711-24d7-4db6-a17f-f3a7a479a536")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IMFPresentationDescriptor // IMFAttributes
{
	#region IMFAttributes

	[PreserveSig]
	int GetItem(
		in Guid guidKey,
		[Out] PropVariant pValue);

	[PreserveSig]
	int GetItemType(
		in Guid guidKey,
		out MFAttributeType pType);

	[PreserveSig]
	int CompareItem(
		in Guid guidKey,
		PropVariant Value,
		[MarshalAs(UnmanagedType.Bool)] out bool pbResult);

	[PreserveSig]
	int Compare(
		IMFAttributes? pTheirs,
		MFAttributeMatchType MatchType,
		[MarshalAs(UnmanagedType.Bool)] out bool pbResult);

	[PreserveSig]
	int GetUINT32(
		in Guid guidKey,
		out uint punValue);

	[PreserveSig]
	int GetUINT64(
		in Guid guidKey,
		out ulong punValue);

	[PreserveSig]
	int GetDouble(
		in Guid guidKey,
		out double pfValue);

	[PreserveSig]
	int GetGUID(
		in Guid guidKey,
		out Guid pguidValue);

	[PreserveSig]
	int GetStringLength(
		in Guid guidKey,
		out uint pcchLength);

	[PreserveSig]
	int GetString(
		in Guid guidKey,
		ref char pwszValue, uint cchBufSize,
		ref uint pcchLength);

	[PreserveSig]
	int GetAllocatedString(
		in Guid guidKey,
		[MarshalAs(UnmanagedType.LPWStr)] out string? ppwszValue,
		out uint pcchLength);

	[PreserveSig]
	int GetBlobSize(
		in Guid guidKey,
		out uint pcbBlobSize);

	[PreserveSig]
	int GetBlob(
		in Guid guidKey,
		ref byte pBuf,
		uint cbBufSize,
		ref uint pcbBlobSize);

	[PreserveSig]
	int GetAllocatedBlob(
		in Guid guidKey,
		[MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] out byte[] ppBuf,
		out uint pcbSize);

	[PreserveSig]
	int GetUnknown(
		in Guid guidKey,
		in Guid riid,
		[MarshalAs(UnmanagedType.IUnknown)] out object? ppv);

	[PreserveSig]
	int SetItem(
		in Guid guidKey,
		PropVariant Value);

	[PreserveSig]
	int DeleteItem(
		in Guid guidKey);

	[PreserveSig]
	int DeleteAllItems();

	[PreserveSig]
	int SetUINT32(
		in Guid guidKey,
		uint unValue);

	[PreserveSig]
	int SetUINT64(
		in Guid guidKey,
		ulong unValue);

	[PreserveSig]
	int SetDouble(
		in Guid guidKey,
		double fValue);

	[PreserveSig]
	int SetGUID(
		in Guid guidKey,
		in Guid guidValue);

	[PreserveSig]
	int SetString(
		in Guid guidKey,
		in char wszValue);

	[PreserveSig]
	int SetBlob(
		in Guid guidKey,
		in byte pBuf,
		uint cbBufSize);

	[PreserveSig]
	int SetUnknown(
		in Guid guidKey,
		[MarshalAs(UnmanagedType.IUnknown)] object? pUnknown);

	[PreserveSig]
	int LockStore();

	[PreserveSig]
	int UnlockStore();

	[PreserveSig]
	int GetCount(
		out uint pcItems);

	[PreserveSig]
	int GetItemByIndex(
		uint unIndex,
		out Guid pguidKey,
		[Out] PropVariant pValue);

	[PreserveSig]
	int CopyAllItems(
		IMFAttributes? pDest);

	#endregion IMFAttributes

	[PreserveSig]
	int GetStreamDescriptorCount(
		out uint pdwDescriptorCount);

	[PreserveSig]
	int GetStreamDescriptorByIndex(
		uint dwIndex,
		[MarshalAs(UnmanagedType.Bool)] out bool pfSelected,
		out IMFStreamDescriptor? ppDescriptor);

	[PreserveSig]
	int SelectStream(
		uint dwDescriptorIndex);

	[PreserveSig]
	int DeselectStream(
		uint dwDescriptorIndex);

	[PreserveSig]
	int Clone(
		out IMFPresentationDescriptor? ppPresentationDescriptor);
}