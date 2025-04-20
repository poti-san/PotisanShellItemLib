#pragma warning disable IDE1006 // ñΩñºÉXÉ^ÉCÉã

using Potisan.Windows.MediaFoundation;

namespace VideoFileThumbnail;

internal sealed partial class MainForm : Form
{
	private readonly MFSourceReader _reader;
	private readonly MFMediaType _inputMediaType;

	public MainForm()
	{
		InitializeComponent();

		var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "test.mp4");
		var stream = MFByteStream.OpenFileToRead(path);
		var reader = MFSourceReader.CreateFromByteStreamWithAdvancedVideoProcessing(stream);
		var inputMediaType = reader.FirstVideoStreamCurrentMediaType;
		var outputMediaType = inputMediaType.CreateWithSameAttributes();
		outputMediaType.Attributes.ForMediaType.SubType = MFImageSubTypeGuids.Rgb32;
		reader.FirstVideoStreamCurrentMediaType = outputMediaType;
		reader.FirstVideoStreamSelection = true;

		_reader = reader;
		_inputMediaType = inputMediaType;
	}

	private void MainForm_Load(object sender, EventArgs e)
	{
		trackBar1.Maximum = checked((int)_reader.Duration);

		UpdateThumbnail();
	}

	public void UpdateThumbnail()
	{
		var exBmp = pictureBox1.Image;
		var result = _reader.ReadSample(MFSourceReaderIndex.FirstVideoStream, 0);
		var (w, h) = _inputMediaType.Attributes.ForMediaType.FrameSize!.Value;
		pictureBox1.Image = result.Sample.CreateBitmap32bppRgbFromMFSample((int)w, (int)h);
		pictureBox1.Refresh();
		exBmp?.Dispose();
	}

	private void trackBar1_ValueChanged(object sender, EventArgs e)
	{
		_reader.SetCurrentPosition(trackBar1.Value);
		UpdateThumbnail();
	}
}
