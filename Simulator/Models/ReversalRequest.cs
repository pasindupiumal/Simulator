using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Simulator.Models
{
    [XmlRoot("TransactionRequest")]
    class ReversalRequest
    {
        public string SequenceNo;
        public string TransType;
        public string TransAmount;
        public string TransCurrency;
        public string OriginalType;
        public string OriginalTime;
        public string IndustryCode;
        public string TransDateTime;
        public string SiteId;
        public string WSNo;
        public string ProxyInfo;
        public string POSInfo;
    }
}
