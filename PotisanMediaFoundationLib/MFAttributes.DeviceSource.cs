namespace Potisan.Windows.MediaFoundation;

public sealed partial class MFDeviceSourceAttributes(MFAttributes attrs)
{
	public MFAttributes Attributes { get; } = attrs;

	public Guid? SourceType
	{
		get => Attributes.GetGuidWithDefault(MFAttributeGuids.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE);
		set => Attributes.SetGuidWithDefault(MFAttributeGuids.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE, value);
	}

	//public void SetSourceTypeVideo

	public bool? VideoCaptureHWSource
	{
		get => MF.UInt32NullableToBool32Nullable(Attributes.GetUInt32WithDefault(MFAttributeGuids.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_VIDCAP_HW_SOURCE));
		set => Attributes.SetUInt32WithDefault(MFAttributeGuids.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_VIDCAP_HW_SOURCE, MF.BoolNullableToUInt32Nullable(value));
	}

	public string? FriendlyName
	{
		get => Attributes.GetStringWithDefault(MFAttributeGuids.MF_DEVSOURCE_ATTRIBUTE_FRIENDLY_NAME);
		set => Attributes.SetStringWithDefault(MFAttributeGuids.MF_DEVSOURCE_ATTRIBUTE_FRIENDLY_NAME, value);
	}

	public Guid? MediaType
	{
		get => Attributes.GetGuidWithDefault(MFAttributeGuids.MF_DEVSOURCE_ATTRIBUTE_MEDIA_TYPE);
		set => Attributes.SetGuidWithDefault(MFAttributeGuids.MF_DEVSOURCE_ATTRIBUTE_MEDIA_TYPE, value);
	}

	public Guid? VideoCaptureCategory
	{
		get => Attributes.GetGuidWithDefault(MFAttributeGuids.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_VIDCAP_CATEGORY);
		set => Attributes.SetGuidWithDefault(MFAttributeGuids.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_VIDCAP_CATEGORY, value);
	}

	//public static Guid MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_VIDCAP_MAX_BUFFERS => new(0x7dd9b730, 0x4f2d, 0x41d5, 0x8f, 0x95, 0xc, 0xc9, 0xa9, 0x12, 0xba, 0x26);
	//public static Guid MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_AUDCAP_ENDPOINT_ID => new(0x30da9258, 0xfeb9, 0x47a7, 0xa4, 0x53, 0x76, 0x3a, 0x7a, 0x8e, 0x1c, 0x5f);

	public string? VideoCaptureSymbolicLink
	{
		get => Attributes.GetStringWithDefault(MFAttributeGuids.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_VIDCAP_SYMBOLIC_LINK);
		set => Attributes.SetStringWithDefault(MFAttributeGuids.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_VIDCAP_SYMBOLIC_LINK, value);
	}

	public string? AudioCaptureSymbolicLink
	{
		get => Attributes.GetStringWithDefault(MFAttributeGuids.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_AUDCAP_SYMBOLIC_LINK);
		set => Attributes.SetStringWithDefault(MFAttributeGuids.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_AUDCAP_SYMBOLIC_LINK, value);
	}

	public MFRole? SourceTypeAudioCaptureRole
	{
		get => (MFRole?)Attributes.GetUInt32WithDefault(MFAttributeGuids.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_AUDCAP_ROLE);
		set => Attributes.SetUInt32WithDefault(MFAttributeGuids.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_AUDCAP_ROLE, (uint?)value);
	}

	//public static Guid MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_VIDCAP_PROVIDER_DEVICE_ID => new(0x36689d42, 0xa06c, 0x40ae, 0x84, 0xcf, 0xf5, 0xa0, 0x34, 0x6, 0x7c, 0xc4);
	//public static Guid MF_DEVSOURCE_ATTRIBUTE_SOURCE_XADDRESS => new(0xbca0be52, 0xc327, 0x44c7, 0x9b, 0x7d, 0x7f, 0xa8, 0xd9, 0xb5, 0xbc, 0xda);
	//public static Guid MF_DEVSOURCE_ATTRIBUTE_SOURCE_STREAM_URL => new(0x9d7b40d2, 0x3617, 0x4043, 0x93, 0xe3, 0x8d, 0x6d, 0xa9, 0xbb, 0x34, 0x92);
	//public static Guid MF_DEVSOURCE_ATTRIBUTE_SOURCE_USERNAME => new(0x5d01add, 0x949f, 0x46eb, 0xbc, 0x8e, 0x8b, 0xd, 0x2b, 0x32, 0xd7, 0x9d);
	//public static Guid MF_DEVSOURCE_ATTRIBUTE_SOURCE_PASSWORD => new(0xa0fd7e16, 0x42d9, 0x49df, 0x84, 0xc0, 0xe8, 0x2c, 0x5e, 0xab, 0x88, 0x74);
}