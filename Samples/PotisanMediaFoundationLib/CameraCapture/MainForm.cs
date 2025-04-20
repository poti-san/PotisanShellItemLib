#pragma warning disable IDE1006 // ñΩñºÉXÉ^ÉCÉã

using System.Collections.Immutable;

using Potisan.Windows.MediaFoundation;

namespace CameraCapture;

internal sealed partial class MainForm : Form
{
	private readonly ImmutableDictionary<Guid, string> _subTypeDict = MFVideoSubTypeGuids.ToValueToNameDictionary();

	public MainForm()
	{
		InitializeComponent();
	}

	private record class DeviceComboBoxItem(string FriendlyName, string SymbolicLink)
	{
		public override string ToString() => FriendlyName;
	}

	private void MainForm_Load(object sender, EventArgs e)
	{
		var activates = MFActivate.GetVideoDeviceSources();
		foreach (var activate in activates)
		{
			var devSrcAttr = activate.Attributes.ForDeviceSource;
			deviceComboBox.Items.Add(new DeviceComboBoxItem(devSrcAttr.FriendlyName!, devSrcAttr.VideoCaptureSymbolicLink!));
		}

		if (deviceComboBox.Items.Count != 0)
			deviceComboBox.SelectedIndex = 0;
	}

	private record class MediaFormatComboBoxItem(int StreamDescIndex, int MediaTypeIndex, string Text)
	{
		public override string ToString() => Text;
	}

	private void deviceComboBox_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (deviceComboBox.SelectedIndex == -1)
		{
			mediaFormatComboBox.Items.Clear();
			return;
		}

		var item = (DeviceComboBoxItem)deviceComboBox.SelectedItem!;
		var symlink = item.SymbolicLink;
		var device = MFMediaSource.CreateVideoCaptureSource(symlink);

		var presentationDesc = device.MFPresentationDescriptor;
		foreach (var (streamDescIndex, (streamDesc, selected)) in presentationDesc.StreamDescriptorsAndDescriptors.Index())
		{
			var handler = streamDesc.MediaTypeHandler;
			foreach (var (mediaTypeIndex, mediaType) in handler.MediaTypes.Index())
			{
				var mtAttrs = mediaType.Attributes.ForMediaType;
				var size = mtAttrs.FrameSize!.Value;
				var subType = mtAttrs.SubType!.Value;
				mediaFormatComboBox.Items.Add(new MediaFormatComboBoxItem(
					streamDescIndex, mediaTypeIndex, $"{size.Width}x{size.Height} {_subTypeDict[subType]}"));
			}
		}

		if (mediaFormatComboBox.Items.Count != 0)
			mediaFormatComboBox.SelectedIndex = 0;
	}

	private MFSourceReader? _reader;
	private uint _width, _height;

	private void mediaFormatComboBox_SelectedIndexChanged(object sender, EventArgs e)
	{
		var deviceInfo = (DeviceComboBoxItem)deviceComboBox.SelectedItem!;
		var symlink = deviceInfo.SymbolicLink;
		var device = MFMediaSource.CreateVideoCaptureSource(symlink);
		try
		{
			var mediaFormatInfo = (MediaFormatComboBoxItem)mediaFormatComboBox.SelectedItem!;

			using var presentationDesc = device.MFPresentationDescriptor;
			using var streamDesc = presentationDesc.GetStreamDescriptorByIndex((uint)mediaFormatInfo.StreamDescIndex);
			using var mediaType = streamDesc.MediaTypeHandler.GetMediaTypeAt((uint)mediaFormatInfo.MediaTypeIndex);
			streamDesc.MediaTypeHandler.CurrentMediaType = mediaType;

			var rgbMediaType = mediaType.CreateForRgb32Bitmap();
			(_width, _height) = rgbMediaType.Attributes.ForMediaType.FrameSize!.Value;

			_reader = MFSourceReader.CreateFromMediaSourceWithAdvancedVideoProcessing(device);
			_reader.SetCurrentMediaType(MFSourceReaderIndex.FirstVideoStream, rgbMediaType);
		}
		catch
		{
			device.Shutdown();
			throw;
		}

		UpdateSnapshot();
	}

	private void UpdateSnapshot()
	{
		if (deviceComboBox.SelectedIndex == -1 || _reader == null)
		{
			pictureBox1.Image = null;
			return;
		}

		for (var i = 0; i < 100; i++)
		{
			var result = _reader.ReadSample(MFSourceReaderIndex.FirstVideoStream);
			if (result.Sample.WrappedObject == null) continue;
			pictureBox1.Image = result.Sample.CreateBitmap32bppRgbFromMFSample((int)_width, (int)_height);
			break;
		}
	}

	private void timer1_Tick(object sender, EventArgs e)
	{
		UpdateSnapshot();
	}
}
