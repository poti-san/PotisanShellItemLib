using System.Diagnostics;

using Potisan.Windows.MediaFoundation.ComTypes;

namespace Potisan.Windows.MediaFoundation;

public class MFMediaSession(object? o) : ComUnknownWrapperBase<IMFMediaSession>(o)
{
	// TODO IMFMediaEventGenerator

	public ComResult SetTopologyNoThrow(MFTopology topology, MFSessionSetTopologyFlag flags = 0)
		=> new(_obj.SetTopology((uint)flags, (IMFTopology)topology.WrappedObject!));

	public void SetTopology(MFTopology topology, MFSessionSetTopologyFlag flags = 0)
		=> SetTopologyNoThrow(topology, flags).ThrowIfError();

	public ComResult ClearTopologiesNoThrow()
		=> new(_obj.ClearTopologies());

	public void ClearTopologies()
		=> ClearTopologiesNoThrow().ThrowIfError();

	public ComResult StartNoThrow(in Guid timeFormat, PropVariant startPosition)
		=> new(_obj.Start(timeFormat, startPosition));

	public void Start(in Guid timeFormat, PropVariant startPosition)
		=> StartNoThrow(timeFormat, startPosition).ThrowIfError();

	public ComResult StartNoThrow(long startPosition100NS)
		=> StartNoThrow(Guid.Empty, PropVariant.InitInt64(startPosition100NS));

	public void Start(long startPosition100NS)
		=> StartNoThrow(startPosition100NS).ThrowIfError();

	public ComResult PauseNoThrow()
		=> new(_obj.Pause());

	public void Pause()
		=> PauseNoThrow().ThrowIfError();

	public ComResult StopNoThrow()
		=> new(_obj.Stop());

	public void Stop()
		=> StopNoThrow().ThrowIfError();

	public ComResult CloseNoThrow()
		=> new(_obj.Close());

	public void Close()
		=> CloseNoThrow().ThrowIfError();

	public ComResult ShutdownNoThrow()
		=> new(_obj.Shutdown());

	public void Shutdown()
		=> ShutdownNoThrow().ThrowIfError();

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<MFClock> ClockNoThrow
		=> new(_obj.GetClock(out var x), new(x));

	public MFClock Clock
		=> ClockNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<MFSessionCapability> SessionCapabilitiesNoThrow
		=> new(_obj.GetSessionCapabilities(out var x), (MFSessionCapability)x);

	public MFSessionCapability SessionCapabilities
		=> SessionCapabilitiesNoThrow.Value;

	public ComResult<MFTopology> GetFullTopologyNoThrow(MFSessionGetFullTopologyFlag flags, ulong topoId)
		=> new(_obj.GetFullTopology((uint)flags, topoId, out var x), new(x));

	public MFTopology GetFullTopology(MFSessionGetFullTopologyFlag flags, ulong topoId)
		=> GetFullTopologyNoThrow(flags, topoId).Value;
}

/// <summary>
/// MFSESSION_SETTOPOLOGY_FLAGS
/// </summary>
[Flags]
public enum MFSessionSetTopologyFlag : uint
{
	Immediate = 0x1, NoResolution = 0x2, ClearCurrent = 0x4
}

/// <summary>
/// MFSESSIONCAP_*
/// </summary>
[Flags]
public enum MFSessionCapability : uint
{
	Start = 0x00000001,
	Seek = 0x00000002,
	Pause = 0x00000004,
	RateForward = 0x00000010,
	RateReverse = 0x00000020,
	DoesNotUseNetwork = 0x00000040,
}

/// <summary>
/// MFSESSION_GETFULLTOPOLOGY_FLAGS
/// </summary>
[Flags]
public enum MFSessionGetFullTopologyFlag
{
	Current = 0x1
}

