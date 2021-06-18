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
            textBox1.Text = Settings.Default["ip"].ToString();
            textBox2.Text = Settings.Default["port"].ToString();
            textBox3.Text = Settings.Default["operatorValue"].ToString();
            textBox4.Text = Settings.Default["industryCode"].ToString();
            textBox5.Text = Settings.Default["siteID"].ToString();
            textBox6.Text = Settings.Default["wsNo"].ToString();
            textBox7.Text = Settings.Default["proxyInfo"].ToString();
            textBox8.Text = Settings.Default["posInfo"].ToString();
            textBox9.Text = Settings.Default["lodgingCode"].ToString();
            textBox10.Text = Settings.Default["guestNo"].ToString();
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

        private void button2_Click(object sender, EventArgs e)
        {
            Settings.Default["ip"] = textBox1.Text;
            Settings.Default["port"] = textBox2.Text;
            Settings.Default["operatorValue"] = textBox3.Text;
            Settings.Default["industryCode"] = textBox4.Text;
            Settings.Default["siteID"] = textBox5.Text;
            Settings.Default["wsNo"] = textBox6.Text;
            Settings.Default["proxyInfo"] = textBox7.Text;
            Settings.Default["posInfo"] = textBox8.Text;
            Settings.Default["lodgingCode"] = textBox9.Text;
            Settings.Default["guestNo"] = textBox10.Text;
            Settings.Default.Save();
            MessageBox.Show("Settings Updated Successfully!", "OPI Simulator", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
