using Simulator.Models;
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
using Simulator.Properties;

namespace Simulator.Forms
{
    public partial class PurchaseUserControl : UserControl
    {
        private RestService restService = null;
        private string baseURL = null;
        private Simulator.Shared.Utils utils = null;

        public PurchaseUserControl()
        {
            InitializeComponent();
            utils = new Simulator.Shared.Utils();
            this.baseURL = utils.getBaseURL(); // Obtain the base URL
            textBox1.Text = this.baseURL;

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void PurchaseUserControl_Load(object sender, EventArgs e)
        {

        }

        public void populateCurrecyCodes()
        {
            Settings.Default.Reload();
            string currCodesString = Settings.Default["currCodes"].ToString();
            string[] currCodes = currCodesString.Split(',');
            comboBox1.Items.Clear();

            for(int i=0; i < currCodes.Length; i=i+2)
            {
                comboBox1.Items.Add(currCodes[i] + " - " + currCodes[i + 1]);
            }

            comboBox1.SelectedItem = comboBox1.Items[0];
        }

        public void clearFields()
        {
            amountTextBox.Text = "100";
            amountTextBox.ReadOnly = false;
            comboBox1.Text = "752";
            comboBox1.Enabled = true;
            button1.Enabled = true;
            button2.Enabled = true;
            textBox1.ReadOnly = false;
            richTextBox1.Text = string.Empty;
            richTextBox2.Text = string.Empty;
            richTextBox3.Text = string.Empty;
            button1.Enabled = false;
            progressBar1.Value = 0;
            textBox1.Text = utils.getBaseURL();
        }

        private async void button2_Click_1(object sender, EventArgs e)
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
                string requestString = restService.GetEncodedPurchaseRequest(inputAmount.ToString(), currCode, true);

                //Display request details
                richTextBox2.Select(0, 0);
                richTextBox2.SelectedText = "\r\n\r\n" + requestString + "\r\n";

                richTextBox2.Select(0, 0);
                richTextBox2.SelectedText = "Purchase Request";

                //Perform transaction
                var response = await restService.PostPurchaseRequest(inputAmount.ToString(), currCode);

                richTextBox1.Select(0, 0);
                richTextBox1.SelectedText = "\r\n\r\n" + response + "\r\n";

                richTextBox1.Select(0, 0);
                richTextBox1.SelectedText = "Purchase Response";

                this.progressBar1.Value = 100;
                this.timer2.Stop();

                //Parse transaction response
                TransactionResponse transactionResponse = restService.DecodeResponse(response);

                if (transactionResponse != null)
                {
                    //Enable reversal button is the purchase is successful.
                    if (transactionResponse.RespCode.Equals("00"))
                    {
                        button1.Enabled = true;
                    }

                    richTextBox3.Select(0, 0);
                    richTextBox3.SelectedText = "\r\n\r\n" + transactionResponse.PrintData + "\r\n";

                    richTextBox3.Select(0, 0);
                    richTextBox3.SelectedText = "\r\n\r\n\tRRN             :  " + transactionResponse.RRN;

                    richTextBox3.Select(0, 0);
                    richTextBox3.SelectedText = "\r\n\r\n\tPAN              :  " + transactionResponse.PAN;

                    richTextBox3.Select(0, 0);
                    richTextBox3.SelectedText = "\r\n\r\n\tAuth Code  :  " + transactionResponse.AuthCode;

                    richTextBox3.Select(0, 0);
                    richTextBox3.SelectedText = "\r\n\r\n\tTID               :  " + transactionResponse.TerminalId;

                    richTextBox3.Select(0, 0);
                    richTextBox3.SelectedText = "Purchase Response - " + transactionResponse.RespText;
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
            button1.Enabled = false;

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

            this.progressBar1.Value = 100;
            this.timer2.Stop();

            //Parse transaction response
            TransactionResponse transactionResponse = restService.DecodeResponse(response);

            if (transactionResponse != null)
            {
                richTextBox3.Select(0, 0);
                richTextBox3.SelectedText = "\r\n\r\n" + transactionResponse.PrintData + "\r\n\r\n\r\n\r\n";

                richTextBox3.Select(0, 0);
                richTextBox3.SelectedText = "\r\n\r\n\tRRN             :  " + transactionResponse.RRN;

                richTextBox3.Select(0, 0);
                richTextBox3.SelectedText = "\r\n\r\n\tPAN              :  " + transactionResponse.PAN;

                richTextBox3.Select(0, 0);
                richTextBox3.SelectedText = "\r\n\r\n\tAuth Code  :  " + transactionResponse.AuthCode;

                richTextBox3.Select(0, 0);
                richTextBox3.SelectedText = "\r\n\r\n\tTID               :  " + transactionResponse.TerminalId;

                richTextBox3.Select(0, 0);
                richTextBox3.SelectedText = "Reversal Response - " + transactionResponse.RespText;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            this.progressBar1.Increment(1);
        }
    }
}
