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
using Simulator.Models;

namespace Simulator.Forms
{
    public partial class ReversalUserControl : UserControl
    {
        private RestService restService = null;
        private string baseURL = null;
        private Simulator.Shared.Utils utils = null;

        public ReversalUserControl()
        {
            InitializeComponent();
            utils = new Simulator.Shared.Utils();
            this.baseURL = utils.getBaseURL(); // Obtain the base URL
            textBox1.Text = this.baseURL;

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            //Setup progress bar settings
            this.progressBar1.Maximum = 100;
            this.progressBar1.Value = 0;
            this.progressBar1.Style = ProgressBarStyle.Marquee;
            this.progressBar1.MarqueeAnimationSpeed = 25;

            button1.Enabled = false;

            //Initialize RestService
            this.baseURL = utils.getBaseURL();
            restService = new RestService(textBox1.Text.ToString());

            //Get the transaction request tailored for the available settings
            string requestString = restService.GetEncodedReversalRequest("0", "752", true);

            //Display request details
            richTextBox2.Select(0, 0);
            richTextBox2.SelectedText = "\r\n\r\n" + requestString + "\r\n\r\n\r\n\r\n";

            richTextBox2.Select(0, 0);
            richTextBox2.SelectedText = "Reversal Last Request";

            //Perform transaction
            var response = await restService.PostReversalRequest("0", "752");

            richTextBox1.Select(0, 0);
            richTextBox1.SelectedText = "\r\n\r\n" + response + "\r\n\r\n\r\n\r\n";

            richTextBox1.Select(0, 0);
            richTextBox1.SelectedText = "Reversal Last Response";

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

        public void clearFields()
        {
            Settings.Default.Reload();

            this.progressBar1.Style = ProgressBarStyle.Continuous;
            button1.Enabled = true;
            textBox1.ReadOnly = false;
            richTextBox1.Text = string.Empty;
            richTextBox2.Text = string.Empty;
            richTextBox3.Text = string.Empty;
            progressBar1.Value = 0;
            textBox1.Text = utils.getBaseURL();
        }
    }
}
