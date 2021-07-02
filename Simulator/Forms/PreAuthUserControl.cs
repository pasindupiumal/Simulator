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
        private int incrementalPreAuthCount = 0;

        private TransactionResponse preAuthResponse = null;
        private TransactionResponse preAuthReversalResponse = null;
        private TransactionResponse preAuthCompletionResponse = null;
        private TransactionResponse preAuthCancelationResponse = null;
        private TransactionResponse incPreAuthResponse = null;

        public PreAuthUserControl()
        {
            InitializeComponent();
            utils = new Simulator.Shared.Utils();
            this.baseURL = utils.getBaseURL(); // Obtain the base URL
            urlTextBox.Text = this.baseURL;
        }

        /// <summary>
        /// Method for resetting the pre-auth user control.
        /// </summary>
        public async void ClearFields()
        {
            Settings.Default.Reload();

            this.progressBar.Style = ProgressBarStyle.Continuous;
            progressBar.Value = 0;
            amountTextBox.Text = Settings.Default["defaultAmount"].ToString();
            amountTextBox.ReadOnly = false;
            currCodesComboBox.Enabled = true;
            preAuthReversalButton.Enabled = false;
            preAuthButton.Enabled = true;
            incPreAuthButton.Enabled = false;
            preAuthCompButton.Enabled = false;
            preAuthCancelButton.Enabled = false;
            preAuthCancelButton.Visible = false;
            urlTextBox.ReadOnly = false;
            resDetailsRichTextBox.Text = string.Empty;
            reqDetailsRichTextBox.Text = string.Empty;
            tranDetailsRichTextBox.Text = string.Empty;
            urlTextBox.Text = utils.getBaseURL();

            if (!(Settings.Default["filePath"].ToString().Length == 0) && !(Settings.Default["currentFileName"].ToString().Length == 0))
            {
                await utils.ExcelWriteNewLine();
            }
        }

        /// <summary>
        /// Method for populating the CurrCodes combo box with currency codes obtained from the settings.
        /// </summary>
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

        /// <summary>
        /// Pre-Auth button on click method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void preAuthButton_Click(object sender, EventArgs e)
        {
            //Setup progress bar settings
            this.progressBar.Maximum = 10;
            this.progressBar.Value = 10;
            this.progressBar.Style = ProgressBarStyle.Marquee;
            this.progressBar.MarqueeAnimationSpeed = 25;

            //Read amount and currency code
            string amount = amountTextBox.Text;
            string currCodeString = currCodesComboBox.Text;

            //Determine whether the provided amount is a number
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
                preAuthButton.Enabled = false;

                string[] currCodeSeperated = currCodeString.Split('-');
                string currCode = currCodeSeperated[0].Trim();

                double inputAmount = amountDouble * 100;

                //Initialize RestService
                this.baseURL = utils.getBaseURL();
                restService = new RestService(urlTextBox.Text.ToString());

                //Get the transaction request tailored for the available settings
                string requestString = restService.GetEncodedPreAuthRequest(inputAmount.ToString(), currCode, true);

                //Display request details
                reqDetailsRichTextBox.Select(0, 0);
                reqDetailsRichTextBox.SelectedText = "\r\n\r\n" + requestString + "\r\n";

                reqDetailsRichTextBox.Select(0, 0);
                reqDetailsRichTextBox.SelectedText = "Pre-Auth Request";

                //Perform transaction
                var response = await restService.PostPreAuthRequest(inputAmount.ToString(), currCode);

                resDetailsRichTextBox.Select(0, 0);
                resDetailsRichTextBox.SelectedText = "\r\n\r\n" + response + "\r\n";

                resDetailsRichTextBox.Select(0, 0);
                resDetailsRichTextBox.SelectedText = "Pre-Auth Response";

                //Parse transaction response
                preAuthResponse = restService.DecodeResponse(response);

                if (preAuthResponse != null)
                {
                    string dccStatus = null;
                    string transactionStatus = null;

                    //Enable reversal button is the purchase is successful.
                    if (preAuthResponse.RespCode.Equals("00"))
                    {
                        preAuthReversalButton.Enabled = true;
                        incPreAuthButton.Enabled = true;
                        preAuthCompButton.Enabled = true;
                        preAuthCancelButton.Enabled = true;
                        amountTextBox.ReadOnly = false;
                        transactionStatus = "SUCCESS";
                    }
                    else
                    {
                        transactionStatus = "FAILED";
                    }

                    tranDetailsRichTextBox.Select(0, 0);
                    tranDetailsRichTextBox.SelectedText = "\r\n\r\n" + preAuthResponse.PrintData + "\r\n";

                    if (preAuthResponse.DCCIndicator == null)
                    {
                        //Do nothing
                    }
                    else if (preAuthResponse.DCCIndicator.Equals("1"))
                    {
                        dccStatus = "YES";

                        if (preAuthResponse.DCCExchangeRate != null)
                        {
                            string firstDigit = preAuthResponse.DCCExchangeRate.Substring(0, 1);
                            string lastDigits = preAuthResponse.DCCExchangeRate.Substring(1, preAuthResponse.DCCExchangeRate.Length - 1);
                            string exchangeRateString = (Double.Parse(lastDigits) / Math.Pow(10, Double.Parse(firstDigit))).ToString();
                            tranDetailsRichTextBox.Select(0, 0);
                            tranDetailsRichTextBox.SelectedText = "\r\n\tExchange Rate\t :  " + exchangeRateString;
                        }

                        if (preAuthResponse.BillingCurrency != null)
                        {
                            tranDetailsRichTextBox.Select(0, 0);
                            tranDetailsRichTextBox.SelectedText = "\r\n\tBilling Currency\t :  " + preAuthResponse.BillingCurrency;
                        }

                        if (preAuthResponse.BillingAmount != null)
                        {
                            double billingAmount = Double.Parse(preAuthResponse.BillingAmount) / 100.00;
                            tranDetailsRichTextBox.Select(0, 0);
                            tranDetailsRichTextBox.SelectedText = "\r\n\tBilling Amount\t :  " + billingAmount.ToString();
                        }

                        tranDetailsRichTextBox.Select(0, 0);
                        tranDetailsRichTextBox.SelectedText = "\r\n\tDCC\t\t :  YES";
                    }
                    else
                    {
                        dccStatus = "NO";
                        tranDetailsRichTextBox.Select(0, 0);
                        tranDetailsRichTextBox.SelectedText = "\r\n\tDCC\t\t :  NO";
                    }

                    if (preAuthResponse.RRN != null)
                    {
                        tranDetailsRichTextBox.Select(0, 0);
                        tranDetailsRichTextBox.SelectedText = "\r\n\tRRN\t\t :  " + preAuthResponse.RRN;
                    }

                    if (preAuthResponse.PAN != null)
                    {
                        tranDetailsRichTextBox.Select(0, 0);
                        tranDetailsRichTextBox.SelectedText = "\r\n\tPAN\t\t :  " + preAuthResponse.PAN;
                    }

                    if (preAuthResponse.AuthCode != null)
                    {
                        tranDetailsRichTextBox.Select(0, 0);
                        tranDetailsRichTextBox.SelectedText = "\r\n\tAuth Code\t :  " + preAuthResponse.AuthCode;
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

                    if (preAuthResponse.TerminalId != null)
                    {
                        tranDetailsRichTextBox.Select(0, 0);
                        tranDetailsRichTextBox.SelectedText = "\r\n\r\n\tTID\t\t :  " + preAuthResponse.TerminalId;
                    }

                    tranDetailsRichTextBox.Select(0, 0);
                    tranDetailsRichTextBox.SelectedText = "Pre-Auth Response - " + preAuthResponse.RespText;

                    await utils.WriteToExcelFile("Pre-Auth", dccStatus, preAuthResponse.TerminalId, preAuthResponse.PAN, preAuthResponse.RRN, amount, transactionStatus, preAuthResponse.RespText);
                }

                //Stop the progress bar
                this.progressBar.Value = 10;
                this.progressBar.Style = ProgressBarStyle.Continuous;
                this.progressBar.MarqueeAnimationSpeed = 0;
            }
            else
            {
                this.progressBar.Style = ProgressBarStyle.Continuous;
                amountTextBox.ForeColor = Color.Red;
                amountTextBox.Text = "Enter a valid amount";
                await Task.Delay(1000);
                //MessageBox.Show("Enter a valid amount", "OPI Simulator", MessageBoxButtons.OK, MessageBoxIcon.Error);
                amountTextBox.Text = string.Empty;
                amountTextBox.ForeColor = Color.Black;
            }
        }

        /// <summary>
        /// Pre-Auth Completion button onclick method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void preAuthCompButton_Click(object sender, EventArgs e)
        {
            //Setup progress bar settings
            this.progressBar.Maximum = 100;
            this.progressBar.Value = 0;
            this.progressBar.Style = ProgressBarStyle.Marquee;
            this.progressBar.MarqueeAnimationSpeed = 25;

            //Read amount and currency code
            string amount = amountTextBox.Text;
            string currCodeString = currCodesComboBox.Text;

            //Determine whether the provided amount is a numbers
            bool isDouble = Double.TryParse(amount, out double amountDouble);
            int decimalCount = 0;

            if (isDouble)
            {
                decimalCount = utils.getDecimalCount(Double.Parse(amount), amount, "en-US");
            }

            if (isDouble && decimalCount <= 2)
            {
                //Record initial state of buttons
                bool reversalButtonStatus = preAuthReversalButton.Enabled;
                bool incPreAuthButtonStatus = incPreAuthButton.Enabled;
                bool preAuthCompButtonStatus = preAuthCompButton.Enabled;
                bool preAuthCancelButtonStatus = preAuthCancelButton.Enabled;

                //Disable buttons accordingly
                preAuthReversalButton.Enabled = false;
                preAuthButton.Enabled = false;
                incPreAuthButton.Enabled = false;
                preAuthCompButton.Enabled = false;
                preAuthCancelButton.Enabled = false;
                amountTextBox.ReadOnly = true;

                string[] currCodeSeperated = currCodeString.Split('-');
                string currCode = currCodeSeperated[0].Trim();

                double inputAmount = Double.Parse(amount);
                inputAmount = inputAmount * 100;

                //Initialize RestService
                this.baseURL = utils.getBaseURL();
                restService = new RestService(urlTextBox.Text.ToString());

                //Get the transaction request tailored for the available settings
                string requestString = restService.GetEncodedPreAuthCompleteRequest(inputAmount.ToString(), currCode, true, preAuthResponse.AuthCode, preAuthResponse.RRN, preAuthResponse.TransToken, preAuthResponse.ExpiryDate, preAuthResponse.PAN);

                //Display request details
                reqDetailsRichTextBox.Select(0, 0);
                reqDetailsRichTextBox.SelectedText = "\r\n\r\n" + requestString + "\r\n\r\n\r\n\r\n";

                reqDetailsRichTextBox.Select(0, 0);
                reqDetailsRichTextBox.SelectedText = "Pre-Auth Completion Request";

                //Perform transaction
                var response = await restService.PostPreAuthCompletionRequest(inputAmount.ToString(), currCode, true, preAuthResponse.AuthCode, preAuthResponse.RRN, preAuthResponse.TransToken, preAuthResponse.ExpiryDate, preAuthResponse.PAN);

                resDetailsRichTextBox.Select(0, 0);
                resDetailsRichTextBox.SelectedText = "\r\n\r\n" + response + "\r\n\r\n\r\n\r\n";

                resDetailsRichTextBox.Select(0, 0);
                resDetailsRichTextBox.SelectedText = "Pre-Auth Completion Response";

                //Parse transaction response
                preAuthCompletionResponse = restService.DecodeResponse(response);

                if (preAuthCompletionResponse != null)
                {
                    string dccStatus = null;
                    string transactionStatus = null;

                    tranDetailsRichTextBox.Select(0, 0);
                    tranDetailsRichTextBox.SelectedText = "\r\n\r\n" + preAuthCompletionResponse.PrintData + "\r\n\r\n\r\n\r\n";

                    if (preAuthCompletionResponse.DCCIndicator == null)
                    {
                        //Do nothing
                    }
                    else if (preAuthCompletionResponse.DCCIndicator.Equals("1"))
                    {
                        dccStatus = "YES";

                        if (preAuthCompletionResponse.DCCExchangeRate != null)
                        {
                            string firstDigit = preAuthCompletionResponse.DCCExchangeRate.Substring(0, 1);
                            string lastDigits = preAuthCompletionResponse.DCCExchangeRate.Substring(1, preAuthCompletionResponse.DCCExchangeRate.Length - 1);
                            string exchangeRateString = (Double.Parse(lastDigits) / Math.Pow(10, Double.Parse(firstDigit))).ToString();
                            tranDetailsRichTextBox.Select(0, 0);
                            tranDetailsRichTextBox.SelectedText = "\r\n\tExchange Rate\t :  " + exchangeRateString;
                        }

                        if (preAuthCompletionResponse.BillingCurrency != null)
                        {
                            tranDetailsRichTextBox.Select(0, 0);
                            tranDetailsRichTextBox.SelectedText = "\r\n\tBilling Currency\t :  " + preAuthCompletionResponse.BillingCurrency;
                        }

                        if (preAuthCompletionResponse.BillingAmount != null)
                        {
                            double billingAmount = Double.Parse(preAuthCompletionResponse.BillingAmount) / 100.00;
                            tranDetailsRichTextBox.Select(0, 0);
                            tranDetailsRichTextBox.SelectedText = "\r\n\tBilling Amount\t :  " + billingAmount.ToString();
                        }

                        tranDetailsRichTextBox.Select(0, 0);
                        tranDetailsRichTextBox.SelectedText = "\r\n\tDCC\t\t :  YES";
                    }
                    else
                    {
                        dccStatus = "NO";
                        tranDetailsRichTextBox.Select(0, 0);
                        tranDetailsRichTextBox.SelectedText = "\r\n\tDCC\t\t :  NO";
                    }

                    if (preAuthCompletionResponse.RRN != null)
                    {
                        tranDetailsRichTextBox.Select(0, 0);
                        tranDetailsRichTextBox.SelectedText = "\r\n\tRRN\t\t :  " + preAuthCompletionResponse.RRN;
                    }

                    if (preAuthCompletionResponse.PAN != null)
                    {
                        tranDetailsRichTextBox.Select(0, 0);
                        tranDetailsRichTextBox.SelectedText = "\r\n\tPAN\t\t :  " + preAuthCompletionResponse.PAN;
                    }

                    if (preAuthCompletionResponse.AuthCode != null)
                    {
                        tranDetailsRichTextBox.Select(0, 0);
                        tranDetailsRichTextBox.SelectedText = "\r\n\tAuth Code\t :  " + preAuthCompletionResponse.AuthCode;
                    }

                    if (currCode.Length != 0)
                    {
                        tranDetailsRichTextBox.Select(0, 0);
                        tranDetailsRichTextBox.SelectedText = "\r\n\tCurrency Code   :  " + currCode;
                    }

                    if (inputAmount.ToString().Length != 0)
                    {
                        tranDetailsRichTextBox.Select(0, 0);
                        tranDetailsRichTextBox.SelectedText = "\r\n\tAmount\t\t :  " + (inputAmount / 100.00).ToString();
                    }

                    if (preAuthCompletionResponse.TerminalId != null)
                    {
                        tranDetailsRichTextBox.Select(0, 0);
                        tranDetailsRichTextBox.SelectedText = "\r\n\r\n\tTID\t\t :  " + preAuthCompletionResponse.TerminalId;
                    }

                    tranDetailsRichTextBox.Select(0, 0);
                    tranDetailsRichTextBox.SelectedText = "Pre-Auth Completion Response - " + preAuthCompletionResponse.RespText;

                    if (preAuthCompletionResponse.RespCode.Equals("00"))
                    {
                        //Diable all buttons
                        preAuthReversalButton.Enabled = false;
                        preAuthButton.Enabled = false;
                        incPreAuthButton.Enabled = false;
                        preAuthCompButton.Enabled = false;
                        preAuthCancelButton.Enabled = false;
                        amountTextBox.ReadOnly = false;

                        transactionStatus = "SUCCESS";
                    }
                    else
                    {
                        //Restore buttons to original status, since the completion is unsuccessful.
                        preAuthReversalButton.Enabled = reversalButtonStatus;
                        preAuthButton.Enabled = false;
                        incPreAuthButton.Enabled = incPreAuthButtonStatus;
                        preAuthCompButton.Enabled = preAuthCompButtonStatus;
                        preAuthCancelButton.Enabled = preAuthCancelButtonStatus;
                        amountTextBox.ReadOnly = false;

                        transactionStatus = "FAILED";
                    }

                    await utils.WriteToExcelFile("Pre-Auth Completion", dccStatus, preAuthCompletionResponse.TerminalId, preAuthCompletionResponse.PAN, preAuthCompletionResponse.RRN, amount, transactionStatus, preAuthCompletionResponse.RespText);
                }

                //Stop the progress bar
                this.progressBar.Style = ProgressBarStyle.Continuous;
                this.progressBar.MarqueeAnimationSpeed = 0;
                this.progressBar.Value = 100;
            }
            else
            {
                this.progressBar.Style = ProgressBarStyle.Continuous;
                amountTextBox.ForeColor = Color.Red;
                amountTextBox.Text = "Enter a valid amount";
                await Task.Delay(1000);
                //MessageBox.Show("Enter a valid amount", "OPI Simulator", MessageBoxButtons.OK, MessageBoxIcon.Error);
                amountTextBox.Text = string.Empty;
                amountTextBox.ForeColor = Color.Black;
            }
        }

        /// <summary>
        /// Pre-Auth Cancelation button on-click method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void preAuthCancelButton_Click(object sender, EventArgs e)
        {
            //Setup progress bar settings
            this.progressBar.Maximum = 100;
            this.progressBar.Value = 0;
            this.progressBar.Style = ProgressBarStyle.Marquee;
            this.progressBar.MarqueeAnimationSpeed = 25;

            //Read amount and currency code
            string amount = amountTextBox.Text;
            string currCodeString = currCodesComboBox.Text;

            //Determine whether the provided amount is a numbers
            bool isDouble = Double.TryParse(amount, out double amountDouble);
            int decimalCount = 0;

            if (isDouble)
            {
                decimalCount = utils.getDecimalCount(Double.Parse(amount), amount, "en-US");
            }

            if (isDouble && decimalCount <= 2)
            {
                //Record initial state of buttons
                bool reversalButtonStatus = preAuthReversalButton.Enabled;
                bool incPreAuthButtonStatus = incPreAuthButton.Enabled;
                bool preAuthCompButtonStatus = preAuthCompButton.Enabled;
                bool preAuthCancelButtonStatus = preAuthCancelButton.Enabled;

                //Disable buttons accordingly
                preAuthReversalButton.Enabled = false;
                preAuthButton.Enabled = false;
                incPreAuthButton.Enabled = false;
                preAuthCompButton.Enabled = false;
                preAuthCancelButton.Enabled = false;
                amountTextBox.ReadOnly = true;

                string[] currCodeSeperated = currCodeString.Split('-');
                string currCode = currCodeSeperated[0].Trim();

                double inputAmount = Double.Parse(amount);
                inputAmount = inputAmount * 100;

                //Initialize RestService
                this.baseURL = utils.getBaseURL();
                restService = new RestService(urlTextBox.Text.ToString());

                //Get the transaction request tailored for the available settings
                string requestString = restService.GetEncodedPreAuthCancelRequest(inputAmount.ToString(), currCode, true, preAuthResponse.RRN, preAuthResponse.TransToken, preAuthResponse.ExpiryDate, preAuthResponse.PAN);

                //Display request details
                reqDetailsRichTextBox.Select(0, 0);
                reqDetailsRichTextBox.SelectedText = "\r\n\r\n" + requestString + "\r\n\r\n\r\n\r\n";

                reqDetailsRichTextBox.Select(0, 0);
                reqDetailsRichTextBox.SelectedText = "Pre-Auth Cancelation Request";

                //Perform transaction
                var response = await restService.PostPreAuthCancelRequest(inputAmount.ToString(), currCode, true, preAuthResponse.RRN, preAuthResponse.TransToken, preAuthResponse.ExpiryDate, preAuthResponse.PAN);

                resDetailsRichTextBox.Select(0, 0);
                resDetailsRichTextBox.SelectedText = "\r\n\r\n" + response + "\r\n\r\n\r\n\r\n";

                resDetailsRichTextBox.Select(0, 0);
                resDetailsRichTextBox.SelectedText = "Pre-Auth Cancelation Response";

                //Parse transaction response
                preAuthCancelationResponse = restService.DecodeResponse(response);

                if (preAuthCancelationResponse != null)
                {
                    string dccStatus = null;
                    string transactionStatus = null;

                    tranDetailsRichTextBox.Select(0, 0);
                    tranDetailsRichTextBox.SelectedText = "\r\n\r\n" + preAuthCancelationResponse.PrintData + "\r\n\r\n\r\n\r\n";

                    if (preAuthCancelationResponse.DCCIndicator == null)
                    {
                        //Do nothing
                    }
                    else if (preAuthCancelationResponse.DCCIndicator.Equals("1"))
                    {
                        dccStatus = "YES";

                        if (preAuthCancelationResponse.DCCExchangeRate != null)
                        {
                            string firstDigit = preAuthCancelationResponse.DCCExchangeRate.Substring(0, 1);
                            string lastDigits = preAuthCancelationResponse.DCCExchangeRate.Substring(1, preAuthCancelationResponse.DCCExchangeRate.Length - 1);
                            string exchangeRateString = (Double.Parse(lastDigits) / Math.Pow(10, Double.Parse(firstDigit))).ToString();
                            tranDetailsRichTextBox.Select(0, 0);
                            tranDetailsRichTextBox.SelectedText = "\r\n\tExchange Rate\t :  " + exchangeRateString;
                        }

                        if (preAuthCancelationResponse.BillingCurrency != null)
                        {
                            tranDetailsRichTextBox.Select(0, 0);
                            tranDetailsRichTextBox.SelectedText = "\r\n\tBilling Currency\t :  " + preAuthCancelationResponse.BillingCurrency;
                        }

                        if (preAuthCancelationResponse.BillingAmount != null)
                        {
                            double billingAmount = Double.Parse(preAuthCancelationResponse.BillingAmount) / 100.00;
                            tranDetailsRichTextBox.Select(0, 0);
                            tranDetailsRichTextBox.SelectedText = "\r\n\tBilling Amount\t :  " + billingAmount.ToString();
                        }

                        tranDetailsRichTextBox.Select(0, 0);
                        tranDetailsRichTextBox.SelectedText = "\r\n\tDCC\t\t :  YES";
                    }
                    else
                    {
                        dccStatus = "NO";
                        tranDetailsRichTextBox.Select(0, 0);
                        tranDetailsRichTextBox.SelectedText = "\r\n\tDCC\t\t :  NO";
                    }

                    if (preAuthCancelationResponse.RRN != null)
                    {
                        tranDetailsRichTextBox.Select(0, 0);
                        tranDetailsRichTextBox.SelectedText = "\r\n\tRRN\t\t :  " + preAuthCancelationResponse.RRN;
                    }

                    if (preAuthCancelationResponse.PAN != null)
                    {
                        tranDetailsRichTextBox.Select(0, 0);
                        tranDetailsRichTextBox.SelectedText = "\r\n\tPAN\t\t :  " + preAuthCancelationResponse.PAN;
                    }

                    if (preAuthCancelationResponse.AuthCode != null)
                    {
                        tranDetailsRichTextBox.Select(0, 0);
                        tranDetailsRichTextBox.SelectedText = "\r\n\tAuth Code\t :  " + preAuthCancelationResponse.AuthCode;
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

                    if (preAuthCancelationResponse.TerminalId != null)
                    {
                        tranDetailsRichTextBox.Select(0, 0);
                        tranDetailsRichTextBox.SelectedText = "\r\n\r\n\tTID\t\t :  " + preAuthCancelationResponse.TerminalId;
                    }

                    tranDetailsRichTextBox.Select(0, 0);
                    tranDetailsRichTextBox.SelectedText = "Pre-Auth Cancelation Response - " + preAuthCancelationResponse.RespText;

                    if (preAuthCancelationResponse.RespCode.Equals("00"))
                    {
                        //Diable all buttons
                        preAuthReversalButton.Enabled = false;
                        preAuthButton.Enabled = false;
                        incPreAuthButton.Enabled = false;
                        preAuthCompButton.Enabled = false;
                        preAuthCancelButton.Enabled = false;
                        amountTextBox.ReadOnly = false;

                        transactionStatus = "SUCCESS";
                    }
                    else
                    {
                        //Restore buttons to original status, since the completion is unsuccessful.
                        preAuthReversalButton.Enabled = reversalButtonStatus;
                        preAuthButton.Enabled = false;
                        incPreAuthButton.Enabled = incPreAuthButtonStatus;
                        preAuthCompButton.Enabled = preAuthCompButtonStatus;
                        preAuthCancelButton.Enabled = preAuthCancelButtonStatus;
                        amountTextBox.ReadOnly = false;

                        transactionStatus = "FAILED";
                    }

                    await utils.WriteToExcelFile("Pre-Auth Cancelation", dccStatus, preAuthCancelationResponse.TerminalId, preAuthCancelationResponse.PAN, preAuthCancelationResponse.RRN, amount, transactionStatus, preAuthCancelationResponse.RespText);
                }

                //Stop the progress bar
                this.progressBar.Style = ProgressBarStyle.Continuous;
                this.progressBar.MarqueeAnimationSpeed = 0;
                this.progressBar.Value = 100;
            }
            else
            {
                this.progressBar.Style = ProgressBarStyle.Continuous;
                amountTextBox.ForeColor = Color.Red;
                amountTextBox.Text = "Enter a valid amount";
                await Task.Delay(1000);
                //MessageBox.Show("Enter a valid amount", "OPI Simulator", MessageBoxButtons.OK, MessageBoxIcon.Error);
                amountTextBox.Text = string.Empty;
                amountTextBox.ForeColor = Color.Black;
            }
        }

        /// <summary>
        /// Pre-Auth Reversal button on click method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void preAuthReversalButton_Click(object sender, EventArgs e)
        {
            //Setup progress bar settings
            this.progressBar.Maximum = 100;
            this.progressBar.Value = 0;
            this.progressBar.Style = ProgressBarStyle.Marquee;
            this.progressBar.MarqueeAnimationSpeed = 25;

            //Read amount and currency code
            string amount = amountTextBox.Text;
            string currCodeString = currCodesComboBox.Text;

            //Determine whether the provided amount is a numbers
            bool isDouble = Double.TryParse(amount, out double amountDouble);
            int decimalCount = 0;

            if (isDouble)
            {
                decimalCount = utils.getDecimalCount(Double.Parse(amount), amount, "en-US");
            }

            if (isDouble && decimalCount <= 2)
            {
                //Disable buttons
                preAuthReversalButton.Enabled = false;
                preAuthButton.Enabled = false;
                incPreAuthButton.Enabled = false;
                preAuthCompButton.Enabled = false;
                preAuthCancelButton.Enabled = false;
                amountTextBox.ReadOnly = true;

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

                resDetailsRichTextBox.Select(0, 0);
                resDetailsRichTextBox.SelectedText = "\r\n\r\n" + response + "\r\n\r\n\r\n\r\n";

                resDetailsRichTextBox.Select(0, 0);
                resDetailsRichTextBox.SelectedText = "Reversal Response";

                //Parse transaction response
                preAuthReversalResponse = restService.DecodeResponse(response);

                if (preAuthReversalResponse != null)
                {
                    string dccStatus = null;
                    string transactionStatus = null;

                    tranDetailsRichTextBox.Select(0, 0);
                    tranDetailsRichTextBox.SelectedText = "\r\n\r\n" + preAuthReversalResponse.PrintData + "\r\n\r\n\r\n\r\n";

                    if (preAuthReversalResponse.DCCIndicator == null)
                    {
                        //Do nothing
                    }
                    else if (preAuthReversalResponse.DCCIndicator.Equals("1"))
                    {
                        dccStatus = "YES";

                        if (preAuthReversalResponse.DCCExchangeRate != null)
                        {
                            string firstDigit = preAuthReversalResponse.DCCExchangeRate.Substring(0, 1);
                            string lastDigits = preAuthReversalResponse.DCCExchangeRate.Substring(1, preAuthReversalResponse.DCCExchangeRate.Length - 1);
                            string exchangeRateString = (Double.Parse(lastDigits) / Math.Pow(10, Double.Parse(firstDigit))).ToString();
                            tranDetailsRichTextBox.Select(0, 0);
                            tranDetailsRichTextBox.SelectedText = "\r\n\tExchange Rate\t :  " + exchangeRateString;
                        }

                        if (preAuthReversalResponse.BillingCurrency != null)
                        {
                            tranDetailsRichTextBox.Select(0, 0);
                            tranDetailsRichTextBox.SelectedText = "\r\n\tBilling Currency\t :  " + preAuthReversalResponse.BillingCurrency;
                        }

                        if (preAuthReversalResponse.BillingAmount != null)
                        {
                            double billingAmount = Double.Parse(preAuthReversalResponse.BillingAmount) / 100.00;
                            tranDetailsRichTextBox.Select(0, 0);
                            tranDetailsRichTextBox.SelectedText = "\r\n\tBilling Amount\t :  " + billingAmount.ToString();
                        }

                        tranDetailsRichTextBox.Select(0, 0);
                        tranDetailsRichTextBox.SelectedText = "\r\n\tDCC\t\t :  YES";
                    }
                    else
                    {
                        dccStatus = "NO";
                        tranDetailsRichTextBox.Select(0, 0);
                        tranDetailsRichTextBox.SelectedText = "\r\n\tDCC\t\t :  NO";
                    }

                    if (preAuthReversalResponse.RRN != null)
                    {
                        tranDetailsRichTextBox.Select(0, 0);
                        tranDetailsRichTextBox.SelectedText = "\r\n\tRRN\t\t :  " + preAuthReversalResponse.RRN;
                    }

                    if (preAuthReversalResponse.PAN != null)
                    {
                        tranDetailsRichTextBox.Select(0, 0);
                        tranDetailsRichTextBox.SelectedText = "\r\n\tPAN\t\t :  " + preAuthReversalResponse.PAN;
                    }

                    if (preAuthReversalResponse.AuthCode != null)
                    {
                        tranDetailsRichTextBox.Select(0, 0);
                        tranDetailsRichTextBox.SelectedText = "\r\n\tAuth Code\t :  " + preAuthReversalResponse.AuthCode;
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

                    if (preAuthReversalResponse.TerminalId != null)
                    {
                        tranDetailsRichTextBox.Select(0, 0);
                        tranDetailsRichTextBox.SelectedText = "\r\n\r\n\tTID\t\t :  " + preAuthReversalResponse.TerminalId;
                    }

                    tranDetailsRichTextBox.Select(0, 0);
                    tranDetailsRichTextBox.SelectedText = "Reversal Response - " + preAuthReversalResponse.RespText;

                    if (preAuthReversalResponse.RespCode.Equals("00"))
                    {
                        //Enable buttons under condition
                        if (incPreAuthAtLeastOnce)
                        {
                            preAuthReversalButton.Enabled = false;
                            preAuthButton.Enabled = false;
                            incPreAuthButton.Enabled = true;
                            preAuthCompButton.Enabled = true;
                            preAuthCancelButton.Enabled = true;
                            amountTextBox.ReadOnly = false;
                        }
                        else
                        {
                            preAuthReversalButton.Enabled = false;
                            preAuthButton.Enabled = false;
                            incPreAuthButton.Enabled = false;
                            preAuthCompButton.Enabled = false;
                            preAuthCancelButton.Enabled = false;
                            amountTextBox.ReadOnly = false;
                        }

                        transactionStatus = "SUCCESS";
                    }
                    else
                    {
                        preAuthReversalButton.Enabled = true;
                        preAuthButton.Enabled = false;
                        incPreAuthButton.Enabled = true;
                        preAuthCompButton.Enabled = true;
                        preAuthCancelButton.Enabled = true;

                        transactionStatus = "FAILED";
                    }

                    await utils.WriteToExcelFile("Pre-Auth Reversal", dccStatus, preAuthReversalResponse.TerminalId, preAuthReversalResponse.PAN, preAuthReversalResponse.RRN, amount, transactionStatus, preAuthReversalResponse.RespText);
                }

                //Stop the progress bar
                this.progressBar.Style = ProgressBarStyle.Continuous;
                this.progressBar.MarqueeAnimationSpeed = 0;
                this.progressBar.Value = 100;
            }
            else
            {
                this.progressBar.Style = ProgressBarStyle.Continuous;
                amountTextBox.ForeColor = Color.Red;
                amountTextBox.Text = "Enter a valid amount";
                await Task.Delay(1000);
                //MessageBox.Show("Enter a valid amount", "OPI Simulator", MessageBoxButtons.OK, MessageBoxIcon.Error);
                amountTextBox.Text = string.Empty;
                amountTextBox.ForeColor = Color.Black;
            }
        }

        /// <summary>
        /// Incremental pre-auth button onclick method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void incPreAuthButton_Click(object sender, EventArgs e)
        {
            //Setup progress bar settings
            this.progressBar.Maximum = 100;
            this.progressBar.Value = 0;
            this.progressBar.Style = ProgressBarStyle.Marquee;
            this.progressBar.MarqueeAnimationSpeed = 25;

            //Read amount and currency code
            string amount = amountTextBox.Text;
            string currCodeString = currCodesComboBox.Text;

            //Determine whether the provided amount is a numbers
            bool isDouble = Double.TryParse(amount, out double amountDouble);
            int decimalCount = 0;

            if (isDouble)
            {
                decimalCount = utils.getDecimalCount(Double.Parse(amount), amount, "en-US");
            }

            if (isDouble && decimalCount <= 2)
            {
                //Record initial state of buttons
                bool reversalButtonStatus = preAuthReversalButton.Enabled;
                bool incPreAuthButtonStatus = incPreAuthButton.Enabled;
                bool preAuthCompButtonStatus = preAuthCompButton.Enabled;
                bool preAuthCancelButtonStatus = preAuthCancelButton.Enabled;

                string[] currCodeSeperated = currCodeString.Split('-');
                string currCode = currCodeSeperated[0].Trim();

                //Disable buttons accordingly
                preAuthReversalButton.Enabled = false;
                preAuthButton.Enabled = false;
                incPreAuthButton.Enabled = false;
                preAuthCompButton.Enabled = false;
                preAuthCancelButton.Enabled = false;
                amountTextBox.ReadOnly = true;

                double inputAmount = Double.Parse(amount);
                inputAmount = inputAmount * 100;

                //Initialize RestService
                this.baseURL = utils.getBaseURL();
                restService = new RestService(urlTextBox.Text.ToString());

                //Get the transaction request tailored for the available settings
                string requestString = restService.GetEncodedIncPreAuthRequest(inputAmount.ToString(), currCode, true, preAuthResponse.RRN, preAuthResponse.TransToken, preAuthResponse.ExpiryDate, preAuthResponse.PAN);

                //Display request details
                reqDetailsRichTextBox.Select(0, 0);
                reqDetailsRichTextBox.SelectedText = "\r\n\r\n" + requestString + "\r\n\r\n\r\n\r\n";

                reqDetailsRichTextBox.Select(0, 0);
                reqDetailsRichTextBox.SelectedText = "Incremental Pre-Auth Request";

                //Perform transaction
                var response = await restService.PostIncPreAuthRequest(inputAmount.ToString(), currCode, true, preAuthResponse.RRN, preAuthResponse.TransToken, preAuthResponse.ExpiryDate, preAuthResponse.PAN);

                resDetailsRichTextBox.Select(0, 0);
                resDetailsRichTextBox.SelectedText = "\r\n\r\n" + response + "\r\n\r\n\r\n\r\n";

                resDetailsRichTextBox.Select(0, 0);
                resDetailsRichTextBox.SelectedText = "Incremental Pre-Auth Response";

                //Parse transaction response
                incPreAuthResponse = restService.DecodeResponse(response);

                if (incPreAuthResponse != null)
                {
                    string dccStatus = null;
                    string transactionStatus = null;

                    tranDetailsRichTextBox.Select(0, 0);
                    tranDetailsRichTextBox.SelectedText = "\r\n\r\n" + incPreAuthResponse.PrintData + "\r\n\r\n\r\n\r\n";

                    if (incPreAuthResponse.DCCIndicator == null)
                    {
                        //Do nothing
                    }
                    else if (incPreAuthResponse.DCCIndicator.Equals("1"))
                    {
                        dccStatus = "YES";

                        if (incPreAuthResponse.DCCExchangeRate != null)
                        {
                            string firstDigit = incPreAuthResponse.DCCExchangeRate.Substring(0, 1);
                            string lastDigits = incPreAuthResponse.DCCExchangeRate.Substring(1, incPreAuthResponse.DCCExchangeRate.Length - 1);
                            string exchangeRateString = (Double.Parse(lastDigits) / Math.Pow(10, Double.Parse(firstDigit))).ToString();
                            tranDetailsRichTextBox.Select(0, 0);
                            tranDetailsRichTextBox.SelectedText = "\r\n\tExchange Rate\t :  " + exchangeRateString;
                        }

                        if (incPreAuthResponse.BillingCurrency != null)
                        {
                            tranDetailsRichTextBox.Select(0, 0);
                            tranDetailsRichTextBox.SelectedText = "\r\n\tBilling Currency\t :  " + incPreAuthResponse.BillingCurrency;
                        }

                        if (incPreAuthResponse.BillingAmount != null)
                        {
                            double billingAmount = Double.Parse(incPreAuthResponse.BillingAmount) / 100.00;
                            tranDetailsRichTextBox.Select(0, 0);
                            tranDetailsRichTextBox.SelectedText = "\r\n\tBilling Amount\t :  " + billingAmount.ToString();
                        }

                        tranDetailsRichTextBox.Select(0, 0);
                        tranDetailsRichTextBox.SelectedText = "\r\n\tDCC\t\t :  YES";
                    }
                    else
                    {
                        dccStatus = "NO";
                        tranDetailsRichTextBox.Select(0, 0);
                        tranDetailsRichTextBox.SelectedText = "\r\n\tDCC\t\t :  NO";
                    }

                    if (incPreAuthResponse.RRN != null)
                    {
                        tranDetailsRichTextBox.Select(0, 0);
                        tranDetailsRichTextBox.SelectedText = "\r\n\tRRN\t\t :  " + incPreAuthResponse.RRN;
                    }

                    if (incPreAuthResponse.PAN != null)
                    {
                        tranDetailsRichTextBox.Select(0, 0);
                        tranDetailsRichTextBox.SelectedText = "\r\n\tPAN\t\t :  " + incPreAuthResponse.PAN;
                    }

                    if (incPreAuthResponse.AuthCode != null)
                    {
                        tranDetailsRichTextBox.Select(0, 0);
                        tranDetailsRichTextBox.SelectedText = "\r\n\tAuth Code\t :  " + incPreAuthResponse.AuthCode;
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

                    if (incPreAuthResponse.TerminalId != null)
                    {
                        tranDetailsRichTextBox.Select(0, 0);
                        tranDetailsRichTextBox.SelectedText = "\r\n\r\n\tTID\t\t :  " + incPreAuthResponse.TerminalId;
                    }

                    tranDetailsRichTextBox.Select(0, 0);
                    tranDetailsRichTextBox.SelectedText = "Incremental Pre-Auth Response - " + incPreAuthResponse.RespText;

                    if (incPreAuthResponse.RespCode.Equals("00"))
                    {
                        //Increase the Incremental Pre-Auth Transactin count
                        incrementalPreAuthCount++;
                        incPreAuthAtLeastOnce = true;

                        //Enable buttons accordingly.
                        preAuthReversalButton.Enabled = true;
                        preAuthButton.Enabled = false;
                        incPreAuthButton.Enabled = true;
                        preAuthCompButton.Enabled = true;
                        preAuthCancelButton.Enabled = true;
                        amountTextBox.ReadOnly = false;

                        transactionStatus = "SUCCESS";
                    }
                    else
                    {
                        //Restore buttons to original status, since the completion is unsuccessful.
                        preAuthReversalButton.Enabled = reversalButtonStatus;
                        preAuthButton.Enabled = false;
                        incPreAuthButton.Enabled = incPreAuthButtonStatus;
                        preAuthCompButton.Enabled = preAuthCompButtonStatus;
                        preAuthCancelButton.Enabled = preAuthCancelButtonStatus;
                        amountTextBox.ReadOnly = false;

                        transactionStatus = "FAILED";
                    }

                    await utils.WriteToExcelFile("Incremental Pre-Auth", dccStatus, incPreAuthResponse.TerminalId, incPreAuthResponse.PAN, incPreAuthResponse.RRN, amount, transactionStatus, incPreAuthResponse.RespText);
                }

                //Stop the progress bar
                this.progressBar.Style = ProgressBarStyle.Continuous;
                this.progressBar.MarqueeAnimationSpeed = 0;
                this.progressBar.Value = 100;
            }
            else
            {
                this.progressBar.Style = ProgressBarStyle.Continuous;
                amountTextBox.ForeColor = Color.Red;
                amountTextBox.Text = "Enter a valid amount";
                await Task.Delay(1000);
                amountTextBox.Text = string.Empty;
                amountTextBox.ForeColor = Color.Black;
            }
        }

        /// <summary>
        /// Onclick method for tranDetCopyButton.
        /// Copies the content of the transaction details rich text box to the clipboard.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void tranDetCopyButton_Click(object sender, EventArgs e)
        {
            if (tranDetailsRichTextBox.Text != null && tranDetailsRichTextBox.Text.Length != 0)
            {
                Clipboard.SetText(tranDetailsRichTextBox.Text);
                label8.ForeColor = Color.Green;
                label8.Text = "Copied";
                label8.Visible = true;
                await Task.Delay(1000);
                label8.Visible = false;
            }
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
                reqDelCopyLabel.ForeColor = Color.Green;
                reqDelCopyLabel.Text = "Copied";
                reqDelCopyLabel.Visible = true;
                await Task.Delay(1000);
                reqDelCopyLabel.Visible = false;
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
            if (resDetailsRichTextBox.Text != null && resDetailsRichTextBox.Text.Length != 0)
            {
                Clipboard.SetText(resDetailsRichTextBox.Text);
                resDetCopyLabel.ForeColor = Color.Green;
                resDetCopyLabel.Text = "Copied";
                resDetCopyLabel.Visible = true;
                await Task.Delay(1000);
                resDetCopyLabel.Visible = false;
            }
        }
    }
}
