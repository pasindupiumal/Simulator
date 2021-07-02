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

        public static void AddCurrencies()
        {
            Currency SEK = new Currency("752", "SEK", 2);
            Currency LKR = new Currency("144", "LKR", 2);
            Currency EUR = new Currency("978", "EUR", 2);
            Currency JPY = new Currency("392", "JPY", 0);
            Currency USD = new Currency("840", "USD", 2);
            Currency GBP = new Currency("826", "GBP", 2);

            currencyList.Add(SEK.CurrencyCode, SEK);
            currencyList.Add(LKR.CurrencyCode, LKR);
            currencyList.Add(EUR.CurrencyCode, EUR);
            currencyList.Add(JPY.CurrencyCode, JPY);
            currencyList.Add(USD.CurrencyCode, USD);
            currencyList.Add(GBP.CurrencyCode, GBP);
        }

        /// <summary>
        /// Method for obtaining the currency details for a given currency code.
        /// </summary>
        /// <param name="currencyCode"></param>
        /// <returns></returns>
        public static Currency GetCurrency(string currencyCode)
        {
            if (currencyList.ContainsKey("752"))
            {
                return currencyList[currencyCode];
            }
            else
            {
                return null;
            }
        }
    }
}
