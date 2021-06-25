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
            LoadSettings();
        }

        /// <summary>
        /// Method for loading settings
        /// </summary>
        private void LoadSettings()
        {
            Settings.Default.Reload();
            urlTextBox.Text = Settings.Default["ip"].ToString();
            defaultAmountTextBox.Text = Settings.Default["defaultAmount"].ToString();
            operatorTextBox.Text = Settings.Default["operatorValue"].ToString();
            industryCodeTextBox.Text = Settings.Default["industryCode"].ToString();
            siteIDTextBox.Text = Settings.Default["siteID"].ToString();
            wsNoTextBox.Text = Settings.Default["wsNo"].ToString();
            proxyInfoTextBox.Text = Settings.Default["proxyInfo"].ToString();
            posInfoTextBox.Text = Settings.Default["posInfo"].ToString();
            lodgingCodeTextBox.Text = Settings.Default["lodgingCode"].ToString();
            guestNoTextBox.Text = Settings.Default["guestNo"].ToString();
            currCodesTextBox.Text = Settings.Default["currCodes"].ToString();
        }

        /// <summary>
        /// Method for saving settings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void saveButton_Click(object sender, EventArgs e)
        {
            Settings.Default["ip"] = urlTextBox.Text;
            Settings.Default["defaultAmount"] = defaultAmountTextBox.Text;
            Settings.Default["operatorValue"] = operatorTextBox.Text;
            Settings.Default["industryCode"] = industryCodeTextBox.Text;
            Settings.Default["siteID"] = siteIDTextBox.Text;
            Settings.Default["wsNo"] = wsNoTextBox.Text;
            Settings.Default["proxyInfo"] = proxyInfoTextBox.Text;
            Settings.Default["posInfo"] = posInfoTextBox.Text;
            Settings.Default["lodgingCode"] = lodgingCodeTextBox.Text;
            Settings.Default["guestNo"] = guestNoTextBox.Text;
            Settings.Default["currCodes"] = currCodesTextBox.Text;
            Settings.Default.Save();
            LoadSettings();

            //Dispaly success message
            savedSuccessLabel.ForeColor = Color.Green;
            savedSuccessLabel.Text = "Settings Updated Successfully!";
            savedSuccessLabel.Visible = true;
            await Task.Delay(2000);
            savedSuccessLabel.Visible = false;

            //MessageBox.Show("Settings Updated Successfully!", "OPI Simulator", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
