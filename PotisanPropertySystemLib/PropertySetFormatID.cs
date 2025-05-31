namespace Potisan.Windows.PropertySystem;

public record struct PropertySetFormatID(Guid Value)
{
	public override readonly string ToString()
	{
		if (Value == KnownPropertySetFormatID.SummaryInformation)
			return "SummaryInformation";
		else if (Value == KnownPropertySetFormatID.DocSummaryInformation)
			return "DocSummaryInformation";
		else if (Value == KnownPropertySetFormatID.UserDefinedProperties)
			return "UserDefinedProperties";
		else
			return Value.ToString("B");
	}
}

/// <summary>
/// 定義済みの<c>FMTID_*</c>定数。
/// </summary>
public static class KnownPropertySetFormatID
{
	// https://learn.microsoft.com/ja-jp/windows/win32/stg/predefined-property-set-format-identifiers
	public static Guid SummaryInformation => new("F29F85E0-4FF9-1068-AB91-08002B27B3D9");
	public static Guid DocSummaryInformation => new("D5CDD502-2E9C-101B-9397-08002B2CF9AE");
	public static Guid UserDefinedProperties => new("D5CDD505-2E9C-101B-9397-08002B2CF9AE");
	//public static Guid DiscardableInformation = new();
	//public static Guid ImageSummaryInformation = new();
	//public static Guid AudioSummaryInformation = new();
	//public static Guid VideoSummaryInformation = new();
	//public static Guid MediaFileSummaryInformation = new();
}