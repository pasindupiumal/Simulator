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
    public partial class SettingsUserControl : UserControl
    {
        public SettingsUserControl()
        {
            InitializeComponent();
            loadSettings();
        }

        private void loadSettings()
        {
            Settings.Default.Reload();
            textBox1.Text = Settings.Default["ip"].ToString();
            textBox2.Text = Settings.Default["defaultAmount"].ToString();
            textBox3.Text = Settings.Default["operatorValue"].ToString();
            textBox4.Text = Settings.Default["industryCode"].ToString();
            textBox5.Text = Settings.Default["siteID"].ToString();
            textBox8.Text = Settings.Default["wsNo"].ToString();
            textBox6.Text = Settings.Default["proxyInfo"].ToString();
            textBox7.Text = Settings.Default["posInfo"].ToString();
            textBox9.Text = Settings.Default["lodgingCode"].ToString();
            textBox10.Text = Settings.Default["guestNo"].ToString();
            textBox11.Text = Settings.Default["currCodes"].ToString();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private async void button2_Click(object sender, EventArgs e)
        {
            Settings.Default["ip"] = textBox1.Text;
            Settings.Default["defaultAmount"] = textBox2.Text;
            Settings.Default["operatorValue"] = textBox3.Text;
            Settings.Default["industryCode"] = textBox4.Text;
            Settings.Default["siteID"] = textBox5.Text;
            Settings.Default["wsNo"] = textBox8.Text;
            Settings.Default["proxyInfo"] = textBox6.Text;
            Settings.Default["posInfo"] = textBox7.Text;
            Settings.Default["lodgingCode"] = textBox9.Text;
            Settings.Default["guestNo"] = textBox10.Text;
            Settings.Default["currCodes"] = textBox11.Text;
            Settings.Default.Save();
            loadSettings();

            //Dispaly success message
            label11.ForeColor = Color.Green;
            label11.Text = "Settings Updated Successfully!";
            label11.Visible = true;
            await Task.Delay(2000);
            label11.Visible = false;
         
            //MessageBox.Show("Settings Updated Successfully!", "OPI Simulator", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }
    }
}
