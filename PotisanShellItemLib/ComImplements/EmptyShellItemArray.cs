using Potisan.Windows.Com.ComTypes;
using Potisan.Windows.PropertySystem;
using Potisan.Windows.Shell.ComTypes;

namespace Potisan.Windows.Shell.ComImplements;

public sealed class EmptyShellItemArray : IShellItemArray
{
	public int BindToHandler(IBindCtx? pbc, in Guid bhid, in Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object ppvOut)
	{
		throw new NotImplementedException();
	}

	public int GetPropertyStore(GetPropertyStoreFlag flags, in Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object ppv)
	{
		throw new NotImplementedException();
	}

	public int GetPropertyDescriptionList(PropertyKey keyType, in Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object ppv)
	{
		throw new NotImplementedException();
	}

	public int GetAttributes(ShellItemAttributeOp AttribFlags, ShellItemAttribute sfgaoMask, out ShellItemAttribute psfgaoAttribs)
	{
		throw new NotImplementedException();
	}

	public int GetCount(out uint pdwNumItems)
	{
		pdwNumItems = 0;
		return CommonHResults.SOK;
	}

	public int GetItemAt(uint dwIndex, out IShellItem? ppsi)
	{
		ppsi = null;
		return CommonHResults.EInvalidArg;
	}

	public int EnumItems(out IEnumShellItems ppenumShellItems)
	{
		ppenumShellItems = new EmptyEnumShellItems();
		return CommonHResults.SOK;
	}
}
