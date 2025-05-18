using Potisan.Windows.Com.ComTypes;

namespace Potisan.Windows.Text.Tsf.ComTypes;

[ComImport]
[Guid("7dcf57ac-18ad-438b-824d-979bffb74b7c")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface ITfCompartmentMgr
{
	[PreserveSig]
	int GetCompartment(
		in Guid rguid,
		out ITfCompartment ppcomp);

	[PreserveSig]
	int ClearCompartment(
		TFClientID tid,
		in Guid rguid);

	[PreserveSig]
	int EnumCompartments(
		out IEnumGUID ppEnum);
}