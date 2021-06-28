using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simulator.Properties;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using System.Diagnostics;

namespace Simulator.Shared
{
    class Utils
    {
        public string getBaseURL()
        {
            string baseURL = null;
            Settings.Default.Reload();

            //if(Settings.Default["port"].ToString().Length == 0)
            //{
            //    baseURL = Settings.Default["ip"].ToString();
            //}
            //else
            //{
            //    baseURL = Settings.Default["ip"].ToString() + ":" + Settings.Default["port"].ToString();
            //}

            baseURL = Settings.Default["ip"].ToString();

            return baseURL;
        }

        public String GetTimestamp(DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssffff");
        }

        public int getDecimalCount(double dVal, string sVal, string culture)
        {
            CultureInfo info = CultureInfo.GetCultureInfo(culture);

            //Get the double value of the string representation, keeping culture in mind
            double test = Convert.ToDouble(sVal, info);

            //Get the decimal separator the specified culture
            char[] sep = info.NumberFormat.NumberDecimalSeparator.ToCharArray();

            //Check to see if the culture-adjusted string is equal to the double
            if (dVal != test)
            {
                //The string conversion isn't correct, so throw an exception
                throw new System.ArgumentException("Double value and String value does not match!");
            }

            //Split the string on the separator 
            string[] segments = sVal.Split(sep);

            switch (segments.Length)
            {
                //Only one segment, so there was not fractional value - return 0
                case 1:
                    return 0;
                //Two segments, so return the length of the second segment
                case 2:
                    return segments[1].Length;

                //More than two segments means it's invalid, so throw an exception
                default:
                    throw new Exception("Counting Decimal Points Error!");
            }
        }

        public void CreateExcelFile()
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

                    String timeStamp = GetTimestamp(DateTime.Now);

                    xlWorkBook.SaveAs(@"" + Settings.Default["filePath"].ToString() + "/" + timeStamp + ".xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                    xlWorkBook.Close(true, misValue, misValue);
                    excelApp.Quit();

                    Marshal.ReleaseComObject(xlWorkSheet);
                    Marshal.ReleaseComObject(xlWorkBook);
                    Marshal.ReleaseComObject(excelApp);

                    Settings.Default.Reload();
                    Settings.Default["logingEnable"] = true;
                    string fileName = timeStamp + ".xls";
                    Settings.Default["currentFileName"] = fileName;
                    Settings.Default.Save();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Exception Creating The Excel File For Logging : {ex.Message}");
                }
            }
        }

        private void WriteToExcelFile()
        {
            Settings.Default.Reload();

            if (!(Settings.Default["filePath"].ToString().Length == 0))
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
                        //Excel.Workbook xlWorkBook;
                        //Excel.Worksheet xlWorkSheet;
                        //object misValue = System.Reflection.Missing.Value;

                        //xlWorkBook = excelApp.Workbooks.Open(Settings.Default["filePath"].ToString(), 0, false, 5, "", "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);
                        //xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                        //xlWorkSheet.Cells[1, 1] = "ID";
                        //xlWorkSheet.Cells[1, 2] = "Name";

                        //utils = new Utils();

                        //String timeStamp = utils.GetTimestamp(DateTime.Now);

                        //xlWorkBook.SaveAs(@"" + Settings.Default["filePath"].ToString() + "/" + timeStamp + ".xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                        //xlWorkBook.Close(true, misValue, misValue);
                        //excelApp.Quit();

                        //Marshal.ReleaseComObject(xlWorkSheet);
                        //Marshal.ReleaseComObject(xlWorkBook);
                        //Marshal.ReleaseComObject(excelApp);

                        //Settings.Default.Reload();
                        //Settings.Default["logingEnable"] = true;
                        //Settings.Default.Save();
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"Exception Creating The Excel File For Logging : {ex.Message}");
                    }
                }
            }
        }
    }
}
