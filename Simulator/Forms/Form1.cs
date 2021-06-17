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

        private void timer2_Tick(object sender, EventArgs e)
        {
            this.progressBar1.Increment(1);
        }
    }
}
