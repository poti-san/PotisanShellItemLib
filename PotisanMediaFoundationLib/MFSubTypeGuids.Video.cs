using System.Collections.Immutable;
using System.Text;

namespace Potisan.Windows.MediaFoundation;

public static class MFVideoSubTypeGuids
{
	public static Guid Base => DefineMediaTypeGuid(0x00000000);
	public static Guid Rgb32 => DefineMediaTypeGuid(22);
	public static Guid Argb32 => DefineMediaTypeGuid(21);
	public static Guid Rgb24 => DefineMediaTypeGuid(20);
	public static Guid Rgb555 => DefineMediaTypeGuid(24);
	public static Guid Rgb565 => DefineMediaTypeGuid(23);
	public static Guid Rgb8 => DefineMediaTypeGuid(41);
	public static Guid L8 => DefineMediaTypeGuid(50);
	public static Guid L16 => DefineMediaTypeGuid(80);
	public static Guid D16 => DefineMediaTypeGuid(81);
	public static Guid AI44 => DefineMediaTypeGuid(FCC("AI44"));
	public static Guid AYUV => DefineMediaTypeGuid(FCC("AYUV"));
	public static Guid YUY2 => DefineMediaTypeGuid(FCC("YUY2"));
	public static Guid YVYU => DefineMediaTypeGuid(FCC("YVYU"));
	public static Guid YVU9 => DefineMediaTypeGuid(FCC("YVU9"));
	public static Guid UYVY => DefineMediaTypeGuid(FCC("UYVY"));
	public static Guid NV11 => DefineMediaTypeGuid(FCC("NV11"));
	public static Guid NV12 => DefineMediaTypeGuid(FCC("NV12"));
	public static Guid NV21 => DefineMediaTypeGuid(FCC("NV21"));
	public static Guid YV12 => DefineMediaTypeGuid(FCC("YV12"));
	public static Guid I420 => DefineMediaTypeGuid(FCC("I420"));
	public static Guid IYUV => DefineMediaTypeGuid(FCC("IYUV"));
	public static Guid Y210 => DefineMediaTypeGuid(FCC("Y210"));
	public static Guid Y216 => DefineMediaTypeGuid(FCC("Y216"));
	public static Guid Y410 => DefineMediaTypeGuid(FCC("Y410"));
	public static Guid Y416 => DefineMediaTypeGuid(FCC("Y416"));
	public static Guid Y41P => DefineMediaTypeGuid(FCC("Y41P"));
	public static Guid Y41T => DefineMediaTypeGuid(FCC("Y41T"));
	public static Guid Y42T => DefineMediaTypeGuid(FCC("Y42T"));
	public static Guid P210 => DefineMediaTypeGuid(FCC("P210"));
	public static Guid P216 => DefineMediaTypeGuid(FCC("P216"));
	public static Guid P010 => DefineMediaTypeGuid(FCC("P010"));
	public static Guid P016 => DefineMediaTypeGuid(FCC("P016"));
	public static Guid V210 => DefineMediaTypeGuid(FCC("v210"));
	public static Guid V216 => DefineMediaTypeGuid(FCC("v216"));
	public static Guid V410 => DefineMediaTypeGuid(FCC("v410"));
	public static Guid MP43 => DefineMediaTypeGuid(FCC("MP43"));
	public static Guid MP4S => DefineMediaTypeGuid(FCC("MP4S"));
	public static Guid M4S2 => DefineMediaTypeGuid(FCC("M4S2"));
	public static Guid MP4V => DefineMediaTypeGuid(FCC("MP4V"));
	public static Guid Wmv1 => DefineMediaTypeGuid(FCC("WMV1"));
	public static Guid Wmv2 => DefineMediaTypeGuid(FCC("WMV2"));
	public static Guid Wmv3 => DefineMediaTypeGuid(FCC("WMV3"));
	public static Guid WVC1 => DefineMediaTypeGuid(FCC("WVC1"));
	public static Guid MSS1 => DefineMediaTypeGuid(FCC("MSS1"));
	public static Guid MSS2 => DefineMediaTypeGuid(FCC("MSS2"));
	public static Guid MPG1 => DefineMediaTypeGuid(FCC("MPG1"));
	public static Guid DVSL => DefineMediaTypeGuid(FCC("dvsl"));
	public static Guid DVSD => DefineMediaTypeGuid(FCC("dvsd"));
	public static Guid DVHD => DefineMediaTypeGuid(FCC("dvhd"));
	public static Guid DV25 => DefineMediaTypeGuid(FCC("dv25"));
	public static Guid DV50 => DefineMediaTypeGuid(FCC("dv50"));
	public static Guid DVH1 => DefineMediaTypeGuid(FCC("dvh1"));
	public static Guid DVC => DefineMediaTypeGuid(FCC("dvc "));
	public static Guid H264 => DefineMediaTypeGuid(FCC("H264"));
	public static Guid H265 => DefineMediaTypeGuid(FCC("H265"));
	public static Guid MJPG => DefineMediaTypeGuid(FCC("MJPG"));
	public static Guid YUV420O => DefineMediaTypeGuid(FCC("420O"));
	public static Guid HEVC => DefineMediaTypeGuid(FCC("HEVC"));
	public static Guid HEVCES => DefineMediaTypeGuid(FCC("HEVS"));
	public static Guid VP80 => DefineMediaTypeGuid(FCC("VP80"));
	public static Guid VP90 => DefineMediaTypeGuid(FCC("VP90"));
	public static Guid ORAW => DefineMediaTypeGuid(FCC("ORAW"));
	public static Guid H263 => DefineMediaTypeGuid(FCC("H263"));
	public static Guid A2R10G10B10 => DefineMediaTypeGuid(31);
	public static Guid A16B16G16R16F => DefineMediaTypeGuid(113);
	public static Guid VP10 => DefineMediaTypeGuid(FCC("VP10"));
	public static Guid AV1 => DefineMediaTypeGuid(FCC("AV01"));
	public static Guid Theora => DefineMediaTypeGuid(FCC("theo"));

