using System.Collections.Immutable;

using Potisan.Windows.MediaFoundation.ComTypes;

namespace Potisan.Windows.MediaFoundation;

public static class MF
{
	public const ushort SdkVersion = 0x0002;
	public const ushort ApiVersion = 0x0070;
	public const uint MFVersion = SdkVersion << 16 | ApiVersion;

	public static void Startup()
	{
		[DllImport("mfplat.dll")]
		static extern int MFStartup(uint Version, uint dwFlags);

		Marshal.ThrowExceptionForHR(MFStartup(MFVersion, 0));
	}

	public static void Shutdown()
	{
		[DllImport("mfplat.dll")]
		static extern int MFShutdown();

		Marshal.ThrowExceptionForHR(MFShutdown());
	}

	public static void LockPlatform()
	{
		[DllImport("mfplat.dll")]
		static extern int MFLockPlatform();

		Marshal.ThrowExceptionForHR(MFLockPlatform());
	}

	public static void UnlockPlatform()
	{
		[DllImport("mfplat.dll")]
		static extern int MFUnlockPlatform();

		Marshal.ThrowExceptionForHR(MFUnlockPlatform());
	}

	public static ImmutableArray<string> SupportedSchemes
	{
		get
		{
			[DllImport("mf.dll")]
			static extern int MFGetSupportedSchemes([Out] PropVariant pPropVarSchemeArray);

			// TODO PROPVARIANT
			var pv = new PropVariant();
			Marshal.ThrowExceptionForHR(MFGetSupportedSchemes(pv));
			return ImmutableCollectionsMarshal.AsImmutableArray((string[])pv.ToObject());
		}
	}

	public static ImmutableArray<string> SupportedMimeTypes
	{
		get
		{
			[DllImport("mf.dll")]
			static extern int MFGetSupportedMimeTypes([Out] PropVariant pPropVarMimeTypeArray);

			// TODO PROPVARIANT
			var pv = new PropVariant();
			Marshal.ThrowExceptionForHR(MFGetSupportedMimeTypes(pv));
			return ImmutableCollectionsMarshal.AsImmutableArray((string[])pv.ToObject());
		}
	}

	/// <summary>
	/// ビデオプロセッサMFTを作成します。
	/// クラスはIMFRealTimeClientEx、IMFTransform、IMFVideoProcessorControl COMインターフェイスをサポートします。
	/// </summary>
	public static ComResult<MFTransform> CreateVideoProcessorMftNoThrow()
	{
		Guid CLSID_VideoProcessorMFT = new(0x88753b26, 0x5b24, 0x49bd, 0xb2, 0xe7, 0xc, 0x44, 0x5c, 0x78, 0xc9, 0x82);
		return ComHelper.CreateInstanceNoThrow<MFTransform, IMFTransform>(CLSID_VideoProcessorMFT);
	}

	/// <inheritdoc cref="CreateVideoProcessorMftNoThrow"/>
	public static MFTransform CreateVideoProcessorMFT()
		=> CreateVideoProcessorMftNoThrow().Value;

	public static ComResult<object> GetServiceNoThrow(in Guid serviceGuid, in Guid iid)
	{
		[DllImport("mfplat.dll")]
		static extern int MFGetService(
			[MarshalAs(UnmanagedType.IUnknown)] object? punkObject, in Guid guidService, in Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object? ppvObject);

		return new(MFGetService(null, serviceGuid, iid, out var x), x!);
	}

	public static object GetService(in Guid serviceGuid, in Guid iid)
		=> GetServiceNoThrow(serviceGuid, iid).Value;

	public static ComResult<TWrapper> GetServiceNoThrow<TWrapper, TInterface>(in Guid serviceGuid)
		where TWrapper : IComUnknownWrapper
		where TInterface : class
		=> IComUnknownWrapper.Wrap<TWrapper>(GetServiceNoThrow(serviceGuid, typeof(TInterface).GUID));

	public static nint AllocHeapMem(nuint size)
	{
		[DllImport("mfplat.dll")]
		static extern nint MFHeapAlloc(nuint nSize, uint dwFlags, nint pszFile, int line, MFAllocationType eat);

		return MFHeapAlloc(size, 0, 0, 0, MFAllocationType.Ignore);
	}

	public static void FreeHeapMem(nint p)
	{
		[DllImport("mfplat.dll")]
		static extern void MFHeapFree(nint pv);

		MFHeapFree(p);
	}

	public static bool? UInt32NullableToBool32Nullable(uint? x)
		=> x switch { 0 => false, null => null, _ => true };

	public static uint? BoolNullableToUInt32Nullable(bool? x)
		=> x switch { false => 0, true => 1, null => null };
}

public enum MFRole : uint
{
	Console = 0,
	Multimedia,
	Communications,
}

public enum MFAllocationType : uint
{
	Dynamic = 0,
	RT,
	Pageable,
	Ignore
}
