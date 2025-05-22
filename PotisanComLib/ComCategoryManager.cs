using System.Collections.Immutable;

using Potisan.Windows.Com.ComTypes;

namespace Potisan.Windows.Com;

/// <summary>
/// COMコンポーネントカテゴリマネージャー。
/// </summary>
/// <param name="o">RCWオブジェクト。</param>
/// <remarks>
/// <c>ICatRegister</c> COMインターフェイスと<c>ICatInformation</c> COMインターフェイスのラッパーです。
/// </remarks>
public sealed class ComCategoryManager(object? o) : ComUnknownWrapperBase<ICatInformation>(o)
{
	private readonly ICatRegister _register = o == null ? null! : (ICatRegister)o;

	public static ComResult<ComCategoryManager> CreateNoThrow()
	{
		Guid CLSID_StdComponentCategoriesMgr = new("0002E005-0000-0000-C000-000000000046");
		return ComHelper.CreateInstanceNoThrow<ComCategoryManager, ICatInformation>(CLSID_StdComponentCategoriesMgr);
	}

	public static ComCategoryManager Create()
		=> CreateNoThrow().Value;

	public override void Dispose()
	{
		base.Dispose();
		// 実際にはbaseで解放済みだが、インスタンスの状態を変えたい。
		Marshal.FinalReleaseComObject(_register);
	}

	public ComResult<ComCategoryEnumerable> GetCategoryEnumerableNoThrow(Lcid lcid)
		=> new(_obj.EnumCategories(lcid.Value, out var x), new(x));

	public ComCategoryEnumerable GetCategoryEnumerable(Lcid lcid)
		=> GetCategoryEnumerableNoThrow(lcid).Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<ComCategoryEnumerable> CategoryEnumerableNoThrow
		=> GetCategoryEnumerableNoThrow(Lcid.UserDefault);

	public ComCategoryEnumerable CategoryEnumerable
		=> CategoryEnumerableNoThrow.Value;

	public ImmutableArray<ComCategoryInfo> Categories
		=> [.. CategoryEnumerable];

	public ComResult<string> GetCategoryDescNoThrow(in Guid categoryId, Lcid? lcid)
		=> new(_obj.GetCategoryDesc(categoryId, (lcid ?? Lcid.UserDefault).Value, out var x), x);

	public string GetCategoryDesc(in Guid categoryId, Lcid? lcid)
		=> GetCategoryDescNoThrow(categoryId, lcid).Value;

	public ComResult<ComGuidEnumerable> GetCategoryClassEnumerableNoThrow(ReadOnlySpan<Guid> implemented, ReadOnlySpan<Guid> required)
		=> new(_obj.EnumClassesOfCategories(
			(uint)implemented.Length, MemoryMarshal.GetReference(implemented),
			(uint)required.Length, MemoryMarshal.GetReference(required),
			out var x), new(x));

	public ComGuidEnumerable GetCategoryClassEnumerable(ReadOnlySpan<Guid> implemented, ReadOnlySpan<Guid> required)
		=> GetCategoryClassEnumerableNoThrow(implemented, required).Value;

	public ComResult<bool> IsCategoryClassNoThrow(in Guid clsid, ReadOnlySpan<Guid> implemented, ReadOnlySpan<Guid> required)
		=> ComResult.HRSuccess(_obj.IsClassOfCategories(clsid,
			(uint)implemented.Length, MemoryMarshal.GetReference(implemented),
			(uint)required.Length, MemoryMarshal.GetReference(required)));

	public bool IsCategoryClass(in Guid clsid, ReadOnlySpan<Guid> implemented, ReadOnlySpan<Guid> required)
		=> IsCategoryClassNoThrow(clsid, implemented, required).Value;

	public ComResult<ComGuidEnumerable> GetCategoryClassImplementedEnumerableNoThrow(in Guid clsid)
		=> new(_obj.EnumImplCategoriesOfClass(clsid, out var x), new(x));

	public ComGuidEnumerable GetCCategoryImplementedlassEnumerable(in Guid clsid)
		=> GetCategoryClassImplementedEnumerableNoThrow(clsid).Value;

	public ComResult<ComGuidEnumerable> GetCategoryClassRequiredEnumerableNoThrow(in Guid clsid)
		=> new(_obj.EnumReqCategoriesOfClass(clsid, out var x), new(x));

	public ComGuidEnumerable GetCategoryClassRequiredEnumerable(in Guid clsid)
		=> GetCategoryClassRequiredEnumerableNoThrow(clsid).Value;

	public ComResult RegisterCategoriesNoThrow(ComCategoryInfo[] infos)
		=> new(_register.RegisterCategories((uint)infos.Length, infos));

	public void RegisterCategories(ComCategoryInfo[] infos)
		=> RegisterCategoriesNoThrow(infos).ThrowIfError();

	public ComResult UnregisterCategoriesNoThrow(ReadOnlySpan<Guid> categoryIds)
		=> new(_register.UnRegisterCategories((uint)categoryIds.Length, MemoryMarshal.GetReference(categoryIds)));

	public void UnregisterCategories(ReadOnlySpan<Guid> categoryIds)
		=> UnregisterCategoriesNoThrow(categoryIds).ThrowIfError();

	public ComResult RegisterClassImplementedCategoriesNoThrow(in Guid clsid, ReadOnlySpan<Guid> categoryIds)
		=> new(_register.RegisterClassImplCategories(clsid, (uint)categoryIds.Length, MemoryMarshal.GetReference(categoryIds)));

	public void RegisterClassImplementedCategories(in Guid clsid, ReadOnlySpan<Guid> categoryIds)
		=> RegisterClassImplementedCategoriesNoThrow(clsid, categoryIds).ThrowIfError();

	public ComResult UnRegisterClassImplementedCategoriesNoThrow(in Guid clsid, ReadOnlySpan<Guid> categoryIds)
		=> new(_register.UnRegisterClassImplCategories(clsid, (uint)categoryIds.Length, MemoryMarshal.GetReference(categoryIds)));

	public void UnRegisterClassImplementedCategories(in Guid clsid, ReadOnlySpan<Guid> categoryIds)
		=> UnRegisterClassImplementedCategoriesNoThrow(clsid, categoryIds).ThrowIfError();

	public ComResult RegisterClassRequiredCategoriesNoThrow(in Guid clsid, ReadOnlySpan<Guid> categoryIds)
		=> new(_register.RegisterClassReqCategories(clsid, (uint)categoryIds.Length, MemoryMarshal.GetReference(categoryIds)));

	public void RegisterClassRequiredCategories(in Guid clsid, ReadOnlySpan<Guid> categoryIds)
		=> RegisterClassRequiredCategoriesNoThrow(clsid, categoryIds).ThrowIfError();

	public ComResult UnRegisterClassRequiredCategoriesNoThrow(in Guid clsid, ReadOnlySpan<Guid> categoryIds)
		=> new(_register.UnRegisterClassReqCategories(clsid, (uint)categoryIds.Length, MemoryMarshal.GetReference(categoryIds)));

	public void UnRegisterClassRequiredCategories(in Guid clsid, ReadOnlySpan<Guid> categoryIds)
		=> UnRegisterClassRequiredCategoriesNoThrow(clsid, categoryIds).ThrowIfError();
}
