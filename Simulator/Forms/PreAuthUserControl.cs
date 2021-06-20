using Simulator.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simulator.Forms
{
    public partial class PreAuthUserControl : UserControl
    {
        private Simulator.Shared.Utils utils = null;
        private string baseURL = null;

        public PreAuthUserControl()
        {
            InitializeComponent();
            utils = new Simulator.Shared.Utils();
            this.baseURL = utils.getBaseURL(); // Obtain the base URL
            textBox1.Text = this.baseURL;
        }

        public void clearFields()
        {
            amountTextBox.Text = "100";
            amountTextBox.ReadOnly = false;
            comboBox1.Enabled = true;
            button1.Enabled = false;
            button2.Enabled = true;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            textBox1.ReadOnly = false;
            richTextBox1.Text = string.Empty;
            richTextBox2.Text = string.Empty;
            richTextBox3.Text = string.Empty;
            progressBar1.Value = 0;
            textBox1.Text = utils.getBaseURL();
        }

        public void populateCurrecyCodes()
        {
            Settings.Default.Reload();
            string currCodesString = Settings.Default["currCodes"].ToString();

            if (currCodesString.Length == 0)
            {
                MessageBox.Show("Please add a currency code into settings!", "OPI Simulator", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string[] currCodes = currCodesString.Split(',');
                comboBox1.Items.Clear();

                for (int i = 0; i < currCodes.Length; i++)
                {
                    comboBox1.Items.Add(currCodes[i]);
                }

                comboBox1.SelectedItem = comboBox1.Items[0];
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
