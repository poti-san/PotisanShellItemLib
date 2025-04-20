using Potisan.Windows.MediaFoundation;

namespace VideoFileThumbnail;

internal static class Program
{
	[STAThread]
	static void Main()
	{
		ApplicationConfiguration.Initialize();
		MF.Startup();
		Application.Run(new MainForm());
	}
}