using Potisan.Windows.PropertySystem.ComTypes;

namespace Potisan.Windows.PropertySystem;

/// <summary>
/// プロパティシステム。プロパティ情報を管理します。
/// </summary>
/// <param name="o">RCWオブジェクト。</param>
/// <example>
/// <code><![CDATA[
/// var propsys = PropertySystem.Create();
/// ]]></code>
/// </example>
/// <remarks><c>IPropertySystem</c> COMインターフェイスのラッパーです。</remarks>
public class PropertySystem(object? o) : ComUnknownWrapperBase<IPropertySystem>(o)
{
	/// <summary>
	/// システムのプロパティストアを作成します。
	/// </summary>
	/// <returns>システムのプロパティストア。</returns>
	public static ComResult<PropertySystem> CreateNoThrow()
	{
		[DllImport("propsys.dll")]
		static extern int PSGetPropertySystem(in Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object? ppv);

		return new(PSGetPropertySystem(typeof(IPropertySystem).GUID, out var x), new(x));
	}

	/// <inheritdoc cref="CreateNoThrow"/>
	public static PropertySystem Create() => CreateNoThrow().Value;

	/// <summary>
	/// プロパティキーに対応するプロパティ記述子を取得します。
	/// </summary>
	/// <param name="key">プロパティキー。</param>
	/// <returns>プロパティ記述子</returns>
	public ComResult<PropertyDescription> GetPropertyDescriptionNoThrow(PropertyKey key)
		=> new(_obj.GetPropertyDescription(key, typeof(IPropertyDescription).GUID, out var x), new(x));

	/// <inheritdoc cref="GetPropertyDescriptionNoThrow(PropertyKey)"/>
	public PropertyDescription GetPropertyDescription(PropertyKey key)
		=> GetPropertyDescriptionNoThrow(key).Value;

	/// <summary>
	/// 既知の名前に対応するプロパティ記述子を取得します。
	/// </summary>
	/// <param name="canonicalName">プロパティキー。</param>
	/// <returns>プロパティ記述子</returns>
	public ComResult<PropertyDescription> GetPropertyDescriptionNoThrow(string canonicalName)
		=> new(_obj.GetPropertyDescriptionByName(canonicalName, typeof(IPropertyDescription).GUID, out var x), new(x));

	/// <inheritdoc cref="GetPropertyDescriptionNoThrow(string)"/>
	public PropertyDescription GetPropertyDescription(string canonicalName)
		=> GetPropertyDescriptionNoThrow(canonicalName).Value;

	/// <summary>
	/// プロパティリスト文字列に対応するプロパティ記述子リストを取得します。
	/// </summary>
	/// <param name="propList">プロパティリスト文字列。</param>
	/// <returns>プロパティリスト。</returns>
	public ComResult<PropertyDescriptionList> GetPropertyDescriptionListNoThrow(string propList)
		=> new(_obj.GetPropertyDescriptionListFromString(propList, typeof(IPropertyDescriptionList).GUID, out var x), new(x));

	/// <inheritdoc cref="GetPropertyDescriptionListNoThrow(string)"/>
	public PropertyDescriptionList GetPropertyDescriptionList(string propList)
		=> GetPropertyDescriptionListNoThrow(propList).Value;

	/// <summary>
	/// <c>PropDescEnumFilter</c>でフィルターされたプロパティ記述子リストを取得します。
	/// </summary>
	/// <param name="filterOn">適用するフィルター。</param>
	/// <returns>プロパティリスト。</returns>
	public ComResult<PropertyDescriptionList> GetPropertyDescriptionListNoThrow(PropDescEnumFilter filterOn)
		=> new(_obj.EnumeratePropertyDescriptions(filterOn, typeof(IPropertyDescriptionList).GUID, out var x), new(x));

	/// <inheritdoc cref="GetPropertyDescriptionListNoThrow(PropDescEnumFilter)"/>
	public PropertyDescriptionList GetPropertyDescriptionList(PropDescEnumFilter filterOn)
		=> GetPropertyDescriptionListNoThrow(filterOn).Value;

	/// <summary>
	/// 全てのプロパティを含むプロパティ記述子。
	/// </summary>
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<PropertyDescriptionList> AllPropertyDescriptionListNoThrow
		=> GetPropertyDescriptionListNoThrow(PropDescEnumFilter.All);
	/// <inheritdoc cref="AllPropertyDescriptionListNoThrow"/>
	public PropertyDescriptionList AllPropertyDescriptionList
		=> GetPropertyDescriptionList(PropDescEnumFilter.All);

