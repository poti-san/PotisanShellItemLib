using PotisanShellItemLib.PropertySystem.ComTypes;

namespace PotisanShellItemLib.PropertySystem;

public class PropertySystem : IComUnknownWrapper
{
	private readonly IPropertySystem _obj;

	/// <summary>
	/// RCWインスタンスをラップします。
	/// </summary>
	/// <param name="o">RCWインスタンス。</param>
	public PropertySystem(object? o)
	{
		_obj = o == null ? null! : (IPropertySystem)o;
	}

	/// <inheritdoc/>
	public object? WrappedObject => _obj;

	/// <inheritdoc/>
	public void Dispose()
	{
		if (_obj != null)
			Marshal.FinalReleaseComObject(_obj);
		GC.SuppressFinalize(this);
	}

	public static ComResult<PropertySystem> CreateNoThrow()
	{
		[DllImport("propsys.dll")]
		static extern int PSGetPropertySystem(in Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object? ppv);

		return new(PSGetPropertySystem(typeof(IPropertySystem).GUID, out var x), new(x));
	}
	public static PropertySystem Create() => CreateNoThrow().Value;


	public ComResult<PropertyDescription> GetPropertyDescriptionNoThrow(PropertyKey key)
		=> new(_obj.GetPropertyDescription(key, typeof(IPropertyDescription).GUID, out var x), new(x));
	public PropertyDescription GetPropertyDescription(PropertyKey key) => GetPropertyDescriptionNoThrow(key).Value;

	public ComResult<PropertyDescription> GetPropertyDescriptionNoThrow(string canonicalName)
		=> new(_obj.GetPropertyDescriptionByName(canonicalName, typeof(IPropertyDescription).GUID, out var x), new(x));
	public PropertyDescription GetPropertyDescription(string canonicalName) => GetPropertyDescriptionNoThrow(canonicalName).Value;

	public ComResult<PropertyDescriptionList> GetPropertyDescriptionListNoThrow(string propList)
		=> new(_obj.GetPropertyDescriptionListFromString(propList, typeof(IPropertyDescriptionList).GUID, out var x), new(x));
	public PropertyDescriptionList GetPropertyDescriptionList(string propList) => GetPropertyDescriptionListNoThrow(propList).Value;

	public ComResult<PropertyDescriptionList> GetPropertyDescriptionListNoThrow(PropDescEnumFilter filterOn)
		=> new(_obj.EnumeratePropertyDescriptions(filterOn, typeof(IPropertyDescriptionList).GUID, out var x), new(x));
	public PropertyDescriptionList GetPropertyDescriptionList(PropDescEnumFilter filterOn) => GetPropertyDescriptionListNoThrow(filterOn).Value;

	public ComResult<PropertyDescriptionList> AllPropertyDescriptionListNoThrow => GetPropertyDescriptionListNoThrow(PropDescEnumFilter.All);
	public PropertyDescriptionList AllPropertyDescriptionList => GetPropertyDescriptionList(PropDescEnumFilter.All);

	public ComResult<PropertyDescriptionList> SystemPropertyDescriptionListNoThrow => GetPropertyDescriptionListNoThrow(PropDescEnumFilter.System);
	public PropertyDescriptionList SystemPropertyDescriptionList => GetPropertyDescriptionList(PropDescEnumFilter.System);

	public ComResult<PropertyDescriptionList> NonSystemPropertyDescriptionListNoThrow => GetPropertyDescriptionListNoThrow(PropDescEnumFilter.NonSystem);
	public PropertyDescriptionList NonSystemPropertyDescriptionList => GetPropertyDescriptionList(PropDescEnumFilter.NonSystem);

	public ComResult<string> FormatForDisplayNoThrow(PropertyKey key, PropVariant value, PROPDESC_FORMAT_FLAGS flags)
		=> new(_obj.FormatForDisplayAlloc(key, value, flags, out var x), x);
	public string FormatForDisplay(PropertyKey key, PropVariant value, PROPDESC_FORMAT_FLAGS flags)
		=> FormatForDisplayNoThrow(key, value, flags).Value;

	public ComResult RegisterPropertySchemaNoThrow(string path) => new(_obj.RegisterPropertySchema(path));
	public void RegisterPropertySchema(string path) => RegisterPropertySchemaNoThrow(path).ThrowIfError();

	public ComResult UnregisterPropertySchemaNoThrow(string path) => new(_obj.UnregisterPropertySchema(path));
	public void UnregisterPropertySchema(string path) => UnregisterPropertySchemaNoThrow(path).ThrowIfError();

	public ComResult RefreshPropertySchemaNoThrow() => new(_obj.RefreshPropertySchema());
	public void RefreshPropertySchema() => RefreshPropertySchemaNoThrow().ThrowIfError();

	public IEnumerable<PropertyKey> AllPropertyKeys
	{
		get
		{
			using var propdescs = AllPropertyDescriptionList;
			return propdescs.Items.Select(propdesc =>
			{
				using (propdesc)
				{
					return propdesc.PropertyKey;
				}
			}).ToArray();
		}
	}

	public IEnumerable<PropertyKey> SystemPropertyKeys
	{
		get
		{
			using var propdescs = SystemPropertyDescriptionList;
			return propdescs.Items.Select(propdesc =>
			{
				using (propdesc)
				{
					return propdesc.PropertyKey;
				}
			}).ToArray();
		}
	}

	public IEnumerable<PropertyKey> NonSystemPropertyKeys
	{
		get
		{
			using var propdescs = NonSystemPropertyDescriptionList;
			return propdescs.Items.Select(propdesc =>
			{
				using (propdesc)
				{
					return propdesc.PropertyKey;
				}
			}).ToArray();
		}
	}
}