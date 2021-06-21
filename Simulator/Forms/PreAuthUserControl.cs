using Simulator.Models;
using Simulator.Properties;
using Simulator.Shared;
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
        private RestService restService = null;
        private bool incPreAuthAtLeastOnce = false;

        private TransactionResponse preAuthResponse = null;
        private TransactionResponse reversalResponse = null;

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

        private async void button2_Click(object sender, EventArgs e)
        {
            //Read amount and currency code
            string amount = amountTextBox.Text;
            string currCodeString = comboBox1.Text;

            string[] currCodeSeperated = currCodeString.Split('-');
            string currCode = currCodeSeperated[0].Trim();


            //Determine whether the provided amount is a numbers
            bool isDouble = Double.TryParse(amount, out double amountDouble);
            int decimalCount = 0;

            if (isDouble)
            {
                decimalCount = utils.getDecimalCount(Double.Parse(amount), amount, "en-US");
            }

            if (isDouble && decimalCount <= 2)
            {
                //Disable the input fields
                amountTextBox.ReadOnly = true;
                comboBox1.Enabled = false;
                textBox1.ReadOnly = true;
                button2.Enabled = false;

                //Setup progress bar settings
                this.progressBar1.Maximum = 100;
                this.progressBar1.Value = 0;
                this.timer2.Start();

                double inputAmount = amountDouble * 100;

                //Initialize RestService
                this.baseURL = utils.getBaseURL();
                restService = new RestService(textBox1.Text.ToString());

                //Get the transaction request tailored for the available settings
                string requestString = restService.GetEncodedPreAuthRequest(inputAmount.ToString(), currCode, true);

                //Display request details
                richTextBox2.Select(0, 0);
                richTextBox2.SelectedText = "\r\n\r\n" + requestString + "\r\n";

                richTextBox2.Select(0, 0);
                richTextBox2.SelectedText = "Pre-Auth Request";

                //Perform transaction
                var response = await restService.PostPreAuthRequest(inputAmount.ToString(), currCode);

                richTextBox1.Select(0, 0);
                richTextBox1.SelectedText = "\r\n\r\n" + response + "\r\n";

                richTextBox1.Select(0, 0);
                richTextBox1.SelectedText = "Pre-Auth Response";

                this.progressBar1.Value = 100;
                this.timer2.Stop();

                //Parse transaction response
                preAuthResponse = restService.DecodeResponse(response);

                if (preAuthResponse != null)
                {
                    //Enable reversal button is the purchase is successful.
                    if (preAuthResponse.RespCode.Equals("00"))
                    {
                        button1.Enabled = true;
                        button3.Enabled = true;
                        button4.Enabled = true;
                        button5.Enabled = true;
                    }

                    richTextBox3.Select(0, 0);
                    richTextBox3.SelectedText = "\r\n\r\n" + preAuthResponse.PrintData + "\r\n";

                    richTextBox3.Select(0, 0);
                    richTextBox3.SelectedText = "\r\n\tRRN             :  " + preAuthResponse.RRN;

                    richTextBox3.Select(0, 0);
                    richTextBox3.SelectedText = "\r\n\tPAN              :  " + preAuthResponse.PAN;

                    richTextBox3.Select(0, 0);
                    richTextBox3.SelectedText = "\r\n\tAuth Code  :  " + preAuthResponse.AuthCode;

                    richTextBox3.Select(0, 0);
                    richTextBox3.SelectedText = "\r\n\r\n\tTID               :  " + preAuthResponse.TerminalId;

                    richTextBox3.Select(0, 0);
                    richTextBox3.SelectedText = "Pre-Auth Response - " + preAuthResponse.RespText;
                }
            }
            else
            {
                MessageBox.Show("Enter a valid amount", "OPI Simulator", MessageBoxButtons.OK, MessageBoxIcon.Error);
                amountTextBox.Text = string.Empty;
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            //Disable buttons
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;

            //Read amount and currency code
            string amount = amountTextBox.Text;
            string currCodeString = comboBox1.Text;

            string[] currCodeSeperated = currCodeString.Split('-');
            string currCode = currCodeSeperated[0].Trim();

            //Setup progress bar settings
            this.progressBar1.Maximum = 100;
            this.progressBar1.Value = 0;
            this.timer2.Start();

            double inputAmount = Double.Parse(amount);
            inputAmount = inputAmount * 100;

            //Initialize RestService
            this.baseURL = utils.getBaseURL();
            restService = new RestService(textBox1.Text.ToString());

            //Get the transaction request tailored for the available settings
            string requestString = restService.GetEncodedReversalRequest(inputAmount.ToString(), currCode, true);

            //Display request details
            richTextBox2.Select(0, 0);
            richTextBox2.SelectedText = "\r\n\r\n" + requestString + "\r\n\r\n\r\n\r\n";

            richTextBox2.Select(0, 0);
            richTextBox2.SelectedText = "Reversal Request";

            //Perform transaction
            var response = await restService.PostReversalRequest(inputAmount.ToString(), currCode);

            richTextBox1.Select(0, 0);
            richTextBox1.SelectedText = "\r\n\r\n" + response + "\r\n\r\n\r\n\r\n";

            richTextBox1.Select(0, 0);
            richTextBox1.SelectedText = "Reversal Response";

            //Enable buttons under condition
            if (incPreAuthAtLeastOnce)
            {
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = true;
                button4.Enabled = true;
                button5.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = false;
            }

            //Parse transaction response
            reversalResponse = restService.DecodeResponse(response);

            if (reversalResponse != null)
            {
                richTextBox3.Select(0, 0);
                richTextBox3.SelectedText = "\r\n\r\n" + reversalResponse.PrintData + "\r\n\r\n\r\n\r\n";

                richTextBox3.Select(0, 0);
                richTextBox3.SelectedText = "\r\n\tRRN             :  " + reversalResponse.RRN;

                richTextBox3.Select(0, 0);
                richTextBox3.SelectedText = "\r\n\tPAN              :  " + reversalResponse.PAN;

                richTextBox3.Select(0, 0);
                richTextBox3.SelectedText = "\r\n\tAuth Code  :  " + reversalResponse.AuthCode;

                richTextBox3.Select(0, 0);
                richTextBox3.SelectedText = "\r\n\r\n\tTID               :  " + reversalResponse.TerminalId;

                richTextBox3.Select(0, 0);
                richTextBox3.SelectedText = "Reversal Response - " + reversalResponse.RespText;
            }

            this.progressBar1.Value = 100;
            this.timer2.Stop();
        }
    }
}
