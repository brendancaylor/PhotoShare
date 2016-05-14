using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace WebApplication.Models
{
    public class PayPalListenerModel
    {
        public PayPalCheckoutInfo _PayPalCheckoutInfo { get; set; }
        public string Status { get; set; }
        public void GetStatus(byte[] parameters)
        {
            //verify the transaction             
            Status = Verify(false, parameters);
        }

        private string Verify(bool isSandbox, byte[] parameters)
        {

            string response = "";
            try
            {

                string url = isSandbox ?
                  "https://www.sandbox.paypal.com/cgi-bin/webscr" : "https://www.paypal.com/cgi-bin/webscr";

                Encoding encoding = Encoding.UTF8;

                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded";

                string strRequest = encoding.GetString(parameters);
                strRequest += "&cmd=_notify-validate";
                req.ContentLength = strRequest.Length;

                //Send the request to PayPal and get the response
                StreamWriter streamOut = new StreamWriter(req.GetRequestStream());
                streamOut.Write(strRequest);
                streamOut.Close();

                StreamReader streamIn = new StreamReader(req.GetResponse().GetResponseStream());
                response = streamIn.ReadToEnd();
                streamIn.Close();


                /*
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
                webRequest.Method = "POST";
                webRequest.ContentType = "application/x-www-form-urlencoded";

                //must keep the original intact and pass back to PayPal with a _notify-validate command
                string data = Encoding.UTF8.GetString(parameters);
                data += "&cmd=_notify-validate";

                webRequest.ContentLength = data.Length;

                //Send the request to PayPal and get the response                 
                using (StreamWriter streamOut = new StreamWriter(webRequest.GetRequestStream(), System.Text.Encoding.UTF8))
                {
                    streamOut.Write(data);
                    streamOut.Close();
                }

                using (StreamReader streamIn = new StreamReader(webRequest.GetResponse().GetResponseStream(), Encoding.UTF8))
                {
                    response = streamIn.ReadToEnd();
                    streamIn.Close();
                }
                 */


            }
            catch (Exception ex)
            {
                response = "ERROR: " + ex.Message;
            }

            return response;

        }
    } 
}