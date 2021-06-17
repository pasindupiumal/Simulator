using Simulator.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Simulator.Shared
{
    class RestService
    {
        private string baseURL = null;
        private HttpClient httpClient = null;
        private XmlSerializer xmlSerializer = null;
        private StringWriterWithEncoding stringWriterWithEncoding = null;
        private XmlWriter xmlWriter = null;
        XmlSerializerNamespaces customNameSpace = null;

        public RestService (string baseURL)
        {
            this.baseURL = baseURL;

            //Set certificate validation properties
            ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) => true);
            ServicePointManager.Expect100Continue = false;
        }


        public async Task<string> Post()
        {
            try
            {
                var request = "<?xml version=\"1.0\" encoding=\"UTF - 8\" standalone=\"yes\"?><TransactionRequest><SequenceNo>000279</SequenceNo><TransType>01</TransType><TransAmount>44400</TransAmount><TransCurrency>752</TransCurrency><TransDateTime>2020-05-29T08:12:37+01:00</TransDateTime><GuestNo>62524</GuestNo><IndustryCode>1</IndustryCode><Operator>01</Operator><CardPresent>2</CardPresent><TaxAmount>0</TaxAmount><RoomRate>0</RoomRate><CheckInDate>20180815</CheckInDate><CheckOutDate>20202020</CheckOutDate><LodgingCode>3</LodgingCode><SiteId>SHELL|FSDH</SiteId><WSNo>MarkusESTLAB.596807909</WSNo><ProxyInfo>OPIV6.2</ProxyInfo><POSInfo>Opera</POSInfo></TransactionRequest>";
                var stringRequest = new StringContent(request, Encoding.UTF8, "application/xml");

                httpClient = new HttpClient();

                var response = await httpClient.PostAsync(this.baseURL, stringRequest);

                if (response.IsSuccessStatusCode)
                {
                    var stringResponse = await response.Content.ReadAsStringAsync();
                    return stringResponse;
                }
                else
                {
                    return "Operation failed. Unsuccessful status code.";
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"RestService Exception : {ex.Message}");
                return "RestService Exception : " + ex.ToString();
            }
        }

        public string GetEncodedRequest(string amount, string currCode)
        {
            try
            {
                var transactionRequest = new TransactionRequest
                {
                    SequenceNo = "000279",
                    TransType = "01",
                    TransAmount = amount,
                    TransCurrency = "752",
                    TransDateTime = "2020-05-29T08:12:37+01:00",
                    GuestNo = "62524",
                    IndustryCode = "1",
                    Operator = "01",
                    CardPresent = "2",
                    TaxAmount = "0",
                    RoomRate = "0",
                    CheckInDate = "20180815",
                    CheckOutDate = "20202020",
                    LodgingCode = "3",
                    SiteId = "SHELL|FSDH",
                    WSNo = "MarkusESTLAB.596807909",
                    ProxyInfo = "OPIV6.2",
                    POSInfo = "Opera"
                };

                xmlSerializer = new XmlSerializer(typeof(TransactionRequest));
                stringWriterWithEncoding = new StringWriterWithEncoding(Encoding.UTF8);
                customNameSpace = new XmlSerializerNamespaces();
                customNameSpace.Add(string.Empty, string.Empty);
                xmlWriter = XmlWriter.Create(stringWriterWithEncoding);
                xmlWriter.WriteStartDocument(true);
                xmlSerializer.Serialize(xmlWriter, transactionRequest, customNameSpace);
                string xmlObject = stringWriterWithEncoding.ToString();

                return xmlObject;
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Encoded Request Generation Exception : {ex.Message}");
                return "Encoded Request Generation Exception : " + ex.ToString();
            }
        }

        public async Task<string> PostEncoded(string amount, string currCode)
        {
            try
            {
                //var request = "<?xml version=\"1.0\" encoding=\"UTF - 8\" standalone=\"yes\"?><TransactionRequest><SequenceNo>000279</SequenceNo><TransType>01</TransType><TransAmount>44400</TransAmount><TransCurrency>752</TransCurrency><TransDateTime>2020-05-29T08:12:37+01:00</TransDateTime><GuestNo>62524</GuestNo><IndustryCode>1</IndustryCode><Operator>01</Operator><CardPresent>2</CardPresent><TaxAmount>0</TaxAmount><RoomRate>0</RoomRate><CheckInDate>20180815</CheckInDate><CheckOutDate>20202020</CheckOutDate><LodgingCode>3</LodgingCode><SiteId>SHELL|FSDH</SiteId><WSNo>MarkusESTLAB.596807909</WSNo><ProxyInfo>OPIV6.2</ProxyInfo><POSInfo>Opera</POSInfo></TransactionRequest>";

                //Get the encoded request
                string xmlObject = GetEncodedRequest(amount, currCode);

                if(!(xmlObject.Contains("Encoded Request Generation Exception")))
                {
                    var stringRequest = new StringContent(xmlObject, Encoding.UTF8, "application/xml");

                    httpClient = new HttpClient();

                    var response = await httpClient.PostAsync(this.baseURL, stringRequest);

                    if (response.IsSuccessStatusCode)
                    {
                        var stringResponse = await response.Content.ReadAsStringAsync();
                        return stringResponse;
                    }
                    else
                    {
                        return "Operation failed. Unsuccessful status code.";
                    }
                }
                else
                {
                    return "Operation failed. Unable to generate encoded request.";
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"RestService Exception : {ex.Message}");
                return "RestService Exception : " + ex.ToString();
            }
        }

        public string DecodeResponse()
        {
            try
            {
                var xmlObject = "<?xml version=\"1.0\" encoding=\"utf - 8\"?><TransactionResponse><SequenceNo>000279</SequenceNo><TransType>01</TransType><RespCode>00</RespCode><RespText>TransactionApproved</RespText><OfflineFlag>N</OfflineFlag><PrintData>New Elavon, abc, 10299#Org. nr111111-1121, , Termid: 00000001, Acq Ref: 0##16/06/2021 08:46:40, PURCHASE#SEK: 444.00#TOTAL: 444.00##************4111, MasterCard#APPROVED ONLINE, PIN Used#Ca1 5 001 AMX 001 47707200##Ref. nr: 000000100039, AID: A0000000041010, TVR: 0000008000, TSI: E800#</PrintData><RRN>000000100039</RRN><PAN>************4111</PAN><ExpiryDate>2212</ExpiryDate><TransToken>000000100039</TransToken><EntryMode>23</EntryMode><IssuerId>02</IssuerId><AuthCode>477072</AuthCode><DCCIndicator>0</DCCIndicator><TerminalId>00000001</TerminalId></TransactionResponse>";

                XmlSerializer xmlDS = new XmlSerializer(typeof(TransactionResponse));
                TextReader textReader = new StringReader(xmlObject);
                TransactionResponse trs = (TransactionResponse) xmlDS.Deserialize(textReader);

                string returnValue = "Sequence Number: " + trs.SequenceNo + " || Terminal ID: " + trs.TerminalId + " || Auth Code: " + trs.AuthCode;

                return returnValue;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Encoded Request Generation Exception : {ex.Message}");
                return "Encoded Request Generation Exception : " + ex.ToString();
            }
        }
    }
}
