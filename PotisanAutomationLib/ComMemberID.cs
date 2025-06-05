namespace Potisan.Windows.Com.Automation;

/// <summary>
/// COM型メンバーの識別子。
/// </summary>
/// <param name="value"></param>
public readonly struct ComMemberID(int value)
{
	public int Value { get; } = value;

	public static ComMemberID ValueID => new(0);
	public static ComMemberID Nil => new(0);
	public static ComMemberID UnknownID => new(-1);
	public static ComMemberID PropertyPutID => new(-3);
	public static ComMemberID NewEnumID => new(-4);
	public static ComMemberID EvaluateID => new(-5);
	public static ComMemberID ConstructorID => new(-6);
	public static ComMemberID DestructorID => new(-7);
	public static ComMemberID CollectID => new(-8);

	public override string ToString()
		=> Value switch
		{
			-8 => "Collect",
			-7 => "Destructor",
			-6 => "Constructor",
			-5 => "Evaluate",
			-4 => "NewEnum",
			-3 => "PropertyPut",
			-1 => "Unknown",
			0 => "Nil | Value",
			>= -999 and <= -500 => $"{Value} (Control)",
			unchecked((int)0x80010000) and <= unchecked((int)0x8001FFFF) => $"{Value} (Control)",
			>= -5499 and <= -5000 => $"{Value} (ActiveX Accessability)",
			>= -2499 and <= -2400 => $"{Value} (VB5)",
			>= -3999 and <= -3900 => $"{Value} (Forms)",
			>= -5550 and <= -5500 => $"{Value} (Forms)",
			>= -1 => $"{Value} (Reserved for Future Use)",
			_ => $"{Value}",
		};
}
