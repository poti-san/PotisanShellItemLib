using Potisan.Windows.Com.ComTypes;

namespace Potisan.Windows.Com;

/// <summary>
/// バインドコンテキスト。
/// </summary>
/// <param name="o">RCWオブジェクト。</param>
/// <remarks>
/// <para><c>IBindCtx</c> COMインターフェイスのラッパーです。</para>
/// </remarks>
public sealed class BindCtx(object? o) : ComUnknownWrapperBase<IBindCtx>(o)
{
	public ComResult RegisterObjectBoundNoThrow(object unk)
		=> new(_obj.RegisterObjectBound(unk));

	public void RegisterObjectBound(object unk)
		=> RegisterObjectBoundNoThrow(unk).ThrowIfError();

	public ComResult RevokeObjectBoundNoThrow(object unk)
		=> new(_obj.RevokeObjectBound(unk));

	public void RevokeObjectBound(object unk)
		=> RevokeObjectBoundNoThrow(unk).ThrowIfError();

	public ComResult ReleaseBoundObjectsNoThrow()
		=> new(_obj.ReleaseBoundObjects());

	public void ReleaseBoundObjects()
		=> ReleaseBoundObjectsNoThrow().ThrowIfError();

	// TODO
	//[PreserveSig]
	//int SetBindOptions(
	//	in BIND_OPTS pbindopts);

	//[PreserveSig]
	//int GetBindOptions(
	//	out BIND_OPTS pbindopts);

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<RunningObjectTable> RunningObjectTableNoThrow
		=> new(_obj.GetRunningObjectTable(out var x), new(x));

	public RunningObjectTable RunningObjectTable
		=> RunningObjectTableNoThrow.Value;

	public ComResult RegisterObjectParamNoThrow(string key, object unk)
		=> new(_obj.RegisterObjectParam(key, unk));

	public void RegisterObjectParam(string key, object unk)
		=> RegisterObjectParamNoThrow(key, unk).ThrowIfError();

	public ComResult<object> GetObjectParamNoThrow(string key)
		=> new(_obj.GetObjectParam(key, out var x), x!);

	public object GetObjectParam(string key)
		=> GetObjectParamNoThrow(key);

	public ComResult<ComStringEnumerable> GetObjectKeyEnumerableNoThrow()
		=> new(_obj.EnumObjectParam(out var x), new(x));

	public ComStringEnumerable GetObjectKeyEnumerable()
		=> GetObjectKeyEnumerableNoThrow().Value;

	public ComResult RevokeObjectParamNoThrow(string key)
		=> new(_obj.RevokeObjectParam(key));

	public void RevokeObjectParam(string key)
		=> RevokeObjectParamNoThrow(key).ThrowIfError();

	public static ComResult<BindCtx> CreateNoThrow()
	{
		[DllImport("ole32.dll")]
		static extern int CreateBindCtx(uint reserved, out IBindCtx ppbc);
		return new(CreateBindCtx(0, out var x), new(x));
	}

	public static BindCtx Create()
		=> CreateNoThrow().Value;
}