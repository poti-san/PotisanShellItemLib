using Potisan.Windows.Com.ComTypes;

namespace Potisan.Windows.Com;

/// <summary>
/// モニカ。IMoniker COMインターフェイスのラッパーです。
/// </summary>
/// <param name="o">RCWインスタンス。</param>
public class Moniker(object? o) : PersistStream(o)
{
	private new readonly IMoniker _obj = o == null ? null! : (IMoniker)o;

	public override string ToString() => $"\"{DisplayName}\" ({SystemMonikerType})";

	public ComResult<TWrapper> BindToObjectNoThrow<TWrapper, TInterface>(BindCtx bc, Moniker? toLeft)
		where TWrapper : IComUnknownWrapper
	{
		var hr = _obj.BindToObject((IBindCtx)bc.WrappedObject!, toLeft?.WrappedObject as IMoniker, typeof(TInterface).GUID, out var x);
		return IComUnknownWrapper.Wrap<TWrapper>(hr, x);
	}

	public TWrapper BindToObject<TWrapper, TInterface>(BindCtx bc, Moniker? toLeft)
		where TWrapper : IComUnknownWrapper
		=> BindToObjectNoThrow<TWrapper, TInterface>(bc, toLeft).Value;

	public ComResult<TWrapper> BindToObjectNoThrow<TWrapper, TInterface>(Moniker? toLeft)
		where TWrapper : IComUnknownWrapper
	{
		var bc = BindCtx.CreateNoThrow();
		if (!bc) return IComUnknownWrapper.Wrap<TWrapper>(bc.HResult, null);
		using (bc.ValueUnchecked)
			return BindToObjectNoThrow<TWrapper, TInterface>(bc.ValueUnchecked, toLeft);
	}

	public TWrapper BindToObject<TWrapper, TInterface>(Moniker? toLeft)
		where TWrapper : IComUnknownWrapper
		=> BindToObjectNoThrow<TWrapper, TInterface>(toLeft).Value;

	public ComResult<TWrapper> BindToStorageNoThrow<TWrapper, TInterface>(BindCtx bc, Moniker? toLeft)
		where TWrapper : IComUnknownWrapper
	{
		return IComUnknownWrapper.Wrap<TWrapper>(
			_obj.BindToStorage((IBindCtx)bc.WrappedObject!, toLeft?.WrappedObject as IMoniker, typeof(TInterface).GUID, out var x), x);
	}

	public TWrapper BindToStorage<TWrapper, TInterface>(BindCtx bc, Moniker? toLeft)
		where TWrapper : IComUnknownWrapper
		=> BindToStorageNoThrow<TWrapper, TInterface>(bc, toLeft).Value;

	public ComResult<TWrapper> BindToStorageNoThrow<TWrapper, TInterface>(Moniker? toLeft)
		where TWrapper : IComUnknownWrapper
	{
		var bc = BindCtx.CreateNoThrow();
		if (!bc) return IComUnknownWrapper.Wrap<TWrapper>(bc.HResult, null);
		using (bc.ValueUnchecked)
			return BindToStorageNoThrow<TWrapper, TInterface>(bc.ValueUnchecked, toLeft);
	}

	public TWrapper BindToStorage<TWrapper, TInterface>(Moniker? toLeft)
		where TWrapper : IComUnknownWrapper
		=> BindToStorageNoThrow<TWrapper, TInterface>(toLeft).Value;
	// TODO  IStorage、 IStream、 ILockBytes

	public ComResult<Moniker> ReduceNoThrow(BindCtx bc, MonikerReduce reduceHowFar, Moniker? toLeft = null)
		=> new(_obj.Reduce((IBindCtx)bc.WrappedObject!, (uint)reduceHowFar, toLeft?.WrappedObject as IMoniker, out var x), new(x));

	public Moniker Reduce(BindCtx bc, MonikerReduce reduceHowFar, Moniker? toLeft = null)
		=> ReduceNoThrow(bc, reduceHowFar, toLeft).Value;

	public ComResult<Moniker> ReduceNoThrow(MonikerReduce reduceHowFar, Moniker? toLeft = null)
	{
		var bc = BindCtx.CreateNoThrow();
		if (!bc) return new(bc.HResult, new(null));
		using (bc.ValueUnchecked)
			return ReduceNoThrow(bc.ValueUnchecked, reduceHowFar, toLeft);
	}

