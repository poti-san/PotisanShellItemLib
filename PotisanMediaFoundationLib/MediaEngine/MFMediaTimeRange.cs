using Potisan.Windows.MediaFoundation.MediaEngine.ComTypes;

namespace Potisan.Windows.MediaFoundation.MediaEngine;

public class MFMediaTimeRange(object? o) : ComUnknownWrapperBase<IMFMediaTimeRange>(o)
{
	public uint Length
		=> _obj.GetLength();

	public ComResult<double> GetStartNoThrow(uint index)
		=> new(_obj.GetStart(index, out var x), x);

	public double GetStart(uint index)
		=> GetStartNoThrow(index).Value;

	public ComResult<double> GetEndNoThrow(uint index)
		=> new(_obj.GetEnd(index, out var x), x);

	public double GetEnd(uint index)
		=> GetEndNoThrow(index).Value;

	public bool ContainsTime(double time)
		=> _obj.ContainsTime(time);

	public ComResult AddRangeNoThrow(double startTime, double endTime)
		=> new(_obj.AddRange(startTime, endTime));

	public void AddRange(double startTime, double endTime)
		=> AddRangeNoThrow(startTime, endTime).ThrowIfError();

	public ComResult ClearNoThrow()
		=> new(_obj.Clear());

	public void Clear()
		=> ClearNoThrow().ThrowIfError();
}
