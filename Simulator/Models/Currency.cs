using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator.Models
{
    class Currency
    {
        private string currencyCode;
        private string currencyDesc;
        private int decimals;

        public Currency(string currencyCode, string currencyDesc, int decimals)
        {
            this.currencyCode = currencyCode;
            this.currencyDesc = currencyDesc;
            this.decimals = decimals;
        }

        public string CurrencyCode { get; set; }
        public string CurrencyDesc { get; set; }
        public int Decimals { get; set; }
    }
}