	public Moniker Reduce(MonikerReduce reduceHowFar, Moniker? toLeft = null)
		=> ReduceNoThrow(reduceHowFar, toLeft).Value;

	public ComResult<Moniker> ComposeWithNoThrow(Moniker right, bool onlyIfNotGeneric)
		=> new(_obj.ComposeWith(right.WrappedObject as IMoniker, onlyIfNotGeneric, out var x), new(x));

	public Moniker ComposeWith(Moniker right, bool onlyIfNotGeneric)
		=> ComposeWithNoThrow(right, onlyIfNotGeneric).Value;

	private ComResult<MonikerEnumerable> GetEnumerableNoThrow(bool forward)
		=> new(_obj.Enum(forward, out var x), new(x));

	public ComResult<MonikerEnumerable> ForwardEnumerableNoThrow
		=> GetEnumerableNoThrow(true);

	public ComResult<MonikerEnumerable> BackwardEnumerableNoThrow
		=> GetEnumerableNoThrow(false);

	public MonikerEnumerable ForwardEnumerable
		=> GetEnumerableNoThrow(true).Value;

	public MonikerEnumerable BackwardEnumerable
		=> GetEnumerableNoThrow(false).Value;

	public ComResult<bool> EqualsNoThrow(Moniker other)
		=> ComResult.HRSuccess(_obj.IsEqual(other.WrappedObject as IMoniker));

	public bool Equals(Moniker other)
		=> EqualsNoThrow(other).Value;

	public ComResult<uint> HashNoThrow
		=> new(_obj.Hash(out var x), x);

	public uint Hash
		=> HashNoThrow.Value;

	public ComResult<bool> DeterminateRunningNoThrow(BindCtx bc, Moniker? toLeft = null, Moniker? newlyRunning = null)
		=> ComResult.HRSuccess(
			_obj.IsRunning((IBindCtx)bc.WrappedObject!, toLeft?.WrappedObject as IMoniker, newlyRunning?.WrappedObject as IMoniker));

	public bool DeterminateRunning(BindCtx bc, Moniker? toLeft = null, Moniker? newlyRunning = null)
		=> DeterminateRunningNoThrow(bc, toLeft, newlyRunning).Value;

	public ComResult<bool> DeterminateRunningNoThrow(Moniker? toLeft = null, Moniker? newlyRunning = null)
	{
		var bc = BindCtx.CreateNoThrow();
		if (!bc) return new(bc.HResult, false);
		using (bc.ValueUnchecked)
			return DeterminateRunningNoThrow(bc.ValueUnchecked, toLeft, newlyRunning);
	}

	public bool DeterminateRunning(Moniker? toLeft = null, Moniker? newlyRunning = null)
		=> DeterminateRunningNoThrow(toLeft, newlyRunning).Value;

	public ComResult<bool> IsRunningNoThrow
		=> DeterminateRunningNoThrow();

	public bool IsRunning
		=> IsRunningNoThrow.Value;

	public ComResult<FileTime> GetTimeOfLastChangeNoThrow(BindCtx bc, Moniker? toLeft = null)
		=> new(_obj.GetTimeOfLastChange((IBindCtx)bc.WrappedObject!, toLeft?.WrappedObject as IMoniker, out var x), x);

	public FileTime GetTimeOfLastChange(BindCtx bc, Moniker? toLeft = null)
		=> GetTimeOfLastChangeNoThrow(bc, toLeft).Value;

	public ComResult<FileTime> GetTimeOfLastChangeNoThrow(Moniker? toLeft = null)
	{
		var bc = BindCtx.CreateNoThrow();
		if (!bc) return new(bc.HResult, new());
		using (bc.ValueUnchecked)
			return GetTimeOfLastChangeNoThrow(bc.ValueUnchecked, toLeft);
	}

	public FileTime GetTimeOfLastChange(Moniker? toLeft = null)
		=> GetTimeOfLastChangeNoThrow(toLeft).Value;

	public ComResult<FileTime> TimeOfLastChangeNoThrow
		=> GetTimeOfLastChangeNoThrow();

