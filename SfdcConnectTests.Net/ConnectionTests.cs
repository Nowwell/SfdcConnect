using Microsoft.VisualStudio.TestTools.UnitTesting;

using SfdcConnect;
using SfdcConnect.Rest;
using SfdcConnect.Testing;
using System.Collections.Generic;

namespace SfdcConnectTests.Net
{
    [TestClass]
    public class ConnectionTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            Test.SetMockCallout(new GenericMockCallout());
            Test.IsTesting = true;


            Sfdc.Rest.loginRequest lr = new Sfdc.Rest.loginRequest();
            lr.username = "seanfife@hotmail.com";
            lr.password = "HH!F`@4*|YG7aFGaJFyEsTT56wTsbY71J79d";
            lr.client_id = "3MVG9szVa2RxsqBaFfy.QEDHf2rObjsuJtd0nc8Ryq4LXX7Ylm9Pn30kHuj1sU_4FFQaOFqcrpp95xWUEV24S";
            lr.client_secret = "D8D46B150BBD86DA71B1B7F49507C8D97894E98ECC80E673785C4293CA8B111B";
            SfdcSession session = new SfdcSession(SfdcConnect.Environment.Production, 54);
            session.Open(SfdcConnect.OAuth.LoginFlow.OAuthUsernamePassword, lr);



            SfdcRestApi api = new SfdcRestApi();
            api.AttachSession(session);

            List<RestServices> serv = api.GetServices();

            session.Close();

        }
    }
}
