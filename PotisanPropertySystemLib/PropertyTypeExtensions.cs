using Potisan.Windows.Com.ComTypes;
using Potisan.Windows.PropertySystem.ComTypes;

namespace Potisan.Windows.PropertySystem;

/// <summary>
/// プロパティ情報を取得する型の拡張メソッド集。
/// </summary>
public static class PropertyTypeExtensions
{
	public static ComResult<PropertyBag> AsPropertyBagNoThrow(this PropertyStore propStore)
	{
		if (propStore.WrappedObject is not IPropertyBag p)
			return new(CommonHResults.ENoInterface, new(null));
		return new(CommonHResults.SOK, new(p));
	}

	public static PropertyBag AsPropertyBag(this PropertyStore propStore)
		=> AsPropertyBagNoThrow(propStore).Value;

	public static ComResult<PropertyStore> AsPropertyStoreNoThrow(this PropertyBag propBag)
	{
		if (propBag.WrappedObject is not IPropertyStore p)
			return new(CommonHResults.ENoInterface, new(null));
		return new(CommonHResults.SOK, new(p));
	}

	public static PropertyStore AsPropertyStore(this PropertyBag propBag)
		=> AsPropertyStoreNoThrow(propBag).Value;
}
