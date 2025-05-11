using System.Globalization;

using Potisan.Windows.PropertySystem;

namespace SystemPropertyInfo;

internal sealed partial class MainForm : Form
{
	private readonly PropertySystem _propSystem;

	public MainForm()
	{
		InitializeComponent();

		_propSystem = PropertySystem.Create();
	}

	private void MainForm_Load(object sender, EventArgs e)
	{
		listView1.BeginUpdate();
		listView1.Items.Clear();
		listView1.Items.AddRange([.. _propSystem.AllPropertyDescriptionList
			.Select(desc => new ListViewItem([
				desc.PropertyKey.FmtID.ToString(),
				desc.PropertyKey.PID.ToString(CultureInfo.InvariantCulture),
				desc.PropertyKey.CanonicalNameNoThrow.ValueUnchecked,
				desc.DisplayNameNoThrow.ValueUnchecked,
				string.Join(", ", desc.GetEnumTypeListNoThrow().ValueUnchecked?.Select(enumType => enumType.DisplayTextNoThrow.ValueUnchecked) ?? []),
			]))]);
		listView1.EndUpdate();

		listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
	}
}
