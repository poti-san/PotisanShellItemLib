namespace SystemPropertyInfo;

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
		SuspendLayout();
		// 
		// listView1
		// 
		listView1.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2, columnHeader3, columnHeader4, columnHeader5 });
		listView1.Dock = DockStyle.Fill;
		listView1.FullRowSelect = true;
		listView1.GridLines = true;
		listView1.HeaderStyle = ColumnHeaderStyle.Nonclickable;
		listView1.Location = new Point(0, 0);
		listView1.Name = "listView1";
		listView1.Size = new Size(800, 450);
		listView1.TabIndex = 0;
		listView1.UseCompatibleStateImageBehavior = false;
		listView1.View = View.Details;
		// 
		// columnHeader1
		// 
		columnHeader1.Text = "PKEY.FMTID";
		// 
		// columnHeader2
		// 
		columnHeader2.Text = "PKEY.PID";
		// 
		// columnHeader3
		// 
		columnHeader3.Text = "PKEY既知名";
		// 
		// columnHeader4
		// 
		columnHeader4.Text = "表示名";
		// 
		// columnHeader5
		// 
		columnHeader5.Text = "列挙型";
		// 
		// MainForm
		// 
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(800, 450);
		Controls.Add(listView1);
		Name = "MainForm";
		Text = "SystemPropertyInfo";
		Load += MainForm_Load;
		ResumeLayout(false);
	}

	#endregion

	private ListView listView1;
	private ColumnHeader columnHeader1;
	private ColumnHeader columnHeader2;
	private ColumnHeader columnHeader3;
	private ColumnHeader columnHeader4;
	private ColumnHeader columnHeader5;
}
