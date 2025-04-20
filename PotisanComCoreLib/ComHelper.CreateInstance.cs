using System.ComponentModel;
using System.Runtime.InteropServices.Marshalling;

using Potisan.Windows.Com.ComTypes;

namespace Potisan.Windows.Com;

public static partial class ComHelper
{
	public static ComResult<object> CreateInstanceNoThrow(in Guid clsid, ComClassContext clsctx = ComClassContext.All)
	{
		Guid IID_IUnknown = new("00000000-0000-0000-C000-000000000046");
		return new(NativeMethods.CoCreateInstance(clsid, null, (uint)clsctx, IID_IUnknown, out var x), x!);
	}

	public static object CreateInstance(in Guid clsid, ComClassContext clsctx = ComClassContext.All)
		=> CreateInstanceNoThrow(clsid, clsctx).Value;

	public static ComResult<TWrapper> CreateInstanceNoThrow<TWrapper, TInterface>(in Guid clsid, ComClassContext clsctx = ComClassContext.All)
		where TWrapper : IComUnknownWrapper
	{
		return IComUnknownWrapper.Wrap<TWrapper>(
			NativeMethods.CoCreateInstance(clsid, null, (uint)clsctx, typeof(TInterface).GUID, out var x), x);
	}

	public static TWrapper CreateInstance<TWrapper, TInterface>(in Guid clsid, ComClassContext clsctx = ComClassContext.All)
		where TWrapper : IComUnknownWrapper
		=> CreateInstanceNoThrow<TWrapper, TInterface>(clsid, clsctx).Value;

	public static ComResult<TWrapper> CreateInstanceNoThrow<TWrapper>(in Guid clsid, ComClassContext clsctx = ComClassContext.All)
		where TWrapper : IComUnknownWrapper
	{
		return CreateInstanceNoThrow<TWrapper, IUnknown>(clsid, clsctx);
	}

	public static TWrapper CreateInstance<TWrapper>(in Guid clsid, ComClassContext clsctx = ComClassContext.All)
		where TWrapper : IComUnknownWrapper
		=> CreateInstanceNoThrow<TWrapper>(clsid, clsctx).Value;

	private static class NativeMethods
	{
		[DllImport("ole32.dll")]
		public static extern int CoCreateInstance(
			in Guid rclsid,
			[MarshalAs(UnmanagedType.IUnknown)] object? pUnkOuter,
			uint dwClsContext,
			in Guid riid,
			[MarshalAs(UnmanagedType.IUnknown)] out object? ppv);
	}
}

[Flags]
public enum ComClassContext : uint
{
	InProcServer = 0x1,
	InProcHandler = 0x2,
	LocalServer = 0x4,
	RemoteServer = 0x10,
	NoCodeDownload = 0x400,
	NoCustomMarshal = 0x1000,
	EnableCodeDownload = 0x2000,
	NoFailureLog = 0x4000,
	DisableAaa = 0x8000,
	EnableAaa = 0x10000,
	FromDefaultContext = 0x20000,
	ActivateX86Server = 0x40000,
	Activate32BitServer = ActivateX86Server,
	Activate64BitServer = 0x80000,
	EnableCloaking = 0x100000,
	[EditorBrowsable(EditorBrowsableState.Never)]
	AppContainer = 0x400000,
	ActivateAaaAsUI = 0x800000,
	ActivateArm32Server = 0x2000000,
	AllowLowerTrustRegistration = 0x4000000,
	PSDll = 0x80000000,

	All = InProcServer | InProcHandler | LocalServer,
	Server = InProcServer | LocalServer,
}
