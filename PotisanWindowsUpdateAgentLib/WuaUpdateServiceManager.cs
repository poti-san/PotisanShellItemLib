using System.Collections.Immutable;

using Potisan.Windows.Diagnostics.Wua.ComTypes;

namespace Potisan.Windows.Diagnostics.Wua;

/// <summary>
/// アップデートサービスの管理機能。
/// </summary>
/// <param name="o">RCWオブジェクト。</param>
/// <example>
/// <code>
///<![CDATA[using Potisan.Windows.Diagnostics.Wua;
///
///var manager = UpdateServiceManager.Create();
///
///foreach (var service in manager.Services)
///{
///	Console.WriteLine(service.Name);
///}
///]]>
/// </code>
/// </example>
public class WuaUpdateServiceManager(object? o) : ComUnknownWrapperBase<IUpdateServiceManager>(o)
{
	public ComDispatch AsDispatch => new(_obj);

	public static ComResult<WuaUpdateServiceManager> CreateNoThrow()
	{
		// {F8D253D9-89A4-4DAA-87B6-1168369F0B21}
		Guid CLSID_UpdateServiceManager = new(0xF8D253D9, 0x89A4, 0x4DAA, 0x87, 0xB6, 0x11, 0x68, 0x36, 0x9F, 0x0B, 0x21);
		// Apartment: Both
		return ComHelper.CreateInstanceNoThrow<WuaUpdateServiceManager, IUpdateServiceManager>(CLSID_UpdateServiceManager, ComClassContext.InProcServer);
	}

	public static WuaUpdateServiceManager Create()
		=> CreateNoThrow().Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<WuaUpdateServiceCollection> ServiceCollectionNoThrow
		=> new(_obj.get_Services(out var x), new(x));

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public WuaUpdateServiceCollection ServiceCollection
		=> ServiceCollectionNoThrow.Value;

	public ImmutableArray<WuaUpdateService> Services
		=> [.. ServiceCollection];

	public ComResult<WuaUpdateService> AddServiceNoThrow(string serviceId, string authorizationCabPath)
		=> new(_obj.AddService(serviceId, authorizationCabPath, out var x), new(x));

	public WuaUpdateService AddService(string serviceId, string authorizationCabPath)
		=> AddServiceNoThrow(serviceId, authorizationCabPath).Value;

	public ComResult RegisterServiceWithAUNoThrow(string serviceId)
		=> new(_obj.RegisterServiceWithAU(serviceId));

	public void RegisterServiceWithAU(string serviceId)
		=> RegisterServiceWithAUNoThrow(serviceId).ThrowIfError();

	public ComResult RemoveServiceNoThrow(string serviceId)
		=> new(_obj.RemoveService(serviceId));

	public void RemoveService(string serviceId)
		=> RemoveServiceNoThrow(serviceId).ThrowIfError();

	public ComResult UnregisterServiceWithAUNoThrow(string serviceId)
		=> new(_obj.UnregisterServiceWithAU(serviceId));

	public void UnregisterServiceWithAU(string serviceId)
		=> UnregisterServiceWithAUNoThrow(serviceId).ThrowIfError();

	public ComResult<WuaUpdateService> AddScanPackageServiceNoThrow(string serviceName, string scanFileLocation, UpdateServiceOption flags)
		=> new(_obj.AddScanPackageService(serviceName, scanFileLocation, (int)flags, out var x), new(x));

	public WuaUpdateService AddScanPackageService(string serviceName, string scanFileLocation, UpdateServiceOption flags)
		=> AddScanPackageServiceNoThrow(serviceName, scanFileLocation, flags).Value;

	public ComResult SetOptionNoThrow(string optionName, object optionValue)
		=> new(_obj.SetOption(optionName, optionValue));

	public void SetOption(string optionName, object optionValue)
		=> SetOptionNoThrow(optionName, optionValue).ThrowIfError();
}

[Flags]
public enum UpdateServiceOption : int
{
	NonVolatileService = 0x1
}