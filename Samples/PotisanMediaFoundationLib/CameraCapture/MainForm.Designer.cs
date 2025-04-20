namespace CameraCapture;

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
		components = new System.ComponentModel.Container();
		toolStrip1 = new ToolStrip();
		toolStripLabel1 = new ToolStripLabel();
		deviceComboBox = new ToolStripComboBox();
		toolStripLabel2 = new ToolStripLabel();
		mediaFormatComboBox = new ToolStripComboBox();
		pictureBox1 = new PictureBox();
		timer1 = new System.Windows.Forms.Timer(components);
		toolStrip1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
		SuspendLayout();
		// 
		// toolStrip1
		// 
		toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
		toolStrip1.Items.AddRange(new ToolStripItem[] { toolStripLabel1, deviceComboBox, toolStripLabel2, mediaFormatComboBox });
		toolStrip1.Location = new Point(0, 0);
		toolStrip1.Name = "toolStrip1";
		toolStrip1.Size = new Size(800, 25);
		toolStrip1.TabIndex = 0;
		// 
		// toolStripLabel1
		// 
		toolStripLabel1.Name = "toolStripLabel1";
		toolStripLabel1.Size = new Size(63, 22);
		toolStripLabel1.Text = "デバイス(&D):";
		// 
		// deviceComboBox
		// 
		deviceComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
		deviceComboBox.Name = "deviceComboBox";
		deviceComboBox.Size = new Size(200, 25);
		deviceComboBox.SelectedIndexChanged += deviceComboBox_SelectedIndexChanged;
		// 
		// toolStripLabel2
		// 
		toolStripLabel2.Name = "toolStripLabel2";
		toolStripLabel2.Size = new Size(34, 22);
		toolStripLabel2.Text = "形式:";
		// 
		// mediaFormatComboBox
		// 
		mediaFormatComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
		mediaFormatComboBox.Name = "mediaFormatComboBox";
		mediaFormatComboBox.Size = new Size(200, 25);
		mediaFormatComboBox.SelectedIndexChanged += mediaFormatComboBox_SelectedIndexChanged;
		// 
		// pictureBox1
		// 
		pictureBox1.Dock = DockStyle.Fill;
		pictureBox1.Location = new Point(0, 25);
		pictureBox1.Name = "pictureBox1";
		pictureBox1.Size = new Size(800, 425);
		pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
		pictureBox1.TabIndex = 1;
		pictureBox1.TabStop = false;
		// 
		// timer1
		// 
		timer1.Enabled = true;
		timer1.Tick += timer1_Tick;
		// 
		// MainForm
		// 
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(800, 450);
		Controls.Add(pictureBox1);
		Controls.Add(toolStrip1);
		Name = "MainForm";
		Text = "CameraCapture";
		Load += MainForm_Load;
		toolStrip1.ResumeLayout(false);
		toolStrip1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
		ResumeLayout(false);
		PerformLayout();
	}

	#endregion

	private ToolStrip toolStrip1;
	private ToolStripLabel toolStripLabel1;
	private ToolStripComboBox deviceComboBox;
	private ToolStripLabel toolStripLabel2;
	private ToolStripComboBox mediaFormatComboBox;
	private PictureBox pictureBox1;
	private System.Windows.Forms.Timer timer1;
}
