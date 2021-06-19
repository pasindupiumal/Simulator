﻿using Simulator.Models;
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
using Simulator.Properties;

namespace Simulator.Shared
{
    class RestService
    {
        private string baseURL = null;
        private HttpClient httpClient = null;
        private XmlSerializer xmlSerializer = null;
        private StringWriterWithEncoding stringWriterWithEncoding = null;
        private XmlWriter xmlWriter = null;
        private XmlSerializerNamespaces customNameSpace = null;
        private TextReader textReader = null;

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

        public string GetEncodedPurchaseRequest(string amount, string currCode, bool seqIncrement)
        {
            try
            {
                //Reload the settings
                Settings.Default.Reload();
                int seqNumber = (int)Settings.Default["sequenceNumber"];
                string dateTime = DateTime.UtcNow.ToString("s") + DateTime.UtcNow.ToString("zzz");

                if (seqIncrement)
                {
                    seqNumber = seqNumber + 1;
                }

                var purchaseRequest = new PurchaseRequest
                {
                    SequenceNo = seqNumber.ToString(),
                    TransType = "01",
                    TransAmount = amount,
                    TransCurrency = currCode,
                    TransDateTime = dateTime,
                    GuestNo = Settings.Default["guestNo"].ToString(),
                    IndustryCode = Settings.Default["industryCode"].ToString(),
                    Operator = Settings.Default["operatorValue"].ToString(),
                    CardPresent = "2",
                    TaxAmount = "0",
                    RoomRate = "0",
                    CheckInDate = "20180815",
                    CheckOutDate = "20202020",
                    LodgingCode = Settings.Default["lodgingCode"].ToString(),
                    SiteId = Settings.Default["siteID"].ToString(),
                    WSNo = Settings.Default["wsNo"].ToString(),
                    ProxyInfo = Settings.Default["proxyInfo"].ToString(),
                    POSInfo = Settings.Default["posInfo"].ToString()
                };

                xmlSerializer = new XmlSerializer(typeof(PurchaseRequest));
                stringWriterWithEncoding = new StringWriterWithEncoding(Encoding.UTF8);
                customNameSpace = new XmlSerializerNamespaces();
                customNameSpace.Add(string.Empty, string.Empty);
                xmlWriter = XmlWriter.Create(stringWriterWithEncoding);
                xmlWriter.WriteStartDocument(true);
                xmlSerializer.Serialize(xmlWriter, purchaseRequest, customNameSpace);
                string xmlObject = stringWriterWithEncoding.ToString();

                Settings.Default["sequenceNumber"] = seqNumber;
                Settings.Default.Save();

                return xmlObject;
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Encoded Purchase Request Generation Exception : {ex.Message}");
                return "Encoded Purchase Request Generation Exception : " + ex.ToString();
            }
        }

        public string GetEncodedReversalRequest(string amount, string currCode, bool seqIncrement)
        {
            try
            {
                //Reload the settings
                Settings.Default.Reload();
                int seqNumber = (int)Settings.Default["sequenceNumber"];
                string dateTime = DateTime.UtcNow.ToString("s") + DateTime.UtcNow.ToString("zzz");

                if (seqIncrement)
                {
                    seqNumber = seqNumber + 1;
                }

                var reversalRequest = new ReversalRequest
                {
                    SequenceNo = seqNumber.ToString(),
                    TransType = "01",
                    TransAmount = amount,
                    TransCurrency = currCode,
                    OriginalType = "01",
                    OriginalTime = "2020-04-28T12:07:12+02:00",
                    IndustryCode = Settings.Default["industryCode"].ToString(),
                    TransDateTime = dateTime,
                    SiteId = Settings.Default["siteID"].ToString(),
                    WSNo = Settings.Default["wsNo"].ToString(),
                    ProxyInfo = Settings.Default["proxyInfo"].ToString(),
                    POSInfo = Settings.Default["posInfo"].ToString()
                };

                xmlSerializer = new XmlSerializer(typeof(ReversalRequest));
                stringWriterWithEncoding = new StringWriterWithEncoding(Encoding.UTF8);
                customNameSpace = new XmlSerializerNamespaces();
                customNameSpace.Add(string.Empty, string.Empty);
                xmlWriter = XmlWriter.Create(stringWriterWithEncoding);
                xmlWriter.WriteStartDocument(true);
                xmlSerializer.Serialize(xmlWriter, reversalRequest, customNameSpace);
                string xmlObject = stringWriterWithEncoding.ToString();

                Settings.Default["sequenceNumber"] = seqNumber;
                Settings.Default.Save();

                return xmlObject;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Encoded Reversal Request Generation Exception : {ex.Message}");
                return "Encoded Reversal Request Generation Exception : " + ex.ToString();
            }
        }

        public async Task<string> PostPurchaseRequest(string amount, string currCode)
        {
            try
            {
                //Get the encoded request
                string xmlObject = GetEncodedPurchaseRequest(amount, currCode, false);

                if(!(xmlObject.Contains("Encoded Purchase Request Generation Exception")))
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

        public async Task<string> PostReversalRequest(string amount, string currCode)
        {
            try
            {
                //Get the encoded request
                string xmlObject = GetEncodedReversalRequest(amount, currCode, false);

                if (!(xmlObject.Contains("Encoded Reversal Request Generation Exception")))
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
                    return "Operation failed. Unable to generate encoded request." + xmlObject;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"RestService Exception : {ex.Message}");
                return "RestService Exception : " + ex.ToString();
            }
        }

        public TransactionResponse DecodeResponse(string xmlObject)
        {
            try
            {
                xmlSerializer = new XmlSerializer(typeof(TransactionResponse));
                textReader = new StringReader(xmlObject);
                TransactionResponse transactionResponse = (TransactionResponse) xmlSerializer.Deserialize(textReader);

                return transactionResponse;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Transaction Response Decoding Exception : {ex.Message}");
                return null;
            }
        }
    }
}
