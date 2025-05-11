using Potisan.Windows.MediaFoundation;

namespace CameraCapture;

internal static class Program
{
	[STAThread]
	static void Main()
	{
		ApplicationConfiguration.Initialize();
		ToolStripManager.Renderer = new ToolStripSystemRenderer();

		MF.Startup();

		Application.Run(new MainForm());
	}
}