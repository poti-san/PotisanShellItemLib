namespace Potisan.Windows.PropertySystem;

/// <summary>
/// プロパティキー。
/// </summary>
/// <param name="FmtID">名前空間。</param>
/// <param name="PID"></param>
/// <example>
/// <code><![CDATA[
/// using Potisan.Windows.PropertySystem;
///
/// var key = PropertyKey.CreateFromCanonicalName("System.Keywords");
/// Console.WriteLine(key.ToStringRaw());
/// // {f29f85e0-4ff9-1068-ab91-08002b27b3d9} 5
/// ]]></code>
/// </example>
/// <remarks>
/// <c>PROPERTYKEY</c>ネイティブ型のラッパーです。
/// </remarks>
[StructLayout(LayoutKind.Sequential)]
public sealed record class PropertyKey(Guid FmtID, uint PID)
{
	public PropertyKey() : this(new(), 0) { }

	public override string ToString()
	{
		if (CanonicalNameNoThrow is { } cr && cr)
		{
			return cr.Value;
		}
		else
		{
			return ToStringRaw();
		}
	}

	public static ComResult<PropertyKey> CreateFromCanonicalNameNoThrow(string canonicalName)
	{
		[DllImport("propsys.dll", CharSet = CharSet.Unicode)]
		static extern int PSGetPropertyKeyFromName(string pszName, [Out] PropertyKey ppropkey);

		var x = new PropertyKey();
		return new(PSGetPropertyKeyFromName(canonicalName, x), x);
	}

	public static PropertyKey CreateFromCanonicalName(string canonicalName)
		=> CreateFromCanonicalNameNoThrow(canonicalName).Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<string> CanonicalNameNoThrow
	{
		get
		{
			[DllImport("propsys.dll", CharSet = CharSet.Unicode)]
			static extern int PSGetNameFromPropertyKey(PropertyKey propkey, out string ppszCanonicalName);

			return new(PSGetNameFromPropertyKey(this, out var x), x);
		}
	}
	public string CanonicalName
		=> CanonicalNameNoThrow.Value;

	public string ToStringRaw()
		=> $"{FmtID:B} {PID}";
}
