using SfdcConnect;
//using SfdcConnect.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SfdcConnectExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            //using (StreamWriter output = new StreamWriter(@"E:\Projects\SfdcConnect\SfdcConnect.v50\Objects\Sfdc.Rest.o.cs"))
            //{
            //    using (StreamReader input = new StreamReader(@"E:\Projects\SfdcConnect\SfdcConnect.v50\Objects\Sfdc.Rest.cs"))
            //    {
            //        string line = "";
            //        while ((line = input.ReadLine()) != null)
            //        {
            //            if(line.Trim() == "" || line.Trim()[0] == '[')
            //            {

            //            }
            //            else
            //            {
            //                output.WriteLine(line);
            //            }
            //        }
            //    }
            //    output.Flush();
            //}


            SfdcConnection conn = new SfdcConnection();


            ////SfdcConnection cc = new SfdcConnection();
            ////cc.ClientId = "";

            ////X509Certificate2 cert = new X509Certificate2(@".\AssetTest.crt");

            ////string token = "";// cc.GenerateAssetToken("private_clear.key", "Asset Token: 1e13a4d3-c612-3600-a1ea-fc43aaf93cb4", "1e13a4d3-c612-3600-a1ea-fc43aaf93cb4", "Asset: 1e13a4d3-c612-3600-a1ea-fc43aaf93cb4", "00141000007f4KAAAY", null, "1e13a4d3-c612-3600-a1ea-fc43aaf93cb4", "deviceKey");
            ////token = cc.GenerateJWTToken("seanfife@hotmail.com", "http://localhost/", "private_clear.key");

            ////Microsoft.IdentityModel.Tokens.SecurityToken securityToken;
            ////bool isValid = cc.IsJwtValid(token, "private_clear.key", out securityToken);


            //SfdcRestApi api = new SfdcRestApi(false, 54, "");
            //SfdcRestApiWrapper conn = new SfdcRestApiWrapper(api);

            //conn.RestApi.Username = "";
            //conn.RestApi.Password = "";
            //conn.RestApi.Token = "";
            //conn.RestApi.ClientId = "";
            //conn.RestApi.ClientSecret = "";


            //conn.RestApi.CallbackEndpoint = "https://127.0.0.1:12345/";
            //conn.RestApi.Open(LoginFlow.WebServer);
            //conn.RestApi.GetServices();

            ////AvailableResources answer = conn.RestApi.GetAvilableResources();

            ////byte[] data = conn.RestApi.GetBlobField("Document", "0151K0000049Z6PQAU", "body");

            ////using(BinaryWriter output = new BinaryWriter(File.OpenWrite(@"document.docx")))
            ////{
            ////    output.Write(data, 0, data.Length);
            ////    output.Flush();
            ////}

            //////Identity identity = conn.Callout<Identity>(answer["identity"], "GET", string.Empty, "application/json", "application/json");

            //////string response = conn.StandardAPICallout(answer["limits"], "GET", string.Empty, string.Empty);
            //////Console.WriteLine(response);

            //Console.ReadKey();
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
