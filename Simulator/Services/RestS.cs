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
    class RestS
    {
        static string BaseURL = "https://192.168.1.109:8080";
        static HttpClient client;
        static RestS()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri(BaseURL)
            };
        }

        //public static async Task<string> Post()
        //{
        //    try
        //    {
        //        XmlSerializer serializer = new XmlSerializer(typeof(TransactionRequest));
        //        StringWriter stringWrtier = new StringWriter();
        //        XmlWriter xmlWriter = XmlWriter.Create(stringWrtier);
        //        //var content = new StringContent(xmlData, Encoding.UTF8, "application/xml");

        //        var transactionRequest = new TransactionRequest
        //        {
        //            SequenceNo = "000279",
        //            TransType = "01",
        //            TransAmount = "44400",
        //            TransCurrency = "752",
        //            TransDateTime = "2020-05-29T08:12:37+01:00",
        //            GuestNo = "62524",
        //            IndustryCode = "1",
        //            Operator = "01",
        //            CardPresent = "2",
        //            TaxAmount = "0",
        //            RoomRate = "0",
        //            CheckInDate = "20180815",
        //            CheckOutDate = "20202020",
        //            LodgingCode = "3",
        //            SiteId = "SHELL|FSDH",
        //            WSNo = "MarkusESTLAB.596807909",
        //            ProxyInfo = "OPIV6.2",
        //            POSInfo = "Opera"
        //        };

        //        serializer.Serialize(xmlWriter, transactionRequest);
        //        string xmlObject = stringWrtier.ToString();

        //        //MediaTypeFormatter xmlFormatter = new XmlMediaTypeFormatter { UseXmlSerializer = true };
        //        //var response = await client.PostAsync("https://192.168.1.109:8080", xmlObject, xmlFormatter);
        //        //var stringResponse = await response.Content.ReadAsStringAsync();

        //        //if (!response.IsSuccessStatusCode)
        //        //{
        //        //    return stringResponse;
        //        //}
        //        //else
        //        //{
        //        //    return "Operation failed";
        //        //}

        //        return xmlObject;
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine($"Post error : {ex.Message}");
        //        return "Exception: " + ex.Message;
        //    }
        //}

        public static async Task<string> Post()
        {
            try
            {
                //System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) => true);
                ServicePointManager.Expect100Continue = false;
                var request = "<?xml version=\"1.0\" encoding=\"UTF - 8\" standalone=\"yes\"?><TransactionRequest><SequenceNo>000279</SequenceNo><TransType>01</TransType><TransAmount>44400</TransAmount><TransCurrency>752</TransCurrency><TransDateTime>2020-05-29T08:12:37+01:00</TransDateTime><GuestNo>62524</GuestNo><IndustryCode>1</IndustryCode><Operator>01</Operator><CardPresent>2</CardPresent><TaxAmount>0</TaxAmount><RoomRate>0</RoomRate><CheckInDate>20180815</CheckInDate><CheckOutDate>20202020</CheckOutDate><LodgingCode>3</LodgingCode><SiteId>SHELL|FSDH</SiteId><WSNo>MarkusESTLAB.596807909</WSNo><ProxyInfo>OPIV6.2</ProxyInfo><POSInfo>Opera</POSInfo></TransactionRequest>";
                var content = new StringContent(request, Encoding.UTF8, "application/xml");
                var response = await client.PostAsync("https://192.168.12.109:8080", content);
                var stringResponse = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return "Operation Failed";
                }
                else
                {
                    return stringResponse;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error : {ex.Message}");
                return ex.ToString();
            }
        }
    }
}
