using System.Collections;

using Potisan.Windows.Network.ComTypes;

namespace Potisan.Windows.Network;

/// <summary>
/// 任意の接続の列挙機能。
/// </summary>
/// <param name="o">RCWオブジェクト。</param>
/// <remarks>
/// <c>IEnumNetSharingPublicConnection</c> COMインターフェイスのラッパーです。
/// </remarks>
public sealed class NetSharingEveryConnectionEnumerable(object? o) : ComUnknownWrapperBase<IEnumNetSharingEveryConnection>(o), IEnumerable<NetConnection>, ICloneable
{
	public IEnumerator<NetConnection> GetEnumerator()
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

	public ComResult<NetConnectionEnumerable> CloneNoThrow()
		=> new(_obj.Clone(out var x), new(x));

	public NetConnectionEnumerable Clone()
		=> CloneNoThrow().Value;

	object ICloneable.Clone()
		=> Clone();
}
