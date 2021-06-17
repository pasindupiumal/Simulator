using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Simulator.Models
{
    [XmlRoot("PurchaseRequest")]
    public class PurchaseRequest
    {        
        public string SequenceNo;
        public string TransType;
        public string TransAmount;
        public string TransCurrency;
        public string TransDateTime;
        public string GuestNo;
        public string IndustryCode;
        public string Operator;
        public string CardPresent;
        public string TaxAmount;
        public string RoomRate;
        public string CheckInDate;
        public string CheckOutDate;
        public string LodgingCode;
        public string SiteId;
        public string WSNo;
        public string ProxyInfo;
        public string POSInfo;
    }
}
