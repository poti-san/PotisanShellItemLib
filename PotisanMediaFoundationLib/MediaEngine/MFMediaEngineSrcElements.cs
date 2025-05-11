using Potisan.Windows.MediaFoundation.MediaEngine.ComTypes;

namespace Potisan.Windows.MediaFoundation.MediaEngine;

// TODO GetUrl等の列挙対応
public class MFMediaEngineSrcElements(object? o) : ComUnknownWrapperBase<IMFMediaEngineSrcElements>(o)
{
	public uint Length
		=> _obj.GetLength();

	public ComResult<string> GetUrlNoThrow(uint index)
		=> new(_obj.GetURL(index, out var x), x!);

	public string GetUrl(uint index)
		=> GetUrlNoThrow(index).Value;

	public ComResult<string> GetTypeNoThrow(uint index)
		=> new(_obj.GetURL(index, out var x), x!);

	public string GetType(uint index)
		=> GetTypeNoThrow(index).Value;

	public ComResult<string> GetMediaNoThrow(uint index)
		=> new(_obj.GetURL(index, out var x), x!);

	public string GetMedia(uint index)
		=> GetMediaNoThrow(index).Value;

	public ComResult AddElementNoThrow(string? url, string? type, string? media)
		=> new(_obj.AddElement(url, type, media));

	public void AddElement(string? url, string? type, string? media)
		=> AddElementNoThrow(url, type, media).ThrowIfError();

	public ComResult RemoveAllElementsNoThrow()
		=> new(_obj.RemoveAllElements());

	public void RemoveAllElements()
		=> RemoveAllElementsNoThrow().ThrowIfError();
}