//EXTERN_GUID(MF_SESSION_TOPOLOADER, 0x1e83d482, 0x1f1c, 0x4571, 0x84, 0x5, 0x88, 0xf4, 0xb2, 0x18, 0x1f, 0x71);
//	EXTERN_GUID(MF_SESSION_GLOBAL_TIME, 0x1e83d482, 0x1f1c, 0x4571, 0x84, 0x5, 0x88, 0xf4, 0xb2, 0x18, 0x1f, 0x72);
//	EXTERN_GUID(MF_SESSION_QUALITY_MANAGER, 0x1e83d482, 0x1f1c, 0x4571, 0x84, 0x5, 0x88, 0xf4, 0xb2, 0x18, 0x1f, 0x73);
//	EXTERN_GUID(MF_SESSION_CONTENT_PROTECTION_MANAGER, 0x1e83d482, 0x1f1c, 0x4571, 0x84, 0x5, 0x88, 0xf4, 0xb2, 0x18, 0x1f, 0x74);
//	EXTERN_GUID(MF_SESSION_SERVER_CONTEXT, 0xafe5b291, 0x50fa, 0x46e8, 0xb9, 0xbe, 0xc, 0xc, 0x3c, 0xe4, 0xb3, 0xa5);
//	EXTERN_GUID(MF_SESSION_REMOTE_SOURCE_MODE, 0xf4033ef4, 0x9bb3, 0x4378, 0x94, 0x1f, 0x85, 0xa0, 0x85, 0x6b, 0xc2, 0x44);
//	EXTERN_GUID(MF_SESSION_APPROX_EVENT_OCCURRENCE_TIME, 0x190e852f, 0x6238, 0x42d1, 0xb5, 0xaf, 0x69, 0xea, 0x33, 0x8e, 0xf8, 0x50);
//	EXTERN_GUID(MF_PMP_SERVER_CONTEXT, 0x2f00c910, 0xd2cf, 0x4278, 0x8b, 0x6a, 0xd0, 0x77, 0xfa, 0xc3, 0xa2, 0x5f);
//	STDAPI MFCreateMediaSession(
//		IMFAttributes* pConfiguration,
//		_Outptr_ IMFMediaSession** ppMediaSession

//		);
//#endif /* WINAPI_FAMILY_PARTITION(WINAPI_PARTITION_DESKTOP) */
//#pragma endregion
//#pragma region PC Application Family
//#if WINAPI_FAMILY_PARTITION(WINAPI_PARTITION_PC_APP)
//STDAPI MFCreatePMPMediaSession(
//    uint dwCreationFlags,
//    IMFAttributes *pConfiguration,
//    _Outptr_ IMFMediaSession** ppMediaSession,
//    _Outptr_opt_ IMFActivate **ppEnablerActivate
//    );

// ;
//#endif /* WINAPI_FAMILY_PARTITION(WINAPI_PARTITION_APP | WINAPI_PARTITION_GAMES) */
//#pragma endregion
//#pragma region Desktop Family
//#if WINAPI_FAMILY_PARTITION(WINAPI_PARTITION_DESKTOP)
//typedef 
//enum _MF_CONNECT_METHOD
//    {
//        MF_CONNECT_DIRECT	= 0,
//        MF_CONNECT_ALLOW_CONVERTER	= 0x1,
//        MF_CONNECT_ALLOW_DECODER	= 0x3,
//        MF_CONNECT_RESOLVE_INDEPENDENT_OUTPUTTYPES	= 0x4,
//        MF_CONNECT_AS_OPTIONAL	= 0x10000,
//        MF_CONNECT_AS_OPTIONAL_BRANCH	= 0x20000
//    } 	MF_CONNECT_METHOD;

//typedef 
//enum _MF_TOPOLOGY_RESOLUTION_STATUS_FLAGS
//    {
//        MF_TOPOLOGY_RESOLUTION_SUCCEEDED	= 0,
//        MF_OPTIONAL_NODE_REJECTED_MEDIA_TYPE	= 0x1,
//        MF_OPTIONAL_NODE_REJECTED_PROTECTED_PROCESS	= 0x2
//    } 	MF_TOPOLOGY_RESOLUTION_STATUS_FLAGS;

