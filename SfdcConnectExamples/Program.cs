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
            SfdcRestApi conn = new SfdcRestApi(false, 54, "");

            conn.ClientId = "";
            conn.ClientSecret = "";
            conn.Username = "";
            conn.Password = "";
            conn.Token = "";

            //conn.OpenAsync();

            //while(conn.State == System.Data.ConnectionState.Connecting)
            //{
            //    Thread.Sleep(1000);
            //}


            conn.Open(LoginFlow.OAuthDesktop, "mystate");
            conn.GetServices();

            //sObject answer = conn.GetRecord("Account", "00141000007f4KAAAY");//, new string[] { "Name", "AccountNumber", "BillingPostalCode" });
            //AvailableResources answer = conn.GetAvilableResources();// GetRecord("Account", "00141000007f4KAAAY");//, DateTime.Now.AddDays(-7), DateTime.Now);

            conn.Close();
            Console.ReadKey();
        }

        public static int FindAvailablePort()
        {
            //There has to be a better way to do this...
            TcpListener server = new TcpListener(IPAddress.Parse("127.0.0.1"), 0);

            server.Start();

            int port = ((IPEndPoint)server.LocalEndpoint).Port;

            server.Stop();

            return port;
        }


    }
}
