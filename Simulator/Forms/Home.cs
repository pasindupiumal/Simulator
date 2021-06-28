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
        private void SetExcelFileSettings()
        {
            Settings.Default.Reload();

            if(!(Settings.Default["filePath"].ToString().Length == 0))
            {
                Excel.Application excelApp = new Excel.Application();

                if (excelApp == null)
                {
                    MessageBox.Show("Excel Library Is Not Installed. Cannot Create Excel Log File.");
                }
                else
                {
                    try
                    {
                        Excel.Workbook xlWorkBook;
                        Excel.Worksheet xlWorkSheet;
                        object misValue = System.Reflection.Missing.Value;

                        xlWorkBook = excelApp.Workbooks.Add(misValue);
                        xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                        xlWorkSheet.Cells[1, 1] = "ID";
                        xlWorkSheet.Cells[1, 2] = "Name";

                        utils = new Utils();

                        String timeStamp = utils.GetTimestamp(DateTime.Now);

                        xlWorkBook.SaveAs(@"" + Settings.Default["filePath"].ToString() + "/" + timeStamp + ".xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                        xlWorkBook.Close(true, misValue, misValue);
                        excelApp.Quit();

                        Marshal.ReleaseComObject(xlWorkSheet);
                        Marshal.ReleaseComObject(xlWorkBook);
                        Marshal.ReleaseComObject(excelApp);

                        Settings.Default.Reload();
                        Settings.Default["logingEnable"] = true;
                        Settings.Default.Save();
                    }
                    catch(Exception ex)
                    {
                        Debug.WriteLine($"Exception Creating The Excel File For Logging : {ex.Message}");
                    }
                }
            }
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
            SetExcelFileSettings();
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
