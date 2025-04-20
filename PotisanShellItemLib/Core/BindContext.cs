using PotisanShellItemLib.Core.ComTypes;

namespace PotisanShellItemLib.Core;

public sealed class BindContext : IComUnknownWrapper
{
	private readonly IBindCtx _obj;

	/// <summary>
	/// RCWインスタンスをラップします。
	/// </summary>
	/// <param name="o">RCWインスタンス。</param>
	public BindContext(object? o)
	{
		_obj = o == null ? null! : (o as IBindCtx ?? throw new InvalidCastException())!;
	}

	/// <inheritdoc/>
	public object? WrappedObject => _obj;

	/// <inheritdoc/>
	public void Dispose()
	{
		if (_obj != null)
			Marshal.FinalReleaseComObject(_obj);
		GC.SuppressFinalize(this);
	}


	public ComResult RegisterObjectBoundNoThrow(object unk) => new(_obj.RegisterObjectBound(unk));
	public void RegisterObjectBound(object unk) => RegisterObjectBoundNoThrow(unk).ThrowIfError();

	public ComResult RevokeObjectBoundNoThrow(object unk) => new(_obj.RevokeObjectBound(unk));
	public void RevokeObjectBound(object unk) => RegisterObjectBoundNoThrow(unk).ThrowIfError();

	public ComResult ReleaseBoundObjectsNoThrow() => new(_obj.ReleaseBoundObjects());
	public void ReleaseBoundObjects() => ReleaseBoundObjectsNoThrow().ThrowIfError();

	// TODO
	//[PreserveSig]
	//int SetBindOptions(
	//	in BIND_OPTS pbindopts);

	//[PreserveSig]
	//int GetBindOptions(
	//	out BIND_OPTS pbindopts);

	//[PreserveSig]
	//int GetRunningObjectTable(
	//	out IRunningObjectTable pprot);

	//[PreserveSig]
	//int RegisterObjectParam(
	//	[MarshalAs(UnmanagedType.LPWStr)] string pszKey,
	//	[MarshalAs(UnmanagedType.IUnknown)] out object? punk);

	//[PreserveSig]
	//int GetObjectParam(
	//	[MarshalAs(UnmanagedType.LPWStr)] string pszKey,
	//	[MarshalAs(UnmanagedType.IUnknown)] out object? ppunk);

	//[PreserveSig]
	//int EnumObjectParam(
	//	out IEnumString? ppenum);

	//[PreserveSig]
	//int RevokeObjectParam(
	//	[MarshalAs(UnmanagedType.LPWStr)] string pszKey);

}