	public FileTime TimeOfLastChange
		=> TimeOfLastChangeNoThrow.Value;

	public ComResult<Moniker> InversedNoThrow
		=> new(_obj.Inverse(out var x), new(x));

	public Moniker Inversed
		=> InversedNoThrow.Value;

	public ComResult<Moniker> CommonPrefixWithNoThrow(Moniker other)
		=> new(_obj.CommonPrefixWith(other?.WrappedObject as IMoniker, out var x), new(x));

	public Moniker CommonPrefixWith(Moniker other)
		=> CommonPrefixWithNoThrow(other).Value;

	public ComResult<Moniker> RelativePathToNoThrow(Moniker other)
		=> new(_obj.RelativePathTo(other?.WrappedObject as IMoniker, out var x), new(x));

	public Moniker RelativePathTo(Moniker other)
		=> RelativePathToNoThrow(other).Value;

	public ComResult<string> GetDisplayNameToNoThrow(BindCtx bc, Moniker? toLeft = null)
		=> new(_obj.GetDisplayName((IBindCtx)bc.WrappedObject!, toLeft?.WrappedObject as IMoniker, out var x), x);

	public string GetDisplayName(BindCtx bc, Moniker? toLeft = null)
		=> GetDisplayNameToNoThrow(bc, toLeft).Value;

	public ComResult<string> GetDisplayNameToNoThrow(Moniker? toLeft = null)
	{
		var bc = BindCtx.CreateNoThrow();
		if (!bc) return new(bc.HResult, "");
		using (bc.ValueUnchecked)
			return GetDisplayNameToNoThrow(bc.ValueUnchecked, toLeft);
	}

	public string GetDisplayName(Moniker? toLeft = null)
		=> GetDisplayNameToNoThrow(toLeft).Value;

	public ComResult<string> DisplayNameNoThrow
		=> GetDisplayNameToNoThrow();

	public string DisplayName
		=> DisplayNameNoThrow.Value;

	public ComResult<Moniker> InternalParseDisplayNameToNoThrow(BindCtx bc, string displayName, Moniker? toLeft = null)
		=> new(_obj.ParseDisplayName((IBindCtx)bc.WrappedObject!, toLeft?.WrappedObject as IMoniker, displayName, out var _, out var x), new(x));

	public Moniker InternalParseDisplayName(BindCtx bc, string displayName, Moniker? toLeft = null)
		=> InternalParseDisplayNameToNoThrow(bc, displayName, toLeft).Value;

	public ComResult<Moniker> InternalParseDisplayNameToNoThrow(string displayName, Moniker? toLeft = null)
	{
		var bc = BindCtx.CreateNoThrow();
		if (!bc) return new(bc.HResult, new(null));
		using (bc.ValueUnchecked)
			return InternalParseDisplayNameToNoThrow(bc.ValueUnchecked, displayName, toLeft);
	}

	public Moniker InternalParseDisplayName(string displayName, Moniker? toLeft = null)
		=> InternalParseDisplayNameToNoThrow(displayName, toLeft).Value;

	public ComResult<MonikerSystem> SystemMonikerTypeNoThrow
		=> new(_obj.IsSystemMoniker(out var x), (MonikerSystem)x);

	public MonikerSystem SystemMonikerType
		=> SystemMonikerTypeNoThrow.Value;

	public static ComResult<Moniker> CreateAntiMonikerNoThrow()
	{
		[DllImport("ole32.dll")]
		static extern int CreateAntiMoniker(out IMoniker ppmk);

		return new(CreateAntiMoniker(out var x), new(x));
	}

	public static Moniker CreateAntiMoniker()
		=> CreateAntiMonikerNoThrow().Value;

	public static ComResult<Moniker> CreateClassMonikerNoThrow(in Guid clsid)
	{
		[DllImport("ole32.dll")]
		static extern int CreateClassMoniker(in Guid rclsid, out IMoniker ppmk);

		return new(CreateClassMoniker(clsid, out var x), new(x));
	}

	public static Moniker CreateClassMoniker(in Guid clsid)
		=> CreateClassMonikerNoThrow(clsid).Value;

