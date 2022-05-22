using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace SfdcConnectTests
{
    /// <summary>
    /// Summary description for RESTEndpoints
    /// </summary>
    [TestClass]
    public class RESTEndpoints
    {
        public RESTEndpoints()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static string username = ConnectionTests.username;
        public static string password = ConnectionTests.password;
        public static string token = ConnectionTests.token;
        public static string clientId = ConnectionTests.clientId;
        public static string clientsecret = ConnectionTests.clientsecret;



        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestMethod1()
        {

        }
    }
}
