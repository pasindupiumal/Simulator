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
            urlTextBox.Text = this.baseURL;

        }
 
        public void PopulateCurrecyCodes()
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
                currCodesComboBox.Items.Clear();

                for (int i = 0; i < currCodes.Length; i++)
                {
                    currCodesComboBox.Items.Add(currCodes[i]);
                }

                currCodesComboBox.SelectedItem = currCodesComboBox.Items[0];
            }
        }

        public void ClearFields()
        {
            Settings.Default.Reload();

            this.progressBar1.Style = ProgressBarStyle.Continuous;
            amountTextBox.Text = Settings.Default["defaultAmount"].ToString();
            amountTextBox.ReadOnly = false;
            currCodesComboBox.Enabled = true;
            reversalButton.Enabled = true;
            purchaseButton.Enabled = true;
            urlTextBox.ReadOnly = false;
            respDetailsRichTextBox.Text = string.Empty;
            reqDetailsRichTextBox.Text = string.Empty;
            tranDetailsRichTextBox.Text = string.Empty;
            reversalButton.Enabled = false;
            progressBar1.Value = 0;
            urlTextBox.Text = utils.getBaseURL();
        }

    

        private async void purchaseButton_Click(object sender, EventArgs e)
        {
            //Setup progress bar settings
            this.progressBar1.Maximum = 100;
            this.progressBar1.Value = 0;
            this.progressBar1.Style = ProgressBarStyle.Marquee;
            this.progressBar1.MarqueeAnimationSpeed = 25;

            //Read amount and currency code
            string amount = amountTextBox.Text;
            string currCodeString = currCodesComboBox.Text;

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
                currCodesComboBox.Enabled = false;
                urlTextBox.ReadOnly = true;
                purchaseButton.Enabled = false;

                double inputAmount = amountDouble * 100;

                //Initialize RestService
                this.baseURL = utils.getBaseURL();
                restService = new RestService(urlTextBox.Text.ToString());

                //Get the transaction request tailored for the available settings
                string requestString = restService.GetEncodedPurchaseRequest(inputAmount.ToString(), currCode, true);

                //Display request details
                reqDetailsRichTextBox.Select(0, 0);
                reqDetailsRichTextBox.SelectedText = "\r\n\r\n" + requestString + "\r\n";

                reqDetailsRichTextBox.Select(0, 0);
                reqDetailsRichTextBox.SelectedText = "Purchase Request";

                //Perform transaction
                var response = await restService.PostPurchaseRequest(inputAmount.ToString(), currCode);

                respDetailsRichTextBox.Select(0, 0);
                respDetailsRichTextBox.SelectedText = "\r\n\r\n" + response + "\r\n";

                respDetailsRichTextBox.Select(0, 0);
                respDetailsRichTextBox.SelectedText = "Purchase Response";

                //Parse transaction response
                TransactionResponse transactionResponse = restService.DecodeResponse(response);

                if (transactionResponse != null)
                {
                    //Enable reversal button is the purchase is successful.
                    if (transactionResponse.RespCode.Equals("00"))
                    {
                        reversalButton.Enabled = true;
                    }

                    tranDetailsRichTextBox.Select(0, 0);
                    tranDetailsRichTextBox.SelectedText = "\r\n\r\n" + transactionResponse.PrintData + "\r\n";

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
                            tranDetailsRichTextBox.Select(0, 0);
                            tranDetailsRichTextBox.SelectedText = "\r\n\tExchange Rate\t :  " + exchangeRateString;
                        }

                        if (transactionResponse.BillingCurrency != null)
                        {
                            tranDetailsRichTextBox.Select(0, 0);
                            tranDetailsRichTextBox.SelectedText = "\r\n\tBilling Currency\t :  " + transactionResponse.BillingCurrency;
                        }

                        if (transactionResponse.BillingAmount != null)
                        {
                            double billingAmount = Double.Parse(transactionResponse.BillingAmount) / 100.00;
                            tranDetailsRichTextBox.Select(0, 0);
                            tranDetailsRichTextBox.SelectedText = "\r\n\tBilling Amount\t :  " + billingAmount.ToString();
                        }

                        tranDetailsRichTextBox.Select(0, 0);
                        tranDetailsRichTextBox.SelectedText = "\r\n\tDCC\t\t :  YES";
                    }
                    else
                    {
                        tranDetailsRichTextBox.Select(0, 0);
                        tranDetailsRichTextBox.SelectedText = "\r\n\tDCC\t\t :  NO";
                    }

                    if (transactionResponse.RRN != null)
                    {
                        tranDetailsRichTextBox.Select(0, 0);
                        tranDetailsRichTextBox.SelectedText = "\r\n\tRRN\t\t :  " + transactionResponse.RRN;
                    }

                    if (transactionResponse.PAN != null)
                    {
                        tranDetailsRichTextBox.Select(0, 0);
                        tranDetailsRichTextBox.SelectedText = "\r\n\tPAN\t\t :  " + transactionResponse.PAN;
                    }

                    if (transactionResponse.AuthCode != null)
                    {
                        tranDetailsRichTextBox.Select(0, 0);
                        tranDetailsRichTextBox.SelectedText = "\r\n\tAuth Code\t :  " + transactionResponse.AuthCode;
                    }

                    if (currCode.Length != 0)
                    {
                        tranDetailsRichTextBox.Select(0, 0);
                        tranDetailsRichTextBox.SelectedText = "\r\n\tCurrency Code   :  " + currCode;
                    }
                    //inputAmount.ToString(), currCode
                    if (inputAmount.ToString().Length != 0)
                    {
                        tranDetailsRichTextBox.Select(0, 0);
                        tranDetailsRichTextBox.SelectedText = "\r\n\tAmount\t\t :  " + (inputAmount / 100.00).ToString();
                    }

                    if (transactionResponse.TerminalId != null)
                    {
                        tranDetailsRichTextBox.Select(0, 0);
                        tranDetailsRichTextBox.SelectedText = "\r\n\r\n\tTID\t\t :  " + transactionResponse.TerminalId;
                    }

                    tranDetailsRichTextBox.Select(0, 0);
                    tranDetailsRichTextBox.SelectedText = "Purchase Response - " + transactionResponse.RespText;
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

        private async void reversalButton_Click(object sender, EventArgs e)
        {
            //Setup progress bar settings
            this.progressBar1.Maximum = 100;
            this.progressBar1.Value = 0;
            this.progressBar1.Style = ProgressBarStyle.Marquee;
            this.progressBar1.MarqueeAnimationSpeed = 25;

            reversalButton.Enabled = false;

            //Read amount and currency code
            string amount = amountTextBox.Text;
            string currCodeString = currCodesComboBox.Text;

            string[] currCodeSeperated = currCodeString.Split('-');
            string currCode = currCodeSeperated[0].Trim();

            double inputAmount = Double.Parse(amount);
            inputAmount = inputAmount * 100;

            //Initialize RestService
            this.baseURL = utils.getBaseURL();
            restService = new RestService(urlTextBox.Text.ToString());

            //Get the transaction request tailored for the available settings
            string requestString = restService.GetEncodedReversalRequest(inputAmount.ToString(), currCode, true);

            //Display request details
            reqDetailsRichTextBox.Select(0, 0);
            reqDetailsRichTextBox.SelectedText = "\r\n\r\n" + requestString + "\r\n\r\n\r\n\r\n";

            reqDetailsRichTextBox.Select(0, 0);
            reqDetailsRichTextBox.SelectedText = "Reversal Request";

            //Perform transaction
            var response = await restService.PostReversalRequest(inputAmount.ToString(), currCode);

            respDetailsRichTextBox.Select(0, 0);
            respDetailsRichTextBox.SelectedText = "\r\n\r\n" + response + "\r\n\r\n\r\n\r\n";

            respDetailsRichTextBox.Select(0, 0);
            respDetailsRichTextBox.SelectedText = "Reversal Response";

            //Parse transaction response
            TransactionResponse transactionResponse = restService.DecodeResponse(response);

            if (transactionResponse != null)
            {
                tranDetailsRichTextBox.Select(0, 0);
                tranDetailsRichTextBox.SelectedText = "\r\n\r\n" + transactionResponse.PrintData + "\r\n\r\n\r\n\r\n";

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
                        tranDetailsRichTextBox.Select(0, 0);
                        tranDetailsRichTextBox.SelectedText = "\r\n\tExchange Rate\t :  " + exchangeRateString;
                    }

                    if (transactionResponse.BillingCurrency != null)
                    {
                        tranDetailsRichTextBox.Select(0, 0);
                        tranDetailsRichTextBox.SelectedText = "\r\n\tBilling Currency\t :  " + transactionResponse.BillingCurrency;
                    }

                    if (transactionResponse.BillingAmount != null)
                    {
                        double billingAmount = Double.Parse(transactionResponse.BillingAmount) / 100.00;
                        tranDetailsRichTextBox.Select(0, 0);
                        tranDetailsRichTextBox.SelectedText = "\r\n\tBilling Amount\t :  " + billingAmount.ToString();
                    }

                    tranDetailsRichTextBox.Select(0, 0);
                    tranDetailsRichTextBox.SelectedText = "\r\n\tDCC\t\t :  YES";
                }
                else
                {
                    tranDetailsRichTextBox.Select(0, 0);
                    tranDetailsRichTextBox.SelectedText = "\r\n\tDCC\t\t :  NO";
                }

                if (transactionResponse.RRN != null)
                {
                    tranDetailsRichTextBox.Select(0, 0);
                    tranDetailsRichTextBox.SelectedText = "\r\n\tRRN\t\t :  " + transactionResponse.RRN;
                }

                if (transactionResponse.PAN != null)
                {
                    tranDetailsRichTextBox.Select(0, 0);
                    tranDetailsRichTextBox.SelectedText = "\r\n\tPAN\t\t :  " + transactionResponse.PAN;
                }

                if (transactionResponse.AuthCode != null)
                {
                    tranDetailsRichTextBox.Select(0, 0);
                    tranDetailsRichTextBox.SelectedText = "\r\n\tAuth Code\t :  " + transactionResponse.AuthCode;
                }

                if (currCode.Length != 0)
                {
                    tranDetailsRichTextBox.Select(0, 0);
                    tranDetailsRichTextBox.SelectedText = "\r\n\tCurrency Code   :  " + currCode;
                }
                //inputAmount.ToString(), currCode
                if (inputAmount.ToString().Length != 0)
                {
                    tranDetailsRichTextBox.Select(0, 0);
                    tranDetailsRichTextBox.SelectedText = "\r\n\tAmount\t\t :  " + (inputAmount / 100.00).ToString();
                }

                if (transactionResponse.TerminalId != null)
                {
                    tranDetailsRichTextBox.Select(0, 0);
                    tranDetailsRichTextBox.SelectedText = "\r\n\r\n\tTID\t\t :  " + transactionResponse.TerminalId;
                }

                tranDetailsRichTextBox.Select(0, 0);
                tranDetailsRichTextBox.SelectedText = "Reversal Response - " + transactionResponse.RespText;
            }

            //Stop the progress bar
            this.progressBar1.Style = ProgressBarStyle.Continuous;
            this.progressBar1.MarqueeAnimationSpeed = 0;
            this.progressBar1.Value = 100;
        }

        private async void reqDetCopyButton_Click(object sender, EventArgs e)
        {
            if (respDetailsRichTextBox.Text != null && respDetailsRichTextBox.Text.Length != 0)
            {
                Clipboard.SetText(reqDetailsRichTextBox.Text);
                reqDetCopyLabel.ForeColor = Color.Green;
                reqDetCopyLabel.Text = "Copied";
                reqDetCopyLabel.Visible = true;
                await Task.Delay(1000);
                reqDetCopyLabel.Visible = false;
            }
        }

        private async void respDetCopyButton_Click(object sender, EventArgs e)
        {
            if (respDetailsRichTextBox.Text != null && respDetailsRichTextBox.Text.Length != 0)
            {
                Clipboard.SetText(respDetailsRichTextBox.Text);
                respDetCopyLabel.ForeColor = Color.Green;
                respDetCopyLabel.Text = "Copied";
                respDetCopyLabel.Visible = true;
                await Task.Delay(1000);
                respDetCopyLabel.Visible = false;
            }
        }

        private async void tranDetCopyButton_Click(object sender, EventArgs e)
        {
            if (tranDetailsRichTextBox.Text != null && tranDetailsRichTextBox.Text.Length != 0)
            {
                Clipboard.SetText(tranDetailsRichTextBox.Text);
                tranDetCopyLabel.ForeColor = Color.Green;
                tranDetCopyLabel.Text = "Copied";
                tranDetCopyLabel.Visible = true;
                await Task.Delay(1000);
                tranDetCopyLabel.Visible = false;
            }
        }
    }
}