	public static ImmutableDictionary<string, Guid> ToNameToValueDictionary()
	{
		return typeof(MFVideoSubTypeGuids).GetProperties()
			.Where(prop => prop.GetValue(null) is Guid)
			.Select(prop => KeyValuePair.Create(prop.Name, (Guid)prop.GetValue(null)!))
			.ToImmutableDictionary();
	}

	public static ImmutableDictionary<Guid, string> ToValueToNameDictionary()
	{
		return typeof(MFVideoSubTypeGuids).GetProperties()
			.Where(prop => prop.GetValue(null) is Guid)
			.Select(prop => KeyValuePair.Create((Guid)prop.GetValue(null)!, prop.Name))
			.ToImmutableDictionary();
	}

	public static string GuidToString(in Guid guid)
	{
		foreach (var prop in typeof(MFVideoSubTypeGuids).GetProperties())
		{
			if (prop.GetValue(null) is Guid propGuid && propGuid == guid)
				return prop.Name;
		}
		throw new NotSupportedException();
	}

	private static uint MulticharLiteralToInt(string s)
	{
		var bytes = Encoding.ASCII.GetBytes(s);
		ArgumentOutOfRangeException.ThrowIfGreaterThan(bytes.Length, 4, nameof(s));
		return bytes.Length switch
		{
			0 => 0,
			1 => bytes[0],
			2 => (uint)(bytes[0] << 8) | bytes[1],
			3 => (uint)(bytes[0] << 16) | (uint)(bytes[1] << 8) | bytes[2],
			4 => (uint)(bytes[0] << 24) | (uint)(bytes[1] << 16) | (uint)(bytes[2] << 8) | bytes[3],
			_ => 0, // dummy for Code Analyzer
		};
	}

	private static uint FCC(string s)
	{
		var ch4 = MulticharLiteralToInt(s);
		return ((ch4 & 0xff) << 24) | ((ch4 & 0xff00) << 8) | ((ch4 & 0xff0000) >> 8) | ((ch4 & 0xff000000) >> 24);
	}

	private static Guid DefineMediaTypeGuid(uint format)
		=> new(format, 0x0000, 0x0010, 0x80, 0x00, 0x00, 0xaa, 0x00, 0x38, 0x9b, 0x71);
}
