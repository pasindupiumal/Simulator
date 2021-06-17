using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator.Models
{
    public class TransactionResponse
    {
        public TransactionResponse() { }

        public string SequenceNo;
        public string TransType;
        public string RespCode;
        public string RespText;
        public string OfflineFlag;
        public string PrintData;
        public string RRN;
        public string PAN;
        public string ExpiryDate;
        public string TransToken;
        public string EntryMode;
        public string IssuerId;
        public string AuthCode;
        public string DCCIndicator;
        public string TerminalId;

    }
}
