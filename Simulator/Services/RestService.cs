using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Simulator.Shared
{
    public static class RestService
    {
        
        private static readonly string baseURL = "http://dummy.restapiexample.com/api/v1/";

        public static async Task<string> GetAllEmployees()
        {
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.GetAsync(baseURL + "employees"))
                {
                    using (HttpContent content = response.Content)
                    {
                        string data = await content.ReadAsStringAsync();

                        if (data != null)
                        {
                            return data;
                        }
                    }
                }
            }
            return string.Empty;
        }

        public static async Task<string> PostPayment()
        {
            var request = "<?xml version=\"1.0\" encoding=\"UTF - 8\" standalone=\"yes\"?><TransactionRequest><SequenceNo>000279</SequenceNo><TransType>01</TransType><TransAmount>44400</TransAmount><TransCurrency>752</TransCurrency><TransDateTime>2020-05-29T08:12:37+01:00</TransDateTime><GuestNo>62524</GuestNo><IndustryCode>1</IndustryCode><Operator>01</Operator><CardPresent>2</CardPresent><TaxAmount>0</TaxAmount><RoomRate>0</RoomRate><CheckInDate>20180815</CheckInDate><CheckOutDate>20202020</CheckOutDate><LodgingCode>3</LodgingCode><SiteId>SHELL|FSDH</SiteId><WSNo>MarkusESTLAB.596807909</WSNo><ProxyInfo>OPIV6.2</ProxyInfo><POSInfo>Opera</POSInfo></TransactionRequest>";
            var stringRequest = new StringContent(request, Encoding.UTF8, "application/json");

            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.PostAsync("https://192.168.1.109:8080", stringRequest))
                {
                    using (HttpContent content = response.Content)
                    {
                        string data = await content.ReadAsStringAsync();

                        if (data != null)
                        {
                            return data;
                        }
                    }
                }
            }
            return string.Empty;
        }


    }
}
