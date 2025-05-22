using System.Diagnostics.CodeAnalysis;

namespace Potisan.Windows.Com;

/// <summary>
/// <see cref="ComTypes.IUnknown"/>の便利機能。
/// </summary>
public static class ComUnknownHelper
{
	/// <summary>
	/// COMインターフェイスのout IUnknownでnullを渡したい場合に使用します。
	/// </summary>
	/// <param name="target"></param>
	public struct IUnknownPointer(object target) : IDisposable
	{
		private nint _p = Marshal.GetIUnknownForObject(target);
		public readonly nint Pointer => _p;

		public void Dispose()
		{
			if (_p == 0) return;
			Marshal.Release(_p);
			_p = 0;
		}

		[UnscopedRef]
		public ref nint RefPointer
		{
			get
			{
				if (_p != 0)
				{
					Marshal.Release(_p);
					_p = 0;
				}
				return ref _p;
			}
		}

		public readonly object Target => Marshal.GetObjectForIUnknown(_p);
	}
}
