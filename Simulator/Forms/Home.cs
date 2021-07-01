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
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using System.Diagnostics;
using Simulator.Shared;

namespace Simulator.Forms
{
    public partial class Home : Form
    {
        private Utils utils = null;

        public Home()
        {
            InitializeComponent();
            ValidateSequenceNumber();
        }

        /// <summary>
        /// Method for validating the Sequence Number. Sequence number increments with each request.
        /// Resets at 1 million. 
        /// </summary>
        private void ValidateSequenceNumber()
        {
            Settings.Default.Reload();

            int seqNumber = (int) Settings.Default["sequenceNumber"];

            if(seqNumber >= 1000000)
            {
                seqNumber = 0;
                Settings.Default["sequenceNumber"] = seqNumber;
                Settings.Default.Save();
            }
        }

        /// <summary>
        /// If the excel file path is set in the settings, create an excel file 
        /// with the current timestamp. 
        /// </summary>
        private async Task SetExcelFileSettings()
        {
            Settings.Default.Reload();

            if(!(Settings.Default["filePath"].ToString().Length == 0))
            {
                utils = new Utils();
                await utils.CreateExcelFile();
            }
        }

        /// <summary>
        /// On load hide all the sub user control forms.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Home_Load(object sender, EventArgs e)
        {
            purchaseUserControl1.Hide();
            preAuthUserControl1.Hide();
            reversalUserControl1.Hide();
            settingsUserControl1.Hide();
            await SetExcelFileSettings();
        }

        /// <summary>
        /// Show the Purchase user control.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void purchaseUserControlButton_Click(object sender, EventArgs e)
        {
            preAuthUserControl1.Hide();
            reversalUserControl1.Hide();
            settingsUserControl1.Hide();

            purchaseUserControl1.ClearFields();
            purchaseUserControl1.PopulateCurrecyCodes();
            purchaseUserControl1.Show();
            purchaseUserControl1.BringToFront();
        }

        /// <summary>
        /// Show Pre Auth user control.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void preAuthUserControlButton_Click(object sender, EventArgs e)
        {
            purchaseUserControl1.Hide();
            reversalUserControl1.Hide();
            settingsUserControl1.Hide();

            preAuthUserControl1.ClearFields();
            preAuthUserControl1.PopulateCurrecyCodes();
            preAuthUserControl1.Show();
            preAuthUserControl1.BringToFront();
        }

        /// <summary>
        /// Show Reversal user control.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void reversalUserControlButton_Click(object sender, EventArgs e)
        {
            purchaseUserControl1.Hide();
            preAuthUserControl1.Hide();
            settingsUserControl1.Hide();

            reversalUserControl1.Show();
            reversalUserControl1.clearFields();
            reversalUserControl1.BringToFront();
        }

        /// <summary>
        /// Show Settings user control.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void settingsUserControlButton_Click(object sender, EventArgs e)
        {
            purchaseUserControl1.Hide();
            preAuthUserControl1.Hide();
            reversalUserControl1.Hide();

            settingsUserControl1.Show();
            settingsUserControl1.BringToFront();
        }
    }
}
