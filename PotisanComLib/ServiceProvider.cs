namespace Potisan.Windows.Com;

/// <summary>
/// サービスプロバイダー。他のCOMオブジェクトの作成に使用されます。
/// </summary>
/// <param name="o">RCWオブジェクト。</param>
/// <remarks><c>IServiceProvider</c> COMインターフェイスのラッパーです。</remarks>
public class ComServiceProvider(object? o) : ComUnknownWrapperBase<ComTypes.IServiceProvider>(o)
{
	public ComResult<object> QueryServiceNoThrow(in Guid serviceGuid, in Guid iid)
		=> new(_obj.QueryService(serviceGuid, iid, out var x), x!);

	public object QueryService(in Guid serviceGuid, in Guid iid)
		=> QueryServiceNoThrow(serviceGuid, iid).Value;

	public ComResult<TWrapper> QueryServiceNoThrow<TWrapper, TInterface>(in Guid serviceGuid)
		where TWrapper : IComUnknownWrapper
		=> IComUnknownWrapper.Wrap<TWrapper>(QueryServiceNoThrow(serviceGuid, typeof(TInterface).GUID));

	public TWrapper QueryService<TWrapper, TInterface>(in Guid serviceGuid)
		where TWrapper : IComUnknownWrapper
		=> QueryServiceNoThrow<TWrapper, TInterface>(serviceGuid).Value;
}
