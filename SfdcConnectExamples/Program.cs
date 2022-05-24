using SfdcConnect;
using SfdcConnect.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SfdcConnectExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            SfdcRestApi api = new SfdcRestApi(false, 54, "");
            SfdcRestApiWrapper conn = new SfdcRestApiWrapper(api);

            conn.RestApi.Username = "";
            conn.RestApi.Password = "";
            conn.RestApi.Token = "";
            conn.RestApi.Password = "";
            conn.RestApi.Token = "";

            conn.RestApi.CallbackEndpoint = "http://127.0.0.1:12345/";
            conn.RestApi.Open(LoginFlow.OAuthDesktop, "mystate");
            conn.RestApi.GetServices();

            AvailableResources answer = conn.RestApi.GetAvilableResources();

            byte[] data = conn.RestApi.GetBlobField("Document", "0151K0000049Z6PQAU", "body");

            using(BinaryWriter output = new BinaryWriter(File.OpenWrite(@"document.docx")))
            {
                output.Write(data, 0, data.Length);
                output.Flush();
            }

            //Identity identity = conn.Callout<Identity>(answer["identity"], "GET", string.Empty, "application/json", "application/json");

            //string response = conn.StandardAPICallout(answer["limits"], "GET", string.Empty, string.Empty);
            //Console.WriteLine(response);

            Console.ReadKey();
        }
    }
}
//conn.OpenAsync();

//while(conn.State == System.Data.ConnectionState.Connecting)
//{
//    Thread.Sleep(1000);
//}


//sObject answer = conn.GetRecord("Account", "00141000007f4KAAAY");//, new string[] { "Name", "AccountNumber", "BillingPostalCode" });

//using (StreamWriter output = new StreamWriter(@"resources.txt"))
//{
//    foreach (string key in answer.Keys)
//    {
//        output.WriteLine("{0}\t{1}", key, answer[key]);
//        Console.WriteLine("{0}\t{1}", key, answer[key]);
//    }
//    output.Flush();
//}
