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
using Microsoft.Office.Interop.Excel;

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

        /// <summary>
        /// Method for creating a new excel file for logging.
        /// </summary>
        public async Task CreateExcelFile()
        {
            await Task.Run(() => {

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

                        Range cells = xlWorkSheet.Cells;
                        cells.NumberFormat = "@";

                        xlWorkSheet.Cells[1, 1] = "Transaction Type";
                        xlWorkSheet.Cells[1, 2] = "DCC";
                        xlWorkSheet.Cells[1, 3] = "TID";
                        xlWorkSheet.Cells[1, 4] = "Card Number";
                        xlWorkSheet.Cells[1, 5] = "RRN";
                        xlWorkSheet.Cells[1, 6] = "Amount";
                        xlWorkSheet.Cells[1, 7] = "Response Status";
                        xlWorkSheet.Cells[1, 8] = "Response Message";

                        string timeStamp = GetTimestamp(DateTime.Now);

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
            });
        }

        /// <summary>
        /// Method to write to excel file
        /// </summary>
        /// <param name="transType"></param>
        /// <param name="dcc"></param>
        /// <param name="tid"></param>
        /// <param name="cardNumber"></param>
        /// <param name="rrn"></param>
        /// <param name="amount"></param>
        /// <param name="response"></param>
        public async Task WriteToExcelFile(string transType, string dcc, string tid, string cardNumber, string rrn, string amount, string response, string respMessage)
        {
            await Task.Run(() =>
            {
                Settings.Default.Reload();

                if (!(Settings.Default["filePath"].ToString().Length == 0) && !(Settings.Default["currentFileName"].ToString().Length == 0))
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

                            string filePath = Settings.Default["filePath"].ToString() + "/" + Settings.Default["currentFileName"].ToString();
                            xlWorkBook = excelApp.Workbooks.Open(filePath, 0, false, 5, "", "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);
                            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                            Excel.Range last = xlWorkSheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell, Type.Missing);
                            Excel.Range range = xlWorkSheet.get_Range("A1", last);

                            int lastUsedRow = last.Row;
                            int lastUsedColumn = last.Column;

                            xlWorkSheet.Cells[lastUsedRow + 1, 1] = transType;
                            xlWorkSheet.Cells[lastUsedRow + 1, 2] = dcc;
                            xlWorkSheet.Cells[lastUsedRow + 1, 3] = tid;
                            xlWorkSheet.Cells[lastUsedRow + 1, 4] = cardNumber;
                            xlWorkSheet.Cells[lastUsedRow + 1, 5] = rrn;
                            xlWorkSheet.Cells[lastUsedRow + 1, 6] = amount;
                            xlWorkSheet.Cells[lastUsedRow + 1, 7] = response;
                            xlWorkSheet.Cells[lastUsedRow + 1, 8] = respMessage;

                            //Auto fit the width of columns
                            xlWorkSheet.Columns.AutoFit();

                            xlWorkBook.Save();
                            xlWorkBook.Close(true, misValue, misValue);
                            excelApp.Quit();

                            Marshal.ReleaseComObject(xlWorkSheet);
                            Marshal.ReleaseComObject(xlWorkBook);
                            Marshal.ReleaseComObject(excelApp);
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine($"Exception Writing Logs To The Excel File : {ex.Message}");
                        }
                    }
                }
                else
                {
                    Debug.WriteLine($"Exception Creating The Excel File For Logging. Folder Path Not Found!");
                    MessageBox.Show("Folder Path Not Found!", "OPI Simulator", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            });
        }
    }
}
