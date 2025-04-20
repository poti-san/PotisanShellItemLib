using System.Diagnostics;

using Potisan.Windows.MediaFoundation.ComTypes;

namespace Potisan.Windows.MediaFoundation;

public class MFTopology(object? o) : ComUnknownWrapperWithMFAttribute<IMFTopology>(o)
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<ulong> TopologyIDNoThrow
		=> new(_obj.GetTopologyID(out var x), x);

	public ulong TopologyID
		=> TopologyIDNoThrow.Value;

	public ComResult AddNodeNoThrow(MFTopologyNode node)
		=> new(_obj.AddNode((IMFTopologyNode)node.WrappedObject!));

	public void AddNode(MFTopologyNode node)
		=> AddNodeNoThrow(node).ThrowIfError();

	public ComResult RemoveNodeNoThrow(MFTopologyNode node)
		=> new(_obj.RemoveNode((IMFTopologyNode)node.WrappedObject!));

	public void RemoveNode(MFTopologyNode node)
		=> RemoveNodeNoThrow(node).ThrowIfError();

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<ushort> NodeCountNoThrow
		=> new(_obj.GetNodeCount(out var x), x);

	public ushort NodeCount
		=> NodeCountNoThrow.Value;

	public ComResult<MFTopologyNode> GetNodeAtNoThrow(ushort index)
		=> new(_obj.GetNode(index, out var x), new(x));

	public MFTopologyNode GetNodeAt(ushort index)
		=> GetNodeAtNoThrow(index).Value;

	public ComResult ClearNoThrow()
		=> new(_obj.Clear());

	public void Clear()
		=> ClearNoThrow().ThrowIfError();

	public ComResult CloneFromNoThrow(MFTopology topology)
		=> new(_obj.CloneFrom(topology._obj));

	public void CloneFrom(MFTopology topology)
		=> CloneFromNoThrow(topology).ThrowIfError();

	public ComResult<MFTopologyNode> GetNodeByIDNoThrow(ulong topoId)
		=> new(_obj.GetNodeByID(topoId, out var x), new(x));

	public MFTopologyNode GetNodeByID(ulong topoId)
		=> GetNodeByIDNoThrow(topoId).Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<MFCollection> SourceNodeCollectionNoThrow
		=> new(_obj.GetSourceNodeCollection(out var x), new(x));

	public MFCollection SourceNodeCollection
		=> SourceNodeCollectionNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<MFCollection> OutputNodeCollectionNoThrow
		=> new(_obj.GetOutputNodeCollection(out var x), new(x));

	public MFCollection OutputNodeCollection
		=> OutputNodeCollectionNoThrow.Value;

	// TODO 属性
}

//EXTERN_GUID(MF_TOPOLOGY_PROJECTSTART, 0x7ed3f802, 0x86bb, 0x4b3f, 0xb7, 0xe4, 0x7c, 0xb4, 0x3a, 0xfd, 0x4b, 0x80);
//	EXTERN_GUID(MF_TOPOLOGY_PROJECTSTOP, 0x7ed3f803, 0x86bb, 0x4b3f, 0xb7, 0xe4, 0x7c, 0xb4, 0x3a, 0xfd, 0x4b, 0x80);
//	EXTERN_GUID(MF_TOPOLOGY_NO_MARKIN_MARKOUT, 0x7ed3f804, 0x86bb, 0x4b3f, 0xb7, 0xe4, 0x7c, 0xb4, 0x3a, 0xfd, 0x4b, 0x80);

//typedef
//enum MFTOPOLOGY_DXVA_MODE
//{
//	MFTOPOLOGY_DXVA_DEFAULT = 0,
//	MFTOPOLOGY_DXVA_NONE = 1,
//	MFTOPOLOGY_DXVA_FULL = 2
//}
//MFTOPOLOGY_DXVA_MODE;

//public static Guid TOPOLOGY_DXVA_MODE => new(0x1e8d34f6, 0xf5ab, 0x4e23, 0xbb, 0x88, 0x87, 0x4a, 0xa3, 0xa1, 0xa7, 0x4d);
//public static Guid TOPOLOGY_ENABLE_XVP_FOR_PLAYBACK => new(0x1967731f, 0xcd78, 0x42fc, 0xb0, 0x26, 0x9, 0x92, 0xa5, 0x6e, 0x56, 0x93);
//public static Guid TOPOLOGY_STATIC_PLAYBACK_OPTIMIZATIONS => new(0xb86cac42, 0x41a6, 0x4b79, 0x89, 0x7a, 0x1a, 0xb0, 0xe5, 0x2b, 0x4a, 0x1b);
//public static Guid TOPOLOGY_PLAYBACK_MAX_DIMS => new(0x5715cf19, 0x5768, 0x44aa, 0xad, 0x6e, 0x87, 0x21, 0xf1, 0xb0, 0xf9, 0xbb);

//typedef
//enum MFTOPOLOGY_HARDWARE_MODE
//{
//	MFTOPOLOGY_HWMODE_SOFTWARE_ONLY = 0,
//	MFTOPOLOGY_HWMODE_USE_HARDWARE = 1,
//	MFTOPOLOGY_HWMODE_USE_ONLY_HARDWARE = 2
//}
//MFTOPOLOGY_HARDWARE_MODE;

//public static Guid TOPOLOGY_HARDWARE_MODE => new(0xd2d362fd, 0x4e4f, 0x4191, 0xa5, 0x79, 0xc6, 0x18, 0xb6, 0x67, 0x6, 0xaf);
//public static Guid TOPOLOGY_PLAYBACK_FRAMERATE => new(0xc164737a, 0xc2b1, 0x4553, 0x83, 0xbb, 0x5a, 0x52, 0x60, 0x72, 0x44, 0x8f);
//public static Guid TOPOLOGY_DYNAMIC_CHANGE_NOT_ALLOWED => new(0xd529950b, 0xd484, 0x4527, 0xa9, 0xcd, 0xb1, 0x90, 0x95, 0x32, 0xb5, 0xb0);
//public static Guid TOPOLOGY_ENUMERATE_SOURCE_TYPES => new(0x6248c36d, 0x5d0b, 0x4f40, 0xa0, 0xbb, 0xb0, 0xb3, 0x05, 0xf7, 0x76, 0x98);
//EXTERN_GUID(MF_TOPOLOGY_START_TIME_ON_PRESENTATION_SWITCH, 0xc8cc113f, 0x7951, 0x4548, 0xaa, 0xd6, 0x9e, 0xd6, 0x20, 0x2e, 0x62, 0xb3);

//EXTERN_GUID(MF_DISABLE_LOCALLY_REGISTERED_PLUGINS, 0x66b16da9, 0xadd4, 0x47e0, 0xa1, 0x6b, 0x5a, 0xf1, 0xfb, 0x48, 0x36, 0x34);
//EXTERN_GUID(MF_LOCAL_PLUGIN_CONTROL_POLICY, 0xd91b0085, 0xc86d, 0x4f81, 0x88, 0x22, 0x8c, 0x68, 0xe1, 0xd7, 0xfa, 0x04);

//STDAPI MFCreateTopology(
//	_Outptr_ IMFTopology ** ppTopo );

public enum MFTopologyType : uint
{
	OutputNode = 0,
	SourceStreamNode,
	TransformNode,
	TeeNode,
}
