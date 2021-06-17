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

namespace Simulator
{
    public partial class Form1 : Form
    {
        private RestService restService = null;

        public Form1()
        {
            InitializeComponent();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            if (button3.Enabled)
            {
                MessageBox.Show("Please submit the purchase request first", "OPI Simulator", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                //Initialize RestService
                restService = new RestService("https://192.168.1.109:8080");
                double amount = Double.Parse(amountTextBox.Text);
                amount = amount * 100;
                this.progressBar1.Maximum = 100;
                this.progressBar1.Value = 0;
                this.timer2.Start();
                var response = await restService.PostEncoded(amount.ToString(), currCodeTextBox.Text);
                this.timer2.Stop();
                this.progressBar1.Value = 100;

                richTextBox1.Text = response;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            this.progressBar1.Increment(1);
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if(button3.Enabled)
            {
                MessageBox.Show("Please submit the purchase request first", "OPI Simulator", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                //Initialize RestService
                restService = new RestService("https://192.168.1.109:8080");
                this.progressBar1.Maximum = 100;
                this.progressBar1.Value = 0;
                this.timer2.Start();
                var response = await restService.Post();
                this.timer2.Stop();
                this.progressBar1.Value = 100;

                richTextBox1.Text = response;
            }
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string amount = amountTextBox.Text;
            string currCode = currCodeTextBox.Text;

            bool isDouble = Double.TryParse(amount, out double amountDouble);

            if (isDouble)
            {
                double inputAmount = amountDouble * 100;

                //Initialize RestService
                restService = new RestService("https://192.168.1.109:8080");

                string requestString = restService.GetEncodedRequest(inputAmount.ToString(), currCode);
                richTextBox2.Text = requestString;

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
    }
}
