using Potisan.Windows.Shell.ComTypes;

namespace Potisan.Windows.Shell.ComImplements;

public sealed class EmptyEnumShellItems : IEnumShellItems
{
	public int Next(uint celt, out IShellItem? rgelt, out uint pceltFetched)
	{
		rgelt = null;
		pceltFetched = 0;
		return CommonHResults.SFalse;
	}

	public int Skip(uint celt)
	{
		throw new NotImplementedException();
	}

	public int Reset()
	{
		throw new NotImplementedException();
	}

	public int Clone(out IEnumShellItems? ppenum)
	{
		ppenum = new EmptyEnumShellItems();
		return CommonHResults.SOK;
	}
}
