namespace Potisan.Windows.MediaFoundation;

public interface IMFWithAttributes : IComUnknownWrapper
{
	MFAttributes Attributes { get; }
}