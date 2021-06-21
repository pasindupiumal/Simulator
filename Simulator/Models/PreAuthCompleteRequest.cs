using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Simulator.Models
{
    [XmlRoot("TransactionRequest")]
    public class PreAuthCompleteRequest
    {
        public string SequenceNo;
        public string TransType;
        public string TransAmount;
        public string TransCurrency;
        public string TransDateTime;
        public string AuthCode;
        public string OriginalRRN;
        public string TransToken;
        public string ExpiryDate;
        public string IssuerId;
        public string PAN;
        public string Operator;
        public string CardPresent;
        public string TaxAmount;
        public string SiteId;
        public string WSNo;
        public string ProxyInfo;
        public string POSInfo;
    }
}
