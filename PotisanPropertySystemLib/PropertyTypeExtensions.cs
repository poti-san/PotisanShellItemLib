using Potisan.Windows.Com.ComTypes;
using Potisan.Windows.PropertySystem.ComTypes;

namespace Potisan.Windows.PropertySystem;

/// <summary>
/// プロパティ情報を取得する型の拡張メソッドを提供します。
/// </summary>
public static class PropertyTypeExtensions
{
	public static PropertyBag? AsPropertyBag(this PropertyStore propStore)
		=> propStore.As<PropertyBag, IPropertyBag>();

	public static PropertyStore? AsPropertyStore(this PropertyBag propBag)
		=> propBag.As<PropertyStore, IPropertyStore>();
}
