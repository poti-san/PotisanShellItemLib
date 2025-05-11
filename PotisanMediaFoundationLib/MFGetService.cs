using Potisan.Windows.MediaFoundation.ComTypes;

namespace Potisan.Windows.MediaFoundation;

public class MFGetService(object? o) : ComUnknownWrapperBase<IMFGetService>(o)
{
	public ComResult<object> GetServiceNoThrow(in Guid serviceGuid, in Guid iid)
		=> new(_obj.GetService(serviceGuid, iid, out var x), x!);

	public object GetService(in Guid serviceGuid, in Guid iid)
		=> GetServiceNoThrow(serviceGuid, iid).Value;

	// TODO 個別のサービス
	// https://learn.microsoft.com/ja-jp/windows/win32/medfound/service-interfaces
}
