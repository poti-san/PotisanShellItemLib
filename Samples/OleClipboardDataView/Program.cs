namespace OleClipboardDataView;

internal static class Program
{
	[STAThread]
	static void Main()
	{
		ApplicationConfiguration.Initialize();
		ToolStripManager.Renderer = new ToolStripSystemRenderer();
		Application.Run(new MainForm());
	}
}