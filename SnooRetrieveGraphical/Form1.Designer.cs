namespace SnooRetrieveGraphical
{
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
			groupBox1 = new GroupBox();
			checkBox1 = new CheckBox();
			LoginPassword = new TextBox();
			LoginUsername = new TextBox();
			label2 = new Label();
			label1 = new Label();
			groupBox2 = new GroupBox();
			LastDate = new DateTimePicker();
			EarliestDate = new DateTimePicker();
			label3 = new Label();
			label4 = new Label();
			groupBox3 = new GroupBox();
			btnHtml = new Button();
			btnCsv = new Button();
			btnJson = new Button();
			lblHtml = new Label();
			lblCsv = new Label();
			lblJson = new Label();
			cbxHtml = new CheckBox();
			cbxCsv = new CheckBox();
			cbxJson = new CheckBox();
			saveFileDialog1 = new SaveFileDialog();
			groupBox4 = new GroupBox();
			ProgressText = new Label();
			RetrieveProgressBar = new ProgressBar();
			BtnStart = new Button();
			groupBox5 = new GroupBox();
			boxAbout = new TextBox();
			groupBox1.SuspendLayout();
			groupBox2.SuspendLayout();
			groupBox3.SuspendLayout();
			groupBox4.SuspendLayout();
			groupBox5.SuspendLayout();
			SuspendLayout();
			// 
			// groupBox1
			// 
			groupBox1.Controls.Add(checkBox1);
			groupBox1.Controls.Add(LoginPassword);
			groupBox1.Controls.Add(LoginUsername);
			groupBox1.Controls.Add(label2);
			groupBox1.Controls.Add(label1);
			groupBox1.Location = new Point(12, 12);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new Size(776, 140);
			groupBox1.TabIndex = 0;
			groupBox1.TabStop = false;
			groupBox1.Text = "Your login details";
			// 
			// checkBox1
			// 
			checkBox1.AutoSize = true;
			checkBox1.Font = new Font("Segoe UI", 7.20000029F, FontStyle.Regular, GraphicsUnit.Point);
			checkBox1.Location = new Point(207, 98);
			checkBox1.Name = "checkBox1";
			checkBox1.Size = new Size(122, 21);
			checkBox1.TabIndex = 3;
			checkBox1.Text = "Show password";
			checkBox1.UseVisualStyleBackColor = true;
			checkBox1.CheckedChanged += checkBox1_CheckedChanged;
			// 
			// LoginPassword
			// 
			LoginPassword.Location = new Point(207, 65);
			LoginPassword.Name = "LoginPassword";
			LoginPassword.PasswordChar = '*';
			LoginPassword.Size = new Size(563, 27);
			LoginPassword.TabIndex = 2;
			// 
			// LoginUsername
			// 
			LoginUsername.Location = new Point(207, 26);
			LoginUsername.Name = "LoginUsername";
			LoginUsername.Size = new Size(563, 27);
			LoginUsername.TabIndex = 1;
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new Point(13, 68);
			label2.Name = "label2";
			label2.Size = new Size(70, 20);
			label2.TabIndex = 0;
			label2.Text = "Password";
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(13, 32);
			label1.Name = "label1";
			label1.Size = new Size(188, 20);
			label1.TabIndex = 0;
			label1.Text = "User name (email address):";
			// 
			// groupBox2
			// 
			groupBox2.Controls.Add(LastDate);
			groupBox2.Controls.Add(EarliestDate);
			groupBox2.Controls.Add(label3);
			groupBox2.Controls.Add(label4);
			groupBox2.Location = new Point(12, 171);
			groupBox2.Name = "groupBox2";
			groupBox2.Size = new Size(776, 115);
			groupBox2.TabIndex = 0;
			groupBox2.TabStop = false;
			groupBox2.Text = "Data to retrieve";
			// 
			// LastDate
			// 
			LastDate.Location = new Point(207, 63);
			LastDate.Name = "LastDate";
			LastDate.Size = new Size(250, 27);
			LastDate.TabIndex = 5;
			// 
			// EarliestDate
			// 
			EarliestDate.Location = new Point(207, 25);
			EarliestDate.Name = "EarliestDate";
			EarliestDate.Size = new Size(250, 27);
			EarliestDate.TabIndex = 4;
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Location = new Point(13, 68);
			label3.Name = "label3";
			label3.Size = new Size(72, 20);
			label3.TabIndex = 0;
			label3.Text = "Last date:";
			// 
			// label4
			// 
			label4.AutoSize = true;
			label4.Location = new Point(13, 32);
			label4.Name = "label4";
			label4.Size = new Size(94, 20);
			label4.TabIndex = 0;
			label4.Text = "Earliest date:";
			// 
			// groupBox3
			// 
			groupBox3.Controls.Add(btnHtml);
			groupBox3.Controls.Add(btnCsv);
			groupBox3.Controls.Add(btnJson);
			groupBox3.Controls.Add(lblHtml);
			groupBox3.Controls.Add(lblCsv);
			groupBox3.Controls.Add(lblJson);
			groupBox3.Controls.Add(cbxHtml);
			groupBox3.Controls.Add(cbxCsv);
			groupBox3.Controls.Add(cbxJson);
			groupBox3.Location = new Point(12, 307);
			groupBox3.Name = "groupBox3";
			groupBox3.Size = new Size(776, 152);
			groupBox3.TabIndex = 0;
			groupBox3.TabStop = false;
			groupBox3.Text = "Results to generate";
			// 
			// btnHtml
			// 
			btnHtml.Enabled = false;
			btnHtml.Location = new Point(717, 105);
			btnHtml.Name = "btnHtml";
			btnHtml.Size = new Size(43, 27);
			btnHtml.TabIndex = 11;
			btnHtml.Text = "...";
			btnHtml.UseVisualStyleBackColor = true;
			btnHtml.Click += btnHtml_Click;
			// 
			// btnCsv
			// 
			btnCsv.Enabled = false;
			btnCsv.Location = new Point(717, 70);
			btnCsv.Name = "btnCsv";
			btnCsv.Size = new Size(43, 27);
			btnCsv.TabIndex = 9;
			btnCsv.Text = "...";
			btnCsv.UseVisualStyleBackColor = true;
			btnCsv.Click += btnCsv_Click;
			// 
			// btnJson
			// 
			btnJson.Enabled = false;
			btnJson.Location = new Point(717, 32);
			btnJson.Name = "btnJson";
			btnJson.Size = new Size(43, 27);
			btnJson.TabIndex = 7;
			btnJson.Text = "...";
			btnJson.UseVisualStyleBackColor = true;
			btnJson.Click += btnJson_Click;
			// 
			// lblHtml
			// 
			lblHtml.BorderStyle = BorderStyle.FixedSingle;
			lblHtml.Enabled = false;
			lblHtml.Location = new Point(276, 105);
			lblHtml.Name = "lblHtml";
			lblHtml.Size = new Size(435, 25);
			lblHtml.TabIndex = 2;
			lblHtml.Text = "-";
			// 
			// lblCsv
			// 
			lblCsv.BorderStyle = BorderStyle.FixedSingle;
			lblCsv.Enabled = false;
			lblCsv.Location = new Point(276, 70);
			lblCsv.Name = "lblCsv";
			lblCsv.Size = new Size(435, 25);
			lblCsv.TabIndex = 2;
			lblCsv.Text = "-";
			// 
			// lblJson
			// 
			lblJson.BorderStyle = BorderStyle.FixedSingle;
			lblJson.Enabled = false;
			lblJson.Location = new Point(276, 34);
			lblJson.Name = "lblJson";
			lblJson.Size = new Size(435, 25);
			lblJson.TabIndex = 2;
			lblJson.Text = "-";
			// 
			// cbxHtml
			// 
			cbxHtml.AutoSize = true;
			cbxHtml.Location = new Point(13, 105);
			cbxHtml.Name = "cbxHtml";
			cbxHtml.Size = new Size(163, 24);
			cbxHtml.TabIndex = 10;
			cbxHtml.Text = "Visual chart (HTML):";
			cbxHtml.UseVisualStyleBackColor = true;
			cbxHtml.CheckedChanged += cbxHtml_CheckedChanged;
			// 
			// cbxCsv
			// 
			cbxCsv.AutoSize = true;
			cbxCsv.Location = new Point(13, 70);
			cbxCsv.Name = "cbxCsv";
			cbxCsv.Size = new Size(189, 24);
			cbxCsv.TabIndex = 8;
			cbxCsv.Text = "CSV (can open in Excel):";
			cbxCsv.UseVisualStyleBackColor = true;
			cbxCsv.CheckedChanged += cbxCsv_CheckedChanged;
			// 
			// cbxJson
			// 
			cbxJson.AutoSize = true;
			cbxJson.Location = new Point(13, 35);
			cbxJson.Name = "cbxJson";
			cbxJson.Size = new Size(238, 24);
			cbxJson.TabIndex = 6;
			cbxJson.Text = "JSON (machine-readable) data:";
			cbxJson.UseVisualStyleBackColor = true;
			cbxJson.CheckedChanged += cbxJson_CheckedChanged;
			// 
			// groupBox4
			// 
			groupBox4.Controls.Add(ProgressText);
			groupBox4.Controls.Add(RetrieveProgressBar);
			groupBox4.Controls.Add(BtnStart);
			groupBox4.Location = new Point(12, 475);
			groupBox4.Name = "groupBox4";
			groupBox4.Size = new Size(776, 152);
			groupBox4.TabIndex = 0;
			groupBox4.TabStop = false;
			groupBox4.Text = "Let's go!";
			// 
			// ProgressText
			// 
			ProgressText.Location = new Point(238, 41);
			ProgressText.Name = "ProgressText";
			ProgressText.Size = new Size(532, 29);
			ProgressText.TabIndex = 2;
			ProgressText.Text = "Click \"retrieve the data\" to connect to your Snoo account.";
			// 
			// RetrieveProgressBar
			// 
			RetrieveProgressBar.Location = new Point(13, 106);
			RetrieveProgressBar.Name = "RetrieveProgressBar";
			RetrieveProgressBar.Size = new Size(757, 29);
			RetrieveProgressBar.TabIndex = 1;
			// 
			// BtnStart
			// 
			BtnStart.Location = new Point(13, 32);
			BtnStart.Name = "BtnStart";
			BtnStart.Size = new Size(203, 38);
			BtnStart.TabIndex = 12;
			BtnStart.Text = "Retrieve the data";
			BtnStart.UseVisualStyleBackColor = true;
			BtnStart.Click += BtnStart_Click;
			// 
			// groupBox5
			// 
			groupBox5.Controls.Add(boxAbout);
			groupBox5.Location = new Point(12, 646);
			groupBox5.Name = "groupBox5";
			groupBox5.Size = new Size(776, 112);
			groupBox5.TabIndex = 0;
			groupBox5.TabStop = false;
			groupBox5.Text = "About";
			// 
			// boxAbout
			// 
			boxAbout.BorderStyle = BorderStyle.None;
			boxAbout.Location = new Point(13, 26);
			boxAbout.Multiline = true;
			boxAbout.Name = "boxAbout";
			boxAbout.ReadOnly = true;
			boxAbout.ScrollBars = ScrollBars.Vertical;
			boxAbout.Size = new Size(757, 75);
			boxAbout.TabIndex = 999;
			boxAbout.TabStop = false;
			// 
			// MainForm
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(800, 770);
			Controls.Add(groupBox5);
			Controls.Add(groupBox4);
			Controls.Add(groupBox3);
			Controls.Add(groupBox2);
			Controls.Add(groupBox1);
			Name = "MainForm";
			Text = "Retrieve Snoo data (VERY UNOFFICIAL)";
			groupBox1.ResumeLayout(false);
			groupBox1.PerformLayout();
			groupBox2.ResumeLayout(false);
			groupBox2.PerformLayout();
			groupBox3.ResumeLayout(false);
			groupBox3.PerformLayout();
			groupBox4.ResumeLayout(false);
			groupBox5.ResumeLayout(false);
			groupBox5.PerformLayout();
			ResumeLayout(false);
		}

		#endregion

		private GroupBox groupBox1;
		private CheckBox checkBox1;
		private TextBox LoginPassword;
		private TextBox LoginUsername;
		private Label label2;
		private Label label1;
		private GroupBox groupBox2;
		private DateTimePicker LastDate;
		private DateTimePicker EarliestDate;
		private Label label3;
		private Label label4;
		private GroupBox groupBox3;
		private Button btnHtml;
		private Button btnCsv;
		private Button btnJson;
		private Label lblHtml;
		private Label lblCsv;
		private Label lblJson;
		private CheckBox cbxHtml;
		private CheckBox cbxCsv;
		private CheckBox cbxJson;
		private SaveFileDialog saveFileDialog1;
		private GroupBox groupBox4;
		private ProgressBar RetrieveProgressBar;
		private Button BtnStart;
		private Label ProgressText;
		private GroupBox groupBox5;
		private TextBox boxAbout;
	}
}