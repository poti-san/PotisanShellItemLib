using Potisan.Windows.Com;
using Potisan.Windows.DXCore.ComTypes;

namespace Potisan.Windows.DXCore;

/// <summary>
/// DXCoreのアダプタファクトリ（バージョン1）。
/// </summary>
/// <param name="o">RCWインスタンス。</param>
/// <example>
/// <code>
///<![CDATA[using Potisan.Windows.DXCore;
///
///var factory = DXCoreAdapterFactory.Create();
///var adapters = factory.CreateAdapterList([DXCoreAdapterAttributeGuids.D3D11Graphics]);
///foreach (var adapter in adapters.Adapters)
///{
///	Console.WriteLine(adapter.DriverDescription);
///}]]>
/// </code>
/// </example>
public class DXCoreAdapterFactory(object? o) : ComUnknownWrapperBase<IDXCoreAdapterFactory>(o)
{
	public ComResult<TWrapper> CreateAdapterListTNoThrow<TWrapper, TInterface>(Guid[] filterAttributes)
		where TWrapper : IComUnknownWrapper
		=> IComUnknownWrapper.Wrap<TWrapper>(_obj.CreateAdapterList(
			(uint)(filterAttributes?.Length ?? 0), filterAttributes, typeof(TInterface).GUID, out var x), x);

	public TWrapper CreateAdapterListT<TWrapper, TInterface>(Guid[] filterAttributes)
		where TWrapper : IComUnknownWrapper
		=> CreateAdapterListTNoThrow<TWrapper, TInterface>(filterAttributes).Value;

	/// <summary>
	/// アダプタリストを作成します。
	/// </summary>
	/// <param name="filterAttributes"><see cref="DXCoreAdapterAttributeGuids"/>から1つ以上の値。</param>
	public ComResult<DXCoreAdapterList> CreateAdapterListNoThrow(Guid[] filterAttributes)
		=> CreateAdapterListTNoThrow<DXCoreAdapterList, IDXCoreAdapterList>(filterAttributes);

	/// <inheritdoc cref="CreateAdapterListNoThrow(Guid[])"/>
	public DXCoreAdapterList CreateAdapterList(Guid[] filterAttributes)
		=> CreateAdapterListNoThrow(filterAttributes).Value;

	public ComResult<object> GetAdapterByLuidNoThrow(in Luid luid, in Guid iid)
		=> new(_obj.GetAdapterByLuid(luid, iid, out var x), x!);

	public object GetAdapterByLuid(in Luid luid, in Guid iid)
		=> GetAdapterByLuidNoThrow(luid, iid).Value;

	public ComResult<TWrapper> GetAdapterByLuidNoThrow<TWrapper, TInterface>(in Luid luid)
		where TWrapper : IComUnknownWrapper
		=> IComUnknownWrapper.Wrap<TWrapper>(GetAdapterByLuidNoThrow(luid, typeof(TInterface).GUID));

	public TWrapper GetAdapterByLuid<TWrapper, TInterface>(in Luid luid)
		where TWrapper : IComUnknownWrapper
		=> GetAdapterByLuidNoThrow<TWrapper, TInterface>(luid).Value;

	public bool IsNotificationTypeSupported(DXCoreNotificationType type)
		=> _obj.IsNotificationTypeSupported(type);

	public ComResult<uint> RegisterEventNotificationNoThrow(object? coreObject, DXCoreNotificationType type, DXCoreNotificationCallback callback, nuint callbackContext = 0)
		=> new(_obj.RegisterEventNotification(coreObject, type, callback, callbackContext, out var x), x);

	public uint RegisterEventNotification(object? coreObject, DXCoreNotificationType type, DXCoreNotificationCallback callback, nuint callbackContext = 0)
		=> RegisterEventNotificationNoThrow(coreObject, type, callback, callbackContext).Value;

	public ComResult<uint> RegisterEventNotificationNoThrow(IComUnknownWrapper coreObject, DXCoreNotificationType type, DXCoreNotificationCallback callback, nuint callbackContext = 0)
		=> RegisterEventNotificationNoThrow(coreObject.WrappedObject, type, callback, callbackContext);

	public uint RegisterEventNotification(IComUnknownWrapper coreObject, DXCoreNotificationType type, DXCoreNotificationCallback callback, nuint callbackContext = 0)
		=> RegisterEventNotificationNoThrow(coreObject, type, callback, callbackContext).Value;

	public ComResult UnregisterEventNotificationNoThrow(uint eventCookie)
		=> new(_obj.UnregisterEventNotification(eventCookie));

	public void UnregisterEventNotification(uint eventCookie)
		=> UnregisterEventNotificationNoThrow(eventCookie).ThrowIfError();

	public static ComResult<object> CreateNoThrow(in Guid iid)
	{
		[DllImport("dxcore.dll")]
		static extern int DXCoreCreateAdapterFactory(
			in Guid riid,
			[MarshalAs(UnmanagedType.IUnknown)] out object? ppvFactory);

		return new(DXCoreCreateAdapterFactory(iid, out var x), x!);
	}

	public static object Create(in Guid iid)
		=> CreateNoThrow(iid).Value;

	public static ComResult<TWrapper> CreateTNoThrow<TWrapper, TInterface>()
		where TWrapper : IComUnknownWrapper
		=> IComUnknownWrapper.Wrap<TWrapper>(CreateNoThrow(typeof(TInterface).GUID));

	public static TWrapper CreateT<TWrapper, TInterface>()
		where TWrapper : IComUnknownWrapper
		=> CreateTNoThrow<TWrapper, TInterface>().Value;

	public static ComResult<DXCoreAdapterFactory> CreateNoThrow()
		=> CreateTNoThrow<DXCoreAdapterFactory, IDXCoreAdapterFactory>();

	public static DXCoreAdapterFactory Create()
		=> CreateNoThrow().Value;
}
