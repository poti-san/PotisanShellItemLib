using System.Diagnostics;

using Potisan.Windows.MediaFoundation.ComTypes;

namespace Potisan.Windows.MediaFoundation;

public class MFTopologyNode(object? o) : ComUnknownWrapperWithMFAttribute<IMFTopologyNode>(o)
{
	public static ComResult<MFTopologyNode> CreateNoThrow(MFTopologyType nodeType)
	{
		[DllImport("mfplat.dll")]
		static extern int MFCreateTopologyNode(
			MFTopologyType NodeType,
			out IMFTopologyNode ppNode);

		return IComUnknownWrapper.Wrap<MFTopologyNode>(MFCreateTopologyNode(nodeType, out var x), x);
	}

	public static MFTopologyNode Create(MFTopologyType nodeType)
		=> CreateNoThrow(nodeType).Value;

	public ComResult SetObjectNoThrow(object? obj)
		=> new(_obj.SetObject(obj));

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<object?> ObjectNoThrow
		=> new(_obj.GetObject(out var x), x);

	public object? Object
	{
		get => ObjectNoThrow.Value;
		set => SetObjectNoThrow(value);
	}

	public ComResult SetObjectNoThrow<TWrapper>(TWrapper? obj)
		where TWrapper : IComUnknownWrapper
		=> new(_obj.SetObject(obj?.WrappedObject));

	public ComResult<TWrapper?> GetObjectNoThrow<TWrapper>()
		where TWrapper : IComUnknownWrapper
		=> IComUnknownWrapper.Wrap<TWrapper>(_obj.GetObject(out var x), x)!;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<MFTopologyType> NodeTypeNoThrow
		=> new(_obj.GetNodeType(out var x), x);

	public MFTopologyType NodeType
		=> NodeTypeNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<ulong> TopoNodeIDNoThrow
		=> new(_obj.GetTopoNodeID(out var x), x);

	public ComResult SetTopoNodeIDNoThrow(ulong topoId)
		=> new(_obj.SetTopoNodeID(topoId));

	public ulong TopoNodeID
	{
		get => TopoNodeIDNoThrow.Value;
		set => SetTopoNodeIDNoThrow(value);
	}

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<uint> InputCountNoThrow
		=> new(_obj.GetInputCount(out var x), x);

	public uint InputCount
		=> InputCountNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<uint> OutputCountNoThrow
		=> new(_obj.GetOutputCount(out var x), x);

	public uint OutputCount
		=> OutputCountNoThrow.Value;

	public ComResult ConnectOutputNoThrow(uint outputIndex, MFTopologyNode downstreamNode, uint inputIndexOnDownstreamNode)
		=> new(_obj.ConnectOutput(outputIndex, (IMFTopologyNode)downstreamNode.WrappedObject!, inputIndexOnDownstreamNode));

	public void ConnectOutput(uint outputIndex, MFTopologyNode downstreamNode, uint inputIndexOnDownstreamNode)
		=> ConnectOutputNoThrow(outputIndex, downstreamNode, inputIndexOnDownstreamNode).ThrowIfError();

	public ComResult DisconnectOutputNoThrow(uint outputIndex)
		=> new(_obj.DisconnectOutput(outputIndex));

	public void DisconnectOutput(uint outputIndex)
		=> DisconnectOutputNoThrow(outputIndex).ThrowIfError();

	public ComResult<(MFTopologyNode upstreamNode, uint outputIndexOnUpstreamNode)> GetInputNoThrow(uint inputIndex)
		=> new(_obj.GetInput(inputIndex, out var x1, out var x2), (new(x1), x2));

	public (MFTopologyNode upstreamNode, uint outputIndexOnUpstreamNode) GetInput(uint inputIndex)
		=> GetInputNoThrow(inputIndex).Value;