	/// <summary>
	/// システムプロパティを含むプロパティ記述子。
	/// </summary>
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<PropertyDescriptionList> SystemPropertyDescriptionListNoThrow
		=> GetPropertyDescriptionListNoThrow(PropDescEnumFilter.System);

	/// <inheritdoc cref="SystemPropertyDescriptionListNoThrow"/>
	public PropertyDescriptionList SystemPropertyDescriptionList
		=> GetPropertyDescriptionList(PropDescEnumFilter.System);

	/// <summary>
	/// 非システムプロパティを含むプロパティ記述子。
	/// </summary>
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<PropertyDescriptionList> NonSystemPropertyDescriptionListNoThrow
		=> GetPropertyDescriptionListNoThrow(PropDescEnumFilter.NonSystem);

	/// <inheritdoc cref="NonSystemPropertyDescriptionListNoThrow"/>
	public PropertyDescriptionList NonSystemPropertyDescriptionList
		=> GetPropertyDescriptionList(PropDescEnumFilter.NonSystem);

	/// <summary>
	/// プロパティを表示用に書式化します。
	/// </summary>
	/// <param name="key">プロパティキー。</param>
	/// <param name="value">プロパティ値。</param>
	/// <param name="flags">書式化方法。</param>
	/// <returns>書式化された文字列。</returns>
	public ComResult<string> FormatForDisplayNoThrow(PropertyKey key, PropVariant value, PropDescFormatFlag flags)
		=> new(_obj.FormatForDisplayAlloc(key, value, flags, out var x), x!);

	/// <inheritdoc cref="FormatForDisplayNoThrow(PropertyKey, PropVariant, PropDescFormatFlag)"/>
	public string FormatForDisplay(PropertyKey key, PropVariant value, PropDescFormatFlag flags)
		=> FormatForDisplayNoThrow(key, value, flags).Value;

	/// <summary>
	/// プロパティスキーマを登録します。
	/// </summary>
	/// <param name="path">プロパティスキーマファイルのパス。</param>
	public ComResult RegisterPropertySchemaNoThrow(string path)
		=> new(_obj.RegisterPropertySchema(path));

	/// <inheritdoc cref="RegisterPropertySchemaNoThrow(string)"/>
	public void RegisterPropertySchema(string path)
		=> RegisterPropertySchemaNoThrow(path).ThrowIfError();

	/// <summary>
	/// プロパティスキーマを登録解除します。
	/// </summary>
	/// <param name="path">プロパティスキーマファイルのパス。</param>
	public ComResult UnregisterPropertySchemaNoThrow(string path)
		=> new(_obj.UnregisterPropertySchema(path));

	/// <inheritdoc cref="UnregisterPropertySchemaNoThrow(string)"/>
	public void UnregisterPropertySchema(string path)
		=> UnregisterPropertySchemaNoThrow(path).ThrowIfError();

	/// <summary>
	/// プロパティスキーマを更新します。
	/// </summary>
	public ComResult RefreshPropertySchemaNoThrow()
		=> new(_obj.RefreshPropertySchema());

	/// <inheritdoc cref="RefreshPropertySchemaNoThrow"/>
	public void RefreshPropertySchema()
		=> RefreshPropertySchemaNoThrow().ThrowIfError();

	/// <summary>
	/// システム及び非システムプロパティキーのイテレーター。
	/// </summary>
	public IEnumerable<PropertyKey> AllPropertyKeys
	{
		get
		{
			using var propdescs = AllPropertyDescriptionList;
			return [.. propdescs.Items.Select(propdesc =>
			{
				using (propdesc)
				{
					return propdesc.PropertyKey;
				}
			})];
		}
	}

	/// <summary>
	/// システムプロパティキーのイテレーター。
	/// </summary>
	public IEnumerable<PropertyKey> SystemPropertyKeys
	{
		get
		{
			using var propdescs = SystemPropertyDescriptionList;
			return [.. propdescs.Items.Select(propdesc =>
			{
				using (propdesc)
				{
					return propdesc.PropertyKey;
				}
			})];
		}
	}

	/// <summary>
	/// 非システムプロパティキーのイテレーター。
	/// </summary>
	public IEnumerable<PropertyKey> NonSystemPropertyKeys
	{
		get
		{
			using var propdescs = NonSystemPropertyDescriptionList;
			return [.. propdescs.Items.Select(propdesc =>
			{
				using (propdesc)
				{
					return propdesc.PropertyKey;
				}
			})];
		}
	}
}