//EXTERN_C const DECLSPEC_SELECTANY PROPERTYKEY MFPKEY_SourceOpenMonitor = { { 0x074d4637, 0xb5ae, 0x465d, 0xaf, 0x17, 0x1a, 0x53, 0x8d, 0x28, 0x59, 0xdd}, 0x02 }; 
//#endif /* WINAPI_FAMILY_PARTITION(WINAPI_PARTITION_DESKTOP | WINAPI_PARTITION_GAMES) */
//#pragma endregion
//#pragma region Application or Games Family
//#if WINAPI_FAMILY_PARTITION(WINAPI_PARTITION_APP | WINAPI_PARTITION_GAMES)
//EXTERN_C const DECLSPEC_SELECTANY PROPERTYKEY MFPKEY_ASFMediaSource_ApproxSeek = { { 0xb4cd270f, 0x244d, 0x4969, 0xbb, 0x92, 0x3f, 0x0f, 0xb8, 0x31, 0x6f, 0x10}, 0x01 }; 
//#endif /* WINAPI_FAMILY_PARTITION(WINAPI_PARTITION_APP | WINAPI_PARTITION_GAMES) */
//#pragma endregion
//#if (WINVER >= _WIN32_WINNT_WIN7) 
//#pragma region Application or Games Family
//#if WINAPI_FAMILY_PARTITION(WINAPI_PARTITION_APP | WINAPI_PARTITION_GAMES)
//EXTERN_C const DECLSPEC_SELECTANY PROPERTYKEY MFPKEY_ASFMediaSource_IterativeSeekIfNoIndex = { { 0x170b65dc, 0x4a4e, 0x407a, 0xac, 0x22, 0x57, 0x7f, 0x50, 0xe4, 0xa3, 0x7c }, 0x01 }; 
//EXTERN_C const DECLSPEC_SELECTANY PROPERTYKEY MFPKEY_ASFMediaSource_IterativeSeek_Max_Count = { { 0x170b65dc, 0x4a4e, 0x407a, 0xac, 0x22, 0x57, 0x7f, 0x50, 0xe4, 0xa3, 0x7c }, 0x02 }; 
//EXTERN_C const DECLSPEC_SELECTANY PROPERTYKEY MFPKEY_ASFMediaSource_IterativeSeek_Tolerance_In_MilliSecond = { { 0x170b65dc, 0x4a4e, 0x407a, 0xac, 0x22, 0x57, 0x7f, 0x50, 0xe4, 0xa3, 0x7c }, 0x03 }; 
//EXTERN_C const DECLSPEC_SELECTANY PROPERTYKEY MFPKEY_Content_DLNA_Profile_ID = { { 0xcfa31b45, 0x525d, 0x4998, 0xbb, 0x44, 0x3f, 0x7d, 0x81, 0x54, 0x2f, 0xa4 }, 0x01 }; 
//EXTERN_C const DECLSPEC_SELECTANY PROPERTYKEY MFPKEY_MediaSource_DisableReadAhead = { { 0x26366c14, 0xc5bf, 0x4c76, 0x88, 0x7b, 0x9f, 0x17, 0x54, 0xdb, 0x5f, 0x9}, 0x01 }; 
//EXTERN_C const DECLSPEC_SELECTANY PROPERTYKEY MFPKEY_SBESourceMode = { { 0x3fae10bb, 0xf859, 0x4192, 0xb5, 0x62, 0x18, 0x68, 0xd3, 0xda, 0x3a, 0x02}, 0x01 }; 
//#endif /* WINAPI_FAMILY_PARTITION(WINAPI_PARTITION_APP | WINAPI_PARTITION_GAMES) */
//#pragma endregion
//#endif // (WINVER >= _WIN32_WINNT_WIN7) 
//#if (WINVER >= _WIN32_WINNT_WIN8) 
//#pragma region Application Family
//#if WINAPI_FAMILY_PARTITION(WINAPI_PARTITION_APP)
//EXTERN_C const DECLSPEC_SELECTANY PROPERTYKEY MFPKEY_PMP_Creation_Callback = { { 0x28bb4de2, 0x26a2, 0x4870, 0xb7, 0x20, 0xd2, 0x6b, 0xbe, 0xb1, 0x49, 0x42}, 0x01 }; 
//EXTERN_C const DECLSPEC_SELECTANY PROPERTYKEY MFPKEY_HTTP_ByteStream_Enable_Urlmon = { { 0xeda8afdf, 0xc171, 0x417f, 0x8d, 0x17, 0x2e, 0x09, 0x18, 0x30, 0x32, 0x92}, 0x01 }; 
//EXTERN_C const DECLSPEC_SELECTANY PROPERTYKEY MFPKEY_HTTP_ByteStream_Urlmon_Bind_Flags = { { 0xeda8afdf, 0xc171, 0x417f, 0x8d, 0x17, 0x2e, 0x09, 0x18, 0x30, 0x32, 0x92}, 0x02 }; 
//EXTERN_C const DECLSPEC_SELECTANY PROPERTYKEY MFPKEY_HTTP_ByteStream_Urlmon_Security_Id = { { 0xeda8afdf, 0xc171, 0x417f, 0x8d, 0x17, 0x2e, 0x09, 0x18, 0x30, 0x32, 0x92}, 0x03 }; 
//EXTERN_C const DECLSPEC_SELECTANY PROPERTYKEY MFPKEY_HTTP_ByteStream_Urlmon_Window = { { 0xeda8afdf, 0xc171, 0x417f, 0x8d, 0x17, 0x2e, 0x09, 0x18, 0x30, 0x32, 0x92}, 0x04 }; 
//EXTERN_C const DECLSPEC_SELECTANY PROPERTYKEY MFPKEY_HTTP_ByteStream_Urlmon_Callback_QueryService = { { 0xeda8afdf, 0xc171, 0x417f, 0x8d, 0x17, 0x2e, 0x09, 0x18, 0x30, 0x32, 0x92}, 0x05 }; 
//EXTERN_C const DECLSPEC_SELECTANY PROPERTYKEY MFPKEY_MediaProtectionSystemId =  { { 0x636b271d, 0xddc7, 0x49e9, 0xa6, 0xc6, 0x47, 0x38, 0x59, 0x62, 0xe5, 0xbd}, 0x01 }; 
//EXTERN_C const DECLSPEC_SELECTANY PROPERTYKEY MFPKEY_MediaProtectionSystemContext =  { { 0x636b271d, 0xddc7, 0x49e9, 0xa6, 0xc6, 0x47, 0x38, 0x59, 0x62, 0xe5, 0xbd}, 0x02 }; 
//EXTERN_C const DECLSPEC_SELECTANY PROPERTYKEY MFPKEY_MediaProtectionSystemIdMapping =  { { 0x636b271d, 0xddc7, 0x49e9, 0xa6, 0xc6, 0x47, 0x38, 0x59, 0x62, 0xe5, 0xbd}, 0x03 }; 
//EXTERN_C const DECLSPEC_SELECTANY PROPERTYKEY MFPKEY_MediaProtectionContainerGuid =  { { 0x42af3d7c, 0xcf, 0x4a0f, 0x81, 0xf0, 0xad, 0xf5, 0x24, 0xa5, 0xa5, 0xb5}, 0x1 }; 
//EXTERN_C const DECLSPEC_SELECTANY PROPERTYKEY MFPKEY_MediaProtectionSystemContextsPerTrack =  { { 0x4454b092, 0xd3da, 0x49b0, 0x84, 0x52, 0x68, 0x50, 0xc7, 0xdb, 0x76, 0x4d }, 0x03 }; 
//#endif /* WINAPI_FAMILY_PARTITION(WINAPI_PARTITION_APP) */
//#pragma endregion
//EXTERN_C const DECLSPEC_SELECTANY PROPERTYKEY MFPKEY_HTTP_ByteStream_Download_Mode = { { 0x817f11b7, 0xa982, 0x46ec, 0xa4, 0x49, 0xef, 0x58, 0xae, 0xd5, 0x3c, 0xa8 }, 0x01 }; 
//#endif // (WINVER >= _WIN32_WINNT_WIN8) 
//#if (WINVER >= _WIN32_WINNT_WINTHRESHOLD) 
//EXTERN_C const DECLSPEC_SELECTANY PROPERTYKEY MFPKEY_HTTP_ByteStream_Caching_Mode = { { 0x86a2403e, 0xc78b, 0x44d7, 0x8b, 0xc8, 0xff, 0x72, 0x58, 0x11, 0x75, 0x08}, 0x01 }; 
//EXTERN_C const DECLSPEC_SELECTANY PROPERTYKEY MFPKEY_HTTP_ByteStream_Cache_Limit = { { 0x86a2403e, 0xc78b, 0x44d7, 0x8b, 0xc8, 0xff, 0x72, 0x58, 0x11, 0x75, 0x08}, 0x02 }; 
