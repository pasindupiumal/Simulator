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

        private void Home_Load(object sender, EventArgs e)
        {
            purchaseUserControl1.Hide();
            preAuthUserControl1.Hide();
            refundUserControl1.Hide();
            reversalUserControl1.Hide();
            settingsUserControl1.Hide();
        }

        private void purchaseUserControlButton_Click(object sender, EventArgs e)
        {
            preAuthUserControl1.Hide();
            refundUserControl1.Hide();
            reversalUserControl1.Hide();
            settingsUserControl1.Hide();

            purchaseUserControl1.clearFields();
            purchaseUserControl1.populateCurrecyCodes();
            purchaseUserControl1.Show();
            purchaseUserControl1.BringToFront();
        }

        private void preAuthUserControlButton_Click(object sender, EventArgs e)
        {
            purchaseUserControl1.Hide();
            refundUserControl1.Hide();
            reversalUserControl1.Hide();
            settingsUserControl1.Hide();

            preAuthUserControl1.Show();
            preAuthUserControl1.BringToFront();
        }

        private void reversalUserControl1_Load(object sender, EventArgs e)
        {

        }

        private void refundUserControlButton_Click(object sender, EventArgs e)
        {
            purchaseUserControl1.Hide();
            preAuthUserControl1.Hide();
            reversalUserControl1.Hide();
            settingsUserControl1.Hide();

            refundUserControl1.Show();
            refundUserControl1.BringToFront();
        }

        private void reversalUserControlButton_Click(object sender, EventArgs e)
        {
            purchaseUserControl1.Hide();
            preAuthUserControl1.Hide();
            refundUserControl1.Hide();
            settingsUserControl1.Hide();

            reversalUserControl1.Show();
            reversalUserControl1.BringToFront();
        }

        private void settingsUserControlButton_Click(object sender, EventArgs e)
        {
            purchaseUserControl1.Hide();
            preAuthUserControl1.Hide();
            refundUserControl1.Hide();
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
    }
}
