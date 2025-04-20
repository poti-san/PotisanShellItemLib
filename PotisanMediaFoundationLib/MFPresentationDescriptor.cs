using System.Collections.Immutable;
using System.Diagnostics;

using Potisan.Windows.MediaFoundation.ComTypes;

namespace Potisan.Windows.MediaFoundation;

/// <summary>
/// MFプレゼンテーション記述子。IMFPresentationDescriptor COMインターフェイスのラッパーです。
/// </summary>
public class MFPresentationDescriptor(object? o) : ComUnknownWrapperWithMFAttribute<IMFPresentationDescriptor>(o), IMFWithAttributes
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<uint> StreamDescriptorCountNoThrow
		=> new(_obj.GetStreamDescriptorCount(out var x), x);

	public uint StreamDescriptorCount
		=> StreamDescriptorCountNoThrow.Value;

	public ComResult<(MFStreamDescriptor Descriptor, bool Selected)> GetStreamDescriptorAndSelectedByIndexNoThrow(uint index)
		=> new(_obj.GetStreamDescriptorByIndex(index, out var x1, out var x2), (new(x2), x1));

	public (MFStreamDescriptor Descriptor, bool Selected) GetStreamDescriptorAndSelectedByIndex(uint index)
		=> GetStreamDescriptorAndSelectedByIndexNoThrow(index).Value;

	public ComResult<MFStreamDescriptor> GetStreamDescriptorByIndexNoThrow(uint index)
		=> GetStreamDescriptorAndSelectedByIndexNoThrow(index) switch
		{
			{ Succeeded: true, HResult: var hr, ValueUnchecked: var value } => new(hr, value.Descriptor),
			{ HResult: var hr } => new(hr, new(null)),
		};

	public MFStreamDescriptor GetStreamDescriptorByIndex(uint index)
		=> GetStreamDescriptorAndSelectedByIndexNoThrow(index).Value.Descriptor;

	public IEnumerable<(MFStreamDescriptor Descriptor, bool Selected)> EnumerateStreamDescriptorsAndSelecteds()
	{
		var c = StreamDescriptorCount;
		for (uint i = 0; i < c; i++)
			yield return GetStreamDescriptorAndSelectedByIndex(i);
	}

	public ImmutableArray<(MFStreamDescriptor Descriptor, bool Selected)> StreamDescriptorsAndDescriptors
		=> [.. EnumerateStreamDescriptorsAndSelecteds()];

	public IEnumerable<MFStreamDescriptor> EnumerateStreamDescriptors()
		=> EnumerateStreamDescriptorsAndSelecteds().Select(pair => pair.Descriptor);

	public ImmutableArray<MFStreamDescriptor> StreamDescriptors
		=> [.. EnumerateStreamDescriptors()];

	public ComResult SelectStreamNoThrow(uint index)
		=> new(_obj.SelectStream(index));

	public void SelectStream(uint index)
		=> SelectStreamNoThrow(index).ThrowIfError();

	public ComResult DeselectStreamNoThrow(uint index)
		=> new(_obj.DeselectStream(index));

	public void DeselectStream(uint index)
		=> DeselectStreamNoThrow(index).ThrowIfError();

	public ComResult<MFPresentationDescriptor> CloneNoThrow()
		=> new(_obj.Clone(out var x), new(x));

	public MFPresentationDescriptor Clone()
		=> CloneNoThrow().Value;
}

// TODO
//STDAPI MFCreatePresentationDescriptor(
//	uint cStreamDescriptors,
//	_In_reads_opt_(cStreamDescriptors ) IMFStreamDescriptor** apStreamDescriptors,
//	_Outptr_ IMFPresentationDescriptor** ppPresentationDescriptor

//	);
//STDAPI MFRequireProtectedEnvironment(
//     _In_ IMFPresentationDescriptor* pPresentationDescriptor
//     );

//STDAPI MFSerializePresentationDescriptor(
//    _In_ IMFPresentationDescriptor * pPD,
//    out uint  pcbData,
//    _Outptr_result_bytebuffer_to_(*pcbData, *pcbData) BYTE ** ppbData);

//STDAPI MFDeserializePresentationDescriptor(
//    uint cbData,
//    _In_reads_( cbData ) BYTE * pbData,
//    _Outptr_ IMFPresentationDescriptor ** ppPD);
