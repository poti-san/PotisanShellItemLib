using Potisan.Windows.Com.ComTypes;

namespace Potisan.Windows.Com;

public class ComUnknown(object? o) : ComUnknownWrapperBase<IUnknown>(o);