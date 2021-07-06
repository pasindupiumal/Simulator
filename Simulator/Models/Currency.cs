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

        public Currency() { }

        public string CurrencyCode()
        {
            return this.currencyCode;
        }

        public string CurrencyDesc()
        {
            return this.currencyDesc;
        }

        public int Decimals()
        {
            return this.decimals;
        }

        public string ConvertToString()
        {
            return this.currencyDesc + "-" + this.decimals + "-" + this.currencyCode;
        }
    }
}
