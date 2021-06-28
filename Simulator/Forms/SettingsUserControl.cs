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

        private void excelButton_Click(object sender, EventArgs e)
        {
            Excel.Application excelApp = new Excel.Application();

            if(excelApp == null)
            {
                MessageBox.Show("Excel Library Is Not Installed");
            }
            else
            {
                MessageBox.Show("Excel Library is installed");

                Excel.Workbook xlWorkBook;
                Excel.Worksheet xlWorkSheet;
                object misValue = System.Reflection.Missing.Value;

                xlWorkBook = excelApp.Workbooks.Add(misValue);
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                xlWorkSheet.Cells[1, 1] = "ID";
                xlWorkSheet.Cells[1, 2] = "Name";

                xlWorkBook.SaveAs(@"C:\Users\Pasindu\Desktop\mySheet.xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                xlWorkBook.Close(true, misValue, misValue);
                excelApp.Quit();

                Marshal.ReleaseComObject(xlWorkSheet);
                Marshal.ReleaseComObject(xlWorkBook);
                Marshal.ReleaseComObject(excelApp);

                MessageBox.Show("Created The Excel File");
            }
        }

        private void filePathButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            //folderBrowserDialog.RootFolder = Environment.SpecialFolder.MyDocuments;
            folderBrowserDialog.Description = "Select the folder for Excel reports";

            //Reload the settings
            Settings.Default.Reload();

            if(folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Settings.Default["filePath"] = folderBrowserDialog.SelectedPath;
                Settings.Default.Save();
            }
        }
    }
}