	public static ComResult<Moniker> CreateFileMonikerNoThrow(string path)
	{
		[DllImport("ole32.dll")]
		static extern int CreateFileMoniker([MarshalAs(UnmanagedType.LPWStr)] string path, out IMoniker ppmk);

		return new(CreateFileMoniker(path, out var x), new(x));
	}

	public static Moniker CreateFileMoniker(string path)
		=> CreateFileMonikerNoThrow(path).Value;

	public static ComResult<Moniker> CreateGenericCompositeNoThrow(Moniker? first, Moniker? rest)
	{
		[DllImport("ole32.dll")]
		static extern int CreateGenericComposite(IMoniker? pmkFirst, IMoniker? pmkRest, out IMoniker? ppmkComposite);

		return new(CreateGenericComposite(first?.WrappedObject as IMoniker, rest?.WrappedObject as IMoniker, out var x), new(x));
	}

	public static Moniker CreateGenericComposite(Moniker? first, Moniker? rest)
		=> CreateGenericCompositeNoThrow(first, rest).Value;

	public static ComResult<Moniker> CreateItemMonikerNoThrow(string delimiter, string item)
	{
		[DllImport("ole32.dll")]
		static extern int CreateItemMoniker(
			[MarshalAs(UnmanagedType.LPWStr)] string lpszDelim,
			[MarshalAs(UnmanagedType.LPWStr)] string lpszItem,
			out IMoniker ppmk);

		return new(CreateItemMoniker(delimiter, item, out var x), new(x));
	}

	public static Moniker CreateItemMoniker(string delimiter, string item)
		=> CreateItemMonikerNoThrow(delimiter, item).Value;

	public static ComResult<Moniker> CreateObjRefMonikerNoThrow(object obj)
	{
		[DllImport("ole32.dll")]
		static extern int CreateObjrefMoniker([MarshalAs(UnmanagedType.IUnknown)] object punk, out IMoniker ppmk);

		return new(CreateObjrefMoniker(obj, out var x), new(x));
	}

	public static Moniker CreateObjRefMoniker(object obj)
		=> CreateObjRefMonikerNoThrow(obj).Value;

	public static ComResult<Moniker> CreatePointerMonikerNoThrow(object obj)
	{
		[DllImport("ole32.dll")]
		static extern int CreatePointerMoniker([MarshalAs(UnmanagedType.IUnknown)] object punk, out IMoniker ppmk);

		return new(CreatePointerMoniker(obj, out var x), new(x));
	}

	public static Moniker CreatePointerMoniker(object obj)
		=> CreatePointerMonikerNoThrow(obj).Value;

	public static ComResult<Moniker> ParseDisplayNameToNoThrow(BindCtx bc, string userName)
	{
		[DllImport("ole32.dll")]
		static extern int MkParseDisplayName(IBindCtx pbc, [MarshalAs(UnmanagedType.LPWStr)] string szUserName, out uint pchEaten, out IMoniker ppmk);

		return new(MkParseDisplayName((IBindCtx)bc.WrappedObject!, userName, out var _, out var x), new(x));
	}

	public static Moniker ParseDisplayName(BindCtx bc, string userName)
		=> ParseDisplayNameToNoThrow(bc, userName).Value;

	public static ComResult<Moniker> ParseDisplayNameToNoThrow(string userName)
	{
		var bc = BindCtx.CreateNoThrow();
		if (!bc) return new(bc.HResult, new(null));
		using (bc.ValueUnchecked)
			return ParseDisplayNameToNoThrow(bc.ValueUnchecked, userName);
	}

	public static Moniker ParseDisplayName(string userName)
		=> ParseDisplayNameToNoThrow(userName).Value;
}

/// <summary>
/// MKSYS
/// </summary>
public enum MonikerSystem : uint
{
	None = 0,
	GenericComposite = 1,
	FileMoniker = 2,
	AntiMoniker = 3,
	ItemMoniker = 4,
	PointerMoniker = 5,
	ClassMoniker = 7,
	ObjRefMoniker = 8,
	SessionMoniker = 9,
	LuaMoniker = 10,
}

/// <summary>
/// MKREDUCE
/// </summary>
public enum MonikerReduce : uint
{
	One = 3 << 16,
	ToUser = 2 << 16,
	ThroughUser = 1 << 16,
	All = 0,
}