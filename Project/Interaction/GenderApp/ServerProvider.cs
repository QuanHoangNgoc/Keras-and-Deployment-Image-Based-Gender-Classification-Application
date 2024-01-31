using Microsoft.Scripting.Hosting.Shell;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GenderApp
{
    public class ServerProvider
    {
        private static ServerProvider instance;
        public static ServerProvider Instance
        {
            get { if (instance == null) instance = new ServerProvider(); return ServerProvider.instance; }
            private set {ServerProvider.instance = value; }
        }

        private string EscapeData(string mes)
        {
            int mes_length = mes.Length;
            if (mes_length <= 32000)
            {
                return Uri.EscapeDataString(mes);
            }

            int idx = 0;
            StringBuilder builder = new StringBuilder();
            String substr = mes.Substring(idx, 32000);
            while (idx < mes_length)
            {
                builder.Append(Uri.EscapeDataString(substr));
                idx += 32000;

                if (idx < mes_length)
                {
                    substr = mes.Substring(idx, Math.Min(32000, mes_length - idx));
                }

            }
            return builder.ToString();
        }

        public string sendPOST(string url_connect, string client_request_key, string mes) 
        {
            try
            {
                // create request and config 
                var request = (HttpWebRequest)WebRequest.Create(url_connect); 
                request.Timeout = 5000;
                request.Method = "POST";

                // cap user's mes and request_key to data 
                var postData = client_request_key + "=" + EscapeData(mes);
                var data = Encoding.ASCII.GetBytes(postData);
            
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;
                // write request    
                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                // receive respond string 
                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                return responseString;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
                Console.WriteLine(ex.StackTrace);
                return null; 
            }
        }

    }
}
