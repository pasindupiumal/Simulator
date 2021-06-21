using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Simulator.Models
{
    [XmlRoot("TransactionRequest")]
    public class PreAuthCancelRequest
    {
        public string SequenceNo;
        public string TransType;
        public string TransAmount;
        public string TransCurrency;
        public string TransDateTime;
        public string GuestNo;
        public string Operator;
        public string CardPresent;
        public string OriginalRRN;
        public string PAN;
        public string IssuerId;
        public string ExpiryDate;
        public string TransToken;
        public string SiteId;
        public string WSNo;
        public string ProxyInfo;
        public string POSInfo;
    }
}
