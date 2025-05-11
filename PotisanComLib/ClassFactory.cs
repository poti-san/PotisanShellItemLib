using Potisan.Windows.Com.ComTypes;

namespace Potisan.Windows.Com;

public class ClassFactory(object? o) : ComUnknownWrapperBase<IClassFactory>(o)
{
	public ComResult<object> CreateInstanceNoThrow(in Guid iid, object? outer = null)
		=> new(_obj.CreateInstance(outer, iid, out var x), x!);

	public object CreateInstance(in Guid iid, object? outer = null)
		=> CreateInstanceNoThrow(iid, outer).Value;

	public ComResult<TWrapper> CreateInstanceNoThrow<TWrapper, TInterface>(object? outer = null)
		where TWrapper : IComUnknownWrapper
		=> IComUnknownWrapper.Wrap<TWrapper>(CreateInstanceNoThrow(typeof(TInterface).GUID, outer));

	public TWrapper CreateInstance<TWrapper, TInterface>(object? outer = null)
		where TWrapper : IComUnknownWrapper
		=> CreateInstanceNoThrow<TWrapper, TInterface>(outer).Value;

	public ComResult LockServerNoThrow(bool locks)
		=> new(_obj.LockServer(locks));

	public void LockServer(bool locks)
		=> LockServerNoThrow(locks).ThrowIfError();

	protected static ComResult<object> GetClassObjectNoThrow(in Guid clsid, ComClassContext clsctx, in Guid iid)
	{
		[DllImport("ole32.dll")]
		static extern int CoGetClassObject(in Guid rclsid, ComClassContext dwClsContext, nint pvReserved, in Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object? ppv);

		return new(CoGetClassObject(clsid, clsctx, 0, iid, out var x), x!);
	}

	public static ComResult<ClassFactory> GetClassObjectNoThrow(in Guid clsid, ComClassContext clsctx = ComClassContext.All)
		=> IComUnknownWrapper.Wrap<ClassFactory>(GetClassObjectNoThrow(clsid, clsctx, typeof(IClassFactory).GUID));

	public static ClassFactory GetClassObject(in Guid clsid, ComClassContext clsctx = ComClassContext.All)
		=> GetClassObjectNoThrow(clsid, clsctx).Value;
}