	public ComResult<(MFTopologyNode downstreamNode, uint outputIndexOnDownstreamNode)> GetOutputNoThrow(uint outputIndex)
		=> new(_obj.GetOutput(outputIndex, out var x1, out var x2), (new(x1), x2));

	public (MFTopologyNode downstreamNode, uint outputIndexOnDownstreamNode) GetOutput(uint outputIndex)
		=> GetOutputNoThrow(outputIndex).Value;

	public ComResult SetOutputPrefTypeNoThrow(uint outputIndex, MFMediaType type)
		=> new(_obj.SetOutputPrefType(outputIndex, (IMFMediaType)type.WrappedObject!));

	public void SetOutputPrefType(uint outputIndex, MFMediaType type)
		=> SetOutputPrefTypeNoThrow(outputIndex, type).ThrowIfError();

	public ComResult<MFMediaType> GetOutputPrefTypeNoThrow(uint outputIndex)
		=> new(_obj.GetOutputPrefType(outputIndex, out var x), new(x));

	public MFMediaType GetOutputPrefType(uint outputIndex)
		=> GetOutputPrefTypeNoThrow(outputIndex).Value;

	public ComResult SetInputPrefTypeNoThrow(uint inputIndex, MFMediaType type)
		=> new(_obj.SetInputPrefType(inputIndex, (IMFMediaType)type.WrappedObject!));

	public void SetInputPrefType(uint inputIndex, MFMediaType type)
		=> SetInputPrefTypeNoThrow(inputIndex, type).ThrowIfError();

	public ComResult<MFMediaType> GetInputPrefTypeNoThrow(uint inputIndex)
		=> new(_obj.GetInputPrefType(inputIndex, out var x), new(x));

	public MFMediaType GetInputPrefType(uint inputIndex)
		=> GetInputPrefTypeNoThrow(inputIndex).Value;

	public ComResult CloneFromNoThrow(MFTopologyNode node)
		=> new(_obj.CloneFrom(node._obj));

	public void CloneFrom(MFTopologyNode node)
		=> CloneFromNoThrow(node).ThrowIfError();

	public ComResult<MFTopologyNode> CloneNoThrow()
	{
		MFTopologyType nodeType;
		{
			var cr = NodeTypeNoThrow;
			if (!cr) return new(cr.HResult, new(null));
			nodeType = cr.ValueUnchecked;
		}

		MFTopologyNode newNode;
		{
			var cr = CreateNoThrow(nodeType);
			if (!cr) return new(cr.HResult, new(null));
			newNode = cr.ValueUnchecked;
		}

		{
			var cr = newNode.CloneFromNoThrow(this);
			return new(cr.HResult, cr ? newNode : new(null));
		}
	}

	public MFTopologyNode Clone()
		=> CloneNoThrow().Value;

	// TODO 属性
}

//public enum MF_TOPONODE_FLUSH_MODE
//{
//	MF_TOPONODE_FLUSH_ALWAYS = 0,
//	MF_TOPONODE_FLUSH_SEEK = (MF_TOPONODE_FLUSH_ALWAYS + 1),
//	MF_TOPONODE_FLUSH_NEVER = (MF_TOPONODE_FLUSH_SEEK + 1)
//}

//EXTERN_GUID(MF_TOPONODE_FLUSH, 0x494bbce8, 0xb031,  0x4e38,  0x97, 0xc4, 0xd5, 0x42, 0x2d, 0xd6, 0x18, 0xdc);

//public enum MF_TOPONODE_DRAIN_MODE
//{
//	MF_TOPONODE_DRAIN_DEFAULT = 0,
//	MF_TOPONODE_DRAIN_ALWAYS = (MF_TOPONODE_DRAIN_DEFAULT + 1),
//	MF_TOPONODE_DRAIN_NEVER = (MF_TOPONODE_DRAIN_ALWAYS + 1)
//}

