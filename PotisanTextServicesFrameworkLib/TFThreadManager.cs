using System.Collections.Immutable;
using System.Diagnostics;

using Potisan.Windows.Text.Tsf.ComTypes;

namespace Potisan.Windows.Text.Tsf;

public class TFThreadManager(object? o) : ComUnknownWrapperBase<ITfThreadMgr>(o)
{
	public static ComResult<TFThreadManager> CreateNoThrow()
	{
		// 分かりにくい原因なのでアサートを発生させます。
		Debug.Assert(Thread.CurrentThread.GetApartmentState() == ApartmentState.STA,
			"シングルスレッドモデルでのみ作成できます。");

		Guid CLSID_TF_ThreadMgr = new("529A9E6B-6587-4F23-AB9E-9C7D683E3C50");
		return ComHelper.CreateInstanceNoThrow<TFThreadManager>(CLSID_TF_ThreadMgr, ComClassContext.InProcServer);
	}

	public static TFThreadManager Create()
		=> CreateNoThrow().Value;

	public ComResult<TFClientID> ActivateNoThrow()
		=> new(_obj.Activate(out var x), x);

	public TFClientID Activate()
		=> ActivateNoThrow().Value;

	public ComResult DeactivateNoThrow()
		=> new(_obj.Deactivate());

	public void Deactivate()
		=> DeactivateNoThrow().ThrowIfError();

	public ActivateScopeType ActivateScope()
	{
		return new(this, Activate());
	}

	public sealed class ActivateScopeType : IDisposable
	{
		public TFThreadManager Manager { get; }
		public TFClientID ClientID { get; }
		private bool _disposed;

		internal ActivateScopeType(TFThreadManager manager, TFClientID id)
		{
			Manager = manager;
			ClientID = id;
		}

		~ActivateScopeType()
		{
			Dispose();
		}

		public void Dispose()
		{
			if (_disposed) return;
			_disposed = true;
			Manager.Deactivate();
			GC.SuppressFinalize(this);
		}
	}

	//[PreserveSig]
	//int CreateDocumentMgr(
	//	out ITfDocumentMgr ppdim);

	//[PreserveSig]
	//int EnumDocumentMgrs(
	//	out IEnumTfDocumentMgrs ppEnum);

	//[PreserveSig]
	//int GetFocus(
	//	out ITfDocumentMgr ppdimFocus);

	//[PreserveSig]
	//int SetFocus(
	//	ITfDocumentMgr pdimFocus);

	//[PreserveSig]
	//int AssociateFocus(
	//	nint hwnd,
	//	ITfDocumentMgr pdimNew,
	//	out ITfDocumentMgr ppdimPrev);

	//[PreserveSig]
	//int IsThreadFocus(
	//	[MarshalAs(UnmanagedType.Bool)] out bool pfThreadFocus);

	//[PreserveSig]
	//int GetFunctionProvider(
	//	in Guid clsid,
	//	out ITfFunctionProvider ppFuncProv);

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<TFFunctionProviderEnumerable> FunctionProviderEnumerableNoThrow
		=> new(_obj.EnumFunctionProviders(out var x), new(x));

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public TFFunctionProviderEnumerable FunctionProviderEnumerable
		=> FunctionProviderEnumerableNoThrow.Value;

	public ImmutableArray<TFFunctionProvider> FunctionProviders
		=> [.. FunctionProviderEnumerable];

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<TFCompartmentManager> GlobalCompartmentNoThrow
		=> new(_obj.GetGlobalCompartment(out var x), new(x));

	public TFCompartmentManager GlobalCompartment
		=> GlobalCompartmentNoThrow.Value;
}
