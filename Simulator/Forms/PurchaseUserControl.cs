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

        private void button3_Click(object sender, EventArgs e)
        {
            //Read amount and currency code
            string amount = amountTextBox.Text;
            string currCode = currCodeTextBox.Text;

            //Determine whether the provided amount is a numbers
            bool isDouble = Double.TryParse(amount, out double amountDouble);

            if (isDouble)
            {
                double inputAmount = amountDouble * 100;

                //Initialize RestService
                this.baseURL = utils.getBaseURL();
                restService = new RestService(this.baseURL);

                //Get the transaction request tailored for the available settings
                string requestString = restService.GetEncodedRequest(inputAmount.ToString(), currCode);

                //Display request details
                richTextBox2.AppendText("IP Address : " + this.baseURL);
                richTextBox2.AppendText("\r\n\r\n" + requestString);

                amountTextBox.ReadOnly = true;
                currCodeTextBox.ReadOnly = true;
                button1.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = false;
                button4.Enabled = true;
            }
            else
            {
                MessageBox.Show("Enter a valid amount", "OPI Simulator", MessageBoxButtons.OK, MessageBoxIcon.Error);
                amountTextBox.Text = string.Empty;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            amountTextBox.ReadOnly = false;
            amountTextBox.Text = string.Empty;
            currCodeTextBox.ReadOnly = false;
            currCodeTextBox.Text = string.Empty;
            button3.Enabled = true;
            richTextBox2.Text = string.Empty;
            button1.Enabled = false;
            button2.Enabled = false;
        }

        private async void button2_Click_1(object sender, EventArgs e)
        {
            if (button3.Enabled)
            {
                MessageBox.Show("Please submit the purchase request first", "OPI Simulator", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                //Setup progress bar settings
                this.progressBar1.Maximum = 100;
                this.progressBar1.Value = 0;
                this.timer2.Start();

                //Initialize RestService
                this.baseURL = utils.getBaseURL();
                restService = new RestService(this.baseURL);

                //Conversions on the amount
                double amount = Double.Parse(amountTextBox.Text);
                amount = amount * 100;

                //Perform transaction
                var response = await restService.PostEncoded(amount.ToString(), currCodeTextBox.Text);
                richTextBox1.Text = response;
                this.progressBar1.Value = 100;
                this.timer2.Stop();

                //Parse transaction response
                TransactionResponse transactionResponse = restService.DecodeResponse(response);

                if (transactionResponse != null)
                {
                    label5.Text = transactionResponse.RespText;
                    label5.BackColor = System.Drawing.Color.Lime;
                    label5.Visible = true;

                    richTextBox3.AppendText("\r\n\r\n\r\n\n\t" + transactionResponse.PrintData);
                }
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (button3.Enabled)
            {
                MessageBox.Show("Please submit the purchase request first", "OPI Simulator", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                //Initialize RestService
                this.baseURL = utils.getBaseURL();
                restService = new RestService(this.baseURL);
                this.progressBar1.Maximum = 100;
                this.progressBar1.Value = 0;
                this.timer2.Start();

                var response = await restService.Post();

                richTextBox1.Text = response;
                this.timer2.Stop();
                this.progressBar1.Value = 100;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            this.progressBar1.Increment(1);
        }
    }
}
