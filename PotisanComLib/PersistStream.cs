namespace Potisan.Windows.Com;

using ComTypes;

/// <summary>
/// ストリームによる永続化。IPersistStream COMインターフェイスのラッパーです。
/// </summary>
/// <param name="o">RCWオブジェクト。</param>
public class PersistStream(object? o) : ComUnknownWrapperBase<IPersistStream>(o)
{
	public ComResult<Guid> ClassIDNoThrow
		=> new(_obj.GetClassID(out var x), x);

	public Guid ClassID
		=> ClassIDNoThrow.Value;

	public ComResult<bool> IsDirtyNoThrow
		=> ComResult.HRSuccess(_obj.IsDirty());

	public bool IsDiry
		=> IsDirtyNoThrow.Value;

	public ComResult LoadNoThrow(ComStream stream)
		=> new(_obj.Load((stream.WrappedObject as IStream)!));

	public void Load(ComStream stream)
		=> LoadNoThrow(stream).ThrowIfError();

	public ComResult SaveNoThrow(ComStream stream, bool clearDirty)
		=> new(_obj.Save((stream.WrappedObject as IStream)!, clearDirty));

	public void Save(ComStream stream, bool clearDirty)
		=> SaveNoThrow(stream, clearDirty).ThrowIfError();
}