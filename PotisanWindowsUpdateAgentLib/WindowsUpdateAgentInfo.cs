using Potisan.Windows.Diagnostics.Wua.ComTypes;

namespace Potisan.Windows.Diagnostics.Wua;

/// <summary>
/// WUA情報。
/// </summary>
/// <param name="o">RCWオブジェクト。</param>
/// <remarks>
/// <see cref="Create"/>で作成できます。
/// </remarks>
/// <example>
/// <code>
///<![CDATA[using Potisan.Windows.Diagnostics.Wua;
///
///var wuaInfo = WindowsUpdateAgentInfo.Create();
///
///Console.WriteLine($"""
///	WUAバージョン:{wuaInfo.ApiMajorVersion}.{wuaInfo.ApiMinorVersion}
///	WUA製品バージョン文字列:{wuaInfo.ProductVersionString}
///	""");]]>
/// </code>
/// </example>
public class WindowsUpdateAgentInfo(object? o) : ComUnknownWrapperBase<IWindowsUpdateAgentInfo>(o)
{
	public ComDispatch AsDispatch => new(_obj);

	public static ComResult<WindowsUpdateAgentInfo> CreateNoThrow()
	{
		// {C2E88C2F-6F5B-4AAA-894B-55C847AD3A2D}
		Guid CLSID_WindowsUpdateAgentInfo = new(0xC2E88C2F, 0x6F5B, 0x4AAA, 0x89, 0x4B, 0x55, 0xC8, 0x47, 0xAD, 0x3A, 0x2D);
		// Both
		return ComHelper.CreateInstanceNoThrow<WindowsUpdateAgentInfo, IWindowsUpdateAgentInfo>(
			CLSID_WindowsUpdateAgentInfo, ComClassContext.InProcServer);
	}

	public static WindowsUpdateAgentInfo Create()
		=> CreateNoThrow().Value;

	public ComResult<object> GetInfoNoThrow(object infoId)
		=> new(_obj.GetInfo(infoId, out var x), x);

	public object GetInfo(object infoId)
		=> GetInfoNoThrow(infoId).Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<int> ApiMajorVersionNoThrow
		=> GetInfoNoThrow("ApiMajorVersion") switch
		{
			{ Succeeded: true, ValueUnchecked: int x } => new(CommonHResults.SOK, x),
			{ Succeeded: true } => new(CommonHResults.EFail, 0),
			{ HResult: var hr } => new(hr, 0)
		};

	public int ApiMajorVersion
		=> ApiMajorVersionNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<int> ApiMinorVersionNoThrow
		=> GetInfoNoThrow("ApiMinorVersion") switch
		{
			{ Succeeded: true, ValueUnchecked: int x } => new(CommonHResults.SOK, x),
			{ Succeeded: true } => new(CommonHResults.EFail, 0),
			{ HResult: var hr } => new(hr, 0)
		};

	public int ApiMinorVersion
		=> ApiMinorVersionNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<string> ProductVersionStringNoThrow
		=> GetInfoNoThrow("ProductVersionString") switch
		{
			{ Succeeded: true, ValueUnchecked: string x } => new(CommonHResults.SOK, x),
			{ Succeeded: true } => new(CommonHResults.EFail, null!),
			{ HResult: var hr } => new(hr, null!)
		};

	public string ProductVersionString
		=> ProductVersionStringNoThrow.Value;
}
