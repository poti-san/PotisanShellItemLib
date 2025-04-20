namespace PotisanShellItemLib.PropertySystem;

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
			return $"{FmtID:B} {PID}";
		}
	}

	public static ComResult<PropertyKey> CreateFromCanonicalName(string canonicalName)
	{
		[DllImport("propsys.dll", CharSet = CharSet.Unicode)]
		static extern int PSGetPropertyKeyFromName(string pszName, [Out] PropertyKey ppropkey);

		var x = new PropertyKey();
		return new(PSGetPropertyKeyFromName(canonicalName, x), x);
	}

	public ComResult<string> CanonicalNameNoThrow
	{
		get
		{
			[DllImport("propsys.dll", CharSet = CharSet.Unicode)]
			static extern int PSGetNameFromPropertyKey(PropertyKey propkey, out string ppszCanonicalName);

			return new(PSGetNameFromPropertyKey(this, out var x), x);
		}
	}
	public string CanonicalName => CanonicalNameNoThrow.Value;
}
