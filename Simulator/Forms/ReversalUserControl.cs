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
            urlTextBox.Text = this.baseURL;

        }

        /// <summary>
        /// Method for resetting the reversal user control.
        /// </summary>
        public void clearFields()
        {
            Settings.Default.Reload();

            this.progressBar.Style = ProgressBarStyle.Continuous;
            reverseLastButton.Enabled = true;
            urlTextBox.ReadOnly = false;
            respDetailsRichTextBox.Text = string.Empty;
            reqDetailsRichTextBox.Text = string.Empty;
            tranDetailsRichTextBox.Text = string.Empty;
            progressBar.Value = 0;
            urlTextBox.Text = utils.getBaseURL();
        }

        /// <summary>
        /// Onclick method for ReverseLast button click.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void reverseLastButton_Click(object sender, EventArgs e)
        {
            //Setup progress bar settings
            this.progressBar.Maximum = 100;
            this.progressBar.Value = 0;
            this.progressBar.Style = ProgressBarStyle.Marquee;
            this.progressBar.MarqueeAnimationSpeed = 25;

            reverseLastButton.Enabled = false;

            //Initialize RestService
            this.baseURL = utils.getBaseURL();
            restService = new RestService(urlTextBox.Text.ToString());

            //Get the transaction request tailored for the available settings
            string requestString = restService.GetEncodedReversalRequest("0", "752", true);

            //Display request details
            reqDetailsRichTextBox.Select(0, 0);
            reqDetailsRichTextBox.SelectedText = "\r\n\r\n" + requestString + "\r\n\r\n\r\n\r\n";

            reqDetailsRichTextBox.Select(0, 0);
            reqDetailsRichTextBox.SelectedText = "Reversal Last Request";

            //Perform transaction
            var response = await restService.PostReversalRequest("0", "752");

            respDetailsRichTextBox.Select(0, 0);
            respDetailsRichTextBox.SelectedText = "\r\n\r\n" + response + "\r\n\r\n\r\n\r\n";

            respDetailsRichTextBox.Select(0, 0);
            respDetailsRichTextBox.SelectedText = "Reversal Last Response";

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

                if (transactionResponse.TerminalId != null)
                {
                    tranDetailsRichTextBox.Select(0, 0);
                    tranDetailsRichTextBox.SelectedText = "\r\n\r\n\tTID\t\t :  " + transactionResponse.TerminalId;
                }

                tranDetailsRichTextBox.Select(0, 0);
                tranDetailsRichTextBox.SelectedText = "Reversal Response - " + transactionResponse.RespText;
            }

            //Stop the progress bar
            this.progressBar.Style = ProgressBarStyle.Continuous;
            this.progressBar.MarqueeAnimationSpeed = 0;
            this.progressBar.Value = 100;
        }

        /// <summary>
        /// Onclick method for reqDetCopyButton.
        /// Copies the content of the request details rich text box to the clipboard.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void reqDetCopyButton_Click(object sender, EventArgs e)
        {
            if (reqDetailsRichTextBox.Text != null && reqDetailsRichTextBox.Text.Length != 0)
            {
                Clipboard.SetText(reqDetailsRichTextBox.Text);
                reqDetCopyLabel.ForeColor = Color.Green;
                reqDetCopyLabel.Text = "Copied";
                reqDetCopyLabel.Visible = true;
                await Task.Delay(1000);
                reqDetCopyLabel.Visible = false;
            }
        }

        /// <summary>
        /// Onclick method for respDetCopyButton.
        /// Copies the content of the response details rich text box to the clipboard.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Onclick method for tranDetCopyButton.
        /// Copies the content of the transaction details rich text box to the clipboard.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void transDetCopyButton_Click(object sender, EventArgs e)
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

        private async void button1_Click(object sender, EventArgs e)
        {
            //await utils.WriteToExcelFile();
        }
    }
}
