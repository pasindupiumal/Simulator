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
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
            validateSequenceNumber();
        }

        /// <summary>
        /// Method for validating the Sequence Number. Sequence number increments with each request.
        /// Resets at 1 million. 
        /// </summary>
        private void validateSequenceNumber()
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

        private void purchaseUserControl1_Load(object sender, EventArgs e)
        {

        }

        private void settingsUserControl1_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// On load hide all the sub user control forms.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Home_Load(object sender, EventArgs e)
        {
            purchaseUserControl1.Hide();
            preAuthUserControl1.Hide();
            reversalUserControl1.Hide();
            settingsUserControl1.Hide();
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

            purchaseUserControl1.clearFields();
            purchaseUserControl1.populateCurrecyCodes();
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

            preAuthUserControl1.clearFields();
            preAuthUserControl1.populateCurrecyCodes();
            preAuthUserControl1.Show();
            preAuthUserControl1.BringToFront();
        }

        private void reversalUserControl1_Load(object sender, EventArgs e)
        {

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

        private void preAuthUserControl1_Load(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void refundUserControlButton_Click(object sender, EventArgs e)
        {

        }
    }
}