//EXTERN_GUID(MF_TOPONODE_DRAIN, 0x494bbce9, 0xb031,  0x4e38,  0x97, 0xc4, 0xd5, 0x42, 0x2d, 0xd6, 0x18, 0xdc);
//EXTERN_GUID(MF_TOPONODE_D3DAWARE, 0x494bbced, 0xb031,  0x4e38,  0x97, 0xc4, 0xd5, 0x42, 0x2d, 0xd6, 0x18, 0xdc);
//EXTERN_GUID(MF_TOPOLOGY_RESOLUTION_STATUS, 0x494bbcde, 0xb031,  0x4e38,  0x97, 0xc4, 0xd5, 0x42, 0x2d, 0xd6, 0x18, 0xdc);
//EXTERN_GUID(MF_TOPONODE_ERRORCODE, 0x494bbcee, 0xb031,  0x4e38,  0x97, 0xc4, 0xd5, 0x42, 0x2d, 0xd6, 0x18, 0xdc);
//EXTERN_GUID(MF_TOPONODE_CONNECT_METHOD, 0x494bbcf1, 0xb031,  0x4e38,  0x97, 0xc4, 0xd5, 0x42, 0x2d, 0xd6, 0x18, 0xdc);
//EXTERN_GUID(MF_TOPONODE_LOCKED, 0x494bbcf7, 0xb031,  0x4e38,  0x97, 0xc4, 0xd5, 0x42, 0x2d, 0xd6, 0x18, 0xdc);
//EXTERN_GUID(MF_TOPONODE_WORKQUEUE_ID, 0x494bbcf8, 0xb031,  0x4e38,  0x97, 0xc4, 0xd5, 0x42, 0x2d, 0xd6, 0x18, 0xdc);
//EXTERN_GUID(MF_TOPONODE_WORKQUEUE_MMCSS_CLASS, 0x494bbcf9, 0xb031,  0x4e38,  0x97, 0xc4, 0xd5, 0x42, 0x2d, 0xd6, 0x18, 0xdc);
//EXTERN_GUID(MF_TOPONODE_DECRYPTOR, 0x494bbcfa, 0xb031,  0x4e38,  0x97, 0xc4, 0xd5, 0x42, 0x2d, 0xd6, 0x18, 0xdc);
//EXTERN_GUID(MF_TOPONODE_DISCARDABLE, 0x494bbcfb, 0xb031,  0x4e38,  0x97, 0xc4, 0xd5, 0x42, 0x2d, 0xd6, 0x18, 0xdc);
//EXTERN_GUID(MF_TOPONODE_ERROR_MAJORTYPE, 0x494bbcfd, 0xb031,  0x4e38,  0x97, 0xc4, 0xd5, 0x42, 0x2d, 0xd6, 0x18, 0xdc);
//EXTERN_GUID(MF_TOPONODE_ERROR_SUBTYPE, 0x494bbcfe, 0xb031,  0x4e38,  0x97, 0xc4, 0xd5, 0x42, 0x2d, 0xd6, 0x18, 0xdc);
//EXTERN_GUID(MF_TOPONODE_WORKQUEUE_MMCSS_TASKID, 0x494bbcff, 0xb031,  0x4e38,  0x97, 0xc4, 0xd5, 0x42, 0x2d, 0xd6, 0x18, 0xdc);
//EXTERN_GUID(MF_TOPONODE_WORKQUEUE_MMCSS_PRIORITY, 0x5001f840, 0x2816, 0x48f4, 0x93, 0x64, 0xad, 0x1e, 0xf6, 0x61, 0xa1, 0x23);
//EXTERN_GUID(MF_TOPONODE_WORKQUEUE_ITEM_PRIORITY, 0xa1ff99be, 0x5e97, 0x4a53, 0xb4, 0x94, 0x56, 0x8c, 0x64, 0x2c, 0x0f, 0xf3);
//EXTERN_GUID(MF_TOPONODE_MARKIN_HERE, 0x494bbd00, 0xb031,  0x4e38,  0x97, 0xc4, 0xd5, 0x42, 0x2d, 0xd6, 0x18, 0xdc);
//EXTERN_GUID(MF_TOPONODE_MARKOUT_HERE, 0x494bbd01, 0xb031,  0x4e38,  0x97, 0xc4, 0xd5, 0x42, 0x2d, 0xd6, 0x18, 0xdc);
//EXTERN_GUID(MF_TOPONODE_DECODER, 0x494bbd02, 0xb031,  0x4e38,  0x97, 0xc4, 0xd5, 0x42, 0x2d, 0xd6, 0x18, 0xdc);
//EXTERN_GUID(MF_TOPONODE_MEDIASTART, 0x835c58ea, 0xe075, 0x4bc7, 0xbc, 0xba, 0x4d, 0xe0, 0x00, 0xdf, 0x9a, 0xe6);
//EXTERN_GUID(MF_TOPONODE_MEDIASTOP, 0x835c58eb, 0xe075, 0x4bc7, 0xbc, 0xba, 0x4d, 0xe0, 0x00, 0xdf, 0x9a, 0xe6);
//EXTERN_GUID(MF_TOPONODE_SOURCE, 0x835c58ec, 0xe075, 0x4bc7, 0xbc, 0xba, 0x4d, 0xe0, 0x00, 0xdf, 0x9a, 0xe6);
//EXTERN_GUID(MF_TOPONODE_PRESENTATION_DESCRIPTOR, 0x835c58ed, 0xe075, 0x4bc7, 0xbc, 0xba, 0x4d, 0xe0, 0x00, 0xdf, 0x9a, 0xe6);
//EXTERN_GUID(MF_TOPONODE_STREAM_DESCRIPTOR, 0x835c58ee, 0xe075, 0x4bc7, 0xbc, 0xba, 0x4d, 0xe0, 0x00, 0xdf, 0x9a, 0xe6);
//EXTERN_GUID(MF_TOPONODE_SEQUENCE_ELEMENTID, 0x835c58ef, 0xe075, 0x4bc7, 0xbc, 0xba, 0x4d, 0xe0, 0x00, 0xdf, 0x9a, 0xe6);
//EXTERN_GUID(MF_TOPONODE_TRANSFORM_OBJECTID, 0x88dcc0c9, 0x293e, 0x4e8b, 0x9a, 0xeb, 0xa, 0xd6, 0x4c, 0xc0, 0x16, 0xb0);
//EXTERN_GUID(MF_TOPONODE_STREAMID, 0x14932f9b, 0x9087, 0x4bb4, 0x84, 0x12, 0x51, 0x67, 0x14, 0x5c, 0xbe, 0x04);
//EXTERN_GUID(MF_TOPONODE_NOSHUTDOWN_ON_REMOVE, 0x14932f9c, 0x9087, 0x4bb4, 0x84, 0x12, 0x51, 0x67, 0x14, 0x5c, 0xbe, 0x04);
//EXTERN_GUID(MF_TOPONODE_RATELESS, 0x14932f9d, 0x9087, 0x4bb4, 0x84, 0x12, 0x51, 0x67, 0x14, 0x5c, 0xbe, 0x04);
//EXTERN_GUID(MF_TOPONODE_DISABLE_PREROLL, 0x14932f9e, 0x9087, 0x4bb4, 0x84, 0x12, 0x51, 0x67, 0x14, 0x5c, 0xbe, 0x04);
//EXTERN_GUID(MF_TOPONODE_PRIMARYOUTPUT, 0x6304ef99, 0x16b2, 0x4ebe, 0x9d, 0x67, 0xe4, 0xc5, 0x39, 0xb3, 0xa2, 0x59);

//STDAPI MFGetTopoNodeCurrentType(
//	IMFTopologyNode* pNode,
//	uint dwStreamIndex,
//	[MarshalAs(UnmanagedType.Bool)] bool fOutput,
//	_Outptr_ IMFMediaType** ppType);