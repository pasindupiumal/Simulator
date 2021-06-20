using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simulator.Properties;


namespace Simulator.Shared
{
    class Utils
    {
        public string getBaseURL()
        {
            string baseURL = null;
            Settings.Default.Reload();

            if(Settings.Default["port"].ToString().Length == 0)
            {
                baseURL = Settings.Default["ip"].ToString();
            }
            else
            {
                baseURL = Settings.Default["ip"].ToString() + ":" + Settings.Default["port"].ToString();
            }
            return baseURL;
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
    }
}
