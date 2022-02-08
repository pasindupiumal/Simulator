using Simulator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator.Shared
{
    /// <summary>
    /// Class for maintaining details of currencies
    /// </summary>
    static class Currencies
    {
        private static Dictionary<string, Currency> currencyList = null;

        static Currencies()
        {
            currencyList = new Dictionary<string, Currency>();
            AddCurrencies();
        }

        private static void AddCurrencies()
        {
            Currency SEK = null;
            Currency LKR = null;
            Currency EUR = null;
            Currency JPY = null;
            Currency USD = null;
            Currency GBP = null;
            Currency ZAR = null;

            SEK = new Currency("752", "SEK", 2);
            LKR = new Currency("144", "LKR", 2);
            EUR = new Currency("978", "EUR", 2);
            JPY = new Currency("392", "JPY", 0);
            USD = new Currency("840", "USD", 2);
            GBP = new Currency("826", "GBP", 2);
            ZAR = new Currency("710", "ZAR", 2);

            currencyList.Add("752", SEK);
            currencyList.Add("144", LKR);
            currencyList.Add("978", EUR);
            currencyList.Add("392", JPY);
            currencyList.Add("840", USD);
            currencyList.Add("826", GBP);
            currencyList.Add("710", ZAR);
        }

        /// <summary>
        /// Method for obtaining the currency details for a given currency code.
        /// </summary>
        /// <param name="currencyCode"></param>
        /// <returns></returns>
        public static Currency GetCurrency(string currencyCode)
        {
            if (!currencyList.ContainsKey(currencyCode))
            {
                return null;
            }
            else
            {
                return currencyList[currencyCode];
            }
        }
    }
}
