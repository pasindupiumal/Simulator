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
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            this.progressBar1.Maximum = 100;
            this.progressBar1.Value = 0;
            this.timer1.Start();
            var response = await RestService.GetAllEmployees();
            this.timer1.Stop();
            this.progressBar1.Value = 100;

            richTextBox1.Text = response;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.progressBar1.Increment(1);
            
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            this.progressBar1.Maximum = 100;
            this.progressBar1.Value = 0;
            this.timer2.Start();
            var response = await RestS.Post();
            this.timer2.Stop();
            this.progressBar1.Value = 100;

            richTextBox1.Text = response;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            this.progressBar1.Increment(1);
        }
    }
}
