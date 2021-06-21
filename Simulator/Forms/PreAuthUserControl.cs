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
            //Setup progress bar settings
            this.progressBar1.Maximum = 10;
            this.progressBar1.Value = 10;
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
                        amountTextBox.ReadOnly = false;
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

                //Stop the progress bar
                this.progressBar1.Value = 10;
                this.progressBar1.Style = ProgressBarStyle.Continuous;
                this.progressBar1.MarqueeAnimationSpeed = 0;
            }
            else
            {
                MessageBox.Show("Enter a valid amount", "OPI Simulator", MessageBoxButtons.OK, MessageBoxIcon.Error);
                amountTextBox.Text = string.Empty;
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            //Setup progress bar settings
            this.progressBar1.Maximum = 100;
            this.progressBar1.Value = 0;
            this.progressBar1.Style = ProgressBarStyle.Marquee;
            this.progressBar1.MarqueeAnimationSpeed = 25;

            //Disable buttons
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            amountTextBox.ReadOnly = true;

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
            preAuthReversalResponse = restService.DecodeResponse(response);

            if (preAuthReversalResponse != null)
            {
                richTextBox3.Select(0, 0);
                richTextBox3.SelectedText = "\r\n\r\n" + preAuthReversalResponse.PrintData + "\r\n\r\n\r\n\r\n";

                richTextBox3.Select(0, 0);
                richTextBox3.SelectedText = "\r\n\tRRN             :  " + preAuthReversalResponse.RRN;

                richTextBox3.Select(0, 0);
                richTextBox3.SelectedText = "\r\n\tPAN              :  " + preAuthReversalResponse.PAN;

                richTextBox3.Select(0, 0);
                richTextBox3.SelectedText = "\r\n\tAuth Code  :  " + preAuthReversalResponse.AuthCode;

                richTextBox3.Select(0, 0);
                richTextBox3.SelectedText = "\r\n\r\n\tTID               :  " + preAuthReversalResponse.TerminalId;

                richTextBox3.Select(0, 0);
                richTextBox3.SelectedText = "Reversal Response - " + preAuthReversalResponse.RespText;

                if (preAuthReversalResponse.RespCode.Equals("00"))
                {
                    //Enable buttons under condition
                    if (incPreAuthAtLeastOnce)
                    {
                        button1.Enabled = false;
                        button2.Enabled = false;
                        button3.Enabled = true;
                        button4.Enabled = true;
                        button5.Enabled = true;
                        amountTextBox.ReadOnly = false;
                    }
                    else
                    {
                        button1.Enabled = false;
                        button2.Enabled = false;
                        button3.Enabled = false;
                        button4.Enabled = false;
                        button5.Enabled = false;
                        amountTextBox.ReadOnly = false;
                    }
                }
                else
                {
                    button1.Enabled = true;
                    button2.Enabled = false;
                    button3.Enabled = true;
                    button4.Enabled = true;
                    button5.Enabled = true;
                }
            }

            //Stop the progress bar
            this.progressBar1.Style = ProgressBarStyle.Continuous;
            this.progressBar1.MarqueeAnimationSpeed = 0;
            this.progressBar1.Value = 100;
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            //Setup progress bar settings
            this.progressBar1.Maximum = 100;
            this.progressBar1.Value = 0;
            this.progressBar1.Style = ProgressBarStyle.Marquee;
            this.progressBar1.MarqueeAnimationSpeed = 25;

            //Record initial state of buttons
            bool button1Status = button1.Enabled;
            bool button3Status = button3.Enabled;
            bool button4Status = button4.Enabled;
            bool button5Status = button5.Enabled;

            //Disable buttons accordingly
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            amountTextBox.ReadOnly = true;

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
            //string authCode, string originalRRN, string transToken, string expiryDate, string pan
            string requestString = restService.GetEncodedPreAuthCompleteRequest(inputAmount.ToString(), currCode, true, preAuthResponse.AuthCode, preAuthResponse.RRN, preAuthResponse.TransToken, preAuthResponse.ExpiryDate, preAuthResponse.PAN);

            //Display request details
            richTextBox2.Select(0, 0);
            richTextBox2.SelectedText = "\r\n\r\n" + requestString + "\r\n\r\n\r\n\r\n";

            richTextBox2.Select(0, 0);
            richTextBox2.SelectedText = "Pre-Auth Completion Request";

            //Perform transaction
            var response = await restService.PostPreAuthCompletionRequest(inputAmount.ToString(), currCode, true, preAuthResponse.AuthCode, preAuthResponse.RRN, preAuthResponse.TransToken, preAuthResponse.ExpiryDate, preAuthResponse.PAN);

            richTextBox1.Select(0, 0);
            richTextBox1.SelectedText = "\r\n\r\n" + response + "\r\n\r\n\r\n\r\n";

            richTextBox1.Select(0, 0);
            richTextBox1.SelectedText = "Pre-Auth Completion Response";

            //Parse transaction response
            preAuthCompletionResponse = restService.DecodeResponse(response);

            if (preAuthCompletionResponse != null)
            {
                richTextBox3.Select(0, 0);
                richTextBox3.SelectedText = "\r\n\r\n" + preAuthCompletionResponse.PrintData + "\r\n\r\n\r\n\r\n";

                richTextBox3.Select(0, 0);
                richTextBox3.SelectedText = "\r\n\tRRN             :  " + preAuthCompletionResponse.RRN;

                richTextBox3.Select(0, 0);
                richTextBox3.SelectedText = "\r\n\tPAN              :  " + preAuthCompletionResponse.PAN;

                richTextBox3.Select(0, 0);
                richTextBox3.SelectedText = "\r\n\tAuth Code  :  " + preAuthCompletionResponse.AuthCode;

                richTextBox3.Select(0, 0);
                richTextBox3.SelectedText = "\r\n\r\n\tTID               :  " + preAuthCompletionResponse.TerminalId;

                richTextBox3.Select(0, 0);
                richTextBox3.SelectedText = "Pre-Auth Completion Response - " + preAuthCompletionResponse.RespText;

                if (preAuthCompletionResponse.RespCode.Equals("00"))
                {
                    //Diable all buttons
                    button1.Enabled = false;
                    button2.Enabled = false;
                    button3.Enabled = false;
                    button4.Enabled = false;
                    button5.Enabled = false;
                    amountTextBox.ReadOnly = false;
                }
                else
                {
                    //Restore buttons to original status, since the completion is unsuccessful.
                    button1.Enabled = button1Status;
                    button2.Enabled = false;
                    button3.Enabled = button3Status;
                    button4.Enabled = button4Status;
                    button5.Enabled = button5Status;
                    amountTextBox.ReadOnly = false;
                }
            }

            //Stop the progress bar
            this.progressBar1.Style = ProgressBarStyle.Continuous;
            this.progressBar1.MarqueeAnimationSpeed = 0;
            this.progressBar1.Value = 100;
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            //Setup progress bar settings
            this.progressBar1.Maximum = 100;
            this.progressBar1.Value = 0;
            this.progressBar1.Style = ProgressBarStyle.Marquee;
            this.progressBar1.MarqueeAnimationSpeed = 25;

            //Record initial state of buttons
            bool button1Status = button1.Enabled;
            bool button3Status = button3.Enabled;
            bool button4Status = button4.Enabled;
            bool button5Status = button5.Enabled;

            //Disable buttons accordingly
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            amountTextBox.ReadOnly = true;

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
            string requestString = restService.GetEncodedPreAuthCancelRequest(inputAmount.ToString(), currCode, true, preAuthResponse.RRN, preAuthResponse.TransToken, preAuthResponse.ExpiryDate, preAuthResponse.PAN);

            //Display request details
            richTextBox2.Select(0, 0);
            richTextBox2.SelectedText = "\r\n\r\n" + requestString + "\r\n\r\n\r\n\r\n";

            richTextBox2.Select(0, 0);
            richTextBox2.SelectedText = "Pre-Auth Cancelation Request";

            //Perform transaction
            var response = await restService.PostPreAuthCancelRequest(inputAmount.ToString(), currCode, true, preAuthResponse.RRN, preAuthResponse.TransToken, preAuthResponse.ExpiryDate, preAuthResponse.PAN);

            richTextBox1.Select(0, 0);
            richTextBox1.SelectedText = "\r\n\r\n" + response + "\r\n\r\n\r\n\r\n";

            richTextBox1.Select(0, 0);
            richTextBox1.SelectedText = "Pre-Auth Cancelation Response";

            //Parse transaction response
            preAuthCancelationResponse = restService.DecodeResponse(response);

            if (preAuthCancelationResponse != null)
            {
                richTextBox3.Select(0, 0);
                richTextBox3.SelectedText = "\r\n\r\n" + preAuthCancelationResponse.PrintData + "\r\n\r\n\r\n\r\n";

                richTextBox3.Select(0, 0);
                richTextBox3.SelectedText = "\r\n\tRRN             :  " + preAuthCancelationResponse.RRN;

                richTextBox3.Select(0, 0);
                richTextBox3.SelectedText = "\r\n\tPAN              :  " + preAuthCancelationResponse.PAN;

                richTextBox3.Select(0, 0);
                richTextBox3.SelectedText = "\r\n\tAuth Code  :  " + preAuthCancelationResponse.AuthCode;

                richTextBox3.Select(0, 0);
                richTextBox3.SelectedText = "\r\n\r\n\tTID               :  " + preAuthCancelationResponse.TerminalId;

                richTextBox3.Select(0, 0);
                richTextBox3.SelectedText = "Pre-Auth Cancelation Response - " + preAuthCancelationResponse.RespText;

                if (preAuthCancelationResponse.RespCode.Equals("00"))
                {
                    //Diable all buttons
                    button1.Enabled = false;
                    button2.Enabled = false;
                    button3.Enabled = false;
                    button4.Enabled = false;
                    button5.Enabled = false;
                    amountTextBox.ReadOnly = false;
                }
                else
                {
                    //Restore buttons to original status, since the completion is unsuccessful.
                    button1.Enabled = button1Status;
                    button2.Enabled = false;
                    button3.Enabled = button3Status;
                    button4.Enabled = button4Status;
                    button5.Enabled = button5Status;
                    amountTextBox.ReadOnly = false;
                }
            }

            //Stop the progress bar
            this.progressBar1.Style = ProgressBarStyle.Continuous;
            this.progressBar1.MarqueeAnimationSpeed = 0;
            this.progressBar1.Value = 100;
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            //Setup progress bar settings
            this.progressBar1.Maximum = 100;
            this.progressBar1.Value = 0;
            this.progressBar1.Style = ProgressBarStyle.Marquee;
            this.progressBar1.MarqueeAnimationSpeed = 25;

            //Record initial state of buttons
            bool button1Status = button1.Enabled;
            bool button3Status = button3.Enabled;
            bool button4Status = button4.Enabled;
            bool button5Status = button5.Enabled;

            //Disable buttons accordingly
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            amountTextBox.ReadOnly = true;

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
            string requestString = restService.GetEncodedIncPreAuthRequest(inputAmount.ToString(), currCode, true, preAuthResponse.RRN, preAuthResponse.TransToken, preAuthResponse.ExpiryDate, preAuthResponse.PAN);

            //Display request details
            richTextBox2.Select(0, 0);
            richTextBox2.SelectedText = "\r\n\r\n" + requestString + "\r\n\r\n\r\n\r\n";

            richTextBox2.Select(0, 0);
            richTextBox2.SelectedText = "Incremental Pre-Auth Request";

            //Perform transaction
            var response = await restService.PostIncPreAuthRequest(inputAmount.ToString(), currCode, true, preAuthResponse.RRN, preAuthResponse.TransToken, preAuthResponse.ExpiryDate, preAuthResponse.PAN);

            richTextBox1.Select(0, 0);
            richTextBox1.SelectedText = "\r\n\r\n" + response + "\r\n\r\n\r\n\r\n";

            richTextBox1.Select(0, 0);
            richTextBox1.SelectedText = "Incremental Pre-Auth Response";

            //Parse transaction response
            incPreAuthResponse = restService.DecodeResponse(response);

            if (incPreAuthResponse != null)
            {
                richTextBox3.Select(0, 0);
                richTextBox3.SelectedText = "\r\n\r\n" + incPreAuthResponse.PrintData + "\r\n\r\n\r\n\r\n";

                richTextBox3.Select(0, 0);
                richTextBox3.SelectedText = "\r\n\tRRN             :  " + incPreAuthResponse.RRN;

                richTextBox3.Select(0, 0);
                richTextBox3.SelectedText = "\r\n\tPAN              :  " + incPreAuthResponse.PAN;

                richTextBox3.Select(0, 0);
                richTextBox3.SelectedText = "\r\n\tAuth Code  :  " + incPreAuthResponse.AuthCode;

                richTextBox3.Select(0, 0);
                richTextBox3.SelectedText = "\r\n\r\n\tTID               :  " + incPreAuthResponse.TerminalId;

                richTextBox3.Select(0, 0);
                richTextBox3.SelectedText = "Incremental Pre-Auth Response - " + incPreAuthResponse.RespText;

                if (incPreAuthResponse.RespCode.Equals("00"))
                {
                    //Increase the Incremental Pre-Auth Transactin count
                    incrementalPreAuthCount++;
                    incPreAuthAtLeastOnce = true;

                    //Diable all buttons
                    button1.Enabled = true;
                    button2.Enabled = false;
                    button3.Enabled = true;
                    button4.Enabled = true;
                    button5.Enabled = true;
                    amountTextBox.ReadOnly = false; ;
                }
                else
                {
                    //Restore buttons to original status, since the completion is unsuccessful.
                    button1.Enabled = button1Status;
                    button2.Enabled = false;
                    button3.Enabled = button3Status;
                    button4.Enabled = button4Status;
                    button5.Enabled = button5Status;
                    amountTextBox.ReadOnly = false;
                }
            }

            //Stop the progress bar
            this.progressBar1.Style = ProgressBarStyle.Continuous;
            this.progressBar1.MarqueeAnimationSpeed = 0;
            this.progressBar1.Value = 100;
        }
    }
}
