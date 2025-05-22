namespace Potisan.Windows.Com;

/// <summary>
/// COMのカテゴリ情報。
/// </summary>
/// <remarks>
/// <c>CATEGORYINFO</c>ネイティブ型のラッパーです。
/// </remarks>
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
public sealed record class ComCategoryInfo
{
	public Guid CategoryID;
	public Lcid Lcid;
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
	public string? Description;

	public void Deconstruct(out Guid catId, out Lcid lcid, out string? desc)
	{
		catId = CategoryID;
		lcid = Lcid;
		desc = Description;
	}
}
