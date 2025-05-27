using System.Collections.Immutable;

using Potisan.Windows.Text.Tsf.ComTypes;

namespace Potisan.Windows.Text.Tsf;

/// <summary>
/// TFSのカテゴリマネージャー。
/// </summary>
/// <param name="o">RCWオブジェクト。</param>
/// <remarks>
/// このクラスはシングルスレッドアパートメントモデルでのみ作成可能です。
/// </remarks>
/// <example>
/// コンソールアプリケーションで使用する場合は<see cref="STAThreadAttribute"/>を指定するか以下のように強制変更します。
/// <code>
///<![CDATA[
///using Potisan.Windows.Text.Tsf;
///
///Thread.CurrentThread.SetApartmentState(ApartmentState.Unknown);
///Thread.CurrentThread.SetApartmentState(ApartmentState.STA);
///
///var catManager = TFCategoryManager.Create();
///var itemGuids = catManager.ItemsInTipCategoryEnumerable;
///
///Console.WriteLine(string.Join(", ", itemGuids.Select(guid => guid.ToString("B")))); 
///]]>
/// </code>
/// </example>
public class TFCategoryManager(object? o) : ComUnknownWrapperBase<ITfCategoryMgr>(o)
{
	public static ComResult<TFCategoryManager> CreateNoThrow()
	{
		// 分かりにくい原因なのでアサートを発生させます。
		Debug.Assert(Thread.CurrentThread.GetApartmentState() == ApartmentState.STA,
			"シングルスレッドモデルでのみ作成できます。");

		Guid CLSID_TF_CategoryMgr = new("A4B544A1-438D-4B41-9325-869523E2D6C7");
		return ComHelper.CreateInstanceNoThrow<TFCategoryManager, ITfCategoryMgr>(CLSID_TF_CategoryMgr, ComClassContext.InProcServer);
	}

	public static TFCategoryManager Create()
		=> CreateNoThrow().Value;

	public ComResult RegisterCategoryNoThrow(in Guid clsid, in Guid catid, in Guid guid)
		=> new(_obj.RegisterCategory(clsid, catid, guid));

	public void RegisterCategory(in Guid clsid, in Guid catid, in Guid guid)
		=> RegisterCategoryNoThrow(clsid, catid, guid).ThrowIfError();

	public ComResult UnregisterCategoryNoThrow(in Guid clsid, in Guid catid, in Guid guid)
		=> new(_obj.UnregisterCategory(clsid, catid, guid));

	public void UnregisterCategory(in Guid clsid, in Guid catid, in Guid guid)
		=> UnregisterCategoryNoThrow(clsid, catid, guid).ThrowIfError();

	public ComResult<ComGuidEnumerable> GetCategoriesInItemEnumerableNoThrow(in Guid guid)
		=> new(_obj.EnumCategoriesInItem(guid, out var x), new(x));

	public ComGuidEnumerable GetCategoriesInItemEnumerable(in Guid guid)
		=> GetCategoriesInItemEnumerableNoThrow(guid).Value;

	public ComResult<ComGuidEnumerable> GetItemsInCategoryEnumerableNoThrow(in Guid catid)
		=> new(_obj.EnumItemsInCategory(catid, out var x), new(x));

	public ComGuidEnumerable GetItemsInCategoryEnumerable(in Guid catid)
		=> GetItemsInCategoryEnumerableNoThrow(catid).Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<ComGuidEnumerable> ItemsInTipCategoryEnumerableNoThrow
		=> GetItemsInCategoryEnumerableNoThrow(TFGuids.TipCategoryGuid);

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComGuidEnumerable ItemsInTipCategoryEnumerable
		=> ItemsInTipCategoryEnumerableNoThrow.Value;

	public ImmutableArray<Guid> ItemsInTipCategory
		=> [.. ItemsInTipCategoryEnumerable];

	public ComResult<Guid> FindClosestCategoryNoThrow(in Guid guid, ReadOnlySpan<Guid> catids)
		=> new(_obj.FindClosestCategory(guid, out var x, catids.GetReference(), checked((uint)catids.Length)), x);

	public Guid FindClosestCategory(in Guid guid, ReadOnlySpan<Guid> catids)
		=> FindClosestCategoryNoThrow(guid, catids).Value;

	public ComResult<Guid> FindClosestCategoryNoThrow(in Guid guid)
		=> new(_obj.FindClosestCategory(guid, out var x, Unsafe.NullRef<Guid>(), 0), x);

	public Guid FindClosestCategory(in Guid guid)
		=> FindClosestCategoryNoThrow(guid).Value;

	public ComResult RegisterGuidDescriptionNoThrow(in Guid clsid, in Guid guid, ReadOnlySpan<char> value)
		=> new(_obj.RegisterGUIDDescription(clsid, guid, value.GetReference(), checked((uint)value.Length)));

	public void RegisterGuidDescription(in Guid clsid, in Guid guid, ReadOnlySpan<char> value)
		=> RegisterGuidDescriptionNoThrow(clsid, guid, value).ThrowIfError();

	public ComResult UnregisterGuidDescriptionNoThrow(in Guid clsid, in Guid guid)
		=> new(_obj.UnregisterGUIDDescription(clsid, guid));

	public void UnregisterGuidDescription(in Guid clsid, in Guid guid)
		=> UnregisterGuidDescriptionNoThrow(clsid, guid).ThrowIfError();

	public ComResult<string> GetGuidDescriptionNoThrow(in Guid guid)
		=> new(_obj.GetGUIDDescription(guid, out var x), x!);

	public string GetGuidDescription(in Guid guid)
		=> GetGuidDescriptionNoThrow(guid).Value;

	public ComResult RegisterGuidDWordNoThrow(in Guid clsid, in Guid guid, uint value)
		=> new(_obj.RegisterGUIDDWORD(clsid, guid, value));

	public void RegisterGuidDWord(in Guid clsid, in Guid guid, uint value)
		=> RegisterGuidDWordNoThrow(clsid, guid, value).ThrowIfError();

	public ComResult UnregisterGuidDWordNoThrow(in Guid clsid, in Guid guid)
		=> new(_obj.UnregisterGUIDDWORD(clsid, guid));

	public void UnregisterGuidDWord(in Guid clsid, in Guid guid)
		=> UnregisterGuidDWordNoThrow(clsid, guid).ThrowIfError();

	public ComResult<uint> GetGuidDWordNoThrow(in Guid guid)
		=> new(_obj.GetGUIDDWORD(guid, out var x), x);

	public uint GetGuidDWord(in Guid guid)
		=> GetGuidDWordNoThrow(guid).Value;

	public ComResult<TFGuidAtom> RegisterGuidNoThrow(in Guid guid)
		=> new(_obj.RegisterGUID(guid, out var x), x);

	public TFGuidAtom RegisterGuid(in Guid guid)
		=> RegisterGuidNoThrow(guid).Value;

	public ComResult<Guid> GetGuidNoThrow(TFGuidAtom atom)
		=> new(_obj.GetGUID(atom, out var x), x);

	public Guid GetGuid(TFGuidAtom atom)
		=> GetGuidNoThrow(atom).Value;

	public ComResult<bool> IsEqualTfGuidAtomNoThrow(TFGuidAtom atom, in Guid guid)
		=> new(_obj.IsEqualTfGuidAtom(atom, guid, out var x), x);

	public bool IsEqualTfGuidAtom(TFGuidAtom atom, in Guid guid)
		=> IsEqualTfGuidAtomNoThrow(atom, guid).Value;
}
