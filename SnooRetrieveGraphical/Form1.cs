using System.Security.Authentication.ExtendedProtection;

namespace SnooRetrieveGraphical
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
		}

		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
			LoginPassword.PasswordChar = checkBox1.Checked ? '\0' : '*';
		}

		private void cbxJson_CheckedChanged(object sender, EventArgs e)
		{
			lblJson.Enabled = cbxJson.Checked;
			btnJson.Enabled = cbxJson.Checked;
			if (string.IsNullOrEmpty(lblJson.Text) || lblJson.Text == "-")
				btnJson_Click(null, null);
		}

		private void cbxCsv_CheckedChanged(object sender, EventArgs e)
		{
			lblCsv.Enabled = cbxCsv.Checked;
			btnCsv.Enabled = cbxCsv.Checked;
			if (string.IsNullOrEmpty(lblCsv.Text) || lblCsv.Text == "-")
				btnCsv_Click(null, null);
		}

		private void cbxHtml_CheckedChanged(object sender, EventArgs e)
		{
			lblHtml.Enabled = cbxHtml.Checked;
			btnHtml.Enabled = cbxHtml.Checked;
			if (string.IsNullOrEmpty(lblHtml.Text) || lblHtml.Text == "-")
				btnHtml_Click(null, null);
		}

		private void btnJson_Click(object sender, EventArgs e)
		{
			saveFileDialog1.FileName = lblJson.Text=="-" ? null : lblJson.Text;
			saveFileDialog1.DefaultExt = "json";
			saveFileDialog1.Filter = "JSON file (*.json)|*.json|All files (*.*)|*.*";
			if (saveFileDialog1.ShowDialog() == DialogResult.OK)
			{
				lblJson.Text = saveFileDialog1.FileName;
				lblJson.TextAlign = ContentAlignment.MiddleRight;
			}
			else
			{
				lblJson.Text = "-";
				cbxJson.Checked = false;
				lblJson.TextAlign = ContentAlignment.MiddleLeft;
			}
		}

		private void btnCsv_Click(object sender, EventArgs e)
		{
			saveFileDialog1.FileName = lblCsv.Text=="-" ? null : lblCsv.Text;
			saveFileDialog1.DefaultExt = "csv";
			saveFileDialog1.Filter = "Comma-separated values (*.csv)|*.csv|All files (*.*)|*.*";
			if (saveFileDialog1.ShowDialog() == DialogResult.OK)
			{
				lblCsv.Text = saveFileDialog1.FileName;
				lblCsv.TextAlign = ContentAlignment.MiddleRight;
			}
			else
			{
				lblCsv.Text = "-";
				cbxCsv.Checked = false;
				lblCsv.TextAlign = ContentAlignment.MiddleLeft;
			}
		}

		private void btnHtml_Click(object sender, EventArgs e)
		{
			saveFileDialog1.FileName = lblHtml.Text=="-" ? null : lblHtml.Text;
			saveFileDialog1.DefaultExt = "html";
			saveFileDialog1.Filter = "HTML chart (*.html)|*.html|All files (*.*)|*.*";
			if (saveFileDialog1.ShowDialog() == DialogResult.OK)
			{
				lblHtml.Text = saveFileDialog1.FileName;
				lblHtml.TextAlign = ContentAlignment.MiddleRight;
			}
			else
			{
				lblHtml.Text = "-";
				cbxHtml.Checked = false;
				lblHtml.TextAlign = ContentAlignment.MiddleLeft;
			}
		}

		private void BtnStart_Click(object sender, EventArgs e)
		{
			// Make sure everything is ok
			if(string.IsNullOrEmpty(LoginUsername.Text) || string.IsNullOrEmpty(LoginPassword.Text))
			{
				MessageBox.Show("Please make sure that the username and password are filled-in.");
				return;
			}
			if(!cbxCsv.Checked && !cbxHtml.Checked && !cbxJson.Checked)
			{
				MessageBox.Show("Please select at least one type of output (JSON, CSV, HTML).");
				return;
			}
			if(EarliestDate.Value.Date > LastDate.Value.Date)
			{
				MessageBox.Show("The \"earliest date\" is later than the \"last date\". Please review and correct them.");
				return;
			}
			var today = DateTime.Now.Date;
			if(EarliestDate.Value.Date >today || LastDate.Value.Date>today)
			{
				MessageBox.Show("The \"earliest date\" and/or \"last date\" is in the future. Please review and correct it.");
				return;
			}
			if((LastDate.Value-EarliestDate.Value).TotalDays>=365)
			{
				if (MessageBox.Show("The date range you've chosen is long (>1 year). This may be slow - each day in the range has to be retrieved individually. Are you sure you want to do this?", "Retrieve", MessageBoxButtons.YesNo) != DialogResult.Yes)
					return;
			}
			if (cbxCsv.Checked && File.Exists(lblCsv.Text) && MessageBox.Show($"The selected CSV output file ({lblCsv}) already exists. Are you sure you want to proceed and overwrite it?", "Retrieve", MessageBoxButtons.YesNo) != DialogResult.Yes)
				return;
			if (cbxJson.Checked && File.Exists(lblJson.Text) && MessageBox.Show($"The selected JSON output file ({lblJson}) already exists. Are you sure you want to proceed and overwrite it?", "Retrieve", MessageBoxButtons.YesNo) != DialogResult.Yes)
				return;
			if (cbxHtml.Checked && File.Exists(lblHtml.Text) && MessageBox.Show($"The selected HTML output file ({lblHtml}) already exists. Are you sure you want to proceed and overwrite it?", "Retrieve", MessageBoxButtons.YesNo) != DialogResult.Yes)
				return;

			var TotalDays = (int)( (LastDate.Value - EarliestDate.Value).TotalDays);
			var _earliest = EarliestDate.Value;

			RetrieveProgressBar.Minimum = 0;
			RetrieveProgressBar.Maximum = TotalDays;
			RetrieveProgressBar.Value = 0;

			try
			{
				var AllData = SnooRetrieve.KeyFunctions.RetrieveAllHistory(LoginUsername.Text, LoginPassword.Text, EarliestDate.Value, LastDate.Value, d =>
				{
					var d1 = new DateTime(d.Year, d.Month, d.Day);
					var val = (int)(d1 - _earliest).TotalDays;
					RetrieveProgressBar.Value = val;
				});

				if (cbxJson.Checked)
					SnooRetrieve.KeyFunctions.DumpAllHistory_JSON(AllData, lblJson.Text);
				if (cbxCsv.Checked)
					SnooRetrieve.KeyFunctions.DumpAllHistory_CSV(AllData, lblCsv.Text);
				if (cbxHtml.Checked)
					SnooRetrieve.KeyFunctions.RenderAll(AllData, lblHtml.Text);
			}
			catch(Exception exc)
			{
				MessageBox.Show("An error occurred: " + exc.Message);
			}

			MessageBox.Show("Retrieval complete!");
		}
	}
}