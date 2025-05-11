namespace Potisan.Windows.Com;

/// <summary>
/// CATEGORYINFO
/// </summary>
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
