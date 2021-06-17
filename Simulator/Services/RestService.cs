using Simulator.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        private XmlWriterSettings xmlWriterSettings = null;

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

        public async Task<string> PostEncoded()
        {
            try
            {
                var transactionRequest = new TransactionRequest
                {
                    SequenceNo = "000279",
                    TransType = "01",
                    TransAmount = "44400",
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
                XmlSerializerNamespaces customNameSpace = new XmlSerializerNamespaces();
                customNameSpace.Add(string.Empty, string.Empty);
                xmlWriter = XmlWriter.Create(stringWriterWithEncoding);
                xmlWriter.WriteStartDocument(true);
                xmlSerializer.Serialize(xmlWriter, transactionRequest, customNameSpace);
                string xmlObject = stringWriterWithEncoding.ToString();

                //var request = "<?xml version=\"1.0\" encoding=\"UTF - 8\" standalone=\"yes\"?><TransactionRequest><SequenceNo>000279</SequenceNo><TransType>01</TransType><TransAmount>44400</TransAmount><TransCurrency>752</TransCurrency><TransDateTime>2020-05-29T08:12:37+01:00</TransDateTime><GuestNo>62524</GuestNo><IndustryCode>1</IndustryCode><Operator>01</Operator><CardPresent>2</CardPresent><TaxAmount>0</TaxAmount><RoomRate>0</RoomRate><CheckInDate>20180815</CheckInDate><CheckOutDate>20202020</CheckOutDate><LodgingCode>3</LodgingCode><SiteId>SHELL|FSDH</SiteId><WSNo>MarkusESTLAB.596807909</WSNo><ProxyInfo>OPIV6.2</ProxyInfo><POSInfo>Opera</POSInfo></TransactionRequest>";
                //var stringRequest = new StringContent(request, Encoding.UTF8, "application/xml");

                //httpClient = new HttpClient();

                //var response = await httpClient.PostAsync(this.baseURL, stringRequest);

                //if (response.IsSuccessStatusCode)
                //{
                //    var stringResponse = await response.Content.ReadAsStringAsync();
                //    return stringResponse;
                //}
                //else
                //{
                //    return "Operation failed. Unsuccessful status code.";
                //}

                return xmlObject;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"RestService Exception : {ex.Message}");
                return "RestService Exception : " + ex.ToString();
            }
        }
    }
}
