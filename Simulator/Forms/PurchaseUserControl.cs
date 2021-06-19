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

        public void clearFields()
        {
            amountTextBox.Text = "100";
            amountTextBox.ReadOnly = false;
            currCodeTextBox.Text = "752";
            currCodeTextBox.ReadOnly = false;
            button1.Enabled = true;
            button2.Enabled = true;
            textBox1.ReadOnly = false;
            richTextBox1.Text = string.Empty;
            richTextBox2.Text = string.Empty;
            richTextBox3.Text = string.Empty;
            button1.Enabled = false;
        }

        private async void button2_Click_1(object sender, EventArgs e)
        {
            //Read amount and currency code
            string amount = amountTextBox.Text;
            string currCode = currCodeTextBox.Text;

            //Determine whether the provided amount is a numbers
            bool isDouble = Double.TryParse(amount, out double amountDouble);

            if (isDouble)
            {
                //Disable the input fields
                amountTextBox.ReadOnly = true;
                currCodeTextBox.ReadOnly = true;
                textBox1.ReadOnly = true;

                //Setup progress bar settings
                this.progressBar1.Maximum = 100;
                this.progressBar1.Value = 0;
                this.timer2.Start();

                double inputAmount = amountDouble * 100;

                //Initialize RestService
                this.baseURL = utils.getBaseURL();
                restService = new RestService(textBox1.Text.ToString());

                //Get the transaction request tailored for the available settings
                string requestString = restService.GetEncodedPurchaseRequest(inputAmount.ToString(), currCode);

                //Display request details
                richTextBox2.AppendText("IP Address : " + this.baseURL);
                richTextBox2.AppendText("\r\n\r\nPurchase Request\r\n\r\n");
                richTextBox2.AppendText(requestString + "\r\n\r\n");

                //Perform transaction
                var response = await restService.PostPurchaseRequest(inputAmount.ToString(), currCodeTextBox.Text);
                richTextBox1.AppendText("Purchase Response\r\n\r\n");
                richTextBox1.AppendText(response + "\r\n\r\n\r\n");
                this.progressBar1.Value = 100;
                this.timer2.Stop();
                button2.Enabled = false;
                button1.Enabled = true;
                

                //Parse transaction response
                TransactionResponse transactionResponse = restService.DecodeResponse(response);

                if (transactionResponse != null)
                {

                    //Label label = new Label();

                    //StringBuilder builder = new StringBuilder();

                    //richTextBox3.ForeColor = Color.Blue;

                    //label.Text = transactionResponse.RespText;

                    //if (transactionResponse.RespText.Equals("TransactionApproved"))
                    //{
                    //    label.BackColor = System.Drawing.Color.Lime;
                    //}
                    //else
                    //{
                    //    label.BackColor = System.Drawing.Color.Red;
                    //}

                    //richTextBox3.AppendText(label.ToString());

                    richTextBox3.AppendText(transactionResponse.RespText);
                    richTextBox3.AppendText("\r\n\r\n" + transactionResponse.PrintData);
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
            string currCode = currCodeTextBox.Text;

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
            string requestString = restService.GetEncodedReversalRequest(inputAmount.ToString(), currCode);

            //Display request details
            richTextBox2.AppendText("Reversal Request\r\n\r\n");
            richTextBox2.AppendText(requestString + "\r\n\r\n");

            //Perform transaction
            var response = await restService.PostReversalRequest(inputAmount.ToString(), currCodeTextBox.Text);
            richTextBox1.AppendText("Reversal Response\r\n\r\n");
            richTextBox1.AppendText(response + "\r\n\r\n\r\n");
            this.progressBar1.Value = 100;
            this.timer2.Stop();

            //Parse transaction response
            TransactionResponse transactionResponse = restService.DecodeResponse(response);

            if (transactionResponse != null)
            {

                //Label label = new Label();

                //StringBuilder builder = new StringBuilder();

                //richTextBox3.ForeColor = Color.Blue;

                //label.Text = transactionResponse.RespText;

                //if (transactionResponse.RespText.Equals("TransactionApproved"))
                //{
                //    label.BackColor = System.Drawing.Color.Lime;
                //}
                //else
                //{
                //    label.BackColor = System.Drawing.Color.Red;
                //}

                //richTextBox3.AppendText(label.ToString());

                richTextBox3.AppendText(transactionResponse.RespText);
                richTextBox3.AppendText("\r\n\r\n" + transactionResponse.PrintData);
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            this.progressBar1.Increment(1);
        }
    }
}
