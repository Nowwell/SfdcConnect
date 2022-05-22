﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SfdcConnect;
using System.Threading.Tasks;
using System.Data;
using System.Threading;

namespace SfdcConnectTests
{
    [TestClass]
    public class UnitTest1
    {
        private string username = "";
        private string password = "";
        private string token = "";

        #region Login Tests

        [TestMethod]
        public void SfdcConnectionNoParameters()
        {
            SfdcConnection conn = new SfdcConnection();

            conn.Username = username;
            conn.Password = password;
            conn.Token = token;

            try
            {
                conn.Open();
            }
            catch
            {
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void SfdcConnectionUriParameter()
        {
            SfdcConnection conn = new SfdcConnection(string.Format("https://login.salesforce.com/services/Soap/u/{0}.0/", 36));

            conn.Username = username;
            conn.Password = password;
            conn.Token = token;

            conn.Open();

            conn.Close();
        }

        [TestMethod]
        public void SfdcConnectionTestAndVersionParameters()
        {
            SfdcConnection conn = new SfdcConnection(false, 36);

            conn.Username = username;
            conn.Password = password;
            conn.Token = token;

            conn.Open();

            conn.Close();
        }

        [TestMethod]
        public void SfdcConnectionNoParametersAsync()
        {
            SfdcConnection conn = new SfdcConnection();

            conn.Username = username;
            conn.Password = password;
            conn.Token = token;

            try
            {
                conn.OpenAsync();
            }
            catch
            {
                Assert.Fail();
            }
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void SfdcConnectionUriParameterAsync()
        {
            SfdcConnection conn = new SfdcConnection(string.Format("https://login.salesforce.com/services/Soap/u/{0}.0", 36));

            conn.Username = username;
            conn.Password = password;
            conn.Token = token;

            conn.OpenAsync();

            int i = 0;
            while (conn.State == ConnectionState.Connecting && i < 60)
            {
                Thread.Sleep(1000);
                i++;
            }

            Assert.IsTrue(conn.State == ConnectionState.Open);

            conn.Close();

            Assert.IsTrue(conn.State == ConnectionState.Closed);
        }

        [TestMethod]
        public void SfdcConnectionUriParameterAsyncSpecifyFunction()
        {
            SfdcConnection conn = new SfdcConnection(string.Format("https://login.salesforce.com/services/Soap/u/{0}.0", 36));

            conn.Username = username;
            conn.Password = password;
            conn.Token = token;

            conn.customLoginCompleted += Conn_loginCompleted;

            conn.OpenAsync();

            int i = 0;
            while (conn.State == ConnectionState.Connecting && i < 60)
            {
                Thread.Sleep(1000);
                i++;
            }

            Assert.IsTrue(conn.State == ConnectionState.Open);

            conn.Close();

            Assert.IsTrue(conn.State == ConnectionState.Closed);
        }
        private void Conn_loginCompleted(object sender, SfdcConnect.SoapObjects.loginCompletedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        [TestMethod]
        public void SfdcConnectionTestAndVersionParametersAsync()
        {
            SfdcConnection conn = new SfdcConnection(false, 36);

            conn.Username = username;
            conn.Password = password;
            conn.Token = token;

            conn.OpenAsync();

            int i = 0;
            while (conn.State == ConnectionState.Connecting && i < 60)
            {
                Thread.Sleep(1000);
                i++;
            }

            Assert.IsTrue(conn.State == ConnectionState.Open);

            conn.Close();

            Assert.IsTrue(conn.State == ConnectionState.Closed);

        }

        [TestMethod]
        public async Task SfdcConnectionNoParametersAwaitAsync()
        {
            SfdcConnection conn = new SfdcConnection();

            conn.Username = username;
            conn.Password = password;
            conn.Token = token;

            try
            {
                await conn.OpenAsync(default(CancellationToken));
            }
            catch
            {
                Assert.Fail();
            }
            Assert.IsTrue(true);
        }

        [TestMethod]
        public async Task SfdcConnectionUriParameterAwaitAsync()
        {
            SfdcConnection conn = new SfdcConnection(string.Format("https://login.salesforce.com/services/Soap/u/{0}.0", 36));

            conn.Username = username;
            conn.Password = password;
            conn.Token = token;

            await conn.OpenAsync(default(CancellationToken));

            int i = 0;
            while (conn.State == ConnectionState.Connecting && i < 60)
            {
                Thread.Sleep(1000);
                i++;
            }

            Assert.IsTrue(conn.State == ConnectionState.Open);

            conn.Close();

            Assert.IsTrue(conn.State == ConnectionState.Closed);
        }

        [TestMethod]
        public async Task SfdcConnectionTestAndVersionParametersAwaitAsync()
        {
            SfdcConnection conn = new SfdcConnection(false, 36);

            conn.Username = username;
            conn.Password = password;
            conn.Token = token;

            CancellationToken cancelToken = new CancellationToken();

            await conn.OpenAsync(cancelToken);

            int i = 0;
            while (conn.State == ConnectionState.Connecting && i < 60)
            {
                Thread.Sleep(1000);
                i++;
            }

            Assert.IsTrue(conn.State == ConnectionState.Open);

            conn.Close();

            Assert.IsTrue(conn.State == ConnectionState.Closed);
        }

        #endregion

        [TestMethod]
        public void SoapApiTest()
        {
            SfdcSoapApi conn = new SfdcSoapApi(false, 36);

            conn.Username = username;
            conn.Password = password;
            conn.Token = token;

            conn.Open();

            SfdcConnect.SoapObjects.DescribeSObjectResult result = conn.describeSObject("Contact");

            conn.Close();
        }

        [TestMethod]
        public void RestApiTest()
        {
            SfdcRestApi conn = new SfdcRestApi(false, 36);

            conn.Username = username;
            conn.Password = password;
            conn.Token = token;

            conn.Open();

            ApiLimits result = conn.GetLimits(true);

            conn.Close();
        }

        [TestMethod]
        public void RestApiTest_Password()
        {
            SfdcRestApi conn = new SfdcRestApi(false, 54);

            conn.Username = username;
            conn.Password = password;
            conn.Token = token;
            conn.ClientId = "3MVG9szVa2RxsqBaFfy.QEDHf2rObjsuJtd0nc8Ryq4LXX7Ylm9Pn30kHuj1sU_4FFQaOFqcrpp95xWUEV24S";
            conn.ClientSecret = "D8D46B150BBD86DA71B1B7F49507C8D97894E98ECC80E673785C4293CA8B111B";

            conn.Open();

            Assert.IsTrue(!string.IsNullOrEmpty(conn.SessionId));

            conn.Close();

            Assert.IsTrue(string.IsNullOrEmpty(conn.SessionId));
        }

        [TestMethod]
        public void RestApiTest_Authorize()
        {
            SfdcRestApi conn = new SfdcRestApi(false, 54);

            //conn.Username = username;
            //conn.Password = password;
            //conn.Token = token;
            conn.ClientId = "3MVG9szVa2RxsqBaFfy.QEDHf2rObjsuJtd0nc8Ryq4LXX7Ylm9Pn30kHuj1sU_4FFQaOFqcrpp95xWUEV24S";
            //conn.ClientSecret = "D8D46B150BBD86DA71B1B7F49507C8D97894E98ECC80E673785C4293CA8B111B";

            conn.Open(SfdcConnect.Objects.LoginFlow.OAuthDesktop, "mystate");

            Assert.IsTrue(!string.IsNullOrEmpty(conn.SessionId));
            Assert.IsTrue(!string.IsNullOrEmpty(conn.RefreshToken));

            conn.Close();

            Assert.IsTrue(string.IsNullOrEmpty(conn.SessionId));
        }

        [TestMethod]
        public void MetadataApiTest()
        {
            SfdcMetadataApi conn = new SfdcMetadataApi(false, 36);

            conn.Username = username;
            conn.Password = password;
            conn.Token = token;

            conn.Open();

            SfdcConnect.MetadataObjects.DescribeMetadataResult dmd = conn.describeMetadata(double.Parse(conn.Version));

            conn.Close();
        }

        [TestMethod]
        public void ApexApiTest()
        {
            SfdcApexApi conn = new SfdcApexApi(false, 36);

            conn.Username = username;
            conn.Password = password;
            conn.Token = token;

            conn.Open();

            SfdcConnect.ApexObjects.CompileClassResult[] ccr = conn.compileClasses(new string[] { "public class TestClass123212 { }" });

            conn.Close();
        }

        //IMPORTANT: This test cannot work on the build server.  Must comment out before commit.
        [TestMethod]
        public void BulkApiTest()
        {
            SfdcBulkApi conn = new SfdcBulkApi(false, 36);

            conn.Username = username;
            conn.Password = password;
            conn.Token = token;

            conn.Open();

            Job job = conn.CreateJob("Contact", ContentType.CSV, Operations.query, ConcurrencyMode.Parallel, "");

            Batch batch = conn.CreateBatch(job, "SELECT Id FROM Contact LIMIT 100", "", "");

            conn.CloseJob(job);

            job = conn.GetJob(job);

            //Wait for the job to complete
            while (job.IsDone == false)
            {
                Thread.Sleep(2000);

                job = conn.GetJob(job);
            }

            //If the batch failed, let us know, if it didn't download the batch
            batch = conn.GetBatch(job, batch);

            if (batch.State == "Failed")
            {
                //log it
            }
            else
            {
                //There's no need to download an empty batch for a backup
                if (batch.NumberRecordsProcessed > 0)
                {
                    //zip file is downloaded to path
                    string path = System.IO.Path.Combine(@"C:\Users\Bunneyto\", "Contact.zip");
                    bool success = conn.GetQueryBatchResults(job, batch, path, true);
                }
            }
        }

        [TestMethod]
        public void ApexRestTest()
        {
            SfdcRestApi conn = new SfdcRestApi(false, 36);

            conn.Username = username;
            conn.Password = password;
            conn.Token = token;

            conn.Open();

            string value = conn.CustomAPICallout("mcjson/getJson", "POST", "{ \"userId\":\"0032C000005vCuM\" }");

            conn.Close();
        }

        [TestMethod]
        public void ToolingApiTest()
        {
            SfdcToolingApi conn = new SfdcToolingApi(false, 36);

            conn.Username = username;
            conn.Password = password;
            conn.Token = token;

            conn.Open();

            SfdcConnect.Tooling.DescribeGlobalResult dgr = conn.describeGlobal();

            conn.Close();
        }
    }
}