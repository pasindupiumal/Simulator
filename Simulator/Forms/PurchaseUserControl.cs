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

        public void clearFields()
        {
            Settings.Default.Reload();

            this.progressBar1.Style = ProgressBarStyle.Continuous;
            amountTextBox.Text = Settings.Default["defaultAmount"].ToString();
            amountTextBox.ReadOnly = false;
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
            //Setup progress bar settings
            this.progressBar1.Maximum = 100;
            this.progressBar1.Value = 0;
            this.progressBar1.Style = ProgressBarStyle.Marquee;
            this.progressBar1.MarqueeAnimationSpeed = 25;

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

                    if (transactionResponse.DCCIndicator == null)
                    {
                        //Do nothing
                    }
                    else if (transactionResponse.DCCIndicator.Equals("1"))
                    {

                        if (transactionResponse.DCCExchangeRate != null)
                        {
                            string firstDigit = transactionResponse.DCCExchangeRate.Substring(0, 1);
                            string lastDigits = transactionResponse.DCCExchangeRate.Substring(1, transactionResponse.DCCExchangeRate.Length - 1);
                            string exchangeRateString = (Double.Parse(lastDigits) / Math.Pow(10, Double.Parse(firstDigit))).ToString();
                            richTextBox3.Select(0, 0);
                            richTextBox3.SelectedText = "\r\n\tExchange Rate\t :  " + exchangeRateString;
                        }

                        if (transactionResponse.BillingCurrency != null)
                        {
                            richTextBox3.Select(0, 0);
                            richTextBox3.SelectedText = "\r\n\tBilling Currency\t :  " + transactionResponse.BillingCurrency;
                        }

                        if (transactionResponse.BillingAmount != null)
                        {
                            double billingAmount = Double.Parse(transactionResponse.BillingAmount) / 100.00;
                            richTextBox3.Select(0, 0);
                            richTextBox3.SelectedText = "\r\n\tBilling Amount\t :  " + billingAmount.ToString();
                        }

                        richTextBox3.Select(0, 0);
                        richTextBox3.SelectedText = "\r\n\tDCC\t\t :  YES";
                    }
                    else
                    {
                        richTextBox3.Select(0, 0);
                        richTextBox3.SelectedText = "\r\n\tDCC\t\t :  NO";
                    }

                    if (transactionResponse.RRN != null)
                    {
                        richTextBox3.Select(0, 0);
                        richTextBox3.SelectedText = "\r\n\tRRN\t\t :  " + transactionResponse.RRN;
                    }

                    if (transactionResponse.PAN != null)
                    {
                        richTextBox3.Select(0, 0);
                        richTextBox3.SelectedText = "\r\n\tPAN\t\t :  " + transactionResponse.PAN;
                    }

                    if (transactionResponse.AuthCode != null)
                    {
                        richTextBox3.Select(0, 0);
                        richTextBox3.SelectedText = "\r\n\tAuth Code\t :  " + transactionResponse.AuthCode;
                    }

                    if (currCode.Length != 0)
                    {
                        richTextBox3.Select(0, 0);
                        richTextBox3.SelectedText = "\r\n\tCurrency Code   :  " + currCode;
                    }
                    //inputAmount.ToString(), currCode
                    if (inputAmount.ToString().Length != 0)
                    {
                        richTextBox3.Select(0, 0);
                        richTextBox3.SelectedText = "\r\n\tAmount\t\t :  " + (inputAmount / 100.00).ToString();
                    }

                    if (transactionResponse.TerminalId != null)
                    {
                        richTextBox3.Select(0, 0);
                        richTextBox3.SelectedText = "\r\n\r\n\tTID\t\t :  " + transactionResponse.TerminalId;
                    }

                    richTextBox3.Select(0, 0);
                    richTextBox3.SelectedText = "Purchase Response - " + transactionResponse.RespText;
                }

                //Stop the progress bar
                this.progressBar1.Style = ProgressBarStyle.Continuous;
                this.progressBar1.MarqueeAnimationSpeed = 0;
                this.progressBar1.Value = 100;
            }
            else
            {
                this.progressBar1.Style = ProgressBarStyle.Continuous;
                amountTextBox.ForeColor = Color.Red;
                amountTextBox.Text = "Enter a valid amount";
                await Task.Delay(1000);
                //MessageBox.Show("Enter a valid amount", "OPI Simulator", MessageBoxButtons.OK, MessageBoxIcon.Error);
                amountTextBox.Text = string.Empty;
                amountTextBox.ForeColor = Color.Black;
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            //Setup progress bar settings
            this.progressBar1.Maximum = 100;
            this.progressBar1.Value = 0;
            this.progressBar1.Style = ProgressBarStyle.Marquee;
            this.progressBar1.MarqueeAnimationSpeed = 25;

            button1.Enabled = false;

            //Read amount and currency code
            string amount = amountTextBox.Text;
            string currCodeString = comboBox1.Text;

            string[] currCodeSeperated = currCodeString.Split('-');
            string currCode = currCodeSeperated[0].Trim();

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

            //Parse transaction response
            TransactionResponse transactionResponse = restService.DecodeResponse(response);

            if (transactionResponse != null)
            {
                richTextBox3.Select(0, 0);
                richTextBox3.SelectedText = "\r\n\r\n" + transactionResponse.PrintData + "\r\n\r\n\r\n\r\n";

                if (transactionResponse.DCCIndicator == null)
                {
                    //Do nothing
                }
                else if (transactionResponse.DCCIndicator.Equals("1"))
                {

                    if (transactionResponse.DCCExchangeRate != null)
                    {
                        string firstDigit = transactionResponse.DCCExchangeRate.Substring(0, 1);
                        string lastDigits = transactionResponse.DCCExchangeRate.Substring(1, transactionResponse.DCCExchangeRate.Length - 1);
                        string exchangeRateString = (Double.Parse(lastDigits) / Math.Pow(10, Double.Parse(firstDigit))).ToString();
                        richTextBox3.Select(0, 0);
                        richTextBox3.SelectedText = "\r\n\tExchange Rate\t :  " + exchangeRateString;
                    }

                    if (transactionResponse.BillingCurrency != null)
                    {
                        richTextBox3.Select(0, 0);
                        richTextBox3.SelectedText = "\r\n\tBilling Currency\t :  " + transactionResponse.BillingCurrency;
                    }

                    if (transactionResponse.BillingAmount != null)
                    {
                        double billingAmount = Double.Parse(transactionResponse.BillingAmount) / 100.00;
                        richTextBox3.Select(0, 0);
                        richTextBox3.SelectedText = "\r\n\tBilling Amount\t :  " + billingAmount.ToString();
                    }

                    richTextBox3.Select(0, 0);
                    richTextBox3.SelectedText = "\r\n\tDCC\t\t :  YES";
                }
                else
                {
                    richTextBox3.Select(0, 0);
                    richTextBox3.SelectedText = "\r\n\tDCC\t\t :  NO";
                }

                if (transactionResponse.RRN != null)
                {
                    richTextBox3.Select(0, 0);
                    richTextBox3.SelectedText = "\r\n\tRRN\t\t :  " + transactionResponse.RRN;
                }

                if (transactionResponse.PAN != null)
                {
                    richTextBox3.Select(0, 0);
                    richTextBox3.SelectedText = "\r\n\tPAN\t\t :  " + transactionResponse.PAN;
                }

                if (transactionResponse.AuthCode != null)
                {
                    richTextBox3.Select(0, 0);
                    richTextBox3.SelectedText = "\r\n\tAuth Code\t :  " + transactionResponse.AuthCode;
                }

                if (currCode.Length != 0)
                {
                    richTextBox3.Select(0, 0);
                    richTextBox3.SelectedText = "\r\n\tCurrency Code   :  " + currCode;
                }
                //inputAmount.ToString(), currCode
                if (inputAmount.ToString().Length != 0)
                {
                    richTextBox3.Select(0, 0);
                    richTextBox3.SelectedText = "\r\n\tAmount\t\t :  " + (inputAmount / 100.00).ToString();
                }

                if (transactionResponse.TerminalId != null)
                {
                    richTextBox3.Select(0, 0);
                    richTextBox3.SelectedText = "\r\n\r\n\tTID\t\t :  " + transactionResponse.TerminalId;
                }

                richTextBox3.Select(0, 0);
                richTextBox3.SelectedText = "Reversal Response - " + transactionResponse.RespText;
            }

            //Stop the progress bar
            this.progressBar1.Style = ProgressBarStyle.Continuous;
            this.progressBar1.MarqueeAnimationSpeed = 0;
            this.progressBar1.Value = 100;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            this.progressBar1.Increment(1);
        }

        private void richTextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
