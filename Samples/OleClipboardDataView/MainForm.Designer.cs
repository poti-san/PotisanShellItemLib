namespace OleClipboardDataView;

partial class MainForm
{
	/// <summary>
	///  Required designer variable.
	/// </summary>
	private System.ComponentModel.IContainer components = null;

	/// <summary>
	///  Clean up any resources being used.
	/// </summary>
	/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	protected override void Dispose(bool disposing)
	{
		if (disposing && (components != null))
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	#region Windows Form Designer generated code

	/// <summary>
	///  Required method for Designer support - do not modify
	///  the contents of this method with the code editor.
	/// </summary>
	private void InitializeComponent()
	{
		listView1 = new ListView();
		columnHeader1 = new ColumnHeader();
		columnHeader2 = new ColumnHeader();
		columnHeader3 = new ColumnHeader();
		columnHeader4 = new ColumnHeader();
		columnHeader5 = new ColumnHeader();
		columnHeader6 = new ColumnHeader();
		columnHeader7 = new ColumnHeader();
		columnHeader8 = new ColumnHeader();
		menuStrip1 = new MenuStrip();
		fetchClipboardToolStripMenuItem = new ToolStripMenuItem();
		resizeColumnsToolStripMenuItem = new ToolStripMenuItem();
		splitContainer1 = new SplitContainer();
		tableLayoutPanel1 = new TableLayoutPanel();
		panel1 = new Panel();
		textBox1 = new TextBox();
		label1 = new Label();
		panel2 = new Panel();
		textBox2 = new TextBox();
		label2 = new Label();
		panel3 = new Panel();
		textBox3 = new TextBox();
		label3 = new Label();
		menuStrip1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
		splitContainer1.Panel1.SuspendLayout();
		splitContainer1.Panel2.SuspendLayout();
		splitContainer1.SuspendLayout();
		tableLayoutPanel1.SuspendLayout();
		panel1.SuspendLayout();
		panel2.SuspendLayout();
		panel3.SuspendLayout();
		SuspendLayout();
		// 
		// listView1
		// 
		listView1.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2, columnHeader3, columnHeader4, columnHeader5, columnHeader6, columnHeader7, columnHeader8 });
		listView1.Dock = DockStyle.Fill;
		listView1.FullRowSelect = true;
		listView1.GridLines = true;
		listView1.HeaderStyle = ColumnHeaderStyle.Nonclickable;
		listView1.Location = new Point(0, 0);
		listView1.Name = "listView1";
		listView1.Size = new Size(800, 266);
		listView1.TabIndex = 0;
		listView1.UseCompatibleStateImageBehavior = false;
		listView1.View = View.Details;
		listView1.SelectedIndexChanged += listView1_SelectedIndexChanged;
		// 
		// columnHeader1
		// 
		columnHeader1.Text = "ClipboardFormat";
		// 
		// columnHeader2
		// 
		columnHeader2.Text = "Supported Tymed";
		// 
		// columnHeader3
		// 
		columnHeader3.Text = "Aspect";
		// 
		// columnHeader4
		// 
		columnHeader4.Text = "Index";
		// 
		// columnHeader5
		// 
		columnHeader5.Text = "TargetDevicePointer";
		// 
		// columnHeader6
		// 
		columnHeader6.Text = "Data Tymed";
		// 
		// columnHeader7
		// 
		columnHeader7.Text = "Data Byte Size";
		// 
		// columnHeader8
		// 
		columnHeader8.Text = "UnknownPointerForRelease";
		// 
		// menuStrip1
		// 
		menuStrip1.Items.AddRange(new ToolStripItem[] { fetchClipboardToolStripMenuItem, resizeColumnsToolStripMenuItem });
		menuStrip1.Location = new Point(0, 0);
		menuStrip1.Name = "menuStrip1";
		menuStrip1.Size = new Size(800, 24);
		menuStrip1.TabIndex = 1;
		menuStrip1.Text = "menuStrip1";
		// 
		// fetchClipboardToolStripMenuItem
		// 
		fetchClipboardToolStripMenuItem.Name = "fetchClipboardToolStripMenuItem";
		fetchClipboardToolStripMenuItem.Size = new Size(67, 20);
		fetchClipboardToolStripMenuItem.Text = "手動更新";
		fetchClipboardToolStripMenuItem.Click += fetchClipboardToolStripMenuItem_Click;
		// 
		// resizeColumnsToolStripMenuItem
		// 
		resizeColumnsToolStripMenuItem.Name = "resizeColumnsToolStripMenuItem";
		resizeColumnsToolStripMenuItem.Size = new Size(81, 20);
		resizeColumnsToolStripMenuItem.Text = "カラム幅調整";
		resizeColumnsToolStripMenuItem.Click += resizeColumnsToolStripMenuItem_Click;
		// 
		// splitContainer1
		// 
		splitContainer1.Dock = DockStyle.Fill;
		splitContainer1.Location = new Point(0, 24);
		splitContainer1.Name = "splitContainer1";
		splitContainer1.Orientation = Orientation.Horizontal;
		// 
		// splitContainer1.Panel1
		// 
		splitContainer1.Panel1.Controls.Add(listView1);
		// 
		// splitContainer1.Panel2
		// 
		splitContainer1.Panel2.Controls.Add(tableLayoutPanel1);
		splitContainer1.Size = new Size(800, 426);
		splitContainer1.SplitterDistance = 266;
		splitContainer1.SplitterWidth = 8;
		splitContainer1.TabIndex = 2;
		// 
		// tableLayoutPanel1
		// 
		tableLayoutPanel1.ColumnCount = 3;
		tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333F));
		tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333F));
		tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333F));
		tableLayoutPanel1.Controls.Add(panel1, 0, 0);
		tableLayoutPanel1.Controls.Add(panel2, 1, 0);
		tableLayoutPanel1.Controls.Add(panel3, 2, 0);
		tableLayoutPanel1.Dock = DockStyle.Fill;
		tableLayoutPanel1.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;
		tableLayoutPanel1.Location = new Point(0, 0);
		tableLayoutPanel1.Name = "tableLayoutPanel1";
		tableLayoutPanel1.RowCount = 1;
		tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
		tableLayoutPanel1.Size = new Size(800, 152);
		tableLayoutPanel1.TabIndex = 1;
		// 
		// panel1
		// 
		panel1.Controls.Add(textBox1);
		panel1.Controls.Add(label1);
		panel1.Dock = DockStyle.Fill;
		panel1.Location = new Point(3, 3);
		panel1.Name = "panel1";
		panel1.Size = new Size(260, 146);
		panel1.TabIndex = 3;
		// 
		// textBox1
		// 
		textBox1.BackColor = SystemColors.Window;
		textBox1.Dock = DockStyle.Fill;
		textBox1.HideSelection = false;
		textBox1.Location = new Point(0, 15);
		textBox1.Multiline = true;
		textBox1.Name = "textBox1";
		textBox1.ReadOnly = true;
		textBox1.ScrollBars = ScrollBars.Vertical;
		textBox1.Size = new Size(260, 131);
		textBox1.TabIndex = 0;
		// 
		// label1
		// 
		label1.Dock = DockStyle.Top;
		label1.Location = new Point(0, 0);
		label1.Name = "label1";
		label1.Size = new Size(260, 15);
		label1.TabIndex = 1;
		label1.Text = "バイナリ";
		// 
		// panel2
		// 
		panel2.Controls.Add(textBox2);
		panel2.Controls.Add(label2);
		panel2.Dock = DockStyle.Fill;
		panel2.Location = new Point(269, 3);
		panel2.Name = "panel2";
		panel2.Size = new Size(260, 146);
		panel2.TabIndex = 4;
		// 
		// textBox2
		// 
		textBox2.BackColor = SystemColors.Window;
		textBox2.Dock = DockStyle.Fill;
		textBox2.HideSelection = false;
		textBox2.Location = new Point(0, 15);
		textBox2.Multiline = true;
		textBox2.Name = "textBox2";
		textBox2.ReadOnly = true;
		textBox2.ScrollBars = ScrollBars.Vertical;
		textBox2.Size = new Size(260, 131);
		textBox2.TabIndex = 1;
		// 
		// label2
		// 
		label2.Dock = DockStyle.Top;
		label2.Location = new Point(0, 0);
		label2.Name = "label2";
		label2.Size = new Size(260, 15);
		label2.TabIndex = 2;
		label2.Text = "ASCII (Shift-JIS)";
		// 
		// panel3
		// 
		panel3.Controls.Add(textBox3);
		panel3.Controls.Add(label3);
		panel3.Dock = DockStyle.Fill;
		panel3.Location = new Point(535, 3);
		panel3.Name = "panel3";
		panel3.Size = new Size(262, 146);
		panel3.TabIndex = 4;
		// 
		// textBox3
		// 
		textBox3.BackColor = SystemColors.Window;
		textBox3.Dock = DockStyle.Fill;
		textBox3.HideSelection = false;
		textBox3.Location = new Point(0, 15);
		textBox3.Multiline = true;
		textBox3.Name = "textBox3";
		textBox3.ReadOnly = true;
		textBox3.ScrollBars = ScrollBars.Vertical;
		textBox3.Size = new Size(262, 131);
		textBox3.TabIndex = 1;
		// 
		// label3
		// 
		label3.Dock = DockStyle.Top;
		label3.Location = new Point(0, 0);
		label3.Name = "label3";
		label3.Size = new Size(262, 15);
		label3.TabIndex = 2;
		label3.Text = "UTF-16LE";
		// 
		// MainForm
		// 
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(800, 450);
		Controls.Add(splitContainer1);
		Controls.Add(menuStrip1);
		MainMenuStrip = menuStrip1;
		Name = "MainForm";
		Text = "OleClipboardDataView";
		FormClosing += MainForm_FormClosing;
		Load += MainForm_Load;
		menuStrip1.ResumeLayout(false);
		menuStrip1.PerformLayout();
		splitContainer1.Panel1.ResumeLayout(false);
		splitContainer1.Panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
		splitContainer1.ResumeLayout(false);
		tableLayoutPanel1.ResumeLayout(false);
		panel1.ResumeLayout(false);
		panel1.PerformLayout();
		panel2.ResumeLayout(false);
		panel2.PerformLayout();
		panel3.ResumeLayout(false);
		panel3.PerformLayout();
		ResumeLayout(false);
		PerformLayout();
	}

	#endregion

	private ListView listView1;
	private ColumnHeader columnHeader1;
	private ColumnHeader columnHeader2;
	private ColumnHeader columnHeader3;
	private ColumnHeader columnHeader4;
	private ColumnHeader columnHeader5;
	private ColumnHeader columnHeader6;
	private ColumnHeader columnHeader7;
	private ColumnHeader columnHeader8;
	private MenuStrip menuStrip1;
	private ToolStripMenuItem fetchClipboardToolStripMenuItem;
	private ToolStripMenuItem resizeColumnsToolStripMenuItem;
	private SplitContainer splitContainer1;
	private TextBox textBox1;
	private TextBox textBox2;
	private TextBox textBox3;
	private TableLayoutPanel tableLayoutPanel1;
	private Panel panel1;
	private Panel panel2;
	private Panel panel3;
	private Label label1;
	private Label label2;
	private Label label3;
}
