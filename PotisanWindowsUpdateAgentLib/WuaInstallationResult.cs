﻿using Potisan.Windows.Diagnostics.Wua.ComTypes;

namespace Potisan.Windows.Diagnostics.Wua;

public sealed class WuaInstallationResult(object? o) : ComUnknownWrapperBase<IInstallationResult>(o)
{
	public ComDispatch? AsDispatch => this.As<ComDispatch, IDispatch>();

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<int> HResultNoThrow
		=> new(_obj.get_HResult(out var x), x);

	public int HResult
		=> HResultNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<bool> RebootRequiredNoThrow
		=> new(_obj.get_RebootRequired(out var x), x);

	public bool RebootRequired
		=> RebootRequiredNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<WuaOperationResultCode> ResultCodeNoThrow
		=> new(_obj.get_ResultCode(out var x), x);

	public WuaOperationResultCode ResultCode
		=> ResultCodeNoThrow.Value;

	public ComResult<WuaUpdateDownloadResult> GetUpdateResultNoThrow(int updateIndex)
		=> new(_obj.GetUpdateResult(updateIndex, out var x), new(x));

	public WuaUpdateDownloadResult GetUpdateResult(int updateIndex)
		=> GetUpdateResultNoThrow(updateIndex).Value;
}
