namespace Potisan.Windows.Com;

using ComTypes;

/// <summary>
/// ファイルによる永続化。IPersistFile COMインターフェイスのラッパーです。
/// </summary>
/// <param name="o">RCWインスタンス。</param>
public class PersistFile(object? o) : ComUnknownWrapperBase<IPersistFile>(o)
{
	public ComResult<Guid> ClassIDNoThrow
		=> new(_obj.GetClassID(out var x), x);

	public Guid ClassID
		=> ClassIDNoThrow.Value;

	public ComResult<bool> IsDirtyNoThrow
		=> ComResult.HRSuccess(_obj.IsDirty());

	public bool IsDiry
		=> IsDirtyNoThrow.Value;

	public ComResult LoadNoThrow(string filename, ComStorageMode mode)
		=> new(_obj.Load(filename, (uint)mode));

	public void Load(string filename, ComStorageMode mode)
		=> LoadNoThrow(filename, mode).ThrowIfError();

	public ComResult SaveNoThrow(string filename, bool remembers)
		=> new(_obj.Save(filename, remembers));

	public void Save(string filename, bool remembers)
		=> SaveNoThrow(filename, remembers).ThrowIfError();

	public ComResult SaveCompletedNoThrow(string filename)
		=> new(_obj.SaveCompleted(filename));

	public void SaveCompleted(string filename)
		=> SaveCompletedNoThrow(filename).ThrowIfError();

	public ComResult<string> CurrentFileNoThrow
		=> new(_obj.GetCurFile(out var x), x!);

	public string CurrentFile
		=> CurrentFileNoThrow.Value;
}