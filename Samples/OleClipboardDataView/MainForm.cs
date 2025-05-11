#pragma warning disable IDE1006 // –½–¼ƒXƒ^ƒCƒ‹

using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

using Potisan.Windows.Com;

namespace OleClipboardDataView;

internal sealed partial class MainForm : Form
{
	public MainForm()
	{
		InitializeComponent();
	}

	private void MainForm_Load(object sender, EventArgs e)
	{
		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		static extern bool AddClipboardFormatListener(nint hwnd);

		AddClipboardFormatListener(Handle);

		UpdateListView1();
		listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
	}

	private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
	{
		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		static extern bool RemoveClipboardFormatListener(nint hwnd);

		RemoveClipboardFormatListener(Handle);
	}

	public void UpdateListView1()
	{
		textBox1.Clear();
		textBox2.Clear();
		textBox3.Clear();

		ComDataObject? dataobj;
		try
		{
			dataobj = ComDataObject.GetClipboard();
		}
		catch
		{
			Thread.Sleep(250);
			dataobj = ComDataObject.GetClipboard();
		}

		listView1.BeginUpdate();
		listView1.Items.Clear();
		if (dataobj != null)
		{
			foreach (var fmtetc in dataobj.FormatEtcGetterEnumerable)
			{
				var medium = dataobj.GetData(fmtetc);
				var item = listView1.Items.Add(new ListViewItem([
					$"{fmtetc.ClipboardFormat} ({fmtetc.ClipboardFormat.ToHashNoString()})",
					fmtetc.Tymed.ToString(),
					fmtetc.Aspect.ToString(),
					fmtetc.Index.ToString(CultureInfo.InvariantCulture),
					fmtetc.TargetDevicePointer.ToString(CultureInfo.InvariantCulture),
					medium.Tymed.ToString(),
					medium.GetByteLength().ToString(CultureInfo.InvariantCulture),
					medium.UnknownPointerForRelease.ToString(CultureInfo.InvariantCulture),
				]));
				item.Tag = medium;
			}
		}
		listView1.EndUpdate();
	}

	protected override void WndProc(ref Message m)
	{
		const int WM_CLIPBOARDUPDATE = 0x031D;

		if (m.Msg == WM_CLIPBOARDUPDATE)
		{
			UpdateListView1();
		}

		base.WndProc(ref m);
	}

	private void fetchClipboardToolStripMenuItem_Click(object sender, EventArgs e)
	{
		UpdateListView1();
	}

	private void resizeColumnsToolStripMenuItem_Click(object sender, EventArgs e)
	{
		listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
	}

	private void listView1_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (listView1.SelectedIndices.Count == 0 || listView1.SelectedItems[0].Tag is not ComStorageMedium medium)
		{
			textBox1.Clear();
			textBox2.Clear();
			textBox3.Clear();
			return;
		}

		const int MaxLen = 1024;
		var data = medium.GetBytes();
		var dataPart = data[..Math.Min(data.Length, MaxLen)];
		textBox1.Text = $"{string.Join(",", dataPart)}{(data.Length > MaxLen ? "..." : "")}";
		textBox2.Text = $"{Encoding.ASCII.GetString(dataPart)}{(data.Length > MaxLen ? "..." : "")}";
		textBox3.Text = $"{Encoding.Unicode.GetString(dataPart)}{(data.Length > MaxLen ? "..." : "")}";
	}
}
