using System.Collections;

using Potisan.Windows.Network.ComTypes;

namespace Potisan.Windows.Network;

/// <summary>
/// ポート割り当ての列挙機能。
/// </summary>
/// <param name="o">RCWオブジェクト。</param>
/// <remarks>
/// <c>IEnumNetSharingPortMapping</c> COMインターフェイスのラッパーです。
/// </remarks>
public sealed class NetSharingPortMappingEnumerable(object? o) : ComUnknownWrapperBase<IEnumNetSharingPortMapping>(o), IEnumerable<NetSharingPortMapping>, ICloneable
{
	public IEnumerator<NetSharingPortMapping> GetEnumerator()
	{
		for (; ; )
		{
			var hr = _obj.Next(1, out var x, out _);
			if (hr == 1) break;
			Marshal.ThrowExceptionForHR(hr);
			yield return new(x);
		}
	}

	IEnumerator IEnumerable.GetEnumerator()
		=> GetEnumerator();

	public ComResult<NetSharingPortMappingEnumerable> CloneNoThrow()
		=> new(_obj.Clone(out var x), new(x));

	public NetSharingPortMappingEnumerable Clone()
		=> CloneNoThrow().Value;

	object ICloneable.Clone()
		=> Clone();
}